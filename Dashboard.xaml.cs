using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace The_Simple_Bank
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        public Dashboard()
        {
            InitializeComponent();
            textblock_username.Text = MainWindow.uname;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         //  UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                  //  usc = new UserControlHome();
                 //   GridMain.Children.Add(usc);
                    MessageBox.Show("User Development :(");
                    break;
                case "ItemCreate":
                    //    usc = new UserControlCreate();
                 //   GridMain.Children.Add(usc);
                    MessageBox.Show("User Development :(");
                    break;
                case "Account":
                    //    usc = new UserControlCreate();
                   // GridMain.Children.Add(usc);
                    MessageBox.Show("User Development :(");
                    break;
                default:
                    MessageBox.Show("Under Development - Default MSG box");
                    break;
            }
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

           // GridCursor.Margin = new Thickness(0, 0, (10 + (100 * index)), 0);

            switch (index)
            {
                case 0:
                    GridMain.Background = Brushes.Aquamarine;
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    break;
                case 2:
                    GridMain.Background = Brushes.CadetBlue;
                    break;
                case 3:
                    GridMain.Background = Brushes.DarkBlue;
                    break;
                case 4:
                    GridMain.Background = Brushes.Firebrick;
                    break;
                case 5:
                    GridMain.Background = Brushes.Gainsboro;
                    break;
                case 6:
                    GridMain.Background = Brushes.HotPink;
                    break;
            }
        }


        private void logout_dashboard_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logout Button is User Development :(");
        }

        private void exit_dashboard_click(object sender, RoutedEventArgs e)   // Forcefully Quit the Application
        {
            if (MessageBox.Show("Do you want to exit?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MessageBox.Show("See you soon.");
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                Application.Current.Shutdown();
            }
        }

    }
}
