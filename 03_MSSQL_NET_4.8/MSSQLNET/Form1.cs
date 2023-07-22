using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MSSQLNET {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        // Подключение к БД
        private SqlConnection sqlConnection = null;
        
        // Фильтрация (поиск) по параметрам в БД 
        private List<string[]> rows = new List<string[]>();
        private List<string[]> filteredList = null;

        private void Form1_Load(object sender, EventArgs e) {
            // При загрузке формы подключаемся к БД
            sqlConnection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            
            // Открытие подключения к БД
            sqlConnection.Open();

            /* Проверка подключения к БД
            if (sqlConnection.State == ConnectionState.Open)
                MessageBox.Show("Подключение установлено!");
            */
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Products", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView2.DataSource = dataSet.Tables[0];

            // Логика для фильтрации в БД
            SqlDataReader reader = null;
            string[] row = null;

            try {
                SqlCommand scommand = new SqlCommand(
                    "SELECT ProductName, QuantityPerUnit, UnitPrice FROM Products",
                    sqlConnection);
                
                reader = scommand.ExecuteReader();
                while (reader.Read()) {
                    row = new string[] {
                        Convert.ToString(reader["ProductName"]),
                        Convert.ToString(reader["QuantityPerUnit"]),
                        Convert.ToString(reader["UnitPrice"])
                    };
                    rows.Add(row);
                }
            }
            catch (Exception exp) {
                MessageBox.Show(exp.Message);
            }
            finally {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            RefreshList(rows);
        }

        private void RefreshList(List<string[]> list) {
            listView2.Items.Clear();

            foreach (string[] str in list)
                listView2.Items.Add(new ListViewItem(str));
        }

        private void textBox4_TextChanged(object sender, EventArgs e) { /* fake */ }

        private void button1_Click(object sender, EventArgs e) {
            // Пример создание команды из С# кода
            /*
            SqlCommand cmd = new SqlCommand(
                $@"INSERT INTO [Students] (Name, Surname, Birthday)
                VALUES (N'{textBox1.Text}', N'{textBox6.Text}', '{textBox5.Text}')", 
                sqlConnection); 
            */
            SqlCommand command = new SqlCommand(
                "INSERT INTO [Students] (Name, Surname, Birthday, Adress, Phone, Email) " +
                "VALUES (@Name, @Surname, @Birthday, @Adress, @Phone, @Email)",
                sqlConnection);

            DateTime date = DateTime.Parse(textBox5.Text);

            command.Parameters.AddWithValue("Name", textBox1.Text);
            command.Parameters.AddWithValue("Surname", textBox6.Text);
            command.Parameters.AddWithValue("Birthday", $"{date.Month}/{date.Day}/{date.Year}");
            command.Parameters.AddWithValue("Adress", textBox4.Text);
            command.Parameters.AddWithValue("Phone", textBox3.Text);
            command.Parameters.AddWithValue("Email", textBox2.Text);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        private void button2_Click(object sender, EventArgs e) {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                textBox7.Text,
                // "SELECT * FROM Products WHERE UnitPrice > 100",
                sqlConnection
            );
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e) {
            listView1.Items.Clear();
            SqlDataReader dataReader = null;

            try {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT ProductName, QuantityPerUnit, UnitPrice FROM Products",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                ListViewItem item = null;

                while (dataReader.Read()) {
                    item = new ListViewItem(new string[] {
                        Convert.ToString(dataReader["ProductName"]),
                        Convert.ToString(dataReader["QuantityPerUnit"]),
                        Convert.ToString(dataReader["UnitPrice"])
                    });
                    listView1.Items.Add(item);
                }
            }
            catch (Exception exp) {
                MessageBox.Show(exp.Message);
            }
            finally {
                if (dataReader != null && !dataReader.IsClosed) { 
                    dataReader.Close(); 
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e) {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter =
                $"ProductName LIKE '%{textBox8.Text}%'";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

            switch (comboBox1.SelectedIndex) {
                case 0:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter =
                        $"UnitsInStock <= 10";
                    break;
                case 1:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = 
                        $"UnitsInStock >= 10 AND UnitsInStock <= 50";
                    break;
                case 2:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter =
                        $"UnitsInStock >= 50";
                    break;
                case 3:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = "";
                    break;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e) {
            filteredList = rows.Where((x) => 
                x[0].ToLower().Contains(textBox9.Text.ToLower())).ToList();

            RefreshList(filteredList);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {

            switch (comboBox2.SelectedIndex) {
                case 0:
                    filteredList = rows.Where((x) => Double.Parse(x[2]) <= 10).ToList();
                    RefreshList(filteredList);
                    break;
                case 1:
                    filteredList = rows.Where((x) => 
                        Double.Parse(x[2]) <= 10 && Double.Parse(x[2]) <= 100).ToList();
                    RefreshList(filteredList);
                    break;
                case 2:
                    filteredList = rows.Where((x) => Double.Parse(x[2]) > 100).ToList();
                    RefreshList(filteredList);
                    break;
                case 3:
                    RefreshList(rows);
                    break;
            }
        }
    }
}