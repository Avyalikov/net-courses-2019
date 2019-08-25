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
    public partial class ClientsGridView : Form
    {
       
        private AddClientForm addClient;
        private ClientStock clientStock;

        public ClientsGridView()
        {
            InitializeComponent();
            LoadData();
           this.addClient = new AddClientForm();
         
        }

        private void LoadData()
        {
            string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=StockExchange;Integrated Security=SSPI;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string query = "SELECT * FROM Clients";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[4]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
            }
            reader.Close();
            sqlConnection.Close();
            foreach (string[]s in data)
            {
                dataGridViewClients.Rows.Add(s);
            }
        }


        private void ShowAddClientForm()
        {

         if (this.addClient.IsDisposed)
            {
                addClient = new AddClientForm();
            }
            AttachAsMDIChild(addClient);
            addClient.Show();
           
        }

        private void ShowStockClientForm()
        {
           int clientId = Convert.ToInt32(this.dataGridViewClients.SelectedCells[0].Value.ToString());
           
            if (this.clientStock==null||this.clientStock.IsDisposed)
            {
                clientStock = new ClientStock(clientId);
            }
          
            clientStock.Show();

        }


        private void AttachAsMDIChild(AddClientForm addform)
        {
            addform.Closing += new CancelEventHandler(addClient_Closing);
           
        }
        private void addClient_Closing(object sender, CancelEventArgs e)
        {
            AddClientForm addClient = (AddClientForm)sender;
            this.LoadData();
        }

        
        private void btn_Add_Client_Click(object sender, EventArgs e)
        {
           this.ShowAddClientForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btn_AddClientStock_Click(object sender, EventArgs e)
        {
           
            this.ShowStockClientForm();
        }

        

    }
}
