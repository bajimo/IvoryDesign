using IvoryDesign.Pages;
using LiteDB;
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

namespace IvoryDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string dataFile = string.Format(@"{0}\CustomerData.db", AppDomain.CurrentDomain.BaseDirectory);

        public MainWindow()
        {
            InitializeComponent();
            Customers customers = new Customers();

            TabItem customersTab = new TabItem();
            customersTab.Header = "Customers";
            customersTab.Content = customers;
            tabControl.Items.Add(customersTab);

        }
    }
}
