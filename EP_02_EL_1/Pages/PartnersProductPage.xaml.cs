using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EP_02_EL_1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PartnersProductPage.xaml
    /// </summary>
    public partial class PartnersProductPage : Page
    {
        private Partners _currentPartner = new Partners();
        public PartnersProductPage(Partners currentPartnerProduct)
        {
            InitializeComponent();

            if (currentPartnerProduct != null)
            {
                _currentPartner = currentPartnerProduct;
            }

            dataGridPartnersProduct.ItemsSource = Entities.GetContext().PartnersProduct.Where(x => x.IDPartner == _currentPartner.ID).ToList();
        }

        private void btnCalculateMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPartnersProduct.SelectedItem is PartnersProduct selectedRow)
            {
                var product = (dataGridPartnersProduct.SelectedItem as PartnersProduct).IDProduct;
                var productType = (dataGridPartnersProduct.SelectedItem as PartnersProduct).Products.IDType;

                double param1 = 1.5;
                double param2 = 2.0;

                int requiredMaterial = CalculateMaterialRequirement(
                    productTypeId: product,
                    materialTypeId: productType,
                    quantity: selectedRow.ProductCount,
                    param1: param1,
                    param2: param2);

                if (requiredMaterial == -1)
                {
                    MessageBox.Show("Ошибка: некорректные данные для расчёта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Для производства {selectedRow.ProductCount} единиц продукции требуется {requiredMaterial} материала.",
                                    "Результат расчёта", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись в таблице.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static int CalculateMaterialRequirement(int productTypeId, int materialTypeId, int quantity, double param1, double param2)
        {
            if (productTypeId <= 0 || materialTypeId <= 0 || quantity <= 0 || param1 <= 0 || param2 <= 0)
                return -1;

            double coefficient = GetProductCoefficient(productTypeId);
            double defectRate = GetMaterialDefectRate(materialTypeId);

            if (coefficient <= 0 || defectRate < 0)
                return -1;

            double materialPerUnit = param1 * param2 * coefficient;
            double totalMaterial = materialPerUnit * quantity;
            totalMaterial += totalMaterial * defectRate;
            return (int)Math.Ceiling(totalMaterial);
        }

        private static double GetProductCoefficient(int productTypeId)
        {
            var productType = Entities.GetContext().ProductType.FirstOrDefault(pt => pt.ID == productTypeId);
            return productType != null ? (double)productType.Coefficient : -1;
        }


        private static double GetMaterialDefectRate(int materialTypeId)
        {
            var materialType = Entities.GetContext().MaterialType.FirstOrDefault(mt => mt.ID == materialTypeId);
            return materialType != null ? (double)materialType.DefectRate : -1;
        }
    }
}
