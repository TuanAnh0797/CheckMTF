using Microsoft.Win32;
using Newtonsoft.Json;
using Quanlybarcode.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CheckMTF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        bool checkloadedconfig;
        Config myconfig;
        bool checkruntab1;
        bool checkruntab2;
        bool checkruntab3;

        public ObservableCollection<ListErrorFileCheck> myListErrorFileCheck { set; get; }
        public ObservableCollection<ListErrorContentFileCheck> myListErrorContentFileCheck { set; get; }

        public ObservableCollection<ListErrorConfigFileCheck> myListErrorConfigFileCheck { set; get; }


        public List<ListErrorFileCheck> myListErrorFileCheckSaveOK;
        public List<ListErrorFileCheck> myListErrorFileCheckSaveNG;
        public List<ListErrorContentFileCheck> myListErrorContentFileCheckSaveOK;
        public List<ListErrorContentFileCheck> myListErrorContentFileCheckSaveNG;
        public List<ListErrorConfigFileCheck> myListErrorConfigFileCheckSaveOK;
        public List<ListErrorConfigFileCheck> myListErrorConfigFileCheckSaveNG;
        private string dateselected;
        private string dateselectedEndtab1;
        private string modelselected;
        private string dateselectedtab2;
        private string dateselectedEndtab2;
        private string modelselectedtab2;
        private string modelselectedtab3;
        public string Dateselected
        {
            set
            {
                dateselected = value;
                OnPropertyChanged();
            }
            get
            {
                return dateselected;
            }
        }
       
        public string Modelselected
        {
            set { modelselected = value; OnPropertyChanged(); }
            get { return modelselected; }
        }
        public string DateselectedEndtab1
        {
            set
            {
                dateselectedEndtab1 = value;
                OnPropertyChanged();
            }
            get
            {
                return dateselectedEndtab1;
            }
        }
        public string Modelselectedtab2
        {
            set { modelselectedtab2 = value; OnPropertyChanged(); }
            get { return modelselectedtab2; }
        }
        public string Dateselectedtab2
        {
            set
            {
                dateselectedtab2 = value;
                OnPropertyChanged();
            }
            get
            {
                return dateselectedtab2;
            }
        }
        public string DateselectedEndtab2
        {
            set
            {
                dateselectedEndtab2 = value;
                OnPropertyChanged();
            }
            get
            {
                return dateselectedEndtab2;
            }
        }
        public string Modelselectedtab3
        {
            set { modelselectedtab3 = value; OnPropertyChanged(); }
            get { return modelselectedtab3; }
        }

        public List<string> cmb_data { get; set; }
        public List<string> cmb_datatab2 { get; set; }

        public List<string> cmb_datatab3 { get; set; }


        public ICommand cmd_startcheck { get; set; }
        public ICommand cmd_exportdatatab1 { get; set; }
        public ICommand cmd_startchecktab2 { get; set; }
        public ICommand cmd_exportdatatab2 { get; set; }
        public ICommand cmd_startchecktab3 { get; set; }
        public ICommand cmd_exportdatatab3 { get; set; }
        public ICommand cmd_Uncheckedtab1 { get; set; }
        public ICommand cmd_Uncheckedtab2 { get; set; }
        public MainViewModel()
        {
            checkruntab1 = false;
            checkruntab2 = false;
            checkruntab3 = false;
            checkloadedconfig = false;
            if (!checkloadedconfig)
            {
                loadconfig();
            }
            myListErrorFileCheck = new ObservableCollection<ListErrorFileCheck>();
            myListErrorContentFileCheck = new ObservableCollection<ListErrorContentFileCheck>();
            myListErrorConfigFileCheck = new ObservableCollection<ListErrorConfigFileCheck>();
            myListErrorFileCheckSaveOK = new List<ListErrorFileCheck>();
            myListErrorFileCheckSaveNG = new List<ListErrorFileCheck>();
            myListErrorContentFileCheckSaveOK = new List<ListErrorContentFileCheck>();
            myListErrorContentFileCheckSaveNG = new List<ListErrorContentFileCheck>();
            myListErrorConfigFileCheckSaveOK = new List<ListErrorConfigFileCheck>();
            myListErrorConfigFileCheckSaveNG = new List<ListErrorConfigFileCheck>();
            cmd_startcheck = new RelayCommand<object>((p) => { return checkaccess(modelselected, dateselected, checkruntab1); }, (p) => { CheckModifyFile(); });
            cmd_exportdatatab1 = new RelayCommand<object>((p) => { return checkexport<ListErrorFileCheck>(myListErrorFileCheck); }, (p) => { exportdatatocsv<ListErrorFileCheck>(myListErrorFileCheck, "FilePath,FileName,DateCreate,DateModify", "ModifyFileCheck"); });
            cmd_Uncheckedtab1 = new RelayCommand<object>((p) => { return true; }, (p) => { DateselectedEndtab1 = null; });
            cmd_Uncheckedtab2 = new RelayCommand<object>((p) => { return true; }, (p) => { DateselectedEndtab2 = null; });
            cmd_startchecktab2 = new RelayCommand<object>((p) => { return checkaccess(modelselectedtab2, dateselectedtab2,checkruntab2); }, (p) => { ChecContentFile(); });
            cmd_exportdatatab2 = new RelayCommand<object>((p) => { return checkexport<ListErrorContentFileCheck>(myListErrorContentFileCheck); }, (p) => { exportdatatocsv<ListErrorContentFileCheck>(myListErrorContentFileCheck, "FilePath,FileName,NumberLine,Header,End,Status","ModifyContentFileCheck"); });
            cmd_startchecktab3 = new RelayCommand<object>((p) => { return checkaccess(modelselectedtab3, "Nocheck", checkruntab3); }, (p) => { CheckModifyFileConfig(); });
            cmd_exportdatatab3 = new RelayCommand<object>((p) => { return checkexport<ListErrorConfigFileCheck>(myListErrorConfigFileCheck); }, (p) => { exportdatatocsv<ListErrorConfigFileCheck>(myListErrorConfigFileCheck, "FilePath,FileName,DateModiFy,Status","ModifyFileConfig"); });

        }
        public bool checkexport<T>(ObservableCollection<T> mylist)
        {
            if (mylist.Count != 0)
            {
                return true;
            }
            return false;
        }
        public void loadconfig()
        {
            try
            {
                string pathrun = Directory.GetCurrentDirectory();
                string contentconfig = File.ReadAllText(pathrun + "\\Config.json");
                myconfig = JsonConvert.DeserializeObject<Config>(contentconfig);
                cmb_data = new List<string>(myconfig.Model.NameModel);
                cmb_datatab2 = new List<string>(myconfig.Model.NameModel);
                cmb_datatab3 = new List<string>(myconfig.ConfigModel.NameModel);
                checkloadedconfig = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public bool checkaccess(string mymodelselected, string mydateselected, bool checkrun)
        {
            try
            {
                if (cmb_data.Where(p => { return p == mymodelselected; }).FirstOrDefault() != null)
                {
                    int indexmodel = 0;
                    for (int i = 0; i < myconfig.Model.NameModel.Count; i++)
                    {
                        if (mymodelselected == myconfig.Model.NameModel[i])
                        {
                            break;
                        }
                        indexmodel++;
                    }
                    foreach (var item in myconfig.Model.Path[indexmodel])
                    {
                        if (item.Length > 10 && (mydateselected != null|| mydateselected == "Nocheck") && myconfig.Model.Path[indexmodel].Count > 0 && !checkrun)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public void CheckModifyFile()
        {
            
                checkruntab1 = true;
                Task.Factory.StartNew(() => {
                    myListErrorFileCheckSaveNG.Clear();
                    myListErrorFileCheckSaveOK.Clear();
                    App.Current.Dispatcher?.Invoke(() =>
                    {
                        myListErrorFileCheck.Clear();
                    });
                    try
                    {

                        DateTime mydatetime = DateTime.Parse(dateselected);
                        string datetimestring = mydatetime.ToString("MM/dd/yy");
                        string datetimeendtab1string = null;
                         if (dateselectedEndtab1 != null)
                         {
                             DateTime mydatetimeend = DateTime.Parse(dateselectedEndtab1);
                             datetimeendtab1string = mydatetimeend.ToString("MM/dd/yy");
                         }
                         //
                        if (dateselectedEndtab1 !=null && (Int32.Parse(datetimeendtab1string.Split('/')[2]) < Int32.Parse(datetimestring.Split('/')[2]) ||(Int32.Parse(datetimeendtab1string.Split('/')[2]) == Int32.Parse(datetimestring.Split('/')[2]) && Int32.Parse(datetimeendtab1string.Split('/')[0]) < Int32.Parse(datetimestring.Split('/')[0]))))
                        {
                            MessageBox.Show("Chọn ngày bắt đầu và ngày kết thúc chưa hợp lệ");
                            checkruntab1 = false;
                        }
                        //
                        else if(dateselectedEndtab1==null)
                        {
                            int indexnamemodel = 0;
                            for (int i = 0; i < myconfig.Model.NameModel.Count; i++)
                            {
                                if (modelselected == myconfig.Model.NameModel[i])
                                {
                                    break;
                                }
                                else
                                {
                                    indexnamemodel++;

                                }
                            }
                            for (int i = 0; i < myconfig.Model.Path[indexnamemodel].Count; i++)
                            {
                                DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                DirectoryInfo[] sd = d.GetDirectories();
                                foreach (DirectoryInfo sd2 in sd)
                                {
                                    if (Regex.IsMatch(sd2.Name, $@"^{datetimestring.Split('/')[2]}年{datetimestring.Split('/')[0]}"))
                                    {
                                        FileInfo[] fin = sd2.GetFiles("*.csv");
                                        foreach (var fcsv in fin)
                                        {
                                            if (Int32.Parse(fcsv.Name.Split('_')[1].Split('.')[2]) == Int32.Parse(fcsv.LastWriteTime.Day.ToString()))
                                            {
                                                App.Current.Dispatcher?.Invoke(() =>
                                                {
                                                    myListErrorFileCheck.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "OK" });
                                                });
                                                myListErrorFileCheckSaveOK.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "OK" });
                                            }
                                            else
                                            {
                                                App.Current.Dispatcher?.Invoke(() =>
                                                {
                                                    myListErrorFileCheck.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "NG" });

                                                });
                                                myListErrorFileCheckSaveNG.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "NG" });
                                            }
                                        }
                                    }
                                }
                            }
                            autoexport<ListErrorFileCheck>(myListErrorFileCheckSaveOK, myListErrorFileCheckSaveNG, modelselected, "CheckFile", "FilePath,FileName,DateCreate,DateModify,Status");
                            checkruntab1 = false;
                        }
                        else
                        {
                            int indexnamemodel = 0;
                            for (int i = 0; i < myconfig.Model.NameModel.Count; i++)
                            {
                                if (modelselected == myconfig.Model.NameModel[i])
                                {
                                    break;
                                }
                                else
                                {
                                    indexnamemodel++;

                                }
                            }
                            for (int yearslected = Int32.Parse(datetimestring.Split('/')[2]); yearslected <= Int32.Parse(datetimeendtab1string.Split('/')[2]); yearslected++)
                            {
                                int startcheckmonth = yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2]) ? Int32.Parse(datetimeendtab1string.Split('/')[0]) : 12;
                                int endcheckmonth;
                                if(Int32.Parse(datetimestring.Split('/')[2]) == Int32.Parse(datetimeendtab1string.Split('/')[2]))
                                {
                                    endcheckmonth = Int32.Parse(datetimestring.Split('/')[0]);
                                }
                                else 
                                {
                                    //if(yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2]))
                                    endcheckmonth = 1;
                                }
                                for (int monthselected = startcheckmonth; monthselected >= endcheckmonth; monthselected--)
                                {
                                     string monthconvert = monthselected.ToString().Length < 2 ? "0" + monthselected.ToString(): monthselected.ToString();
                                    for (int i = 0; i < myconfig.Model.Path[indexnamemodel].Count; i++)
                                    {
                                        DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                        DirectoryInfo[] sd = d.GetDirectories();
                                        foreach (DirectoryInfo sd2 in sd)
                                        {
                                            if (Regex.IsMatch(sd2.Name, $@"^{yearslected.ToString()}年{monthconvert}"))
                                            {
                                                FileInfo[] fin = sd2.GetFiles("*.csv");
                                                foreach (var fcsv in fin)
                                                {
                                                    if (Int32.Parse(fcsv.Name.Split('_')[1].Split('.')[2]) == Int32.Parse(fcsv.LastWriteTime.Day.ToString()))
                                                    {
                                                        App.Current.Dispatcher?.Invoke(() =>
                                                        {
                                                            myListErrorFileCheck.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "OK" });
                                                        });
                                                        myListErrorFileCheckSaveOK.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "OK" });
                                                    }
                                                    else
                                                    {
                                                        App.Current.Dispatcher?.Invoke(() =>
                                                        {
                                                            myListErrorFileCheck.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "NG" });

                                                        });
                                                        myListErrorFileCheckSaveNG.Add(new ListErrorFileCheck() { NameModel = modelselected, FilePath = sd2.FullName, FileName = fcsv.Name, DateCreate = fcsv.Name.Split('_')[1], DateModify = fcsv.LastWriteTime.ToString("dd/MM/yyyy"), Statuscheck = "NG" });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                autoexport<ListErrorFileCheck>(myListErrorFileCheckSaveOK, myListErrorFileCheckSaveNG, modelselected, "CheckFile", "FilePath,FileName,DateCreate,DateModify,Status");
                                checkruntab1 = false;
                            }
                        }


                     }
                    catch (Exception ex)
                    {
                         checkruntab1 = false;
                         MessageBox.Show(ex.ToString());

                    }
                 });
        }
        public void ChecContentFile()
        {
            
                checkruntab2 = true;
                Task.Factory.StartNew(() =>
                {
                try
                {
                    myListErrorContentFileCheckSaveNG.Clear();
                        myListErrorContentFileCheckSaveOK.Clear();
                    App.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        myListErrorContentFileCheck.Clear();
                    }));
                        
                        DateTime mydatetime = DateTime.Parse(dateselectedtab2);
                        string datetimestring = mydatetime.ToString("MM/dd/yy");
                        string datetimeendtab1string = null;
                        if (dateselectedEndtab2 != null)
                        {
                            DateTime mydatetimeend = DateTime.Parse(dateselectedEndtab2);
                            datetimeendtab1string = mydatetimeend.ToString("MM/dd/yy");
                        }
                        if (dateselectedEndtab2 != null && (Int32.Parse(datetimeendtab1string.Split('/')[2]) < Int32.Parse(datetimestring.Split('/')[2]) || (Int32.Parse(datetimeendtab1string.Split('/')[2]) == Int32.Parse(datetimestring.Split('/')[2]) && Int32.Parse(datetimeendtab1string.Split('/')[0]) < Int32.Parse(datetimestring.Split('/')[0]))))
                        {
                            MessageBox.Show("Chọn ngày bắt đầu và ngày kết thúc chưa hợp lệ");
                            checkruntab2 = false;
                        }
                        else if (dateselectedEndtab2 == null)
                        {
                            int indexnamemodel = 0;
                            for (int i = 0; i < myconfig.Model.NameModel.Count; i++)
                            {
                                if (modelselectedtab2 == myconfig.Model.NameModel[i])
                                {
                                    break;
                                }
                                else
                                {
                                    indexnamemodel++;
                                }
                            }

                            for (int i = 0; i < myconfig.Model.Path[indexnamemodel].Count; i++)
                            {
                                DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                DirectoryInfo[] sd = d.GetDirectories();
                                foreach (DirectoryInfo sd2 in sd)
                                {
                                    if (Regex.IsMatch(sd2.Name, $@"^{datetimestring.Split('/')[2]}年{datetimestring.Split('/')[0]}"))
                                    {
                                        bool checkOKNG = false;
                                        int countline = 0;
                                        FileInfo[] fin = sd2.GetFiles("*.csv");
                                        foreach (var fcsv in fin)
                                        {
                                            List<string> listdata = new List<string>();
                                            using (StreamReader strreader = new StreamReader(sd2.FullName + "\\" + $"{fcsv.Name}", Encoding.UTF8))
                                            {
                                                while (!strreader.EndOfStream)
                                                {
                                                    listdata.Add(strreader.ReadLine());
                                                }
                                                strreader.Close();
                                            }
                                            foreach (string line in listdata)
                                            {

                                                string[] arraydata;
                                                arraydata = line.Split(',');

                                                if (arraydata.Length > 87)
                                                {
                                                    if (arraydata[0].Trim('\0') != arraydata[88].Trim('\0') && arraydata[0].Trim('\0').Length > 10 && arraydata[0].Trim('\0') != "3190100000000" && arraydata[0].Trim('\0') != "3290100000000" && arraydata[88].Trim('\0') != "3290100000000" && arraydata[88].Trim('\0') != "3290100000000" && arraydata[0].Trim('\0') != "0003190100000000" && arraydata[0].Trim('\0') != "0003290100000000" && arraydata[88].Trim('\0') != "0003290100000000" && arraydata[88].Trim('\0') != "0003290100000000")
                                                    {
                                                        App.Current.Dispatcher.Invoke(() =>
                                                        {
                                                            myListErrorContentFileCheck.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[88].Trim('\0'), Statuscheck = "NG" });

                                                        });
                                                        myListErrorContentFileCheckSaveNG.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[88].Trim('\0'), Statuscheck = "NG" });
                                                        checkOKNG = true;
                                                    }
                                                }
                                                countline++;
                                            }
                                            if (!checkOKNG)
                                            {
                                                App.Current.Dispatcher.Invoke(() =>
                                                {
                                                    myListErrorContentFileCheck.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = "", HeaderLine = "", EndLine = "", Statuscheck = "OK" });

                                                });
                                                myListErrorContentFileCheckSaveOK.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = "", HeaderLine = "", EndLine = "", Statuscheck = "OK" });

                                            }
                                        }
                                    }
                                }
                            }
                            autoexport<ListErrorContentFileCheck>(myListErrorContentFileCheckSaveOK, myListErrorContentFileCheckSaveNG, modelselectedtab2, "CheckContentFile", "FilePath,FileName,NumberLine,Header,End,Status");
                            checkruntab2 = false;
                        }
                        else
                        {
                            int indexnamemodel = 0;
                            for (int i = 0; i < myconfig.Model.NameModel.Count; i++)
                            {
                                if (modelselectedtab2 == myconfig.Model.NameModel[i])
                                {
                                    break;
                                }
                                else
                                {
                                    indexnamemodel++;
                                }
                            }
                            for (int yearslected = Int32.Parse(datetimestring.Split('/')[2]); yearslected <= Int32.Parse(datetimeendtab1string.Split('/')[2]); yearslected++)
                            {
                                int startcheckmonth = yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2]) ? Int32.Parse(datetimeendtab1string.Split('/')[0]) : 12;
                                int endcheckmonth;
                                if (Int32.Parse(datetimestring.Split('/')[2]) == Int32.Parse(datetimeendtab1string.Split('/')[2]))
                                {
                                    endcheckmonth = Int32.Parse(datetimestring.Split('/')[0]);
                                }
                                else
                                {
                                    //yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2])
                                    endcheckmonth = 1;
                                }
                                for (int monthselected = startcheckmonth; monthselected >= endcheckmonth; monthselected--)
                                {
                                    string monthconvert = monthselected.ToString().Length < 2 ? "0" + monthselected.ToString() : monthselected.ToString();
                                    for (int i = 0; i < myconfig.Model.Path[indexnamemodel].Count; i++)
                                    {
                                        DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                        DirectoryInfo[] sd = d.GetDirectories();
                                        foreach (DirectoryInfo sd2 in sd)
                                        {
                                            if (Regex.IsMatch(sd2.Name, $@"^{yearslected}年{monthconvert}"))
                                            {
                                                bool checkOKNG = false;
                                                int countline = 0;
                                                FileInfo[] fin = sd2.GetFiles("*.csv");
                                                foreach (var fcsv in fin)
                                                {
                                                    List<string> listdata = new List<string>();
                                                    using (StreamReader strreader = new StreamReader(sd2.FullName + "\\" + $"{fcsv.Name}", Encoding.UTF8))
                                                    {
                                                        while (!strreader.EndOfStream)
                                                        {
                                                            listdata.Add(strreader.ReadLine());
                                                        }
                                                        strreader.Close();
                                                    }
                                                    foreach (string line in listdata)
                                                    {

                                                        string[] arraydata;
                                                        arraydata = line.Split(',');

                                                        if (arraydata.Length > 87)
                                                        {
                                                            if (arraydata[0].Trim('\0') != arraydata[88].Trim('\0') && arraydata[0].Trim('\0').Length > 10 && arraydata[0].Trim('\0') != "3190100000000" && arraydata[0].Trim('\0') != "3290100000000" && arraydata[88].Trim('\0') != "3290100000000" && arraydata[88].Trim('\0') != "3290100000000" && arraydata[0].Trim('\0') != "0003190100000000" && arraydata[0].Trim('\0') != "0003290100000000" && arraydata[88].Trim('\0') != "0003290100000000" && arraydata[88].Trim('\0') != "0003290100000000")
                                                            {
                                                                App.Current.Dispatcher.Invoke(() =>
                                                                {
                                                                    myListErrorContentFileCheck.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[88].Trim('\0'), Statuscheck = "NG" });

                                                                });
                                                                myListErrorContentFileCheckSaveNG.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[88].Trim('\0'), Statuscheck = "NG" });
                                                                checkOKNG = true;
                                                            }
                                                        }
                                                        countline++;
                                                    }
                                                    if (!checkOKNG)
                                                    {
                                                        App.Current.Dispatcher.Invoke(() =>
                                                        {
                                                            myListErrorContentFileCheck.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = "", HeaderLine = "", EndLine = "", Statuscheck = "OK" });

                                                        });
                                                        myListErrorContentFileCheckSaveOK.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = "", HeaderLine = "", EndLine = "", Statuscheck = "OK" });

                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            autoexport<ListErrorContentFileCheck>(myListErrorContentFileCheckSaveOK, myListErrorContentFileCheckSaveNG, modelselectedtab2, "CheckContentFile", "FilePath,FileName,NumberLine,Header,End,Status");
                            checkruntab2 = false;

                        }

                    }
                    catch (Exception ex)
                    {
                        checkruntab2 = false;

                        MessageBox.Show(ex.ToString());
                    }

                });

            
        }
        public void CheckModifyFileConfig()
        {
            checkruntab3 = true;
            Task.Factory.StartNew(() => {
                myListErrorConfigFileCheckSaveNG.Clear();
                myListErrorConfigFileCheckSaveOK.Clear();
                App.Current.Dispatcher?.Invoke(() =>
                {
                    myListErrorConfigFileCheck.Clear();
                });
                try
                {
                    int indexnamemodel = 0;
                    for (int i = 0; i < myconfig.ConfigModel.NameModel.Count; i++)
                    {
                        if (modelselectedtab3 == myconfig.ConfigModel.NameModel[i])
                        {
                            break;
                        }
                        else
                        {
                            indexnamemodel++;

                        }
                    }
                    for (int i = 0; i < myconfig.ConfigModel.Path[indexnamemodel].Count; i++)
                    {
                        DirectoryInfo d = new DirectoryInfo(myconfig.ConfigModel.Path[indexnamemodel][i]);
                        DirectoryInfo[] sd = d.GetDirectories();
                        foreach (DirectoryInfo sd2 in sd)
                        {
                                FileInfo[] fin = sd2.GetFiles("*.csv");
                                foreach (var fcsv in fin)
                                {
                                    if (fcsv.LastWriteTime.Day == 1 && fcsv.LastWriteTime.Month == 1 && fcsv.LastWriteTime.Year == 2019)
                                    {
                                        App.Current.Dispatcher?.Invoke(() =>
                                        {
                                            myListErrorConfigFileCheck.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = sd2.FullName, FileName = fcsv.Name, DateModify = fcsv.LastWriteTime.ToString("HH:mm dd/MM/yyyy"), Statuscheck = "OK" });
                                        });
                                        myListErrorConfigFileCheckSaveOK.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = sd2.FullName, FileName = fcsv.Name, DateModify = fcsv.LastWriteTime.ToString("HH:mm dd/MM/yyyy"), Statuscheck = "OK" });
                                    }
                                    else
                                    {
                                        App.Current.Dispatcher?.Invoke(() =>
                                        {
                                            myListErrorConfigFileCheck.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = sd2.FullName, FileName = fcsv.Name, DateModify = fcsv.LastWriteTime.ToString("HH:mm dd/MM/yyyy"), Statuscheck = "NG" });

                                        });
                                        myListErrorConfigFileCheckSaveNG.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = sd2.FullName, FileName = fcsv.Name, DateModify = fcsv.LastWriteTime.ToString("HH:mm dd/MM/yyyy"), Statuscheck = "NG" });
                                    }
                                }
                        }
                    }
                    autoexport<ListErrorConfigFileCheck>(myListErrorConfigFileCheckSaveOK, myListErrorConfigFileCheckSaveNG, modelselectedtab3, "CheckModifyFileConfig", " FilePath,FileName,DateModify,Status");
                    checkruntab3 = false;
                }
                catch (Exception ex)
                {
                    checkruntab3 = false;
                    MessageBox.Show(ex.ToString());

                }
            });
        }

        public void exportdatatocsv<T>(ObservableCollection<T> listexport, string firstline, string namefile)
        {
            string FileName = namefile + " " + DateTime.Now.ToString("HH:mm_ddMMyyyy");
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text file (*.csv)|*.csv";
            if (sf.ShowDialog() == true)
            {
                //using (FileStream fstr = new FileStream(sf.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                //{
                using (StreamWriter sw = new StreamWriter(sf.FileName, append: true, Encoding.UTF8))
                {
                    sw.WriteLine(firstline);
                    foreach (var fcsv in listexport)
                    {
                        //sw.WriteLine(fcsv.FilePath + "," + fcsv.FileName+","+fcsv.DateCreate+","+fcsv.DateModify);
                        sw.WriteLine(fcsv.ToString());

                    }
                }
                //}
            }
        }
        public void autoexport<T>(List<T> listok, List<T> listng, string mymodelselect, string NameFolder, string firstrow)
        {
            if (listok.Count > 0)
            {
                using (StreamWriter sw = new StreamWriter(myconfig.FolderDataOK.Replace(@"\", @"\\")+"\\" + NameFolder+"_" + mymodelselect + "_" + DateTime.Now.ToString("HHmmss_ddMMyyyy") + ".csv", append: true, Encoding.UTF8))
                {
                    sw.WriteLine(firstrow);
                    foreach (var fcsv in listok)
                    {
                        sw.WriteLine(fcsv.ToString());

                    }
                }
            }
            if (listng.Count > 0)
            {
                using (StreamWriter sw = new StreamWriter(myconfig.FolderDataNG.Replace(@"\", @"\\") + "\\" + NameFolder + "_" + mymodelselect + "_" + DateTime.Now.ToString("HHmmss_ddMMyyyy") + ".csv", append: true, Encoding.UTF8))
                {
                    sw.WriteLine(firstrow);
                    foreach (var fcsv in listng)
                    {
                        sw.WriteLine(fcsv.ToString());
                    }
                }
            }
        }
    }
}
