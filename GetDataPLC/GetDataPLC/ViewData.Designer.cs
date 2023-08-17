
namespace GetDataPLC
{
    partial class ViewData
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
            this.dtg_dataview = new System.Windows.Forms.DataGridView();
            this.cmb_listmachine = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Reload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_dataview)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg_dataview
            // 
            this.dtg_dataview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg_dataview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtg_dataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_dataview.Location = new System.Drawing.Point(8, 52);
            this.dtg_dataview.MultiSelect = false;
            this.dtg_dataview.Name = "dtg_dataview";
            this.dtg_dataview.ReadOnly = true;
            this.dtg_dataview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtg_dataview.Size = new System.Drawing.Size(784, 390);
            this.dtg_dataview.TabIndex = 15;
            // 
            // cmb_listmachine
            // 
            this.cmb_listmachine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_listmachine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_listmachine.FormattingEnabled = true;
            this.cmb_listmachine.Location = new System.Drawing.Point(113, 14);
            this.cmb_listmachine.Name = "cmb_listmachine";
            this.cmb_listmachine.Size = new System.Drawing.Size(239, 21);
            this.cmb_listmachine.TabIndex = 16;
            this.cmb_listmachine.SelectedIndexChanged += new System.EventHandler(this.cmb_listmachine_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Select Table";
            // 
            // btn_Reload
            // 
            this.btn_Reload.Location = new System.Drawing.Point(369, 10);
            this.btn_Reload.Name = "btn_Reload";
            this.btn_Reload.Size = new System.Drawing.Size(64, 29);
            this.btn_Reload.TabIndex = 18;
            this.btn_Reload.Text = "Reload";
            this.btn_Reload.UseVisualStyleBackColor = true;
            this.btn_Reload.Click += new System.EventHandler(this.btn_Reload_Click);
            // 
            // ViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Reload);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmb_listmachine);
            this.Controls.Add(this.dtg_dataview);
            this.Name = "ViewData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dtg_dataview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtg_dataview;
        private System.Windows.Forms.ComboBox cmb_listmachine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Reload;
    }
}