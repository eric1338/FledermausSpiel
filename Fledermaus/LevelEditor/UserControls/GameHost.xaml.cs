using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LevelEditor.UserControls
{
    /// <summary>
    /// Interaction logic for GameHost.xaml
    /// </summary>
    public partial class GameHost : UserControl
    {

        public IntPtr MainWindowHandle { get; set; }


        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public GameHost()
        {
            InitializeComponent();


            try
            {
                //External exe inside WPF Window 
                System.Windows.Forms.Panel _pnlSched = new System.Windows.Forms.Panel();

                //WindowsFormsHost windowsFormsHost1 = this.host;

                this.host.Child = _pnlSched;


                //_Grid.Children.Add(_pnlSched);

                ProcessStartInfo psi = new ProcessStartInfo(System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\Fledermaus\bin\Debug\Fledermaus.exe");
          
                psi.CreateNoWindow = false;//.WindowStyle = ProcessWindowStyle.Normal;
                
                Process PR = Process.Start(psi);

                PR.WaitForInputIdle(); // true if the associated process has reached an idle state.

                System.Threading.Thread.Sleep(3000);

                IntPtr hwd = PR.MainWindowHandle;
                SetParent(PR.MainWindowHandle, _pnlSched.Handle);  // loading exe to the wpf window.

            }
            catch (Exception ex)
            {
                //Nothing...
            }
        }
    }
}
