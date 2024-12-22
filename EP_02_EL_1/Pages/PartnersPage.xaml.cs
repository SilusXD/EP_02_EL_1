using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PartnersPage.xaml
    /// </summary>
    /// 
    public class InfoPartner
    {
        public int id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string DirectorName { get; set; }
        public string PhoneNumber { get; set; }
        public int Rating { get; set; }
        public int Discount { get; set; }
    }
    public partial class PartnersPage : Page
    {
        private int clickCount = 0;
        public PartnersPage()
        {
            InitializeComponent();

            var infoPartners = new List<InfoPartner>();
            var partners = Entities.GetContext().Partners.ToList();

            for (int i = 0; i < partners.Count(); i++)
            {
                InfoPartner infoPartner = new InfoPartner();
                infoPartner.id = partners[i].ID;
                infoPartner.Type = partners[i].PartnersType.Type;
                infoPartner.Name = partners[i].Name;
                infoPartner.DirectorName = partners[i].DirectorsLastName + ' ' +
                                           partners[i].DirectorsFirstName + ' ' +
                                           partners[i].DirectorsMidleName;
                infoPartner.PhoneNumber = "+7 " + partners[i].PhoneNumber;
                infoPartner.Rating = partners[i].Rating;

                int productCount = 0;
                for (int j = 0; j < Entities.GetContext().PartnersProduct.Count(); j++)
                {
                    if (Entities.GetContext().PartnersProduct.ToList()[j].IDPartner == partners[i].ID)
                    {
                        productCount += Entities.GetContext().PartnersProduct.ToList()[j].ProductCount;
                    }
                }

                if (productCount < 10000)
                {
                    infoPartner.Discount = 0;
                }
                else if (productCount >= 10000 && productCount <= 50000)
                {
                    infoPartner.Discount = 5;
                }
                else if (productCount >= 50000 && productCount <= 300000)
                {
                    infoPartner.Discount = 10;
                }
                else
                {
                    infoPartner.Discount = 15;
                }


                infoPartners.Add(infoPartner);
            }

            listViewPartners.ItemsSource = infoPartners;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPartnersPage(null));
        }

        private void listViewPartners_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentpartner = listViewPartners.SelectedItem as InfoPartner;

            if (currentpartner == null)
            {
                return;
            }
            var list = Entities.GetContext().Partners.ToList();

            NavigationService.Navigate(new AddPartnersPage(list.FirstOrDefault(x => x.ID == currentpartner.id)));
        }

        private void btnPartnersProduct_Click(object sender, RoutedEventArgs e)
        {
            var currentpartner = (sender as Button).DataContext as InfoPartner;

            if (currentpartner == null)
            {
                return;
            }
            var list = Entities.GetContext().Partners.ToList();

            NavigationService.Navigate(new PartnersProductPage(list.FirstOrDefault(x => x.ID == currentpartner.id)));
        }
    }
}
