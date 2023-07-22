using System;
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
        }
    }
}
