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

namespace GetDataPLC
{
    public partial class ViewData : Form
    {
        public ViewData()
        {
            InitializeComponent();
            try
            {
                getlistfunction();
            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.Message);
            }
        }
        public void getlistfunction()
        {
            DataTable dt = DBConnect.StoreFillDS("dbo.GetListFunction", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                cmb_listmachine.DataSource = dt;
                cmb_listmachine.DisplayMember = "NameFunction";
                cmb_listmachine.ValueMember = "NameFunction";
            }
            cmb_listmachine.Refresh();
        }
        private void btn_Reload_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DBConnect.connection_string))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"Select top 100 * from {cmb_listmachine.Text}(nolock) order by TimeUpdate desc", conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(dt);
                    dtg_dataview.DataSource = dt;
                    dtg_dataview.Refresh();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void cmb_listmachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_listmachine.Text != "")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DBConnect.connection_string))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand($"Select top 100 * from {cmb_listmachine.Text}(nolock) order by TimeUpdate desc" , conn);
                        DataTable dt = new DataTable();
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        adap.Fill(dt);
                        dtg_dataview.DataSource = dt;
                        dtg_dataview.Refresh();
                    }
                }
                catch (Exception ex)
                {

                    
                }
            }
            
        }
    }
}
