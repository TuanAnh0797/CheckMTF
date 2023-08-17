using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetDataPLC
{
    public partial class SetupGetData : Form
    {
        public delegate void Mydelegate();
        public Mydelegate UpdateCombobox;
        public SetupGetData()
        {
            try
            {
                InitializeComponent();
                getdataconfig();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public void getdataconfig()
        {
            DataTable dt = DBConnect.StoreFillDS("dbo.GetDataConfigConnectToServerNew", CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                dtg_dataconfig.DataSource = dt;
            }
            dtg_dataconfig.Refresh();
        }
        //public void SearchDataConfig()
        //{
        //    DataTable dt = DBConnect.StoreFillDS("dbo.SearchDataConfigConnectToServer", CommandType.StoredProcedure, txb_namemachinesearch.Text);
        //    if (dt.Rows.Count > 0)
        //    {
        //        dtg_dataconfig.DataSource = dt;
        //    }
        //    dtg_dataconfig.Refresh();
        //}
        private void cmb_datatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_datatype1.Text != "String")
            {
                txb_lenghtdevicedata1.Text = "2";
                txb_lenghtdevicedata1.Enabled = false;
            }
            else
            {
                txb_lenghtdevicedata1.Enabled = true;
            }
        }

        //private void btn_search_Click(object sender, EventArgs e)
        //{
        //    SearchDataConfig();
        //}

        //private void btn_reload_Click(object sender, EventArgs e)
        //{
        //    getdataconfig();
        //}

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                int rs = (int)DBConnect.excutenonquery("Update_ConfigConnectToServerNew", CommandType.StoredProcedure,
                txb_namefunction.Text,
                 txb_ipaddress.Text,
                Convert.ToInt32(txb_portnumber.Text),
                cmb_TypeDeviceTrigerRead.Text, Convert.ToInt32(txb_NameDeviceTrigerRead.Text),
                cmb_TypeDeviceTrigerReadComplete.Text, Convert.ToInt32(txb_NameDeviceTrigerReadComplete.Text),

                cmb_typedevicedata1.Text, Convert.ToInt32(txb_namedevicedata1.Text), Convert.ToInt32(txb_lenghtdevicedata1.Text), cmb_datatype1.Text,

                cmb_typedevicedata2.Text, Convert.ToInt32(txb_namedevicedata2.Text), Convert.ToInt32(txb_lenghtdevicedata2.Text), cmb_datatype2.Text,

                  cmb_typedevicedata3.Text, Convert.ToInt32(txb_namedevicedata3.Text), Convert.ToInt32(txb_lenghtdevicedata3.Text), cmb_datatype3.Text,

                   cmb_typedevicedata4.Text, Convert.ToInt32(txb_namedevicedata4.Text), Convert.ToInt32(txb_lenghtdevicedata4.Text), cmb_datatype4.Text,

                    cmb_typedevicedata5.Text, Convert.ToInt32(txb_namedevicedata5.Text), Convert.ToInt32(txb_lenghtdevicedata5.Text), cmb_datatype5.Text,

                     cmb_typedevicedata6.Text, Convert.ToInt32(txb_namedevicedata6.Text), Convert.ToInt32(txb_lenghtdevicedata6.Text), cmb_datatype6.Text,

                      cmb_typedevicedata7.Text, Convert.ToInt32(txb_namedevicedata7.Text), Convert.ToInt32(txb_lenghtdevicedata7.Text), cmb_datatype7.Text

                      );
                getdataconfig();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dtg_dataconfig_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (dtg_dataconfig.SelectedRows.Count > 0)
                {
                    // Lấy dòng được chọn
                    DataGridViewRow selectedRow = dtg_dataconfig.SelectedRows[0];
                    // Lấy giá trị từ ô cột cần lấy (thay "ColumnName" bằng tên cột thực sự)
                    txb_namefunction.Text = selectedRow.Cells["NameFunction"].Value.ToString();
                    txb_ipaddress.Text = selectedRow.Cells["IpAddressServer"].Value.ToString();
                    txb_portnumber.Text = selectedRow.Cells["PortServer"].Value.ToString();
                    cmb_TypeDeviceTrigerRead.Text = selectedRow.Cells["TypeTriger"].Value.ToString();
                    txb_NameDeviceTrigerRead.Text = selectedRow.Cells["DeviceTriger"].Value.ToString();
                    cmb_TypeDeviceTrigerReadComplete.Text = selectedRow.Cells["TypeCompleted"].Value.ToString();
                    txb_NameDeviceTrigerReadComplete.Text = selectedRow.Cells["DeviceCompleted"].Value.ToString();
                    //
                    cmb_typedevicedata1.Text = selectedRow.Cells["TypeData1"].Value.ToString();
                    txb_namedevicedata1.Text = selectedRow.Cells["DeviceData1"].Value.ToString();
                    txb_lenghtdevicedata1.Text = selectedRow.Cells["LengthData1"].Value.ToString();
                    cmb_datatype1.Text = selectedRow.Cells["DataType1"].Value.ToString();
                    //
                    cmb_typedevicedata2.Text = selectedRow.Cells["TypeData2"].Value.ToString();
                    txb_namedevicedata2.Text = selectedRow.Cells["DeviceData2"].Value.ToString();
                    txb_lenghtdevicedata2.Text = selectedRow.Cells["LengthData2"].Value.ToString();
                    cmb_datatype2.Text = selectedRow.Cells["DataType2"].Value.ToString();
                    //
                    cmb_typedevicedata3.Text = selectedRow.Cells["TypeData3"].Value.ToString();
                    txb_namedevicedata3.Text = selectedRow.Cells["DeviceData3"].Value.ToString();
                    txb_lenghtdevicedata3.Text = selectedRow.Cells["LengthData3"].Value.ToString();
                    cmb_datatype3.Text = selectedRow.Cells["DataType3"].Value.ToString();
                    //
                    cmb_typedevicedata4.Text = selectedRow.Cells["TypeData4"].Value.ToString();
                    txb_namedevicedata4.Text = selectedRow.Cells["DeviceData4"].Value.ToString();
                    txb_lenghtdevicedata4.Text = selectedRow.Cells["LengthData4"].Value.ToString();
                    cmb_datatype4.Text = selectedRow.Cells["DataType4"].Value.ToString();
                    //
                    cmb_typedevicedata5.Text = selectedRow.Cells["TypeData5"].Value.ToString();
                    txb_namedevicedata5.Text = selectedRow.Cells["DeviceData5"].Value.ToString();
                    txb_lenghtdevicedata5.Text = selectedRow.Cells["LengthData5"].Value.ToString();
                    cmb_datatype5.Text = selectedRow.Cells["DataType5"].Value.ToString();
                    //
                    cmb_typedevicedata6.Text = selectedRow.Cells["TypeData6"].Value.ToString();
                    txb_namedevicedata6.Text = selectedRow.Cells["DeviceData6"].Value.ToString();
                    txb_lenghtdevicedata6.Text = selectedRow.Cells["LengthData6"].Value.ToString();
                    cmb_datatype6.Text = selectedRow.Cells["DataType6"].Value.ToString();
                    //
                    cmb_typedevicedata7.Text = selectedRow.Cells["TypeData7"].Value.ToString();
                    txb_namedevicedata7.Text = selectedRow.Cells["DeviceData7"].Value.ToString();
                    txb_lenghtdevicedata7.Text = selectedRow.Cells["LengthData7"].Value.ToString();
                    cmb_datatype7.Text = selectedRow.Cells["DataType7"].Value.ToString();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            try
            {
                int rs = (int)DBConnect.excutenonquery("Add_ConfigConnectToServerNew", CommandType.StoredProcedure,
                txb_namefunction.Text,
                txb_ipaddress.Text,
                Convert.ToInt32(txb_portnumber.Text),
                cmb_TypeDeviceTrigerRead.Text, Convert.ToInt32(txb_NameDeviceTrigerRead.Text),
                cmb_TypeDeviceTrigerReadComplete.Text, Convert.ToInt32(txb_NameDeviceTrigerReadComplete.Text),

                cmb_typedevicedata1.Text, Convert.ToInt32(txb_namedevicedata1.Text), Convert.ToInt32(txb_lenghtdevicedata1.Text), cmb_datatype1.Text,

                cmb_typedevicedata2.Text, Convert.ToInt32(txb_namedevicedata2.Text), Convert.ToInt32(txb_lenghtdevicedata2.Text), cmb_datatype2.Text,

                  cmb_typedevicedata3.Text, Convert.ToInt32(txb_namedevicedata3.Text), Convert.ToInt32(txb_lenghtdevicedata3.Text), cmb_datatype3.Text,

                   cmb_typedevicedata4.Text, Convert.ToInt32(txb_namedevicedata4.Text), Convert.ToInt32(txb_lenghtdevicedata4.Text), cmb_datatype4.Text,

                    cmb_typedevicedata5.Text, Convert.ToInt32(txb_namedevicedata5.Text), Convert.ToInt32(txb_lenghtdevicedata5.Text), cmb_datatype5.Text,

                     cmb_typedevicedata6.Text, Convert.ToInt32(txb_namedevicedata6.Text), Convert.ToInt32(txb_lenghtdevicedata6.Text), cmb_datatype6.Text,

                      cmb_typedevicedata7.Text, Convert.ToInt32(txb_namedevicedata7.Text), Convert.ToInt32(txb_lenghtdevicedata7.Text), cmb_datatype7.Text

                      );


                getdataconfig();
                UpdateCombobox?.Invoke();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
