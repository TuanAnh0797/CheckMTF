using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMTF
{
    public class ListErrorContentFileCheck
    {
        private string nameModel;
        private string filePath;
        private string fileName;
        private string numberLine;
        private string headerLine;
        private string endLine;
        private string statuscheck;

        public string FilePath { get => filePath; set => filePath = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public string NumberLine { get => numberLine; set => numberLine = value; }
        public string HeaderLine { get => headerLine; set => headerLine = value; }
        public string EndLine { get => endLine; set => endLine = value; }
        public string Statuscheck { get => statuscheck; set => statuscheck = value; }
        public string NameModel { get => nameModel; set => nameModel = value; }

        public override string ToString()
        {
            return this.nameModel+","+ this.filePath + "," + this.fileName + "," + this.numberLine + "," + this.headerLine + ',' + this.endLine +  ',' + this.statuscheck;
        }


    }
}
