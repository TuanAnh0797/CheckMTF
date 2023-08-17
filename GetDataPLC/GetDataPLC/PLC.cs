using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCProtocolLibrary;

namespace GetDataPLC
{
    public partial class PLC : Form
    {
        bool CheckConnect;
        bool StartStop =true;
        public string namemachine;
        ConnectPLC plc;
        TemplateData listdata;
        public delegate void Mydelegate(string MC);
        public Mydelegate AddMC;
        public PLC(int inputWidth, int inputHeight, string inputNamemachine)
        {
            try
            {
                InitializeComponent();
                Size = new Size(inputWidth, inputHeight);
                namemachine = inputNamemachine;
                txb_namemachine.Text = namemachine;
                this.Text = namemachine;
                plc = new ConnectPLC(DBConnect.connection_string, namemachine);
                listdata = plc.GetTemplateDatas();
                txb_IPAddress.Text = listdata.IpAddressServer;
                txb_portnumber.Text = listdata.PortServer.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    if (StartStop)
        //    {
        //        btn_start?.Invoke(new Action(() =>
        //        {
        //            btn_start.Text = "Start";
        //            btn_start.BackColor = Color.Green;
        //            StartStop = false;

        //        }));

        //    }
        //    else
        //    {
        //        btn_start?.Invoke(new Action(() =>
        //        {
        //            btn_start.Text = "Stop";
        //            btn_start.BackColor = Color.Red;
        //            StartStop = true;
        //        }));
        //    }
        //    while(StartStop)
        //    {
        //        try
        //        {
        //            string messager = await plc.TempleReadDataPLCASCII(listdata, namemachine, 10000);
        //            if (messager == "Finish")
        //            {
        //                if (CheckConnect)
        //                {
        //                    savelog(messager);
        //                    CheckConnect = false;
        //                }
        //                lbl_Status?.Invoke(new Action(() =>
        //                {
        //                    lbl_Status.Text = "Conntected";
        //                    lbl_Status.BackColor = Color.Green;
        //                }));
        //                txb_messageError?.Invoke(new Action(() =>
        //                {
        //                    txb_messageError.Text = "";
        //                }));
        //            }
        //            else
        //            {
        //                if(!CheckConnect)
        //                {
        //                    savelog(messager);
        //                    CheckConnect = true;
        //                }
        //                lbl_Status?.Invoke(new Action(() =>
        //                {
        //                    lbl_Status.Text = "Conntecting..";
        //                    lbl_Status.BackColor = Color.Yellow;
        //                }));
        //                txb_messageError?.Invoke(new Action(() =>
        //                {
        //                    txb_messageError.Text = messager;
        //                }));
        //            }
                    
        //        }
        //        catch (Exception ex)
        //        {

        //            MessageBox.Show(ex.Message);
        //        }
        //    }
       // }

        private void PLC_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(MessageBox.Show($"Bạn có chắc chắn muốn dừng lấy dữ liệu tại {namemachine}","Thông báo",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                StartStop = false;
                AddMC?.Invoke(namemachine);
               
            }
            else
            {
                e.Cancel = true;
            }
        }
        public void savelog(string log)
        {
            try
            {
                string filePath = Directory.GetCurrentDirectory() + "\\logdisconnect.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(log);
                }
            }
            catch (Exception)
            {
            }
            
        }

        private async void PLC_Load(object sender, EventArgs e)
        {
            while (StartStop)
            {
                try
                {
                    string messager = await plc.TempleReadDataPLCASCII(listdata, namemachine, 5000);
                    if (messager == "Finish")
                    {
                        if (CheckConnect)
                        {
                            savelog(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")+","+namemachine+","+ "Reconnect");
                            CheckConnect = false;
                        }
                        lbl_Status?.Invoke(new Action(() =>
                        {
                            lbl_Status.Text = "Conntected";
                            lbl_Status.BackColor = Color.Green;
                        }));
                        txb_messageError?.Invoke(new Action(() =>
                        {
                            txb_messageError.Text = "";
                        }));
                    }
                    else
                    {
                        if (!CheckConnect)
                        {
                            savelog(messager);
                            CheckConnect = true;
                        }
                        lbl_Status?.Invoke(new Action(() =>
                        {
                            lbl_Status.Text = "Conntecting..";
                            lbl_Status.BackColor = Color.Yellow;
                        }));
                        txb_messageError?.Invoke(new Action(() =>
                        {
                            txb_messageError.Text = messager;
                        }));
                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Begin"))
                    {

                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                   
                }
            }
        }

        private void PLC_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
