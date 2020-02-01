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
using System.Windows.Forms;

// This project is for testing and demonstrating, it will be deleted
//
namespace SerializationExample
{
    class Program
    {
        //biến toàn cục.
        static RightClickShell cursor;
        static DirectoryShell root;
        static InsertDeleteManager insert_deleted = new InsertDeleteManager((DirectoryShell)root);
        static FileStream fs;
        static void Main(string[] args)
        {
            try
            {
                root = DeSerializeTree(); //lấy cây đã lưu.
                
            }
            catch
            {
                fs.Close();
                Console.WriteLine("this is a new tree.");
                root = new DirectoryShell() { Name = "Directory\\Background" };// tạo cây mới.
            }
            root.SetParentForChild();// vì quá kém cõi nên phải set cha thủ công, ko thể tự động.
            cursor = (DirectoryShell)root;// đây là con trỏ hiện tại để tui test.
            while (true)
            {
                int input;
                PrintMenu();// in menu lệnh chưa hoàn chỉnh.
                input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Insert();// thêm, dựa theo cursor
                        break;
                    case 2:
                        Delete();// xóa cursor khỏi cây.
                        break;
                    case 3:
                        PrintTree((DirectoryShell)root);
                        break;
                    case 4:
                        CursorAccess(ref cursor);// duyệt cây theo user.
                        break;
                    default:
                        SerializeTree();// lưu lại.
                        insert_deleted.ApplyChange();// ghi vào registry
                        break;
                }
                if (input == 0)
                {
                    break;
                }
            }

        }

        private static void CursorAccess(ref RightClickShell cursor)
        {
            PrintMap(cursor);
            int input = int.Parse(Console.ReadLine());
            switch(input)
            {
                case -2:
                    break;
                case -1:
                    cursor = cursor.Parent;
                    break;
                default:
                    cursor = ((DirectoryShell)cursor).Children[input];
                    break;
            }
        }

        private static void PrintMap(RightClickShell cursor)
        {
            if (cursor.Type == RightClickShellType.ExecutableShell)
                return;
           foreach(RightClickShell x in ((DirectoryShell)cursor).Children)
            {
                Console.WriteLine(((DirectoryShell)cursor).Children.IndexOf(x).ToString()+ ". "+x.Name);
            }
        }

        public static void SerializeTree()
        {
            File.WriteAllText("BackGroundShortcuts.xml","");
            fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(DirectoryShell));
            xmlSerializer_background.Serialize(fs, root);
            fs.Close();
        }

        public static DirectoryShell DeSerializeTree()
        {
            XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(DirectoryShell));
            fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            DirectoryShell res = (DirectoryShell)xmlSerializer_background.Deserialize(fs);
            fs.Close();
            return res;
        }
        private static void Delete()
        {
            DirectoryShell p = cursor.Parent;
            insert_deleted.Delete(ref cursor);
            //cursor = p;
            //cursor.Parent.Children.Remove(cursor);
        }

        private static void Insert()
        {
            Console.Write("Name:");
            String name = Console.ReadLine();
            Console.Write("Type:");
            RightClickShellType type = (RightClickShellType)int.Parse(Console.ReadLine());
            RightClickShell added;
            switch (type)
            {
                case RightClickShellType.DirectoryShell:
                    added = new DirectoryShell() { Name = name };
                    insert_deleted.Add(ref cursor, ref added);
                    break;
                case RightClickShellType.ExecutableShell:
                    added = new ExecutableShell() { Name = name,Command = "MyCommand" };
                    insert_deleted.Add(ref cursor, ref added);
                    break;
            }
        }
        public static void PrintTree(DirectoryShell root,String level_header="")
        {
            RightClickShell current;
            Stack<RightClickShell> queue = new Stack<RightClickShell>();
            queue.Push(root);
            while (queue.Count > 0)
            {
                current = queue.Pop();
                switch (current.Type)
                {
                    case RightClickShellType.DirectoryShell:
                        foreach (RightClickShell sub in ((DirectoryShell)current).Children)
                        {
                            queue.Push(sub);
                        }
                        break;
                    default:
                        break;

                }
                Console.WriteLine(current.getRegistryPath());
                
            }
            
        }

        public static string GetLevel(RightClickShell x)
        {
            String res = string.Empty;
            RightClickShell current = x.Parent;
            while(current != null)
            {
                res = current.Name+ "\\"+res ;
                current = current.Parent;
            }
            return res;
        }

        private static void PrintMenu()
        {
            Console.Write("Chọn 1 trong những tính năng sau:0\n" +
                          "1. Thêm vào\n" +
                          "2. Xóa \n"+
                          "0. Thoat\n" +
                          "---------------------------------\n");
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
