using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
namespace TestRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistryKey x = Registry.ClassesRoot.OpenSubKey(@"Directory\\Background\\shell");
            RegistryKey xx = x.OpenSubKey("cmd\\command");
            Console.WriteLine(xx.GetValue(""));
        }
    }
}
