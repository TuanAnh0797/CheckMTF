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
    }
    public class ConfigModel
    {
        public List<string> NameModel { get; set; }
        public List<List<string>> Path { get; set; }
    }
    public class Config
    {
        public Model Model { get; set; }
        public ConfigModel ConfigModel { get; set; }
        public string FolderDataOK { get; set; }
        public string FolderDataNG { get; set;}
    }


}
