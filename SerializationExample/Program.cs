﻿using System;
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
using RightClickShell;

namespace SerializationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            RightClickShell.Directory execute = new RightClickShell.Directory(Registry.ClassesRoot.OpenSubKey("Directory\\Background"));
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
