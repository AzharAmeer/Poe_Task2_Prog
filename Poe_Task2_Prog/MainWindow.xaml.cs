using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Poe_Task2_Prog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
        }
      

        private void registerButt_Click(object sender, RoutedEventArgs e)
        {
            //This is the login form where the user will start the prgram and from there the user will be transfered/directed to the next page 
            new Register().Show();
            this.Hide();
        }

        private void loginButt_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Hide();
        }
    }
}
