using Microsoft.Win32;
using Newtonsoft.Json;
using Quanlybarcode.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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
        public CancellationToken cttab1;
        public CancellationToken cttab2;
        public CancellationToken cttab3;
        public CancellationTokenSource ctstab1;
        public CancellationTokenSource ctstab2;
        public CancellationTokenSource ctstab3;

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
        public ObservableCollection<cmb_data_class> cmb_data { get; set; }
        public ObservableCollection<cmb_data_class> cmb_datatab2 { get; set; }
        public ObservableCollection<cmb_data_class> cmb_datatab3 { get; set; }
        public ICommand cmd_startcheck { get; set; }
        public ICommand cmd_exportdatatab1 { get; set; }
        public ICommand cmd_startchecktab2 { get; set; }
        public ICommand cmd_exportdatatab2 { get; set; }
        public ICommand cmd_startchecktab3 { get; set; }
        public ICommand cmd_exportdatatab3 { get; set; }
        public ICommand cmd_Uncheckedtab1 { get; set; }
        public ICommand cmd_Uncheckedtab2 { get; set; }
        public ICommand cmd_keydownOK { get; set; }
        public ICommand cmd_keydownNG { get; set; }

        public ICommand cmd_Updatecmb { get; set; }
        


        public MainViewModel()
        {

            if(myconfig == null)
            {
                myconfig = new Config();
            }
            if (cmb_data == null)
            {
                cmb_data = new ObservableCollection<cmb_data_class>();
            }
            if (cmb_datatab2 == null)
            {
                cmb_datatab2 = new ObservableCollection<cmb_data_class>();
            }
            if (cmb_datatab3 == null)
            {
                cmb_datatab3 = new ObservableCollection<cmb_data_class>();
            }
            checkruntab1 = false;
            checkruntab2 = false;
            checkruntab3 = false;
           
                loadconfig();
            
            myListErrorFileCheck = new ObservableCollection<ListErrorFileCheck>();
            myListErrorContentFileCheck = new ObservableCollection<ListErrorContentFileCheck>();
            myListErrorConfigFileCheck = new ObservableCollection<ListErrorConfigFileCheck>();
            myListErrorFileCheckSaveOK = new List<ListErrorFileCheck>();
            myListErrorFileCheckSaveNG = new List<ListErrorFileCheck>();
            myListErrorContentFileCheckSaveOK = new List<ListErrorContentFileCheck>();
            myListErrorContentFileCheckSaveNG = new List<ListErrorContentFileCheck>();
            myListErrorConfigFileCheckSaveOK = new List<ListErrorConfigFileCheck>();
            myListErrorConfigFileCheckSaveNG = new List<ListErrorConfigFileCheck>();
            cmd_startcheck = new RelayCommand<object>((p) => { return checkaccess(modelselected, dateselected, checkruntab1,cmb_data); }, (p) => { CheckModifyFile(); });
            cmd_exportdatatab1 = new RelayCommand<object>((p) => { return checkexport<ListErrorFileCheck>(myListErrorFileCheck); }, (p) => { exportdatatocsv<ListErrorFileCheck>(myListErrorFileCheck, "FilePath,FileName,DateCreate,DateModify", "ModifyFileCheck"); });
            cmd_Uncheckedtab1 = new RelayCommand<object>((p) => { return true; }, (p) => { DateselectedEndtab1 = null; });
            cmd_Uncheckedtab2 = new RelayCommand<object>((p) => { return true; }, (p) => { DateselectedEndtab2 = null; });
            cmd_startchecktab2 = new RelayCommand<object>((p) => { return checkaccess(modelselectedtab2, dateselectedtab2,checkruntab2,cmb_datatab2); }, (p) => { ChecContentFile(); });
            cmd_exportdatatab2 = new RelayCommand<object>((p) => { return checkexport<ListErrorContentFileCheck>(myListErrorContentFileCheck); }, (p) => { exportdatatocsv<ListErrorContentFileCheck>(myListErrorContentFileCheck, "FilePath,FileName,NumberLine,Header,End,Status","ModifyContentFileCheck"); });
            cmd_startchecktab3 = new RelayCommand<object>((p) => { return checkaccess(modelselectedtab3, "Nocheck", checkruntab3,cmb_datatab3); }, (p) => { CheckModifyFileConfig(); });
            cmd_exportdatatab3 = new RelayCommand<object>((p) => { return checkexport<ListErrorConfigFileCheck>(myListErrorConfigFileCheck); }, (p) => { exportdatatocsv<ListErrorConfigFileCheck>(myListErrorConfigFileCheck, "FilePath,FileName,DateModiFy,Status","ModifyFileConfig"); });
            cmd_keydownOK = new RelayCommand<object>((p) => { return true; }, (p) => { openresultfolderOK();});
            cmd_keydownNG = new RelayCommand<object>((p) => { return true; }, (p) => { openresultfolderNG(); });
            cmd_Updatecmb = new RelayCommand<object>((p) => { return true; },(p)=> { saveconfig(); loadconfig(); });
        }
        public void openresultfolderOK()
        {
            Process.Start("explorer.exe",myconfig.FolderDataOK);
            
            
        }
        public void openresultfolderNG()
        {
            Process.Start("explorer.exe", myconfig.FolderDataNG);

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
            App.Current.Dispatcher.Invoke(() =>
            {
                cmb_data.Clear();
                cmb_datatab2.Clear();
                cmb_datatab3.Clear();
                for (int i = 0; i < myconfig.Model.NameModel.Count; i++)
                {
                    cmb_data.Add(new cmb_data_class() { Cmb_displaymember = myconfig.Model.NameModel[i], Is_checked = myconfig.Model.IsCheckedTab1[i] });
                    cmb_datatab2.Add(new cmb_data_class() { Cmb_displaymember = myconfig.Model.NameModel[i], Is_checked = myconfig.Model.IsCheckedTab2[i],IsCheckContent = myconfig.Model.IsCheckContent[i]});
                }
                for (int i = 0; i < myconfig.ConfigModel.NameModel.Count; i++)
                {
                    cmb_datatab3.Add(new cmb_data_class() { Cmb_displaymember = myconfig.ConfigModel.NameModel[i], Is_checked = myconfig.ConfigModel.IsChecked[i] });
                }
            });
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void saveconfig()
        {
            if(myconfig.Model.NameModel.Count > myconfig.Model.IsCheckedTab1.Count)
            {
                for (int i = 0; i < myconfig.Model.NameModel.Count - myconfig.Model.IsCheckedTab1.Count; i++)
                {
                    myconfig.Model.IsCheckedTab1.Add("False");
                }
            }
            if (myconfig.Model.NameModel.Count > myconfig.Model.IsCheckedTab2.Count)
            {
                for (int i = 0; i < myconfig.Model.NameModel.Count - myconfig.Model.IsCheckedTab2.Count; i++)
                {
                    myconfig.Model.IsCheckedTab2.Add("False");
                }
            }
            if (myconfig.ConfigModel.NameModel.Count > myconfig.ConfigModel.IsChecked.Count)
            {
                for (int i = 0; i < myconfig.ConfigModel.NameModel.Count - myconfig.ConfigModel.IsChecked.Count; i++)
                {
                    myconfig.ConfigModel.IsChecked.Add("False");
                }
            }
            if (myconfig.Model.NameModel.Count > myconfig.Model.Path.Count)
            {
                for (int i = 0; i < myconfig.Model.NameModel.Count - myconfig.Model.Path.Count; i++)
                {
                    myconfig.Model.Path.Add(new List<string>() {@"\\192.168.2.165\d3"});
                }
            }
            if (myconfig.Model.NameModel.Count > myconfig.Model.EndLine.Count)
            {
                for (int i = 0; i < myconfig.Model.NameModel.Count - myconfig.Model.EndLine.Count; i++)
                {
                    myconfig.Model.EndLine.Add("87");
                }
            }
            if (myconfig.Model.NameModel.Count > myconfig.Model.IsCheckContent.Count)
            {
                for (int i = 0; i < myconfig.Model.NameModel.Count - myconfig.Model.IsCheckContent.Count; i++)
                {
                    myconfig.Model.IsCheckContent.Add("False");
                }
            }
            if (myconfig.ConfigModel.NameModel.Count > myconfig.ConfigModel.Path.Count)
            {
                for (int i = 0; i < myconfig.ConfigModel.NameModel.Count - myconfig.ConfigModel.Path.Count; i++)
                {
                    myconfig.ConfigModel.Path.Add(new List<string>() { @"\\192.168.2.165\Config" });
                }
            }
            if (Int32.Parse(myconfig.Model.MonthCheck)!= DateTime.Now.Month)
            {
                myconfig.Model.IsCheckedTab1.Clear();
                myconfig.Model.IsCheckedTab2.Clear();
                for (int i = 0; i < myconfig.Model.NameModel.Count; i++)
                {
                    myconfig.Model.IsCheckedTab1.Add("False");
                    myconfig.Model.IsCheckedTab2.Add("False");
                }
                myconfig.Model.MonthCheck = DateTime.Now.Month.ToString();
            }
            if(Int32.Parse(myconfig.ConfigModel.MonthCheck)!= DateTime.Now.Month)
            {
                myconfig.ConfigModel.IsChecked.Clear();
                for (int i = 0; i < myconfig.ConfigModel.NameModel.Count; i++)
                {
                    myconfig.ConfigModel.IsChecked.Add("False");
                }
                myconfig.ConfigModel.MonthCheck = DateTime.Now.Month.ToString();
            }
            string data = JsonConvert.SerializeObject(myconfig);
            string ta = data.Replace("\"NameModel\"", "\n\n" + "\"NameModel\"").Replace("\"Path\"", "\n\n" + "\"Path\"").Replace("\"EndLine\"", "\n\n" + "\"EndLine\"").Replace("\"IsCheckedTab1\"", "\n\n" + "\"IsCheckedTab1\"").Replace("\"IsCheckedTab2\"", "\n\n" + "\"IsCheckedTab2\"").Replace("\"MonthCheck\"", "\n\n" + "\"MonthCheck\"").Replace("\"ConfigModel\"", "\n\n" + "\"ConfigModel\"").Replace("\"IsChecked\"", "\n\n" + "\"IsChecked\"").Replace("\"FolderDataOK\"", "\n\n" + "\"FolderDataOK\"").Replace("\"FolderDataNG\"", "\n\n" + "\"FolderDataNG\"").Replace(",[",","+"\n"+"[").Replace("[[","["+"\n"+"[").Replace("\"IsCheckContent\"", "\n\n"+"\"IsCheckContent\"");
            using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\Config.json", append: false, Encoding.UTF8))
            {
               sw.Write(ta);
            }
            
        }
        public bool checkaccess(string mymodelselected, string mydateselected, bool checkrun, ObservableCollection<cmb_data_class> cmb_data)
        {
            try
            {
                if (cmb_data.Where(p => { return p.Cmb_displaymember == mymodelselected; }).FirstOrDefault() != null)
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
                        if (item.Length > 10 && (mydateselected != null || mydateselected == "Nocheck") && myconfig.Model.Path[indexmodel].Count > 0 && !checkrun)
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
            string pathconnect = "";
                checkruntab1 = true;
                ctstab1 = new CancellationTokenSource();
                cttab1 = ctstab1.Token;

            Task.Factory.StartNew(() => {
                    myListErrorFileCheckSaveNG.Clear();
                    myListErrorFileCheckSaveOK.Clear();
                    App.Current.Dispatcher?.Invoke(() =>
                    {
                        myListErrorFileCheck.Clear();
                    });
            try
            {
                if (cttab1.IsCancellationRequested)
                        {
                            cttab1.ThrowIfCancellationRequested();
                        }
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
                                pathconnect = myconfig.Model.Path[indexnamemodel][i];
                                DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                DirectoryInfo[] sd = d.GetDirectories();
                                if (cttab1.IsCancellationRequested)
                                {
                                    cttab1.ThrowIfCancellationRequested();
                                }
                                foreach (DirectoryInfo sd2 in sd)
                                {
                                    if (cttab1.IsCancellationRequested)
                                    {
                                        cttab1.ThrowIfCancellationRequested();
                                    }
                                    if (Regex.IsMatch(sd2.Name, $@"^{datetimestring.Split('/')[2]}年{datetimestring.Split('/')[0]}"))
                                    {
                                        FileInfo[] fin = sd2.GetFiles("*.csv");
                                        foreach (var fcsv in fin)
                                        {
                                            if (cttab1.IsCancellationRequested)
                                            {
                                                cttab1.ThrowIfCancellationRequested();
                                            }
                                            if (Int32.Parse(fcsv.Name.Split('_')[1].Split('.')[2]) == Int32.Parse(fcsv.LastWriteTime.Day.ToString()) || (fcsv.LastWriteTime.Hour == 0 && fcsv.LastWriteTime.Minute <10))
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
                                autoexport<ListErrorFileCheck>(myListErrorFileCheckSaveOK, myListErrorFileCheckSaveNG, modelselected, "No." + i.ToString() + "_" + modelselected + "_" + "CheckFile", "FilePath,FileName,DateCreate,DateModify,Status",modelselected,"Check_Modify_D3");
                            myListErrorFileCheckSaveOK.Clear();
                            myListErrorFileCheckSaveNG.Clear();
                            }
                            checkruntab1 = false;
                            myconfig.Model.IsCheckedTab1[indexnamemodel] = "True";
                        saveconfig();
                        //loadconfig();
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
                                if (cttab1.IsCancellationRequested)
                                {
                                    cttab1.ThrowIfCancellationRequested();
                                }
                                int startcheckmonth = yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2]) ? Int32.Parse(datetimeendtab1string.Split('/')[0]) : 12;
                                int endcheckmonth;
                                if(Int32.Parse(datetimestring.Split('/')[2]) == Int32.Parse(datetimeendtab1string.Split('/')[2]))
                                {
                                    endcheckmonth = Int32.Parse(datetimestring.Split('/')[0]);
                                }
                                else 
                                {
                                    if(yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2]))
                                    {
                                        endcheckmonth = 1;
                                    }
                                    else
                                    {
                                        endcheckmonth = Int32.Parse(datetimestring.Split('/')[0]);
                                    }
                                    
                                }
                                for (int monthselected = startcheckmonth; monthselected >= endcheckmonth; monthselected--)
                                {
                                    if (cttab1.IsCancellationRequested)
                                    {
                                        cttab1.ThrowIfCancellationRequested();
                                    }
                                    string monthconvert = monthselected.ToString().Length < 2 ? "0" + monthselected.ToString(): monthselected.ToString();
                                    for (int i = 0; i < myconfig.Model.Path[indexnamemodel].Count; i++)
                                    {
                                        if (cttab1.IsCancellationRequested)
                                        {
                                            cttab1.ThrowIfCancellationRequested();
                                        }
                                        pathconnect = myconfig.Model.Path[indexnamemodel][i];
                                        DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                        DirectoryInfo[] sd = d.GetDirectories();
                                        foreach (DirectoryInfo sd2 in sd)
                                        {
                                            if (cttab1.IsCancellationRequested)
                                            {
                                                cttab1.ThrowIfCancellationRequested();
                                            }
                                            if (Regex.IsMatch(sd2.Name, $@"^{yearslected.ToString()}年{monthconvert}"))
                                            {
                                                FileInfo[] fin = sd2.GetFiles("*.csv");
                                                foreach (var fcsv in fin)
                                                {
                                                    if (cttab1.IsCancellationRequested)
                                                    {
                                                        cttab1.ThrowIfCancellationRequested();
                                                    }
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
                                        autoexport<ListErrorFileCheck>(myListErrorFileCheckSaveOK, myListErrorFileCheckSaveNG, modelselected,"Month_"+monthconvert+"_Year_"+yearslected+"_No."+i.ToString()+"_"+ modelselected + "_CheckFile", "FilePath,FileName,DateCreate,DateModify,Status", modelselected, "Check_Modify_D3");
                                        myListErrorFileCheckSaveOK.Clear();
                                        myListErrorFileCheckSaveNG.Clear();
                                    }
                                }
                                checkruntab1 = false;
                            }
                        }


                }
                catch (System.IO.IOException ex)
                {
                    checkruntab1 = false;

                    MessageBox.Show("Không thể kết nối đến folder: " + pathconnect + "\nHãy kiểm tra lại kết nối mạng hoặc sharefolder");
                    ctstab1.Cancel();

                }
                catch (Exception ex)
                {
                    checkruntab1 = false;

                    MessageBox.Show(ex.ToString());
                    ctstab1.Cancel();

                }
                finally
                {
                    ctstab1.Dispose();
                }
            },cttab1);
        }
        public void ChecContentFile()
        {
            ctstab2 = new CancellationTokenSource();
            cttab2 = ctstab2.Token;
            string pathconnect = "";
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
                        if (cttab1.IsCancellationRequested)
                        {
                            cttab1.ThrowIfCancellationRequested();
                        }
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
                            if (cttab1.IsCancellationRequested)
                            {
                                cttab1.ThrowIfCancellationRequested();
                            }
                            for (int i = 0; i < myconfig.Model.Path[indexnamemodel].Count; i++)
                            {
                                if (cttab1.IsCancellationRequested)
                                {
                                    cttab1.ThrowIfCancellationRequested();
                                }
                                pathconnect = myconfig.Model.Path[indexnamemodel][i];
                                DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                DirectoryInfo[] sd = d.GetDirectories();
                                foreach (DirectoryInfo sd2 in sd)
                                {
                                    if (cttab1.IsCancellationRequested)
                                    {
                                        cttab1.ThrowIfCancellationRequested();
                                    }
                                    if (Regex.IsMatch(sd2.Name, $@"^{datetimestring.Split('/')[2]}年{datetimestring.Split('/')[0]}"))
                                    {
                                        bool checkOKNG = false;
                                        int countline = 0;
                                        FileInfo[] fin = sd2.GetFiles("*.csv");
                                        foreach (var fcsv in fin)
                                        {
                                            if (cttab1.IsCancellationRequested)
                                            {
                                                cttab1.ThrowIfCancellationRequested();
                                            }
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
                                                if (cttab1.IsCancellationRequested)
                                                {
                                                    cttab1.ThrowIfCancellationRequested();
                                                }
                                                string[] arraydata;
                                                arraydata = line.Split(',');

                                                if (arraydata.Length > 87)
                                                {
                                                    if (arraydata[0].Trim('\0') != arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') && arraydata[0].Trim('\0').Length > 10 && arraydata[0].Trim('\0') != "3190100000000" && arraydata[0].Trim('\0') != "3290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "3290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "3290100000000" && arraydata[0].Trim('\0') != "0003190100000000" && arraydata[0].Trim('\0') != "0003290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "0003290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "0003290100000000")
                                                    {
                                                        App.Current.Dispatcher.Invoke(() =>
                                                        {
                                                            myListErrorContentFileCheck.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0'), Statuscheck = "NG" });

                                                        });
                                                        myListErrorContentFileCheckSaveNG.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0'), Statuscheck = "NG" });
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
                                autoexport<ListErrorContentFileCheck>(myListErrorContentFileCheckSaveOK, myListErrorContentFileCheckSaveNG, modelselectedtab2,"No."+i.ToString()+"_"+ modelselectedtab2+"_CheckContentFile", "FilePath,FileName,NumberLine,Header,End,Status", modelselectedtab2, "Check_Content_D3");
                                myListErrorContentFileCheckSaveOK.Clear();
                                myListErrorContentFileCheckSaveNG.Clear();

                            }
                            checkruntab2 = false;
                            myconfig.Model.IsCheckedTab2[indexnamemodel] = "True";
                            saveconfig();
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
                            if (cttab1.IsCancellationRequested)
                            {
                                cttab1.ThrowIfCancellationRequested();
                            }
                            for (int yearslected = Int32.Parse(datetimestring.Split('/')[2]); yearslected <= Int32.Parse(datetimeendtab1string.Split('/')[2]); yearslected++)
                            {
                                if (cttab1.IsCancellationRequested)
                                {
                                    cttab1.ThrowIfCancellationRequested();
                                }
                                int startcheckmonth = yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2]) ? Int32.Parse(datetimeendtab1string.Split('/')[0]) : 12;
                                int endcheckmonth;
                                if (Int32.Parse(datetimestring.Split('/')[2]) == Int32.Parse(datetimeendtab1string.Split('/')[2]))
                                {
                                    endcheckmonth = Int32.Parse(datetimestring.Split('/')[0]);
                                }
                                else
                                {
                                        if (yearslected == Int32.Parse(datetimeendtab1string.Split('/')[2]))
                                        {
                                            endcheckmonth = 1;
                                        }
                                        else
                                        {
                                            endcheckmonth = Int32.Parse(datetimestring.Split('/')[0]);
                                        }
                                    
                                }
                                for (int monthselected = startcheckmonth; monthselected >= endcheckmonth; monthselected--)
                                {
                                    if (cttab1.IsCancellationRequested)
                                    {
                                        cttab1.ThrowIfCancellationRequested();
                                    }
                                    string monthconvert = monthselected.ToString().Length < 2 ? "0" + monthselected.ToString() : monthselected.ToString();
                                    for (int i = 0; i < myconfig.Model.Path[indexnamemodel].Count; i++)
                                    {
                                        if (cttab1.IsCancellationRequested)
                                        {
                                            cttab1.ThrowIfCancellationRequested();
                                        }
                                        pathconnect = myconfig.Model.Path[indexnamemodel][i];
                                        DirectoryInfo d = new DirectoryInfo(myconfig.Model.Path[indexnamemodel][i]);
                                        DirectoryInfo[] sd = d.GetDirectories();
                                        foreach (DirectoryInfo sd2 in sd)
                                        {
                                            if (cttab1.IsCancellationRequested)
                                            {
                                                cttab1.ThrowIfCancellationRequested();
                                            }
                                            if (Regex.IsMatch(sd2.Name, $@"^{yearslected}年{monthconvert}"))
                                            {
                                                bool checkOKNG = false;
                                                int countline = 0;
                                                FileInfo[] fin = sd2.GetFiles("*.csv");
                                                foreach (var fcsv in fin)
                                                {
                                                    if (cttab1.IsCancellationRequested)
                                                    {
                                                        cttab1.ThrowIfCancellationRequested();
                                                    }
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
                                                        if (cttab1.IsCancellationRequested)
                                                        {
                                                            cttab1.ThrowIfCancellationRequested();
                                                        }
                                                        string[] arraydata;
                                                        arraydata = line.Split(',');

                                                        if (arraydata.Length > 87)
                                                        {
                                                            if (arraydata[0].Trim('\0') != arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') && arraydata[0].Trim('\0').Length > 10 && arraydata[0].Trim('\0') != "3190100000000" && arraydata[0].Trim('\0') != "3290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "3290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "3290100000000" && arraydata[0].Trim('\0') != "0003190100000000" && arraydata[0].Trim('\0') != "0003290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "0003290100000000" && arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0') != "0003290100000000")
                                                            {
                                                                App.Current.Dispatcher.Invoke(() =>
                                                                {
                                                                    myListErrorContentFileCheck.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0'), Statuscheck = "NG" });

                                                                });
                                                                myListErrorContentFileCheckSaveNG.Add(new ListErrorContentFileCheck() { NameModel = modelselectedtab2, FilePath = sd2.FullName, FileName = fcsv.Name, NumberLine = countline.ToString(), HeaderLine = arraydata[0].Trim('\0'), EndLine = arraydata[Int32.Parse(myconfig.Model.EndLine[indexnamemodel])].Trim('\0'), Statuscheck = "NG" });
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
                                        autoexport<ListErrorContentFileCheck>(myListErrorContentFileCheckSaveOK, myListErrorContentFileCheckSaveNG, modelselectedtab2, "Month_" + monthconvert + "_Year_" + yearslected + "_No." + i.ToString() + "_" + modelselectedtab2 + "CheckContentFile", "FilePath,FileName,NumberLine,Header,End,Status", modelselectedtab2, "Check_Content_D3");
                                        myListErrorContentFileCheckSaveOK.Clear();
                                        myListErrorContentFileCheckSaveNG.Clear();


                                    }
                                }
                            }
                            checkruntab2 = false;

                        }

                    }
                    catch (System.IO.IOException ex)
                    {
                        checkruntab2 = false;
                        ctstab2.Cancel();
                        MessageBox.Show("Không thể kết nối đến folder: " + pathconnect + "\nHãy kiểm tra lại kết nối mạng hoặc sharefolder");
                    }
                    catch (Exception ex)
                    {
                        checkruntab2 = false;
                        ctstab2.Cancel();
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        ctstab2.Dispose();
                    }

                },cttab2);

            
        }
        public void CheckModifyFileConfig()
        {
            checkruntab3 = true;
            ctstab3 = new CancellationTokenSource();
            cttab3 = ctstab3.Token;
            string pathconnect = "";
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
                    if (cttab1.IsCancellationRequested)
                    {
                        cttab1.ThrowIfCancellationRequested();
                    }
                    for (int i = 0; i < myconfig.ConfigModel.Path[indexnamemodel].Count; i++)
                    {
                        if (cttab1.IsCancellationRequested)
                        {
                            cttab1.ThrowIfCancellationRequested();
                        }
                        pathconnect = myconfig.ConfigModel.Path[indexnamemodel][i];
                        string[] csvfile = Directory.GetFiles(myconfig.ConfigModel.Path[indexnamemodel][i], "*.csv", SearchOption.AllDirectories);
                                foreach (var fcsv in csvfile)
                                {
                                    if ( File.GetLastWriteTime(fcsv).Month > 4 && File.GetLastWriteTime(fcsv).Year == 2023 || File.GetLastWriteTime(fcsv).Year > 2023)
                                    {
                                         App.Current.Dispatcher?.Invoke(() =>
                                         {
                                              myListErrorConfigFileCheck.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = fcsv, FileName = fcsv.Split('\\').Last(), DateModify = File.GetLastWriteTime(fcsv).ToString("HH:mm dd/MM/yyyy"), Statuscheck = "NG" });

                                         });
                                         myListErrorConfigFileCheckSaveNG.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = fcsv, FileName = fcsv.Split('\\').Last(), DateModify = File.GetLastWriteTime(fcsv).ToString("HH:mm dd/MM/yyyy"), Statuscheck = "NG" });

                                    }
                                    else
                                    {
                                         App.Current.Dispatcher?.Invoke(() =>
                                         {
                                             myListErrorConfigFileCheck.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = fcsv, FileName = fcsv.Split('\\').Last(), DateModify = File.GetLastWriteTime(fcsv).ToString("HH:mm dd/MM/yyyy"), Statuscheck = "OK" });
                                         });
                                         myListErrorConfigFileCheckSaveOK.Add(new ListErrorConfigFileCheck() { NameModel = modelselectedtab3, FilePath = fcsv, FileName = fcsv.Split('\\').Last(), DateModify = File.GetLastWriteTime(fcsv).ToString("HH:mm dd/MM/yyyy"), Statuscheck = "OK" });

                                    }
                                }
                        autoexport<ListErrorConfigFileCheck>(myListErrorConfigFileCheckSaveOK, myListErrorConfigFileCheckSaveNG, modelselectedtab3,"No."+ i.ToString()+ "_CheckModifyFileConfig", " FilePath,FileName,DateModify,Status", modelselectedtab3, "Check_Modify_Config");

                    }
                    checkruntab3 = false;
                    myconfig.ConfigModel.IsChecked[indexnamemodel] = "True";
                    saveconfig();
                }
                catch (System.IO.IOException ex)
                {
                    checkruntab3 = false;
                    ctstab3.Cancel();
                    MessageBox.Show("Không thể kết nối đến folder: " + pathconnect + "\nHãy kiểm tra lại kết nối mạng hoặc sharefolder");
                }
                catch (Exception ex)
                {
                    checkruntab3 = false;
                    ctstab3.Cancel();
                    MessageBox.Show(ex.ToString());

                }
                finally
                {
                    ctstab3.Dispose();
                }

            },cttab3);
        }

        public void exportdatatocsv<T>(ObservableCollection<T> listexport, string firstline, string namefile)
        {
            string FileName = namefile + " " + DateTime.Now.ToString("HH:mm_ddMMyyyy");
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text file (*.csv)|*.csv";
            if (sf.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(sf.FileName, append: true, Encoding.UTF8))
                {
                    sw.WriteLine(firstline);
                    foreach (var fcsv in listexport)
                    {
                        sw.WriteLine(fcsv.ToString());

                    }
                }
            }
        }
        public void autoexport<T>(List<T> listok, List<T> listng, string mymodelselect, string NameFolder, string firstrow, string Line, string mode)
        {
            if (listok.Count > 0)
            {
                bool exists = System.IO.Directory.Exists(myconfig.FolderDataOK + "\\" + Line+"\\"+mode);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(myconfig.FolderDataOK + "\\" + Line+"\\"+mode);
                }
                using (StreamWriter sw = new StreamWriter(myconfig.FolderDataOK + "\\" + Line + "\\"+mode+"\\"+ NameFolder + "_" + mymodelselect + "_" + DateTime.Now.ToString("HHmmss_ddMMyyyy") + ".csv", append: true, Encoding.UTF8))
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
                bool exists = System.IO.Directory.Exists(myconfig.FolderDataNG + "\\" + Line +"\\"+mode);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(myconfig.FolderDataNG + "\\" + Line + "\\" + mode);
                }

                using (StreamWriter sw = new StreamWriter(myconfig.FolderDataNG + "\\" + Line + "\\"+mode+"\\" + NameFolder + "_" + mymodelselect + "_" + DateTime.Now.ToString("HHmmss_ddMMyyyy") + ".csv", append: true, Encoding.UTF8))
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
    public class Convertcmb : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            string data = (string)value;
            if(data!=null )
            {
                return data;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            cmb_data_class data = (cmb_data_class)value;
            if (data != null)
            {
                return data.Cmb_displaymember;
            }
            return null;
            
        }
    }
}
