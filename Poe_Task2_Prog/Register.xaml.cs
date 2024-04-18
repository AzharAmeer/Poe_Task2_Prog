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
using Task2POE.Library;

namespace Poe_Task2_Prog
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        
        public Register()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private void Displaying()
        {
            con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("Select * from [dbo].[Student] ", con);
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            con.Close();
        }

        private void RegisteredButt_Click(object sender, RoutedEventArgs e)
        {
            //try and catch  to make sure the user enters the correct type of input and the question will repeat itself till the user enters the correct data type
            try
            {
                //Instance for the class library
                var studInfo = new StudentInfo();





                con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");


                // Open Database Connection
                con.Open();
                studInfo.studentNum = int.Parse(studNumTxt.Text);
                studInfo.StudentName = nameTxt.Text;
                studInfo.StudentSurname = surnameTxt.Text;
                studInfo.Username = usernameTxt.Text;
                studInfo.Password = getPasswordHash(passwordTxt.Text);
                cmd = new SqlCommand("INSERT INTO [dbo].[Student](StudentNumber, Name, Surname, Username, Password)" + "VALUES ('" + studInfo.studentNum + "', '" + studInfo.StudentName + "', '" + studInfo.StudentSurname + "', '" + studInfo.Username + "', '" + studInfo.Password + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("You have been registered successfully "); ;
                con.Close();
                Displaying();


                new HoursAndDate(studInfo.studentNum).Show();
                this.Hide();

            }
            catch (FormatException)
            {
                //This code will let the user know if they have entered a wrong input/ data type
                MessageBox.Show("You have to enter a number where not asked or letter or you have not filled in everything", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
