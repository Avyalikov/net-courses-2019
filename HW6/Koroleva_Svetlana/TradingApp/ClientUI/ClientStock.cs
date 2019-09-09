using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class ClientStock : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=StockExchange;Integrated Security=SSPI;");

        private readonly int clientID;

        public ClientStock(int clientID)
        {
            InitializeComponent();
            this.clientID = clientID;
            this.txt_Client.Text = clientID.ToString();

        }

        private void ClientStock_Load(object sender, EventArgs e)
        {

            
        }

        private void btn_AddStock_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                var stock = (stockExchangeDataSet.Stocks.Where(s => s.StockPrefix == (comboBoxStock.Text)));

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO ClientStocks VALUES('" + this.clientID + "','" + stock.First().StockID + "','" + txt_Quantity.Text + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                txt_Client.Clear();
                txt_Quantity.Clear();



                MessageBox.Show("New client stock was added to the base");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ClientStock_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stockExchangeDataSet.Stocks' table. You can move, or remove it, as needed.
            this.stocksTableAdapter.Fill(this.stockExchangeDataSet.Stocks);

        }
    }
}
