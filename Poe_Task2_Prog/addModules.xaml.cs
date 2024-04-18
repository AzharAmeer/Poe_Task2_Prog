using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for addModules.xaml
    /// </summary>
    public partial class addModules : Window
    {
        public addModules(int studentNum)
        {
            InitializeComponent();
            StudentNum = studentNum;
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public int StudentNum { get; }

        private void Displaying()
        {
            con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("Select * from [dbo].[Module] ", con);
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            con.Close();
        }


        private void clearButt_Click(object sender, RoutedEventArgs e)
        {
            Empty();
        }



        private void saveButt_Click(object sender, RoutedEventArgs e)
        {
            //try and catch  to make sure the user enters the correct type of input and the question will repeat itself till the user enters the correct data type
            try
            {
                //Openning database
                con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");
                //Instances for the library
                var modInfo = new ModuleStoring();
                var semesterDetails = new SemesterStore();

                //Stroing the module detials as well as credits and hours a week into the variables in my class library.
                modInfo.modCode = modCodeTxt.Text;
                modInfo.modName = modNameTxt.Text;
                modInfo.numCred = int.Parse(credsTxt.Text);
                modInfo.hoursWeek = int.Parse(hoursTxt.Text);

                //Opening database
                con.Open();
                //Selecting from the Module table where it will store what the user has entered 
                SqlCommand modRef = new SqlCommand("select ModuleCode from [dbo].[Module] where ModuleCode='" + modInfo.modCode + "'");
                modRef.Connection = con;
                //These few lines of code collects all the columns and rows that is stored in the database
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(modRef);
                da.Fill(dt);
                //Closing database
                con.Close();

                //first select the number of weeks from semster table

                con.Open();
                //Making sure that it selects from the right table in the database 
                SqlCommand infoOnSemester = new SqlCommand("select s.NumberOfWeeks from Semester S , StudSemester st where (s.SemesterID = st.SemesterID) and (st.StudentNumber ='" + StudentNum + "')");
                infoOnSemester.Connection = con;
                int semWeeks;
                semWeeks = (Int32)infoOnSemester.ExecuteScalar();
                con.Close();



                int selfStudy = ((modInfo.numCred * 10) / semWeeks) - modInfo.hoursWeek;


                //The if statment is to ensure that it stores in the table that is situated in the database, it will store in the table as well as the bridging table
                if (dt.Rows.Count == 0)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[Module](ModuleCode, ModuleName, Credits, HoursAWeek)" + "VALUES ('" + modInfo.modCode + "', '" + modInfo.modName + "', '" + modInfo.numCred + "', '" + modInfo.hoursWeek + "')", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("INSERT INTO [dbo].[StudMod] (StudentNumber, ModuleCode, SelfStudy)" + "VALUES ('" + StudentNum + "', '" + modInfo.modCode + "' , '" + selfStudy + "')", con);
                    MessageBox.Show("Your module details has been successfully saved ! ");
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Empty();
                }
                else
                {
                    //Storing data in the bridging table
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO StudMod(StudentNumber, ModuleCode)" + "VALUES('" + StudentNum + "','" + modInfo.modCode + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your module details has been successfully saved ! ");
                    con.Close();
                    Empty();

                }


            }
            catch (FormatException)
            {
                //This code will let the user know if they have entered a wrong input/ data type
                MessageBox.Show("You have to enter a number where not asked or letter or you have not filled in everything", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //A method for if the user makes a mistake they will have a option to clear all inputs
        public void Empty()
        {
            //Clears the texts
            modCodeTxt.Text = "";
            modNameTxt.Text = "";
            credsTxt.Text = "";
            hoursTxt.Text = "";

        }
        private void nextButt_Click(object sender, RoutedEventArgs e)
        {
            new HoursAndDate(StudentNum).Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Displaying(StudentNum).Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new ListingModules(StudentNum).Show();
            this.Hide();
        }
    }
}
