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
            int[] a = new int[10];
            a[0] = 10;
            int x = a[0];
            Console.WriteLine(Object.ReferenceEquals(a[0], x));
        }
    }
}
