using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GetDataPLC
{
    public partial class MainForm : Form
    {
        DataTable dt;
        List<string> ListMachine = new List<string>();
        public MainForm()
        {
            try
            {
                InitializeComponent();
                GetListMachine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void setupGetDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupGetData p = new SetupGetData();
            p.UpdateCombobox = new SetupGetData.Mydelegate(GetListMachine);
            p.Show();

        }
        public void GetListMachine()
        {
            dt = DBConnect.StoreFillDS("GetListMachineNew", CommandType.StoredProcedure);
            DataRow newRow = dt.NewRow();
            newRow["NameFunction"] = "--Select Machine--";
            dt.Rows.InsertAt(newRow, 0);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListMachine.Add(dt.Rows[i]["NameFunction"].ToString());
                    cmb_listmachine.Items.Add(dt.Rows[i]["NameFunction"].ToString());
                }
            }
        }

        private void cmb_listmachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_listmachine.SelectedIndex > 0)
                {
                    float columnWidth = tableLayoutPanel1.Width / tableLayoutPanel1.ColumnCount;
                    float columnheigt = tableLayoutPanel1.Height / tableLayoutPanel1.RowCount;
                    if (tableLayoutPanel1.Controls.Count < (tableLayoutPanel1.RowCount * tableLayoutPanel1.ColumnCount))
                    {
                        PLC p = new PLC(Convert.ToInt32(columnWidth), Convert.ToInt32(columnheigt), cmb_listmachine.Text.ToString());
                        Size sizepanel = new Size(tableLayoutPanel1.Width, tableLayoutPanel1.Height);
                        p.MaximumSize = sizepanel;
                        p.TopLevel = false;
                        tableLayoutPanel1.Controls.Add(p);
                        cmb_listmachine.Items.Remove(cmb_listmachine.Text);
                        cmb_listmachine.SelectedIndex = 0;
                        p.AddMC = new PLC.Mydelegate(AddMachineWhenClosePLC);
                        p.Show();

                    }
                    else
                    {
                        MessageBox.Show("Full");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        public void AddMachineWhenClosePLC(string MC)
        {
            cmb_listmachine.Items.Add(MC);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < ListMachine.Count; i++)
                {

                    float columnWidth = tableLayoutPanel1.Width / tableLayoutPanel1.ColumnCount;
                    float columnheigt = tableLayoutPanel1.Height / tableLayoutPanel1.RowCount;
                    if (tableLayoutPanel1.Controls.Count < (tableLayoutPanel1.RowCount * tableLayoutPanel1.ColumnCount))
                    {
                        PLC p = new PLC(Convert.ToInt32(columnWidth), Convert.ToInt32(columnheigt), ListMachine[i].ToString());
                        Size sizepanel = new Size(tableLayoutPanel1.Width, tableLayoutPanel1.Height);
                        p.MaximumSize = sizepanel;
                        p.TopLevel = false;
                        tableLayoutPanel1.Controls.Add(p);
                        cmb_listmachine.Items.RemoveAt(1);
                        cmb_listmachine.SelectedIndex = 0;
                        p.AddMC = new PLC.Mydelegate(AddMachineWhenClosePLC);
                        p.Show();

                    }
                    else
                    {
                        MessageBox.Show("Full");
                    }

                }
                button1.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewData p = new ViewData();
            p.Show();
        }

        private void tableLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if(tableLayoutPanel1.Controls.Count == 0)
            {
                button1.Enabled = true;
            }
        }
    }
}
