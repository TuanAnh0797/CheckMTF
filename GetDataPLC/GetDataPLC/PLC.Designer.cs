namespace GetDataPLC
{
    partial class PLC
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
            this.btn_start = new System.Windows.Forms.Button();
            this.txb_IPAddress = new System.Windows.Forms.TextBox();
            this.txb_portnumber = new System.Windows.Forms.TextBox();
            this.lbl_IPAddress = new System.Windows.Forms.Label();
            this.lbl_portnumber = new System.Windows.Forms.Label();
            this.txb_messageError = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txb_namemachine = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Green;
            this.btn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Location = new System.Drawing.Point(235, 42);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(55, 48);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Visible = false;
            // 
            // txb_IPAddress
            // 
            this.txb_IPAddress.Location = new System.Drawing.Point(82, 43);
            this.txb_IPAddress.Name = "txb_IPAddress";
            this.txb_IPAddress.ReadOnly = true;
            this.txb_IPAddress.Size = new System.Drawing.Size(149, 20);
            this.txb_IPAddress.TabIndex = 2;
            // 
            // txb_portnumber
            // 
            this.txb_portnumber.Location = new System.Drawing.Point(82, 70);
            this.txb_portnumber.Name = "txb_portnumber";
            this.txb_portnumber.ReadOnly = true;
            this.txb_portnumber.Size = new System.Drawing.Size(149, 20);
            this.txb_portnumber.TabIndex = 3;
            // 
            // lbl_IPAddress
            // 
            this.lbl_IPAddress.AutoSize = true;
            this.lbl_IPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_IPAddress.Location = new System.Drawing.Point(4, 45);
            this.lbl_IPAddress.Name = "lbl_IPAddress";
            this.lbl_IPAddress.Size = new System.Drawing.Size(71, 15);
            this.lbl_IPAddress.TabIndex = 4;
            this.lbl_IPAddress.Text = "IPAddress";
            // 
            // lbl_portnumber
            // 
            this.lbl_portnumber.AutoSize = true;
            this.lbl_portnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_portnumber.Location = new System.Drawing.Point(5, 72);
            this.lbl_portnumber.Name = "lbl_portnumber";
            this.lbl_portnumber.Size = new System.Drawing.Size(33, 15);
            this.lbl_portnumber.TabIndex = 5;
            this.lbl_portnumber.Text = "Port";
            // 
            // txb_messageError
            // 
            this.txb_messageError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_messageError.Location = new System.Drawing.Point(4, 147);
            this.txb_messageError.Multiline = true;
            this.txb_messageError.Name = "txb_messageError";
            this.txb_messageError.ReadOnly = true;
            this.txb_messageError.Size = new System.Drawing.Size(287, 113);
            this.txb_messageError.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Status";
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.BackColor = System.Drawing.Color.Gold;
            this.lbl_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(114, 99);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Padding = new System.Windows.Forms.Padding(5);
            this.lbl_Status.Size = new System.Drawing.Size(101, 28);
            this.lbl_Status.TabIndex = 9;
            this.lbl_Status.Text = "Connecting..";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message Error";
            // 
            // txb_namemachine
            // 
            this.txb_namemachine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_namemachine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txb_namemachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txb_namemachine.Location = new System.Drawing.Point(4, 8);
            this.txb_namemachine.Name = "txb_namemachine";
            this.txb_namemachine.ReadOnly = true;
            this.txb_namemachine.Size = new System.Drawing.Size(287, 19);
            this.txb_namemachine.TabIndex = 7;
            this.txb_namemachine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 266);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_namemachine);
            this.Controls.Add(this.txb_messageError);
            this.Controls.Add(this.lbl_portnumber);
            this.Controls.Add(this.lbl_IPAddress);
            this.Controls.Add(this.txb_portnumber);
            this.Controls.Add(this.txb_IPAddress);
            this.Controls.Add(this.btn_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PLC";
            this.Text = "PLC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PLC_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PLC_FormClosed);
            this.Load += new System.EventHandler(this.PLC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.TextBox txb_IPAddress;
        private System.Windows.Forms.TextBox txb_portnumber;
        private System.Windows.Forms.Label lbl_IPAddress;
        private System.Windows.Forms.Label lbl_portnumber;
        private System.Windows.Forms.TextBox txb_messageError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_namemachine;
    }
}