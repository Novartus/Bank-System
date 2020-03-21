using System.Windows;
using System.Windows.Controls;

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
           // UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    //   usc = new UserControlHome();
                    // GridMain.Children.Add(usc);
                    MessageBox.Show("User Development :(");
                    break;
                case "ItemCreate":
                    //    usc = new UserControlCreate();
                    //  GridMain.Children.Add(usc);
                    MessageBox.Show("User Development :(");
                    break;
                default:
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

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
