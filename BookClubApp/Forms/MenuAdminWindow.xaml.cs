using BookClubApp.Class;
using BookClubApp.Pages;
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
using System.Windows.Shapes;

namespace BookClubApp.Forms
{
    /// <summary>
    /// Логика взаимодействия для MenuAdminWindow.xaml
    /// </summary>
    public partial class MenuAdminWindow : Window
    {
        public MenuAdminWindow() {
            InitializeComponent();

        }
        private void Window_Closed(object sender, EventArgs e) { Application.Current.Shutdown(); }

        private void btnProfile_Click(object sender, RoutedEventArgs e) {
            if (borderProfile.Visibility == Visibility.Hidden) { borderProfile.Visibility = Visibility.Visible; }
            else if (borderProfile.Visibility == Visibility.Visible) { borderProfile.Visibility = Visibility.Hidden; }
            FrameListProduct.Visibility = Visibility.Hidden;
            FrameListUser.Visibility = Visibility.Hidden;
            btnBack.Visibility = Visibility.Hidden;

            fieldLogin.Text = Databank.pLogin + " [ID: " + Databank.pId + "]";
            fieldRole.Text = Databank.pRole;
        }

        private void btnListUser_Click(object sender, RoutedEventArgs e) {
            if (borderProfile.Visibility == Visibility.Visible) { borderProfile.Visibility = Visibility.Hidden; }
            if (FrameListUser.Visibility == Visibility.Hidden) {
                FrameListUser.Visibility = Visibility.Visible;
                FrameListProduct.Visibility = Visibility.Hidden;
                btnBack.Visibility = Visibility.Hidden;
                FrameListUser.Navigate(new ListUserPage());
                Manager.FrameListUser = FrameListUser;
            }
            else if (FrameListUser.Visibility == Visibility.Visible) {
                FrameListUser.Visibility = Visibility.Hidden;
                FrameListProduct.Visibility = Visibility.Hidden;
                btnBack.Visibility = Visibility.Hidden;
            }
        }

        private void FrameListUser_ContentRendered(object sender, EventArgs e) {
            if (FrameListUser.CanGoBack) {
                btnBack.Visibility = Visibility.Visible;
            }
            else { btnBack.Visibility = Visibility.Hidden; }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e) {
            if (FrameListUser.Visibility == Visibility.Visible) {
                Manager.FrameListUser.GoBack();
                return;
            }
            if (FrameListProduct.Visibility == Visibility.Visible) {
                Manager.FrameListProduct.GoBack();
                return;
            }
        }

        private void btnListProduct_Click(object sender, RoutedEventArgs e) {
            if (borderProfile.Visibility == Visibility.Visible) { borderProfile.Visibility = Visibility.Hidden; }
            if (FrameListUser.Visibility == Visibility.Hidden) {
                FrameListProduct.Visibility = Visibility.Visible;
                FrameListUser.Visibility = Visibility.Hidden;
                btnBack.Visibility = Visibility.Hidden;
                FrameListProduct.Navigate(new ListProductPage());
                Manager.FrameListProduct = FrameListProduct;
            }
            else if (FrameListProduct.Visibility == Visibility.Visible) {
                FrameListProduct.Visibility = Visibility.Hidden;
                FrameListUser.Visibility = Visibility.Hidden;
                btnBack.Visibility = Visibility.Hidden;
            }
        }

        private void FrameListProduct_ContentRendered(object sender, EventArgs e) {
            if (FrameListProduct.CanGoBack) { btnBack.Visibility = Visibility.Visible; }
            else { btnBack.Visibility = Visibility.Hidden; }
        }
    }
}
