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

// Another Library included in project
using RightClickShells;
using System.Windows.Forms;

// This project is for testing and demonstrating, it will be deleted
//
namespace SerializationExample
{
    class Program
    {
        DirectoryShell cursor;
        DirectoryShell root = new DirectoryShell() { name = "Root" };
        static void Main(string[] args)
        {
            Program program_obj = new Program();
            program_obj.cursor = program_obj.root;
            InsertDeleteManager insert_deleted = new InsertDeleteManager(program_obj.root);

            
        }
        public static void SerializeTree()
        {
            FileStream fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            DirectoryShell execute = new DirectoryShell(Registry.ClassesRoot.OpenSubKey("Directory\\Background"));
            XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(DirectoryShell));
            xmlSerializer_background.Serialize(fs, execute);
            fs.Close();
        }

        public static void DeSerializeTree()
        {
            XmlSerializer xmlSerializer_background = new XmlSerializer(typeof(DirectoryShell));
            FileStream fs = new FileStream("BackGroundShortcuts.xml", FileMode.OpenOrCreate);
            DirectoryShell directory = (DirectoryShell)xmlSerializer_background.Deserialize(fs);
            foreach (RightClickShell child in directory.Children)
            {
                ExecuteAbleShell t;
                if (child.type == RightClickShellType.ExecutableShell)
                {
                    t = (ExecuteAbleShell)child;
                    Console.WriteLine(t.Command);
                    break;
                }
            }
        }
        public static void PrintTree(DirectoryShell root,String level_header="")
        {
            DirectoryShell current;
            Console.WriteLine(level_header+ root.name);
            Queue<DirectoryShell> queue = new Queue<DirectoryShell>();
            
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                Console.WriteLine( GetLevel(current) + current.name);
                foreach (DirectoryShell sub in current.Children)
                {
                    queue.Enqueue(sub);
                }
            }
            
        }
        public static string GetLevel(DirectoryShell x)
        {
            String res = string.Empty;
            DirectoryShell current = x.Parent;
            while(current != null)
            {
                res += current.name+ "\\" ;
                current = current.Parent;
            }
            return res;
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
