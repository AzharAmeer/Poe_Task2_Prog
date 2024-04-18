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

namespace Poe_Task2_Prog
{
    /// <summary>
    /// Interaction logic for ListingModules.xaml
    /// </summary>
    public partial class ListingModules : Window
    {
        public ListingModules(int studentNum)
        {
            InitializeComponent();
            StudentNum = studentNum;
            
        }

        public int StudentNum { get; }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        //This method is for the datagrid which will display in the page 
        private void display()
        {
            //Connection string of database
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");

            //Using sql query this is capturing the data from the correct tables and rows
            SqlCommand cmd = new SqlCommand("Select m.ModuleCode , m.ModuleName , m.Credits , m.HoursAWeek from StudMod st, Module m where (st.ModuleCode = M.ModuleCode) and (st.StudentNumber = '" + StudentNum +"')", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter sdr = new SqlDataAdapter(cmd);
            sdr.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void SelfDisplay()
        {
            //Connection string of database
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");

            //Using sql query this is capturing the data from the correct tables and rows
            SqlCommand cmd = new SqlCommand("Select sm.ModuleCode , sm.SelfStudy from Module M , StudMod sm where (sm.ModuleCode = M.ModuleCode) and (sm.StudentNumber = '" + StudentNum + " ')", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter sdr = new SqlDataAdapter(cmd);
            sdr.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            con.Close();
        }
   

      
   
        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            display();
        }
        //takes you to the displaying screen
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Displaying(StudentNum).Show();
            this.Hide();
        }
        //takes you to the adding module screen
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new addModules(StudentNum).Show();
            this.Hide();
        }
        //takes you to the module viewing screen
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new ListingModules(StudentNum).Show();
            this.Hide();
        }
        //takes you to the login screen
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }
        //takes you to the login screen
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }



        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try {
                //Connection string of database
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Azhar\\source\\repos\\Poe_Task2_Prog\\Poe_Task2_Prog\\moduleDatabase.mdf;Integrated Security=True");

                con.Open();
                string newModCode = newModCodeTxt.Text;
                string newModname = newModNametxt.Text;
                int newCredits = int.Parse(newCreditstxt.Text);
                int newHours = int.Parse(newHoursWeektxt.Text);
                cmd = new SqlCommand("INSERT INTO Module(ModuleCode, ModuleName, Credits, hoursAWeek)" + "VALUES ('" + newModCode + "', '" + newModname + "', '" + newCredits + "' ,' " + newHours + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("one record inserted:");
                con.Close();
                display();

            }catch(FormatException){
                //This code will let the user know if they have entered a wrong input/ data type
                MessageBox.Show("You have to enter a number where not asked or letter or you have not filled in everything", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            display();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SelfDisplay();
        }
    }
}
