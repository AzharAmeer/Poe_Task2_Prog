using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace Poe_Task2_Prog
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            
            
        }
        public int StudentNum { get; set; }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private void LoginingButt_Click(object sender, RoutedEventArgs e)
        {
            //try and catch  to make sure the user enters the correct type of input and the question will repeat itself till the user enters the correct data type
            try {


                //Connection string of database
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");

                //Openning database
                con.Open();
                //Selecting columns from the row where the student number and password is already captured and stored
                SqlCommand cmd = new SqlCommand("select * from Student where StudentNumber='" + StudentIDLoginTxt.Text + "' and Password='" + getPasswordHash(passwordLoginTxt.Password) + "'");
                cmd.Connection = con;
                //These few lines of code collects all the columns and rows that is stored in the database
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                //Closing database
                con.Close();

                //If statement. This is to verify/ check if the user has entered the correct student number / password. 
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("You have entered an invalid student number or password, Double check and try again", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
                    Empty();
                    dt.Dispose();

                }
                else
                {
                    StudentNum = int.Parse(StudentIDLoginTxt.Text);
                    MessageBox.Show("Welcome back !");
                    new ListingModules(StudentNum).Show();
                    this.Hide();
                }

            }
            catch (FormatException)
            {
                //This code will let the user know if they have entered a wrong input/ data type
                MessageBox.Show("You have to enter a number where not asked or letter or you have not filled in everything", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //This is a method to clear the textboxes
        public void Empty()
        {
            StudentIDLoginTxt.Text = " ";
            passwordLoginTxt.Password = " ";
        }

        private void backButt_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }
        public string getPasswordHash(string input)
        {
            MD5 HashCode = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = HashCode.ComputeHash(Encoding.UTF8.GetBytes(input));


            // Create a new String builder to collect the bytes and create a string
            StringBuilder sBuilder = new StringBuilder();

            // Loops through each byte of the hashed data and formats each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
