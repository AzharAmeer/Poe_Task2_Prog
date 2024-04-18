using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Task2POE.Library;

namespace Poe_Task2_Prog
{
    /// <summary>
    /// Interaction logic for HoursAndDate.xaml
    /// </summary>
    public partial class HoursAndDate : Window
    {
        public HoursAndDate(int studentNum)
        {
            InitializeComponent();
            StudentNum = studentNum;
        }

        
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public int weeks;

        public int StudentNum { get; }


        //Saves the detials inserted
        private void saveButt_Click(object sender, RoutedEventArgs e)
        {

            //try and catch  to make sure the user enters the correct type of input and the question will repeat itself till the user enters the correct data type
            try
            {
                //Instance for the class library
                var semesterInfo = new SemesterStore();



                //Storing my database
                con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");

                //Storing the weeks in semester as well as the date chosen from the user in a class library 
                semesterInfo.week = int.Parse(weeksInSemesterTxt.Text);
                semesterInfo.start = (DateTime)calendar.SelectedDate;

                //opening database
                con.Open();
                //Making sure that the users inputs is stored in the semster table which is in the database
                cmd = new SqlCommand("INSERT INTO Semester(NumberOfWeeks, StartDate)" + "VALUES ('" + semesterInfo.week + "', '" + semesterInfo.start + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                //opening database
                con.Open();
                //Making sure that it selects from the right table in the database 
                SqlCommand infoOnSemester = new SqlCommand("SELECT SemesterID FROM Semester WHERE NumberOfWeeks ='" + semesterInfo.week + "'AND StartDate ='" + semesterInfo.start + " '");
                infoOnSemester.Connection = con;
                int ID;
                ID = (Int32)infoOnSemester.ExecuteScalar();
                con.Close();

                //Stores the data in a database
                con.Open();
                cmd = new SqlCommand("INSERT INTO StudSemester(SemesterID ,StudentNumber )" + "VALUES('" + ID + "', '" + StudentNum + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                //Messagebox to show that the details is stored properly
                MessageBox.Show("Your details is saved");



            }
            catch (FormatException)
            {
                //This code will let the user know if they have entered a wrong input/ data type
                MessageBox.Show("You have to enter a number where not asked or letter or you have not filled in everything", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Clears textboxes
        private void clearButt_Click(object sender, RoutedEventArgs e)
        {
            Empty();
        }
        //Method which clears textboxes
        public void Empty()
        {
            weeksInSemesterTxt.Text = " ";
        }

        //takes you to the login screen
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }
        //takes you to the adding module screen
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new addModules(StudentNum).Show();
            this.Hide();
        }
        
        //takes you to the login screen
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }
        //takes you to the displaying screen
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new Displaying(StudentNum).Show();
            this.Hide();
        }
        //takes you to the module viewing screen
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new ListingModules(StudentNum).Show();
            this.Hide();
        }
    }
}
