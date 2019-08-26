namespace ClientUI
{
    partial class ClientStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBoxStock = new System.Windows.Forms.ComboBox();
            this.btn_AddStock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Client = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Quantity = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.stockExchangeDataSet = new ClientsUI.StockExchangeDataSet();
            this.stocksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stocksTableAdapter = new ClientsUI.StockExchangeDataSetTableAdapters.StocksTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.stockExchangeDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stocksBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxStock
            // 
            this.comboBoxStock.DataSource = this.stocksBindingSource;
            this.comboBoxStock.DisplayMember = "StockPrefix";
            this.comboBoxStock.FormattingEnabled = true;
            this.comboBoxStock.Location = new System.Drawing.Point(297, 88);
            this.comboBoxStock.Name = "comboBoxStock";
            this.comboBoxStock.Size = new System.Drawing.Size(121, 24);
            this.comboBoxStock.TabIndex = 0;
            // 
            // btn_AddStock
            // 
            this.btn_AddStock.Location = new System.Drawing.Point(324, 253);
            this.btn_AddStock.Name = "btn_AddStock";
            this.btn_AddStock.Size = new System.Drawing.Size(75, 23);
            this.btn_AddStock.TabIndex = 1;
            this.btn_AddStock.Text = "Add Stock";
            this.btn_AddStock.UseVisualStyleBackColor = true;
            this.btn_AddStock.Click += new System.EventHandler(this.btn_AddStock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "ClientId";
            // 
            // txt_Client
            // 
            this.txt_Client.Location = new System.Drawing.Point(297, 30);
            this.txt_Client.Name = "txt_Client";
            this.txt_Client.ReadOnly = true;
            this.txt_Client.Size = new System.Drawing.Size(121, 22);
            this.txt_Client.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Stock";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Quantity";
            // 
            // txt_Quantity
            // 
            this.txt_Quantity.Location = new System.Drawing.Point(297, 158);
            this.txt_Quantity.Name = "txt_Quantity";
            this.txt_Quantity.Size = new System.Drawing.Size(121, 22);
            this.txt_Quantity.TabIndex = 6;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(437, 253);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // stockExchangeDataSet
            // 
            this.stockExchangeDataSet.DataSetName = "StockExchangeDataSet";
            this.stockExchangeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stocksBindingSource
            // 
            this.stocksBindingSource.DataMember = "Stocks";
            this.stocksBindingSource.DataSource = this.stockExchangeDataSet;
            // 
            // stocksTableAdapter
            // 
            this.stocksTableAdapter.ClearBeforeFill = true;
            // 
            // ClientStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.txt_Quantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Client);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_AddStock);
            this.Controls.Add(this.comboBoxStock);
            this.Name = "ClientStock";
            this.Text = "ClientStock";
            this.Load += new System.EventHandler(this.ClientStock_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.stockExchangeDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stocksBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxStock;
        private System.Windows.Forms.Button btn_AddStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Client;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Quantity;
        private System.Windows.Forms.Button btn_Cancel;
        private ClientsUI.StockExchangeDataSet stockExchangeDataSet;
        private System.Windows.Forms.BindingSource stocksBindingSource;
        private ClientsUI.StockExchangeDataSetTableAdapters.StocksTableAdapter stocksTableAdapter;
    }
}