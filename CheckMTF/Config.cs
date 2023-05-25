using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMTF
{
    public class Model
    {
        public List<string> NameModel { get; set; }
        public List<List<string>> Path { get; set; }
        public List<string> EndLine { get; set; }
        public List<string> IsCheckedTab1 { get; set; }
        public List<string> IsCheckedTab2 { get; set; }
        public List<string> IsCheckContent { get; set; }
        public string MonthCheck { get; set; }
    }
    public class ConfigModel
    {
        public List<string> NameModel { get; set; }
        public List<List<string>> Path { get; set; }
        public List<string> IsChecked { get; set; }
        public string MonthCheck { get; set; }
    }
    public class Config
    {
        public Model Model { get; set; }
        public ConfigModel ConfigModel { get; set; }
        public string FolderDataOK { get; set; }
        public string FolderDataNG { get; set;}
    }


}
