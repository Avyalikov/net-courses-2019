namespace ClientUI
{
    partial class ClientsGridView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsGridView));
            this.dataGridViewClients = new System.Windows.Forms.DataGridView();
            this.btn_AddClient = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownBtn_Menu = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_AddClientStock = new System.Windows.Forms.Button();
            this.ClientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewClients
            // 
            this.dataGridViewClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClientID,
            this.ClientLastName,
            this.ClientFirstName,
            this.ClientPhone});
            this.dataGridViewClients.Location = new System.Drawing.Point(100, 47);
            this.dataGridViewClients.MultiSelect = false;
            this.dataGridViewClients.Name = "dataGridViewClients";
            this.dataGridViewClients.ReadOnly = true;
            this.dataGridViewClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewClients.Size = new System.Drawing.Size(586, 174);
            this.dataGridViewClients.TabIndex = 0;
            // 
            // btn_AddClient
            // 
            this.btn_AddClient.Location = new System.Drawing.Point(777, 47);
            this.btn_AddClient.Name = "btn_AddClient";
            this.btn_AddClient.Size = new System.Drawing.Size(161, 23);
            this.btn_AddClient.TabIndex = 1;
            this.btn_AddClient.Text = "Add new client";
            this.btn_AddClient.UseVisualStyleBackColor = true;
            this.btn_AddClient.Click += new System.EventHandler(this.btn_Add_Client_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownBtn_Menu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1092, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownBtn_Menu
            // 
            this.toolStripDropDownBtn_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.toolStripDropDownBtn_Menu.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownBtn_Menu.Image")));
            this.toolStripDropDownBtn_Menu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownBtn_Menu.Name = "toolStripDropDownBtn_Menu";
            this.toolStripDropDownBtn_Menu.Size = new System.Drawing.Size(67, 22);
            this.toolStripDropDownBtn_Menu.Text = "Menu";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(95, 22);
            this.toolStripMenuItem1.Text = "Info";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btn_AddClientStock
            // 
            this.btn_AddClientStock.Location = new System.Drawing.Point(777, 115);
            this.btn_AddClientStock.Name = "btn_AddClientStock";
            this.btn_AddClientStock.Size = new System.Drawing.Size(161, 23);
            this.btn_AddClientStock.TabIndex = 3;
            this.btn_AddClientStock.Text = "Add Client Stock";
            this.btn_AddClientStock.UseVisualStyleBackColor = true;
            this.btn_AddClientStock.Click += new System.EventHandler(this.btn_AddClientStock_Click);
            // 
            // ClientID
            // 
            this.ClientID.Frozen = true;
            this.ClientID.HeaderText = "ClientID";
            this.ClientID.Name = "ClientID";
            this.ClientID.ReadOnly = true;
            // 
            // ClientLastName
            // 
            this.ClientLastName.Frozen = true;
            this.ClientLastName.HeaderText = "Last Name";
            this.ClientLastName.Name = "ClientLastName";
            this.ClientLastName.ReadOnly = true;
            // 
            // ClientFirstName
            // 
            this.ClientFirstName.HeaderText = "First Name";
            this.ClientFirstName.Name = "ClientFirstName";
            this.ClientFirstName.ReadOnly = true;
            // 
            // ClientPhone
            // 
            this.ClientPhone.HeaderText = "Phone";
            this.ClientPhone.Name = "ClientPhone";
            this.ClientPhone.ReadOnly = true;
            // 
            // ClientsGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 450);
            this.Controls.Add(this.btn_AddClientStock);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btn_AddClient);
            this.Controls.Add(this.dataGridViewClients);
            this.Name = "ClientsGridView";
            this.Text = "Clients";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewClients;
        private System.Windows.Forms.Button btn_AddClient;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownBtn_Menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btn_AddClientStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientPhone;
    }
}

