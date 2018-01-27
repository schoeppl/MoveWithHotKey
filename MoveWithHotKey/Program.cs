using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MoveWithHotKey
{
    static class Program
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ConfigurationForm());
            Application.Run(new MoveToAppContext());
        }

        public class MoveToAppContext : ApplicationContext
        {

            private KeyboardHook keyboardHook = new KeyboardHook();

            private string TargetFolder = "VIP";

            private NotifyIcon trayIcon;
            
            public MoveToAppContext()
            {
                // Initialize Tray Icon
                this.trayIcon = new NotifyIcon();
                this.trayIcon.Text = "Moves selection into Folder `VIP` (`shift` + `m`)";
                this.trayIcon.Icon = new System.Drawing.Icon("moveapp.ico");
                this.trayIcon.ContextMenu = new ContextMenu();
                this.trayIcon.ContextMenu.MenuItems.Add(new MenuItem("Exit", Exit));
                this.trayIcon.Visible = true;

                keyboardHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(HookKeyPressed);
                keyboardHook.RegisterHotKey(ModifierKeys.Shift, Keys.M);
            }

            private void HookKeyPressed(object sender, KeyPressedEventArgs e)
            {
                List<string> filelist = getSelectedItems();
                
                if(e.Modifier == ModifierKeys.Shift && e.Key == Keys.M)
                {
                    MoveFilesToVIP(filelist);
                }
            }

            private void MoveFilesToVIP(List<string> filelist)
            {
                foreach (var file in filelist)
                {
                    DirectoryInfo directoryInfo = System.IO.Directory.GetParent(file);
                    CheckAndCreateTargetDir(directoryInfo.ToString() + "\\" + TargetFolder);
                    File.Move(file, directoryInfo.ToString() + "\\" + TargetFolder + "\\" + Path.GetFileName(file));
                }
            }

            private void CheckAndCreateTargetDir (string dir)
            {
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
            }
            void Exit(object sender, EventArgs e)
            {
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                this.trayIcon.Visible = false;
                keyboardHook.Dispose();
                Application.Exit();
            }


            private List<string> getSelectedItems()
            {
                IntPtr handle = GetForegroundWindow();
                List<string> selected = new List<string>();
                var shell = new Shell32.Shell();
                foreach (SHDocVw.InternetExplorer window in shell.Windows())
                {
                    if (window.HWND == (int)handle)
                    {
                        Shell32.FolderItems items = ((Shell32.IShellFolderViewDual2)window.Document).SelectedItems();
                        foreach (Shell32.FolderItem item in items)
                        {
                            selected.Add(item.Path);
                        }
                    }
                }
                return selected;
            }
        }

    }
}
