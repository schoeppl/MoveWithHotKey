﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoveWithHotKey
{
    public partial class ConfigurationForm : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        private KeyboardHook keyboardHook;
        private Shell32.Shell shell;

        private string defaultFolder = "VIP";
        private Keys defaultKey = Keys.M;
        private MoveWithHotKey.ModifierKeys defaultModifierKey = MoveWithHotKey.ModifierKeys.Shift;
        private int defaultWaitingTimeForFocus = 800;
        private bool defaultDoOverwrite = false;

        private string savedFolder;
        private Keys savedKey;
        private MoveWithHotKey.ModifierKeys savedModifier;
        private int savedWaitingTimeForFocus;
        private bool savedDoOverwrite;

        private Keys tempKey;
        private int tempWaitingTimeForFocus;

        private bool valideKeyEntered = false;

        public ConfigurationForm()
        {
            InitializeComponent();
            InitializeModifierList();
            InitializeWaitintTimeBox();
            InitializeDefaults();
            InitializeNotifyIcon();
            InitializeKeyboardHook();
            InitializeShell();

            LoadCurrentConfig();
            RegisterHotKey();
        }

        private void InitializeModifierList()
        {
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Alt);
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Control);
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Shift);
            this.lb_ModifierKeys.Items.Add(MoveWithHotKey.ModifierKeys.Win);
        }

        private void InitializeWaitintTimeBox()
        {
            for (int i = 0; i <= 10; i++)
            {
                cb_waitingTime.Items.Add(i * 100);
            }   
        }

        private void InitializeDefaults()
        {
            savedFolder = defaultFolder;
            savedKey = tempKey = defaultKey;
            savedModifier = defaultModifierKey;
            savedDoOverwrite = defaultDoOverwrite;
            savedWaitingTimeForFocus = tempWaitingTimeForFocus = defaultWaitingTimeForFocus;
        }

        private void InitializeNotifyIcon()
        {
            SetNotifyIconText();
            this.notifyIcon.Icon = Properties.Resources.moveapp;
            this.notifyIcon.ContextMenu = new ContextMenu();
            this.notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Exit", Exit));
            this.notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Config", Config));
            this.notifyIcon.BalloonTipText = "Don't forgett to close the app, when you are done";
            this.notifyIcon.Visible = false;
        }

        private void SetNotifyIconText()
        {
            this.notifyIcon.Text = "Moves selection into Folder `" + savedFolder + "` (`" + savedModifier.ToString() + "` + `" + savedKey.ToString() + "`)";
        }

        private void InitializeKeyboardHook()
        {
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(HookKeyPressed);
        }

        private void InitializeShell()
        {
            shell = new Shell32.Shell();
        }

        private void LoadCurrentConfig()
        {
            tb_path.Text = savedFolder;
            tb_key.Text = savedKey.ToString();
            lb_ModifierKeys.SelectedIndex = lb_ModifierKeys.Items.IndexOf(savedModifier);
            cb_waitingTime.SelectedIndex = cb_waitingTime.Items.IndexOf(savedWaitingTimeForFocus);
            ResetTempValues();
        }

        private void RegisterHotKey()
        {
            keyboardHook.RegisterHotKey(savedModifier, savedKey);
        }

        private void ResetTempValues()
        {
            tempWaitingTimeForFocus = savedWaitingTimeForFocus;
            tempKey = savedKey;
        }

        private void HookKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                List<string> filelist = GetSelectedItems();
                HotKeyActionSelect(e, filelist);
                SetSelectToFocusedItem(filelist);
            }
        }

        private List<string> GetSelectedItems()
        {
            IntPtr handle = GetForegroundWindow();
            List<string> selected = new List<string>();
            foreach (SHDocVw.InternetExplorer window in shell.Windows())
            {
                if (window.HWND == (int)handle)
                {
                    Shell32.FolderItems items = ((Shell32.IShellFolderViewDual2)window.Document).SelectedItems();
                    foreach (Shell32.FolderItem item in items)
                    {
                        if (!item.IsFolder) selected.Add(item.Path);
                    }
                }
            }
            return selected;
        }

        private void HotKeyActionSelect(KeyPressedEventArgs e, List<string> filelist)
        {
            if (e.Modifier == savedModifier && e.Key == savedKey)
            {
                MoveFilesRelativ(filelist);
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

        private void CheckAndCreateTargetDir(string dir)
        {
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
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

        private void SetSelectToFocusedItem(List<string> doNotFocusOn)
        {
            IntPtr handle = GetForegroundWindow();
            foreach (SHDocVw.InternetExplorer window in shell.Windows())
            {
                if (window.HWND == (int)handle)
                {
                    WaitAndTryToFocus(doNotFocusOn, window);
                }
            }
        }

        private void WaitAndTryToFocus(List<string> doNotFocusOn, SHDocVw.InternetExplorer window)
        {
            bool done = false;
            int waited = 0;
            while (!done && waited < savedWaitingTimeForFocus)
            {
                Shell32.FolderItem item = ((Shell32.IShellFolderViewDual2)window.Document).FocusedItem;
                if (item != null)
                {
                    if (doNotFocusOn.Contains(item.Path))
                    {
                        waited += 100;
                        Thread.Sleep(100);
                    }
                    else
                    {
                        ((Shell32.IShellFolderViewDual2)window.Document).SelectItem(item, (int)SVSIF.SVSI_SELECT);
                        done = true;
                    }
                }
            }
        }

        //Form Events

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

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(keyboardHook != null)
            {
                keyboardHook.Dispose();
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

        private void but_save_Click(object sender, EventArgs e)
        {
            savedFolder = tb_path.Text;
            savedKey = tempKey;
            savedWaitingTimeForFocus = tempWaitingTimeForFocus;
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

        private void cb_waitingTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempWaitingTimeForFocus = (int)cb_waitingTime.Items[cb_waitingTime.SelectedIndex];
        }

        [Flags]
        public enum SVSIF : uint
        {
            SVSI_SELECT = 1
        }
    }
}
