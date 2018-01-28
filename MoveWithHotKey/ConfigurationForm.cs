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

        private string defaultFolder = "VIP";
        private Keys defaultKey = Keys.M;
        private MoveWithHotKey.ModifierKeys defaultModifierKey = MoveWithHotKey.ModifierKeys.Shift;

        private string savedFolder;
        private Keys savedKey;
        private MoveWithHotKey.ModifierKeys savedModifier;
        private bool savedDoOverwrite = false;

        private Keys tempKey;

        private bool valideKeyEntered = false;

        public ConfigurationForm()
        {
            InitializeComponent();
            InitializeModifierList();
            InitializeDefaults();
            InitializeNotifyIcon();
            InitializeKeyboardHook();
            LoadCurrentConfig();
            RegisterHotKey();
        }

        private void LoadCurrentConfig()
        {
            tb_path.Text = savedFolder;
            tb_key.Text = savedKey.ToString();
            lb_ModifierKeys.SelectedIndex = lb_ModifierKeys.Items.IndexOf(savedModifier);
        }

        private void InitializeModifierList()
        {
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Alt);
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Control);
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Shift);
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Win);
        }

        private void InitializeDefaults()
        {
            savedFolder = defaultFolder;
            savedKey = defaultKey;
            tempKey = defaultKey;
            savedModifier = defaultModifierKey;
        }

        private void InitializeNotifyIcon()
        {
            SetNotifyIconText();
            this.notifyIcon.Icon = new System.Drawing.Icon("moveapp.ico");
            this.notifyIcon.ContextMenu = new ContextMenu();
            this.notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Exit", Exit));
            this.notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Config", Config));
            this.notifyIcon.BalloonTipText = "Don't forgett to close the app, when you are done";
            this.notifyIcon.Visible = false;
        }

        private void SetNotifyIconText()
        {
            this.notifyIcon.Text = "Moves selection into Folder `"+ savedFolder + "` (`" + savedModifier.ToString() + "` + `" + savedKey.ToString() + "`)";
        }
        private void InitializeKeyboardHook()
        {
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(HookKeyPressed);
        }

        private void RegisterHotKey()
        {
            keyboardHook.RegisterHotKey(savedModifier, savedKey);
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

                if (e.Modifier == savedModifier && e.Key == savedKey)
                {
                    MoveFilesRelativ(filelist);
                }
            }
        }

        private void MoveFilesRelativ(List<string> filelist)
        {
            foreach (var file in filelist)
            {
                DirectoryInfo directoryInfo = System.IO.Directory.GetParent(file);
                string targetDir = directoryInfo.ToString() + "\\" + savedFolder;
                CheckAndCreateTargetDir(targetDir);

                string targetPath = directoryInfo.ToString() + "\\" + savedFolder + "\\" + Path.GetFileName(file);
                if (savedDoOverwrite)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    targetPath = FindValideTargetFilename(targetDir, Path.GetFileName(file));
                }
                MoveFile(file, targetPath);

            }
        }

        private static void MoveFile(string file, string targetPath)
        {
            try
            {
                File.Move(file, targetPath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string FindValideTargetFilename(string targetDir, string fileName)
        {
            string path = targetDir + "\\" + fileName;
            string extension = Path.GetExtension(path);
            int c = 1;

            if (File.Exists(path))
            {
                while (File.Exists(path.Insert(path.Length - (extension.Length), "_" + c)))
                {
                    c++;
                }
                path = path.Insert(path.Length - (extension.Length), "_" + c);
            }

            return path;
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

        private void but_save_Click(object sender, EventArgs e)
        {
            savedFolder = tb_path.Text;
            savedKey = tempKey;
            savedModifier = (MoveWithHotKey.ModifierKeys)lb_ModifierKeys.Items[lb_ModifierKeys.SelectedIndex];
            keyboardHook.Dispose();
            InitializeKeyboardHook();
            RegisterHotKey();
            SetNotifyIconText();
        }

        private void but_load_Click(object sender, EventArgs e)
        {
            LoadCurrentConfig();
        }

        private void tb_key_KeyDown(object sender, KeyEventArgs e)
        {
            valideKeyEntered = false;

            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                valideKeyEntered = true;
                tempKey = e.KeyCode;
            }else
            {
                MessageBox.Show("Please use a valide key (A - Z)");
            }
        }

        private void tb_key_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (valideKeyEntered)
            {
                tb_key.Text = tempKey.ToString();
            }

            e.Handled = true;
        }
    }
}
