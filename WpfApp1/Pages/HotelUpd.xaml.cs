using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для HotelUpd.xaml
    /// </summary>
    public partial class HotelUpd : Page
    {
        Hotel hotel;
        bool add;
        public HotelUpd(Hotel hotel)
        {
            InitializeComponent();
            add = false;
            cmbCount();
            this.hotel = hotel;
            tbName.Text = hotel.Name;
            tbCountStar.Text = Convert.ToString(hotel.CountOfStars);
            cmbCountry.SelectedValue = hotel.CountryCode;
            tbDescp.Text = hotel.Description;
        }

        public HotelUpd()
        {
            InitializeComponent();
            add = true;
            cmbCount();
        }

        public void cmbCount()
        {
            cmbCountry.ItemsSource = Base.EH.Country.ToList();
            cmbCountry.SelectedValuePath = "Code";
            cmbCountry.DisplayMemberPath = "Name";
        }
        private void btnSAve_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text != "" && tbCountStar.Text != "" && tbDescp.Text != "" && cmbCountry.SelectedIndex != -1)
            {
                if (add == true)
                {
                    hotel = new Hotel();
                }
                hotel.Name = tbName.Text;
                Regex r1 = new Regex("^[0-5]{1}$");
                if (r1.IsMatch(tbCountStar.Text) == true)
                {
                    hotel.CountOfStars = Convert.ToInt32(tbCountStar.Text);
                    hotel.CountryCode = Convert.ToString(cmbCountry.SelectedValue);
                    hotel.Description = tbDescp.Text;
                    if (add == true)
                    {
                        Base.EH.Hotel.Add(hotel);
                    }
                    Base.EH.SaveChanges();
                    if (add == true)
                    {
                        MessageBox.Show("Успешное добавление записи!!!");
                    }
                    else
                    {
                        MessageBox.Show("Успешное изменение записи!!!");
                    }
                    FrameC.frameM.Navigate(new HotelP());
                }
                else
                {
                    MessageBox.Show("Количество звезд лежит в диапазоне от 0 до 5");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!!!");
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new HotelP());
        }
    }
}
