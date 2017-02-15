using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Management;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const int OPEN_EXISTING = 3;
        const uint GENERIC_READ = 0x80000000;
        const uint GENERIC_WRITE = 0x40000000;
        const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int CloseHandle(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int DeviceIoControl
            (IntPtr deviceHandle, uint ioControlCode,
              IntPtr inBuffer, int inBufferSize,
              IntPtr outBuffer, int outBufferSize,
              ref int bytesReturned, IntPtr overlapped);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern IntPtr CreateFile
            (string filename, uint desiredAccess,
              uint shareMode, IntPtr securityAttributes,
              int creationDisposition, int flagsAndAttributes,
              IntPtr templateFile);

        List<string> kvp;

        //Получение списка букв USB накопителей
        private void UsbDiskList()
        {
            string diskName = string.Empty;
            kvp = new List<string>();
            //предварительно очищаем список
            comboBox1.Items.Clear();

            //Получение списка накопителей подключенных через интерфейс USB
            foreach (System.Management.ManagementObject drive in
                      new System.Management.ManagementObjectSearcher(
                       "select * from Win32_DiskDrive where InterfaceType='USB'").Get())
            {
                //Получаем букву накопителя
                foreach (System.Management.ManagementObject partition in
                   new System.Management.ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"]
                      + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (System.Management.ManagementObject disk in
                       new System.Management.ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                          + partition["DeviceID"]
                          + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {
                        //Получение буквы устройства
                        diskName = disk["Name"].ToString().Trim();
                        comboBox1.Items.Add(diskName + " (" + drive["Model"] + ")");
                        kvp.Add(diskName);
                    }
                }
            }
        }

        //метод для извлечения USB накопителя
        static void EjectDrive(string driveLetter)
        {
            string path = "\\\\.\\" + driveLetter;

            IntPtr handle = CreateFile(path, GENERIC_READ | GENERIC_WRITE, 0,
                IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            if ((long)handle == -1)
            {
                MessageBox.Show("Невозможно извлечь USB устройство!",
           "Извлечение USB накопителей", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int dummy = 0;
            DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0,
                IntPtr.Zero, 0, ref dummy, IntPtr.Zero);
            int returnValue = DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA,
                     IntPtr.Zero, 0, IntPtr.Zero, 0, ref dummy, IntPtr.Zero);
            CloseHandle(handle);
            MessageBox.Show("USB устройство, успешно извлечено!",
         "Извлечение USB накопителей", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadInfo()
        {
            //Загрузка букв USB накопителей при запуске программы
            UsbDiskList();
            //Выбор первого устройства в списке
            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        static readonly string[] SizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }

        public Form1()
        {
            InitializeComponent();
            LoadInfo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            
            foreach (var drive in allDrives)
            {
                allDisks.Items.Insert(0, drive.Name);
            }

            allDisks.SetSelected(0, true);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                allDisks.SetSelected(0, true);
                allDisks.Items.Remove(String.Format("{0}:\\", comboBox1.SelectedItem.ToString()[0]));
                EjectDrive(kvp[comboBox1.SelectedIndex]);
            }
            LoadInfo();
        }

        private void allDisks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diskName = allDisks.SelectedItem.ToString();
            
            pictureBox1.Invalidate();
            pictureBox1.Update();
            pictureBox1.Refresh();

            DriveInfo di = new DriveInfo(allDisks.SelectedItem.ToString());

            long availableFreeSpace = di.AvailableFreeSpace;
            string driveFormat = di.DriveFormat;
            string name = di.Name;
            long totalSize = di.TotalSize;


            string  text = String.Format("name {0}\n", name);
            text += String.Format("drive Format {0}\n", driveFormat);
            text += String.Format("available FreeSpace {0}\n", SizeSuffix(availableFreeSpace));
            text += String.Format("total Size {0}", SizeSuffix(totalSize));

            labelInfoAboutDisk.Text = text;

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            float offset = 0F;
            RectangleF rect;
            StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            
            DriveInfo di = new DriveInfo(allDisks.SelectedItem.ToString());

                if (di.IsReady)
                {
                    rect = new RectangleF(offset, 0F, 90F, 90F);
                    e.Graphics.FillEllipse(Brushes.Green, rect);

                    float proc = (float)(di.TotalSize - di.TotalFreeSpace) / (float)di.TotalSize;

                    e.Graphics.FillPie(Brushes.Gray,
                        rect.X, rect.Y, rect.Width, rect.Height,
                        180F, 360F * proc);

                    offset += rect.Width + 10F;
                
            }
        }
       
    }
}
