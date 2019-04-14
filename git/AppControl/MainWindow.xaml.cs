using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppControl
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread checkThread = null;
        public MainWindow()
        {
            InitializeComponent();
            ExcuteFolderControl();
            checkThread = new Thread(RunClock);
            checkThread.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExcuteFolderControl();
        }
        private void RunClock()
        {
            while(true)
            {
                DateTime t = DateTime.Now;
                if (t.Hour == 10 && t.Minute == 0 && t.Second == 0)
                {
                    ExcuteFolderControl();
                }
                Thread.Sleep(30000);
            }
            
        }
        private void ExcuteFolderControl()
        {
            Process myExe = new Process();
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            // FileName 是要執行的檔案
            myExe.StartInfo.FileName = str+"\\FolderControl.exe";
            myExe.StartInfo.Arguments = "C:\\photo "+txt_deleteTime.Text;
            myExe.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myExe.Start();


            myExe.WaitForExit(10000);
            myExe.CloseMainWindow();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            checkThread.Abort();
            checkThread = null;
        }
    }
}
