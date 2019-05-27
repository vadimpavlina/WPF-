using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace GRID
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
        private void UpdateGrid()
        {
            string dbName = "myFirstDataBase .sqlite";
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            //string enterInfo = txtID.Text;
            string query = $"Select ID, NAME, AGE, ADDRESS, SALARY FROM COMPANY";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            DataColumn id = new DataColumn("id", typeof(int));
            DataColumn name = new DataColumn("name", typeof(string));
            DataColumn age = new DataColumn("age", typeof(int));
            DataColumn address = new DataColumn("address", typeof(string));
            DataColumn salary = new DataColumn("salary", typeof(int));
           
            dt.Columns.Add(id);
            dt.Columns.Add(name);
            dt.Columns.Add(age);
            dt.Columns.Add(address);
            dt.Columns.Add(salary);

            //-----------------
            while (reader.Read())
            {
                DataRow row = dt.NewRow();
                row["id"] = reader["ID"].ToString();
                row["name"] = reader["NAME"].ToString();
                row["age"] = reader["AGE"].ToString();
                row["address"] = reader["ADDRESS"].ToString();
                row["salary"] = reader["SALARY"].ToString();

                dt.Rows.Add(row);
            }

            dgTable.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dbName = "myFirstDataBase .sqlite";
                SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
                con.Open();
                int idPerson = int.Parse(txtID.Text);
                string namePerson = txtName.Text;
                int agePerson = int.Parse(txtAge.Text);
                string addressPerson = txtAddress.Text;
                int salaryPerson = int.Parse(txtSalary.Text);
                string query = $"INSERT INTO COMPANY( ID, NAME, AGE, ADDRESS, SALARY)VALUES ({idPerson},'{namePerson}', {agePerson}, '{addressPerson}', {salaryPerson} );";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                UpdateGrid();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dgTable.SelectedItem as DataRowView;
            int id = int.Parse(row.Row.ItemArray[0].ToString());
            MessageBox.Show(id.ToString());
            txtID.Text = row.Row.ItemArray[0].ToString();
            txtName.Text = row.Row.ItemArray[1].ToString();
            txtAge.Text = row.Row.ItemArray[2].ToString();
            txtAddress.Text = row.Row.ItemArray[3].ToString();
            txtSalary.Text = row.Row.ItemArray[4].ToString();
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dbName = "myFirstDataBase .sqlite";
                SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
                con.Open();
                string query = $" SELECT* FROM COMPANY";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                UpdateGrid();
            }
            catch (Exception)
            {

                throw;
            }
          
           
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string dbName = "myFirstDataBase .sqlite";
            SQLiteConnection con = new SQLiteConnection($"Data Source={dbName}");
            con.Open();
            DataRowView row = dgTable.SelectedItem as DataRowView;
            int id = int.Parse(row.Row.ItemArray[0].ToString());
            string query = $"DELETE FROM COMPANY WHERE ID = {id}";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            UpdateGrid();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
