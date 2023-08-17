using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocolLibrary
{
    public class TemplateData
    {
        private string nameFunction;
        private string ipAddressServer;
        private int portServer;
        private string typeTriger;
        private int deviceTriger;
        private string typeCompleted;
        private int deviceCompleted;
        private List<TemplateDataRead>  listdataread = new List<TemplateDataRead>();
        public TemplateData()
        {

        }
        public string IpAddressServer { get => ipAddressServer; set => ipAddressServer = value; }
        public int PortServer { get => portServer; set => portServer = value; }
        public string TypeTriger { get => typeTriger; set => typeTriger = value; }
        public int DeviceTriger { get => deviceTriger; set => deviceTriger = value; }
        public string TypeCompleted { get => typeCompleted; set => typeCompleted = value; }
        public int DeviceCompleted { get => deviceCompleted; set => deviceCompleted = value; }
        public List<TemplateDataRead> Listdataread { get => listdataread; set => listdataread = value; }
        public string NameFunction { get => nameFunction; set => nameFunction = value; }
    }
    public class TemplateDataRead
    {
        private string typeData;
        private int deviceData;
        private int lengthData;
        private string dataType;
        public string TypeData { get => typeData; set => typeData = value; }
        public int DeviceData { get => deviceData; set => deviceData = value; }
        public int LengthData { get => lengthData; set => lengthData = value; }
        public string DataType { get => dataType; set => dataType = value; }
    }
}
