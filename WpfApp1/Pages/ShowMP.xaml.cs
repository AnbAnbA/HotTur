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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowMP.xaml
    /// </summary>
    public partial class ShowMP : Page
    {
        List<Tour> listFilter = Base.EH.Tour.ToList();
        public ShowMP()
        {
            InitializeComponent();
            LVH.ItemsSource=Base.EH.Tour.ToList();
            List<Type> type = Base.EH.Type.ToList();
            cbt.Items.Add("Все типы");
            for (int i = 0; i < type.Count; i++)
            {
                cbt.Items.Add(type[i].Name);
            }
            cbt.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;
        }

        private void tbSear_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cbAc_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        void Filter()
        {
            List<Tour> list = Base.EH.Tour.ToList(); 
            string t = cbt.SelectedValue.ToString();
            int index = cbt.SelectedIndex;
            List<TypeOfTour> types = Base.EH.TypeOfTour.Where(z => z.Type.Name == t).ToList();
            if (index != 0)
            {
                listFilter = new List<Tour>();
                foreach (TypeOfTour tot in types)
                {
                    foreach (Tour tour in list)
                    {
                        if (tour.Id == tot.TourId)
                        {
                            listFilter.Add(tour);
                        }
                    }
                }
            }
            else
            {
                listFilter = Base.EH.Tour.ToList();
            }

            if (!string.IsNullOrWhiteSpace(tbSear.Text)) 
            {
                listFilter = listFilter.Where(z => z.Name.ToLower().Contains(tbSear.Text.ToLower())).ToList();
            }

            if (cbAc.IsChecked == true) 
            {
                listFilter = listFilter.Where(z => z.IsActual == true).ToList();
            }

            switch (cmbSort.SelectedIndex) 
            {
                case 1:
                    listFilter.Sort((x, y) => x.Price.CompareTo(y.Price));
                    break;
                case 2:
                    listFilter.Sort((x, y) => x.Price.CompareTo(y.Price));
                    listFilter.Reverse();
                    break;
            }

            LVH.ItemsSource = listFilter;
            int price = 0;
            foreach (Tour tour in listFilter) 
            {
                price += (int)tour.Price * tour.TicketCount;
            }
            tbcost.Text = string.Format("Общая стоимость туров: {0:C2}", price);
            if (listFilter.Count == 0)
            {
                MessageBox.Show("нет записей");
            }
        }


        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new HotelP());
        }
    }
}
