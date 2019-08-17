using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClientUI
{
    public partial class AddClient : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=StockExchange;Integrated Security=SSPI;");

        public AddClient()
        {
            InitializeComponent();
           
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {

                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Clients VALUES('" + txtBox_clientLastName.Text + "','" + txtBox_clientFirstName.Text + "','" + txtBox_clientPhone.Text + "','" + Convert.ToDecimal(txt_Balanse.Text) + "')";
                cmd.ExecuteNonQuery();
                connection.Close();
                txtBox_clientLastName.Clear();
                txtBox_clientFirstName.Clear();
                txtBox_clientPhone.Clear();
                txt_Balanse.Clear();



                MessageBox.Show("New client was added to the base");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
             this.Close();
         
        }

    }
}
