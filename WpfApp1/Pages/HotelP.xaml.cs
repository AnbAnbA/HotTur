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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для HotelP.xaml
    /// </summary>
    public partial class HotelP : Page
    {
        PartialHotel ph = new PartialHotel();
        List<Hotel> listHotel = Base.EH.Hotel.ToList();
        public HotelP()
        {
            InitializeComponent();
            dgHotels.ItemsSource = Base.EH.Hotel.ToList();
            ph.Countlist = listHotel.Count;
            ph.CountPage = 10;
            dgHotels.ItemsSource = listHotel.Skip(0).Take(ph.CountPage).ToList();  
            DataContext = ph;
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new Pages.ShowMP());
        }

        private void btnAddH_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new Pages.HotelUpd());
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Hotel hotel = Base.EH.Hotel.FirstOrDefault(z => z.Id == index);
            FrameC.frameM.Navigate(new Pages.HotelUpd(hotel));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Hotel hotel = Base.EH.Hotel.FirstOrDefault(z => z.Id == index);
            List<HotelOfTour> hotelOftour = Base.EH.HotelOfTour.Where(x => x.HotelId == hotel.Id).ToList();
            if (hotelOftour.Count > 0)
            {
                int kolHO = hotelOftour.Count;
                int kol = 0;
                foreach (HotelOfTour hotelOfTour in hotelOftour)
                {
                    kol++;
                    Tour hotelList = Base.EH.Tour.FirstOrDefault(x => x.Id == hotelOfTour.TourId);
                    if (hotelList.IsActual == false)
                    {
                        if (MessageBox.Show("Вы хотите удалить {0}", Name, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (kol == kolHO)
                            {
                                Base.EH.Hotel.Remove(hotel);
                                Base.EH.SaveChanges();
                                MessageBox.Show("Отель удален!");
                                FrameC.frameM.Navigate(new HotelP());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данный отель не может быть удален!");
                    }
                }

            }
            else
            {
                Base.EH.Hotel.Remove(hotel);
                Base.EH.SaveChanges();
                FrameC.frameM.Navigate(new HotelP());
            }
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ph.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                ph.CountPage = 10; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            ph.Countlist = listHotel.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            dgHotels.ItemsSource = listHotel.Skip(0).Take(ph.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            ph.CurrentPage = 1; // текущая страница - это страница 1
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    ph.CurrentPage--;
                    break;
                case "next":
                    ph.CurrentPage++;
                    break;
                case "firstPage":
                    ph.CurrentPage = 1;
                    break;
                case "lastPage":
                    ph.CurrentPage = ph.CountPages;
                    break;
                default:
                    ph.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            dgHotels.ItemsSource = listHotel.Skip(ph.CurrentPage * ph.CountPage - ph.CountPage).Take(ph.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице

        }
    }
}
