using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace The_Simple_Bank
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }
        static string uname;
        static string userid;

        private string createuid()
        {
           return (DateTime.Now.ToString("MMddmmssff"));
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
                   // temp_text.Text = hash;
                }
            }
        }
        #region SignUp Window Buttons 
        private void signup_username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void CreatePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void ConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion Buttons
        private void PasswordKeyDown(object sender, KeyEventArgs e)  //Button Click Method
        {
            if (e.Key == Key.Enter)
                Button_signup.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }


        private void Button_signup_Click(object sender, RoutedEventArgs e)
        {
            string uid = createuid(); //passing value as a string UserID
            int user_id = int.Parse(uid); //UserID integer Value
            string hashedPassword = Security.HashSHA2(ConfirmPassword.Password.ToString() + uid);  //SHA-256 Hash + Salt (UserID)

            #region Check Form validation
            if ( Security.HashSHA2(CreatePassword.Password.ToString()+uid) != Security.HashSHA2(ConfirmPassword.Password.ToString()+uid))
            {
                MessageBox.Show("Confirm password must be same as password");
                CreatePassword.Focus();
            }

            if(CreateUserName.Text.Length == 0)
            {
                MessageBox.Show("Please Enter valid username");
                CreateUserName.Focus();
            }

            if((CreatePassword.Password.Length == 0 ) || (ConfirmPassword.Password.Length == 0))
            {
                MessageBox.Show("Enter password");
            }

            if(  (!Regex.IsMatch(email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))  )
            {
                MessageBox.Show("Enter valid email");
                email.Focus();
            }

            #endregion 
            else
            {
                
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=ACER-NOVARTUS\SQLEXPRESS; Initial Catalog=LoginDB; Integrated Security=True;");
                try
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    String query = "Insert into Table_Users (Userid, Username, Password, Email) values('"+ user_id + "','" + this.CreateUserName.Text + "','" + hashedPassword + "','" + this.email.Text + "'); ";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    SqlDataReader sqlDataReader;
                    sqlDataReader = sqlCommand.ExecuteReader();
                    MessageBox.Show(@"Your UserID: "+uid+"\nPlease Check your mail for more details", "Account Details");
                    while (sqlDataReader.Read())
                    {

                    }
                    uname = this.CreateUserName.Text;
                    userid = uid;

                    SMTP.SendMail(this.email.Text);

                    this.Close();
                    MainWindow loginscreen = new MainWindow();
                    loginscreen.Show();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    uid = null;
                    hashedPassword = null;
                    userid = null;
                    user_id = 0 ;
                    email.Text = null;
                    sqlConnection.Close();
                }
            }
        }

        internal class SMTP
        {
            static internal void SendMail(string email)
            {

                string filename = @"C:\Bank_Mail.html";
                string mailbody = System.IO.File.ReadAllText(filename);
                mailbody = mailbody.Replace("##NAME##", uname);
                mailbody = mailbody.Replace("##USERID##", userid);
                string to = email;
                string from = "simplecoffeeshop24.7@gmail.com";
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Auto Response Email";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential("simplecoffeeshop24.7@gmail.com", "Ded#Sec#2000");
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = basicCredential;
                try
                {
                    client.Send(message);
                    MessageBox.Show("Please Check your email !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\nMail Delivery Failed! :( ");
                }
            }
        }    // Send Mail using SMTP Protocol

        private void Button_signup_cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sign up canceled!");
            this.Close();
            MainWindow loginscreen = new MainWindow();
            loginscreen.Show();
        }

      
    }
}