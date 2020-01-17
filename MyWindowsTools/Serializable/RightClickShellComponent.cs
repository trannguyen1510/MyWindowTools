using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MyWindowsTools.Serializable
{

    public class RightClickShellComponent
    {
        List<RegistryKey> FileTypes;
        RegistryKey Directory;
        public RightClickShellComponent()
        {
            FileTypes = new List<RegistryKey>();
            Directory = Registry.ClassesRoot.OpenSubKey(@"Directory\shell",RegistryKeyPermissionCheck.ReadWriteSubTree,System.Security.AccessControl.RegistryRights.FullControl);
            foreach(string x in Directory.GetSubKeyNames()) 
            {
                Console.WriteLine(x);
            }
        }
<<<<<<< HEAD
=======

>>>>>>> 10f9f24589d4ff616739b028a66ca456ac7b4b24
    }
}
