using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Management.Instrumentation;

namespace CSharp_Bai9_SystemInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            long DungLuongRAM = 0;
            // Tên máy tính
            lblName.Text = Environment.MachineName.ToString();
            // Lấy dung lượng bộ nhớ RAM
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
            ("SELECT * FROM Win32_Computersystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                DungLuongRAM = long.Parse(obj["TotalPhysicalMemory"].ToString());//Byte
                DungLuongRAM = DungLuongRAM / (1024 * 1024 * 1024); // đổi sang GB
            }
            lblRam.Text = DungLuongRAM.ToString() + " GB";

            // Tên CPU
            string sCPUName = "";
            ManagementObjectSearcher searcher2 = new ManagementObjectSearcher
            ("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher2.Get())
            {
                sCPUName = obj["Name"].ToString();
            }
            lblCPU.Text = sCPUName;
            // Thông tin đĩa cứng (HDD)
            string sHDDName = "";
            float sHDDSize = 0;
            ManagementObjectSearcher searcher3 = new ManagementObjectSearcher
            ("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject obj in searcher3.Get())
            {
                foreach (PropertyData pd in obj.Properties)
                {
                    if (pd.Name == "Model")
                    {
                        sHDDName = pd.Value.ToString();
                    }
                    if (pd.Name == "Size")
                    {
                        string size = pd.Value.ToString(); // byte
                        sHDDSize = float.Parse(size) / (1024 * 1024 * 1024); //GB
                    }
                }
            }
            lblHDD.Text = sHDDName + " - Size: " + sHDDSize.ToString() + " GB";

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
