using IvoryDesign.Data;
using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IvoryDesign.Pages
{
    public partial class Customers : UserControl
    {
        ContextMenu dataGridContext = new ContextMenu();
        MenuItem deleteRow = new MenuItem();
        Customer selectedCustomer;


        public Customers()
        {
            InitializeComponent();
            UpdateDataGrid();

            deleteRow.Header = "Delete Row";
            deleteRow.Click += DeleteRow_Click;
            dataGridContext.Items.Add(deleteRow);
            dataGrid.ContextMenu = dataGridContext;

            dataGrid.SelectedCellsChanged += DataGrid_SelectedCellsChanged1;

            addData.Click += AddData_Click;
            clearData.Click += ClearData_Click;
            updateData.Click += UpdateData_Click;
            dataGrid.SelectedCellsChanged += DataGrid_SelectedCellsChanged;
        }

        private void DataGrid_SelectedCellsChanged1(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count > 0)
            {
                selectedCustomer = (Customer)e.AddedCells[0].Item;
            }
            else
            {
                selectedCustomer = null;
            }
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer == null) return;

            string message = string.Format("Are you sure you want to delete {0}", selectedCustomer.Name);
            string caption = "Customers";
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            using (var db = new LiteDatabase(MainWindow.dataFile))
            {
                var col = db.GetCollection<Customer>("customers");
                col.Delete(selectedCustomer.Id);
            }
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
            using (var db = new LiteDatabase(MainWindow.dataFile))
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
            using (var db = new LiteDatabase(MainWindow.dataFile))
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
            using (var db = new LiteDatabase(MainWindow.dataFile))
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
