using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace MCProtocolLibrary
{
    public class ConnectPLC
    {
        private string connection_String;
        private string nameFuntion;

        public string Connection_String { get => connection_String; set => connection_String = value; }
        public string NameFuntion { get => nameFuntion; set => nameFuntion = value; }

        //public string TableSaveData { get => tableSaveData; set => tableSaveData = value; }

        public ConnectPLC(string inputconnectionstring, string inputNameMachine)
        {
            connection_String = inputconnectionstring;
            nameFuntion = inputNameMachine;

        }
        public static byte[] Converttextdevicetohexdevice(string namedevice)
        {
            byte[] bytereturn = null;
            byte[] X = { 0x9C };
            byte[] Y = { 0x9D };
            byte[] M = { 0x90 };
            byte[] L = { 0x92 };
            byte[] B = { 0xA0 };
            byte[] D = { 0xA8 };
            byte[] W = { 0xB4 };
            byte[] ZR = { 0xB0 };

            Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();
            data.Add("X", X);
            data.Add("Y", Y);
            data.Add("M", M);
            data.Add("L", L);
            data.Add("B", B);
            data.Add("D", D);
            data.Add("W", W);
            data.Add("ZR", ZR);

            foreach (var item in data)
            {
                if (namedevice == item.Key)
                {
                    bytereturn = item.Value;
                }
            }
            return bytereturn;
        }
        public TemplateData GetTemplateDatas()
        {
            TemplateData Data = new TemplateData();
            // GetConfig
            using (SqlConnection conn = new SqlConnection(Connection_String))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"Select * from ConfigConnectToServerNew Where NameFunction = '{nameFuntion}'", conn);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Data.NameFunction = (string)dt.Rows[0]["NameFunction"];
                    Data.IpAddressServer = (string)dt.Rows[0]["IpAddressServer"];
                    Data.PortServer = (int)dt.Rows[0]["PortServer"];
                    Data.TypeTriger = (string)dt.Rows[0]["TypeTriger"];
                    Data.DeviceTriger = (int)dt.Rows[0]["DeviceTriger"];
                    Data.TypeCompleted = (string)dt.Rows[0]["TypeCompleted"];
                    Data.DeviceCompleted = (int)dt.Rows[0]["DeviceCompleted"];
                    for (int i = 7; i < dt.Columns.Count; i += 4)
                    {
                        if (dt.Rows[0][i].ToString() != "" && dt.Rows[0][i + 1].ToString() != "" && dt.Rows[0][i + 2].ToString() != "" && dt.Rows[0][i + 3].ToString() != "")
                            Data.Listdataread.Add(new TemplateDataRead()
                            {
                                TypeData = (string)dt.Rows[0][i],
                                DeviceData = (int)dt.Rows[0][i + 1],
                                LengthData = (int)dt.Rows[0][i + 2],
                                DataType = (string)dt.Rows[0][i + 3]
                            });
                    }
                }
                conn.Close();
            };
            return Data;
        }
        public async Task<string> TempleReadDataPLCASCII(TemplateData DataTemplete, string NameMachine, int Timeout)
        {
            try
            {

                using (TcpClient client = new TcpClient())
                {
                    //CMD Get Data Result
                    byte[] FinalCmdGetData = new byte[21];
                    byte[] PathCmdGetData = { 0x50, 0x00, 0x00, 0xff, 0xff, 0x03, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x01, 0x04, 0x00, 0x00 };

                    //CMD Check Exist Data
                    byte[] FinalCmdCheckExistData = new byte[21];
                    byte[] PathCmdCheckExistData = { 0x50, 0x00, 0x00, 0xff, 0xff, 0x03, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x01, 0x04, 0x01, 0x00 };
                    byte[] DeviceCheckExistData = BitConverter.GetBytes(DataTemplete.DeviceTriger);
                    byte[] TypeDeviceCheckExistData = Converttextdevicetohexdevice(DataTemplete.TypeTriger);
                    byte[] NumberofDeviceCheckExistData = BitConverter.GetBytes(1);
                    Buffer.BlockCopy(PathCmdCheckExistData, 0, FinalCmdCheckExistData, 0, PathCmdCheckExistData.Length);
                    Buffer.BlockCopy(DeviceCheckExistData, 0, FinalCmdCheckExistData, PathCmdCheckExistData.Length, 3);
                    Buffer.BlockCopy(TypeDeviceCheckExistData, 0, FinalCmdCheckExistData, PathCmdCheckExistData.Length + 3, 1);
                    Buffer.BlockCopy(NumberofDeviceCheckExistData, 0, FinalCmdCheckExistData, PathCmdCheckExistData.Length + 4, 2);

                    //CMD Clear Data
                    byte[] On = { 0x10 };
                    byte[] FinalCmdClearData = new byte[22];
                    byte[] PathCmdClearData = { 0x50, 0x00, 0x00, 0xff, 0xff, 0x03, 0x00, 0x0D, 0x00, 0x00, 0x00, 0x01, 0x14, 0x01, 0x00 };
                    byte[] DeviceClearData = BitConverter.GetBytes(DataTemplete.DeviceCompleted);
                    byte[] TypeDeviceClearData = Converttextdevicetohexdevice(DataTemplete.TypeCompleted);
                    byte[] NumberofDeviceClear = { 0x01, 0x00 };
                    Buffer.BlockCopy(PathCmdClearData, 0, FinalCmdClearData, 0, PathCmdClearData.Length);
                    Buffer.BlockCopy(DeviceClearData, 0, FinalCmdClearData, PathCmdClearData.Length, 3);
                    Buffer.BlockCopy(TypeDeviceClearData, 0, FinalCmdClearData, PathCmdClearData.Length + 3, 1);
                    Buffer.BlockCopy(NumberofDeviceClear, 0, FinalCmdClearData, PathCmdClearData.Length + 4, 2);
                    Buffer.BlockCopy(On, 0, FinalCmdClearData, PathCmdClearData.Length + 6, 1);
                    // cancell
                    CancellationTokenSource writeCancellationToken = new CancellationTokenSource();
                    CancellationTokenSource readCancellationToken = new CancellationTokenSource();
                    List<string> listdataread = new List<string>();
                    int checkexist = 0;
                    byte[] DataCheckExist = new byte[100];
                    byte[] BuffCabi = new byte[100];
                    byte[] BuffData = new byte[100];
                    byte[] BuffResult = new byte[100];
                    byte[] DataClear = new byte[100];
                    int savedata = 0;
                    //OpenConnect
                    CancellationTokenSource connectCancellationToken = new CancellationTokenSource();
                    Task connectTask = client.ConnectAsync(IPAddress.Parse(DataTemplete.IpAddressServer), DataTemplete.PortServer);
                    if (await Task.WhenAny(connectTask, Task.Delay(Timeout, connectCancellationToken.Token)) != connectTask)
                    {
                        connectCancellationToken.Cancel();
                        throw new TimeoutException("Error timed out Open Connection .");
                    }
                    await connectTask;
                    NetworkStream stream = client.GetStream();
                    //Check Exist Data
                    //
                    Task writeTask = stream.WriteAsync(FinalCmdCheckExistData, 0, FinalCmdCheckExistData.Length, writeCancellationToken.Token);
                    if (await Task.WhenAny(writeTask, Task.Delay(Timeout, writeCancellationToken.Token)) != writeTask)
                    {
                        writeCancellationToken.Cancel();
                        throw new TimeoutException("Error Trigger Exist Send timed out.");
                    }
                    await writeTask;
                    Task<int> readTask = stream.ReadAsync(DataCheckExist, 0, DataCheckExist.Length, readCancellationToken.Token);
                    if (await Task.WhenAny(readTask, Task.Delay(Timeout, readCancellationToken.Token)) != readTask)
                    {
                        readCancellationToken.Cancel();
                        throw new TimeoutException("Error Trigger Exist Receive timed out.");
                    }
                    int bytesRead = await readTask;
                    if (DataCheckExist[9] == 0 && DataCheckExist[10] == 0)
                    {
                        checkexist = BitConverter.ToInt32(DataCheckExist, 11);
                    }
                    if (checkexist != 0)
                    {

                        foreach (TemplateDataRead item in DataTemplete.Listdataread)
                        {
                            Array.Clear(BuffData, 0, BuffData.Length);
                            byte[] DeviceStoreResult = BitConverter.GetBytes(item.DeviceData);
                            byte[] TypeDeviceStoreResult = Converttextdevicetohexdevice(item.TypeData);
                            byte[] LengthDeviceStoreResult = BitConverter.GetBytes(item.LengthData);
                            Buffer.BlockCopy(PathCmdGetData, 0, FinalCmdGetData, 0, PathCmdGetData.Length);
                            Buffer.BlockCopy(DeviceStoreResult, 0, FinalCmdGetData, PathCmdGetData.Length, 3);
                            Buffer.BlockCopy(TypeDeviceStoreResult, 0, FinalCmdGetData, PathCmdGetData.Length + 3, 1);
                            Buffer.BlockCopy(LengthDeviceStoreResult, 0, FinalCmdGetData, PathCmdGetData.Length + 4, 2);

                            Task writeTaskCabi = stream.WriteAsync(FinalCmdGetData, 0, FinalCmdGetData.Length, writeCancellationToken.Token);
                            if (await Task.WhenAny(writeTask, Task.Delay(Timeout, writeCancellationToken.Token)) != writeTask)
                            {
                                writeCancellationToken.Cancel();
                                throw new TimeoutException("Error Data Cabi Send timed out.");
                            }
                            await writeTaskCabi;
                            Task<int> readTaskCabi = stream.ReadAsync(BuffData, 0, BuffData.Length, readCancellationToken.Token);
                            if (await Task.WhenAny(readTask, Task.Delay(Timeout, readCancellationToken.Token)) != readTask)
                            {
                                readCancellationToken.Cancel();
                                throw new TimeoutException("Error Data Cabi Receive timed out.");
                            }
                            int bytesReadCabi = await readTaskCabi;
                            if (BuffCabi[9] == 0 && BuffCabi[10] == 0)
                            {
                                if (item.DataType == "Float")
                                {
                                    byte[] buff1 = new byte[] { BuffData[11], BuffData[12], BuffData[13], BuffData[14] };
                                    float fl = BitConverter.ToSingle(buff1, 0);
                                    listdataread.Add(fl.ToString());
                                }
                                else if (item.DataType == "DEC")
                                {
                                    byte[] buff1 = new byte[] { BuffData[11], BuffData[12], BuffData[13], BuffData[14] };
                                    int Dec = BitConverter.ToInt32(buff1, 0);
                                    listdataread.Add(Dec.ToString());
                                }
                                else
                                {
                                    listdataread.Add(Encoding.ASCII.GetString(BuffData, 11, findnull(BuffData)).Trim('\0').Trim('\r').Trim('\n'));
                                }
                            }
                            
                        }
                        if (listdataread.Count > 0)
                        {
                            int countdata = listdataread.Count;
                            if (countdata < 7)
                            {
                                for (int i = countdata; i < 7; i++)
                                {
                                    listdataread.Add("");
                                }
                            }
                            // lưu dữ liệu
                            using (SqlConnection conn = new SqlConnection(Connection_String))
                            {
                                conn.Open();
                                string query = "";
                                if (listdataread[0].Length >= 19 && listdataread[0].Contains("NR"))
                                {
                                    query = $"Insert into {DataTemplete.NameFunction} (Data1, Data2, Data3, Data4, Data5,Data6,Data7, TimeUpdate) Values('{listdataread[0].Substring(0, 12)}','{listdataread[1]}','{listdataread[2]}','{listdataread[3]}','{listdataread[4]}','{listdataread[5]}','{listdataread[6]}','{listdataread[7]}','{DateTime.Now}') ";
                                }
                                else
                                {
                                    query = $"Insert into {DataTemplete.NameFunction} (Data1, Data2, Data3, Data4, Data5,Data6,Data7, TimeUpdate) Values('{listdataread[0]}','{listdataread[1]}','{listdataread[2]}','{listdataread[3]}','{listdataread[4]}','{listdataread[5]}','{listdataread[6]}','{listdataread[7]}','{DateTime.Now}') ";

                                }
                                SqlCommand cmd = new SqlCommand(query, conn);
                                savedata = cmd.ExecuteNonQuery();
                                conn.Close();
                            };
                            // Xóa dữ liệu PLC
                            if (savedata > 0)
                            {
                                Task writeTaskClear = stream.WriteAsync(FinalCmdClearData, 0, FinalCmdClearData.Length, writeCancellationToken.Token);
                                if (await Task.WhenAny(writeTask, Task.Delay(Timeout, writeCancellationToken.Token)) != writeTask)
                                {
                                    writeCancellationToken.Cancel();
                                    throw new TimeoutException("Error Confirm Completed Send timed out.");
                                }
                                await writeTaskClear;
                                Task<int> readTaskClear = stream.ReadAsync(DataClear, 0, DataClear.Length, readCancellationToken.Token);
                                if (await Task.WhenAny(readTask, Task.Delay(Timeout, readCancellationToken.Token)) != readTask)
                                {
                                    readCancellationToken.Cancel();
                                    throw new TimeoutException("Error Confirm Completed Receive timed out.");
                                }
                                int bytesReadClear = await readTaskClear;
                            }

                        }
                    }

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") + "," + NameMachine + "," + ex.Message;
            }
            return "Finish";

        }
        public int findnull(byte[] input)
        {
            for (int i = 12; i < input.Length; i++)
            {
                if (input[i] == 0)
                {
                    return i;
                }
            }
            return input.Length;
        }
    }
}