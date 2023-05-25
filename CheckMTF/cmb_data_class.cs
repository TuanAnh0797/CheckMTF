using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMTF
{
    public class cmb_data_class
    {
        private string cmb_displaymember;
        private string is_checked;
        private string isCheckContent;

        public string Cmb_displaymember { get => cmb_displaymember; set => cmb_displaymember = value; }
        public string Is_checked { get => is_checked; set => is_checked = value; }
        public string IsCheckContent { get => isCheckContent; set => isCheckContent = value; }
    }
}
