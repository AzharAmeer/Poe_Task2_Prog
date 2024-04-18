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
    /// Interaction logic for Displaying.xaml
    /// </summary>
    public partial class Displaying : Window
    {
        public Displaying(int studentNum)
        {
            InitializeComponent();
            StudentNum = studentNum;
        }
       

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public int StudentNum { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new addModules(StudentNum).Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ListingModules(StudentNum).Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new Displaying(StudentNum).Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {

            //Openning database
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");

            // Selecting the row that should display in the listbox
            string com = "Select  m.ModuleCode from StudMod st, Module m where (st.ModuleCode = M.ModuleCode) and (st.StudentNumber = '" + StudentNum + "')";


            SqlDataAdapter adpt = new SqlDataAdapter(com, con);

            DataSet myDataSet = new DataSet();

            adpt.Fill(myDataSet, "Module");

            DataTable myDataTable = myDataSet.Tables[0];

            DataRow tempRow = null;


            //For each loop to ensure that the module name displays in the listbox
            foreach (DataRow tempRow_Variable in myDataTable.Rows)

            {

                tempRow = tempRow_Variable;

                listbox.Items.Add((tempRow["ModuleCode"] + ""));

            }
        


        }

        private void viewButt_Click(object sender, RoutedEventArgs e)
        {

            display();

          


        }
        private void display()
        {
            //try and catch  to make sure the user enters the correct type of input and the question will repeat itself till the user enters the correct data type
            try
            {


                //Instance for class library
                var hoursStud = new ModuleStoring();

                hoursStud.hoursWantedToStudy = int.Parse(hoursADaytxt.Text);
                hoursStud.HoursStart = (DateTime)picker.SelectedDate;
                string detailList = listbox.SelectedItem.ToString();

                //Connection string of database
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");




                //Using sql query this is capturing the data from the correct tables and rows
                con.Open();
                cmd = new SqlCommand("Update StudMod  set  StudMod.hourSpecific ='" + hoursStud.hoursWantedToStudy + "' where  (StudMod.ModuleCode ='" + listbox.SelectedItem.ToString() + "') and (StudMod.StudentNumber ='" + StudentNum + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                SqlCommand ko = new SqlCommand(@"
                                          select 
                                          sm.ModuleCode , sm.hourSpecific 
                                          from StudMod sm 
                                          where (sm.ModuleCode = '" + detailList + "') and (sm.StudentNumber ='" + StudentNum + "')", con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter sdr = new SqlDataAdapter(ko);
                sdr.Fill(dt);
                dataGrid2.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (FormatException)
            {
                //This code will let the user know if they have entered a wrong input/ data type
                MessageBox.Show("You have to enter a number where not asked or letter or you have not filled in everything", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

//recording all inputs, 
//checking if input is right 
//assigning input to local variables
//post amount of hours they studied to studmod table
//populate grid