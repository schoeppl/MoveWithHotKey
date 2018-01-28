using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoveWithHotKey
{
    public partial class ConfigurationForm : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        private KeyboardHook keyboardHook;

        private string TargetFolder = "VIP";

        public ConfigurationForm()
        {
            InitializeComponent();

            InitializeNotifyIcon();

            InitializeKeyboardHook();

            RegisterHotKeys();
        }

        private void InitializeNotifyIcon()
        {
            this.notifyIcon.Text = "Moves selection into Folder `VIP` (`shift` + `m`)";
            this.notifyIcon.Icon = new System.Drawing.Icon("moveapp.ico");
            this.notifyIcon.ContextMenu = new ContextMenu();
            this.notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Exit", Exit));
            this.notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Config", Config));
            this.notifyIcon.BalloonTipText = "Don't forgett to close the app, when you are done";
        }

        private void InitializeKeyboardHook()
        {
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(HookKeyPressed);
        }

        private void RegisterHotKeys()
        {
            keyboardHook.RegisterHotKey(MoveWithHotKey.ModifierKeys.Shift, Keys.M);
        }

        private void ConfigurationForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void HookKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                List<string> filelist = getSelectedItems();

                if (e.Modifier == MoveWithHotKey.ModifierKeys.Shift && e.Key == Keys.M)
                {
                    MoveFilesToVIP(filelist);
                }
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

        private void CheckAndCreateTargetDir(string dir)
        {
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            this.notifyIcon.Visible = false;
            keyboardHook.Dispose();
            Application.Exit();
        }

        private void Config(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
            this.Show();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
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

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(keyboardHook != null)
            {
                keyboardHook.Dispose();
            }
        }
    }
}
