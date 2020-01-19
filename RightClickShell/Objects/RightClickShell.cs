using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.IO;

namespace RightClickShells
{
    //[XmlInclude(typeof(Directory))]
    [Serializable]
    public class RightClickShell : ISerializable
    {
        [XmlAttribute] public String name;
        //[XmlAttribute] public String icon;
        //[XmlAttribute] public String position;
        //[XmlAttribute] public String ShowWhileHold;
        [XmlIgnore] public DirectoryShell Parent = null;
        [XmlIgnore] public RightClickShellType type;
        public RightClickShell()
        { 
        }
        public RightClickShell(SerializationInfo info, StreamingContext context)
        {
            name = info.GetString("Name");
            //icon = info.GetString("Icon");
            //ShowWhileHold = info.GetString("ShowWhileHold");
            //position = info.GetString("position");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", name, name.GetType());
            //info.AddValue("Icon", name, name.GetType());
            //info.AddValue("Position", position, position.GetType());
            //info.AddValue("ShowWhileHold", ShowWhileHold, ShowWhileHold.GetType());
        }
        
        protected RightClickShellType GetTypeOfRegistryKey(RegistryKey input)
        {
            foreach (string s in input.GetSubKeyNames())
            {
                if (s == "shell" || s == "Shell")
                    return RightClickShellType.DirectoryShell;
                if (s == "command" || s == "Command")
                {
                    return RightClickShellType.ExecutableShell;
                }
            }
            return RightClickShellType.Null;
        }
        [OnDeserialized()]
        public void KnowingFather(StreamingContext context)
        {
            Console.WriteLine(context.Context);
            Console.WriteLine(context.State);
            //FileStream fs = new FileStream("Output.txt", FileMode.OpenOrCreate);
            //byte[] state_bytes = new UTF8Encoding(true).GetBytes(context.State.ToString()+"\n");
            //byte[] Context_bytes = new UTF8Encoding(true).GetBytes(context.Context.ToString() + "\n");
            //fs.Write(state_bytes, 0,state_bytes.Length);
            //fs.Write(Context_bytes,state_bytes.Length,Context_bytes.Length);
            //fs.Close();
        }
        public String getFullPath()
        {
            String res = "/" + this.name;
            DirectoryShell trace_back = this.Parent;
            while (trace_back != null)
            {
                res = "/" + trace_back.name + res;
                trace_back = trace_back.Parent;
            }
            return res;
        }
    }
}
