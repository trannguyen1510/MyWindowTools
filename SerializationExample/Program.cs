using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Win32;

// Another Library included in project
using RightClickShells;

// This project is for testing and demonstrating, it will be deleted
//
namespace SerializationExample
{
    class Program
    {
        static FileStream fs;
        static XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(DirectoryShell));
        static DirectoryShell Root = new DirectoryShell() { name = "Root"};
        static List<RightClickShell> inserted= new List<RightClickShell>();
        static void Main(string[] args)
        {
            while (true)
            {
                int input;
                PrintMenu();
                input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Insert();
                        break;
                    case 2:
                        Delete();
                        break;
                    default:
                        break;
                }
                if (input == 0)
                {
                    break;
                }
            }
            

        }

        private static void Delete()
        {
            throw new NotImplementedException();
        }

        private static void Insert()
        {
            Console.Write("Name:");
            String name = Console.ReadLine();
            Console.Write("Type:");
            RightClickShellType type = (RightClickShellType)int.Parse(Console.ReadLine());
            switch (type)
            {
                case RightClickShellType.DirectoryShell:
                    Root.Children.Add(new DirectoryShell() { name = name });
                    break;
                case RightClickShellType.ExecutableShell:
                    Root.Children.Add(new ExecutableShell() { name = name, command ="do something"});
                    break;
            }
        }

        private static void PrintMenu()
        {
            Console.Write("Chọn 1 trong những tính năng sau:0\n" +
                          "1. Thêm vào\n" +
                          "2. Xóa \n"+
                          "0. Thoat\n" +
                          "---------------------------------\n");
        }

        static void ReadXmlFiles()
        {
            fs = new FileStream("Objects", FileMode.OpenOrCreate);
            DirectoryShell directory = (DirectoryShell)xmlSerializer_background.Deserialize(fs);
            foreach (RightClickShell child in directory.Children)
            {
                ExecutableShell t;
                if (child.type == RightClickShellType.ExecutableShell)
                {
                    t = (ExecutableShell)child;
                    Console.WriteLine(t.command);
                    break;
                }
            }
            fs.Close();
        }
        static void SerializeDirectoryShell(DirectoryShell root)
        {
            fs = new FileStream("Objects", FileMode.OpenOrCreate);
            xmlSerializer_background.Serialize(fs, root);
            fs.Close();
        }
    }
    /// <summary>
    /// this class is for demmonstrations
    /// </summary>
    
    [Serializable]
    public class MyClass : ISerializable
    {
        //the properties which have XmlAttribute tags is stored at <ObjectType NameProperties1 = "Its value" >
        //other is store at another <ObjectType>
        //See the output files for examples
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
        //this constructor for deserialize
        public MyClass(SerializationInfo info, StreamingContext context)
        {
            RegName = info.GetString("RegName");
            RegCommand = info.GetString("RegCommand");
            //deserialize a list is a thing
            Children = (List<MyClass>)info.GetValue("Children",typeof(List<MyClass>));
        }
        /// <summary>
        /// this functions is overriding the GetObjectData in Interface Iserializable. This function is for serialize
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RegName", RegName, RegName.GetType());
            info.AddValue("RegCommand", RegCommand, RegCommand.GetType());
            info.AddValue("Children", RegCommand, typeof(List<MyClass>));
        }
    }
}
