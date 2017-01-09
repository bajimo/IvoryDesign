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
        string dataFile = string.Format(@"{0}\CustomerData.db", AppDomain.CurrentDomain.BaseDirectory);

        public MainWindow()
        {
            InitializeComponent();

            addData.Click += AddData_Click;
            clearData.Click += ClearData_Click;
            updateData.Click += UpdateData_Click;
            dataGrid.SelectedCellsChanged += DataGrid_SelectedCellsChanged;


            UpdateDataGrid();
        }


        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells != null && e.AddedCells.Count > 0)
            {
                Customer customer = e.AddedCells[0].Item as Customer;
                firstName.Text = customer.FirstName;
                lastName.Text = customer.LastName;
                address.Text = customer.Address;
            }
        }

        private void UpdateDataGrid()
        {
            List<Customer> AllCustomers;
            using (var db = new LiteDatabase(dataFile))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Customer>("customers");

                AllCustomers = col.FindAll().ToList();
            }

            if (AllCustomers != null)
            {
                dataGrid.ItemsSource = AllCustomers;
            }
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new LiteDatabase(dataFile))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Customer>("customers");

                int results = col.Count(Query.And(Query.EQ("FirstName", firstName.Text), Query.EQ("LastName", lastName.Text)));
                if (results == 0)
                {
                    // Create your new customer instance
                    var customer = new Customer
                    {
                        FirstName = firstName.Text,
                        LastName = lastName.Text,
                        Address = address.Text
                    };

                    col.EnsureIndex(x => x.Name);

                    ClearData();

                    // Insert new customer document (Id will be auto-incremented)
                    col.Insert(customer);
                }
            }

            UpdateDataGrid();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new LiteDatabase(dataFile))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Customer>("customers");

                Customer customer = col.FindOne(Query.EQ("Name", string.Format("{0} {1}", firstName.Text, lastName.Text)));
                if (customer != null)
                {
                    customer.Address = address.Text;
                    col.Update(customer);
                }
            }
            UpdateDataGrid();
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            firstName.Text = "";
            lastName.Text = "";
            address.Text = "";
        }
    }
}
