using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Security.Cryptography;
using System.Text;

namespace The_Simple_Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string uname;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void userid_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }



        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if ((Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled)
            {
                if (password.ToolTip == null)
                {
                    ToolTip tt = new ToolTip();
                    tt.Content = "Warning: CapsLock is on";
                    tt.PlacementTarget = sender as UIElement;
                    tt.Placement = PlacementMode.Bottom;
                    password.ToolTip = tt;
                    tt.IsOpen = true;
                }
            }
            else
            {
                var currentToolTip = password.ToolTip as ToolTip;
                if (currentToolTip != null)
                {
                    currentToolTip.IsOpen = false;
                }
                password.ToolTip = null;
            }//Check if CAPS is ON or OFF through ToolTip 
        }

        public class Security
        {
            public static string HashSHA2(string value) //SHA256
            {
                using (var sha256 = SHA256.Create())
                {
                    // Send a sample text to hash.  
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                    // Get the hashed string.  
                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    // Print the string.   
                    return hash;
                    //   MessageBox.Show(hash);
                }
            }
        }
        private void PasswordKeyDown(object sender, KeyEventArgs e)  //Button Click Method
        {
            if (e.Key == Key.Enter)
            Button_login.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void Button_login_Click(object sender, RoutedEventArgs e)
        {
            String message = "Invalid Credentials";
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=ACER-NOVARTUS\SQLEXPRESS; Initial Catalog=LoginDB; Integrated Security=True;");
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                
                String query = "SELECT COUNT(1) FROM Table_Users WHERE Userid=@Userid AND Password=@Password";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Userid", int.Parse(userid.Text.Trim()));
                sqlCommand.Parameters.AddWithValue("@Password", Security.HashSHA2(password.Password.ToString() + userid.Text.Trim().ToString()) );
                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                
                if (count == 1)
                {
                   // uname = userid.Text;
                    MessageBox.Show("Correct Details !");
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(message);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                uname = "";
                password.Password = "";
                userid.Text = "";
                sqlConnection.Close();
            }

        }

        private void Button_exit_Click(object sender, RoutedEventArgs e) // Forcefully Quit the Application
        {
            if (MessageBox.Show("Do you want to exit?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MessageBox.Show("See you soon.");
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                Application.Current.Shutdown();

            }
        }

        private void coffee_mousedown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Contact me:
            System.Diagnostics.Process.Start("http://www.github.com/Novartus");
        }

        private void Button_Signup_Click(object sender, RoutedEventArgs e)
        {
            Signup signup = new Signup();
           // this.Content = signup;  for PAGES ONLY
            this.Hide();
            signup.Show();

        }
    }
}
