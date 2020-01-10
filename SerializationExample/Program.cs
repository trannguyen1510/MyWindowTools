using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using RightClickShell;

namespace SerializationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            RightClickShell.Directory execute = new RightClickShell.Directory(Registry.ClassesRoot.OpenSubKey("Directory\\Background"));
            MyClass x = new MyClass("Name", "Command");
            FileStream fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(RightClickShell.Directory));
            xmlSerializer_background.Serialize(fs, execute);
            fs.Close();
            fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            RightClickShell.Directory directory = (RightClickShell.Directory)xmlSerializer_background.Deserialize(fs);
            foreach(RightClickShell.RightClickShell child in directory.Children)
            {
                ExecuteAble t;
                switch (child.type)
                {
                    case RightClickShellType.Directory:
                        continue;
                    case RightClickShellType.Executable:
                        t = (ExecuteAble)child;
                        Console.WriteLine(t.Command);
                        break;
                }
            }
            fs.Close();
        }
        
    }
    [Serializable]
    public class MyClass : ISerializable
    {
        [XmlAttribute]public String RegName;
        [XmlAttribute]public String RegCommand;

        public List<MyClass> Children=new List<MyClass>();
        public MyClass()
        {

        }
        public MyClass(String RegName,String RegCommand)
        {

            this.RegName = RegName;
            this.RegCommand = RegCommand;
        }
        public MyClass(SerializationInfo info, StreamingContext context)
        {
            RegName = info.GetString("RegName");
            RegCommand = info.GetString("RegCommand");
            Children.Add((MyClass)info.GetValue("Children",typeof(MyClass)));
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RegName", RegName, RegName.GetType());
            info.AddValue("RegCommand", RegCommand, RegCommand.GetType());
            Console.WriteLine(Children.Count);
            if(Children.Count>0)
                foreach(MyClass x in Children)
                {
                    info.AddValue("Children", x, x.GetType());
                }   
        }
    }
}
