using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MSSQLNET {
    public partial class Form1 : Form {

        // Подключение к БД
        private SqlConnection sqlConnection = null;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // При загрузке формы подключаемся к БД
            sqlConnection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            
            // Открытие подключения к БД
            sqlConnection.Open();

            // Проверка подключения к БД
            if (sqlConnection.State == ConnectionState.Open)
                MessageBox.Show("Подключение установлено!");
        }

        private void textBox4_TextChanged(object sender, EventArgs e) {

        }

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
    }
}
