using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMTF
{
    public class ListErrorFileCheck
    {
        private string nameModel;
        private string filePath;
        private string fileName;
        private string dateCreate;
        private string dateModify;
        private string statuscheck;
        public string FilePath { get => filePath; set => filePath = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public string DateCreate { get => dateCreate; set => dateCreate = value; }
        public string DateModify { get => dateModify; set => dateModify = value; }
        public string Statuscheck { get => statuscheck; set => statuscheck = value; }
        public string NameModel { get => nameModel; set => nameModel = value; }

        public override string ToString()
        {
            return this.nameModel+","+ this.filePath + "," + this.fileName + "," + this.dateCreate + "," + this.dateModify +','+this.statuscheck;
        }
    }
}
