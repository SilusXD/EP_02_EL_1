using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для AddPartnersPage.xaml
    /// </summary>
    public partial class AddPartnersPage : Page
    {
        private Partners _currentPartner = new Partners();
        public AddPartnersPage(Partners currentPartnersProduct)
        {
            InitializeComponent();

            if (currentPartnersProduct != null)
            {
                _currentPartner = currentPartnersProduct;
            }

            DataContext = _currentPartner;
        }

        private void txtBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockName.Visibility = Visibility.Visible;
            if (txtBoxName.Text.Length > 0)
            {
                txtBlockName.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxDirectorLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockDirectorLastName.Visibility = Visibility.Visible;
            if (txtBoxDirectorLastName.Text.Length > 0)
            {
                txtBlockDirectorLastName.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxDirectorFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockDirectorFirstName.Visibility = Visibility.Visible;
            if (txtBoxDirectorFirstName.Text.Length > 0)
            {
                txtBlockDirectorFirstName.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockEmail.Visibility = Visibility.Visible;
            if (txtBoxEmail.Text.Length > 0)
            {
                txtBlockEmail.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockPhoneNumber.Visibility = Visibility.Visible;
            if (txtBoxPhoneNumber.Text.Length > 0)
            {
                txtBlockPhoneNumber.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxAdress_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockAdress.Visibility = Visibility.Visible;
            if (txtBoxAdress.Text.Length > 0)
            {
                txtBlockAdress.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxINN_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockINN.Visibility = Visibility.Visible;
            if (txtBoxINN.Text.Length > 0)
            {
                txtBlockINN.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxRating_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockRating.Visibility = Visibility.Visible;
            if (txtBoxRating.Text.Length > 0)
            {
                txtBlockRating.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxDirectorMidleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBlockDirectorMidleName.Visibility = Visibility.Visible;
            if (txtBoxDirectorMidleName.Text.Length > 0)
            {
                txtBlockDirectorMidleName.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrEmpty(cmbPartnersType.Text) || string.IsNullOrEmpty(txtBoxName.Text) || 
                string.IsNullOrEmpty(txtBoxDirectorLastName.Text) || string.IsNullOrEmpty(txtBoxDirectorFirstName.Text) || string.IsNullOrEmpty(txtBoxDirectorMidleName.Text) ||
                string.IsNullOrEmpty(txtBoxEmail.Text) || string.IsNullOrEmpty(txtBoxPhoneNumber.Text) || string.IsNullOrEmpty(txtBoxAdress.Text) ||
                string.IsNullOrEmpty(txtBoxINN.Text) || string.IsNullOrEmpty(txtBoxRating.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            _currentPartner.IDType = Entities.GetContext().PartnersType.FirstOrDefault(x => x.Type == cmbPartnersType.Text).ID;

            if (_currentPartner.ID == 0)
                Entities.GetContext().Partners.Add(_currentPartner);

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
