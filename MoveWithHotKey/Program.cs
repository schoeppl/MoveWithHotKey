using System;
using System.Collections.Generic;
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

            private NotifyIcon trayIcon;

            private HotKey moveToNice;
            
            public MoveToAppContext()
            {
                // Initialize Tray Icon
                this.trayIcon = new NotifyIcon();
                this.trayIcon.Text = "Moves selection into Folder `VIP` (`shift` + `m`)";
                this.trayIcon.Icon = new System.Drawing.Icon("moveapp.ico");
                this.trayIcon.ContextMenu = new ContextMenu();
                this.trayIcon.ContextMenu.MenuItems.Add(new MenuItem("Exit", Exit));
                this.trayIcon.Visible = true;

                keyboardHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(anyKeyPress);
                keyboardHook.RegisterHotKey(ModifierKeys.Shift, Keys.M);
            }

            private void anyKeyPress(object sender, KeyPressedEventArgs e)
            {
                throw new NotImplementedException();
            }

            void Exit(object sender, EventArgs e)
            {
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                this.trayIcon.Visible = false;
                moveToNice.Dispose();
                Application.Exit();
            }

            private void OnMoveToNice(HotKey hotKey)
            {
                var items = getSelectedItems();
                foreach (var item in items)
                {
                    MessageBox.Show(item);
                }
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
