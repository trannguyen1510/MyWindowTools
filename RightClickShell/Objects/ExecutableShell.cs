using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Microsoft.Win32;
using System.IO;

namespace RightClickShells
{
    //[XmlInclude(typeof(Directory))]
    [Serializable]
    public class ExecutableShell : RightClickShell,ISerializable
    {
        
        public String command;
        public ExecutableShell()
        {
            this.type = RightClickShellType.ExecutableShell;
        }
        public ExecutableShell(RegistryKey registryKey)
        {
            this.name = registryKey.Name.Split('\\')[registryKey.Name.Split('\\').Length - 1];
            command = (String)registryKey.OpenSubKey("command").GetValue("");
        }
        ExecutableShell(SerializationInfo info, StreamingContext context):base(info,context)
        {
            this.type = RightClickShellType.ExecutableShell;
            command = info.GetString("Command");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info,context);
            info.AddValue("Command", command, command.GetType());
        }
        [OnDeserialized()]
        public void KnowingFather(StreamingContext context)
        {
            Console.WriteLine(context.Context);
            Console.WriteLine(context.State);
            //FileStream fs = new FileStream("Output.txt", FileMode.OpenOrCreate);
            //byte[] state_bytes = new UTF8Encoding(true).GetBytes(context.State.ToString() + "\n");
            //byte[] Context_bytes = new UTF8Encoding(true).GetBytes(context.Context.ToString() + "\n");
            //fs.Write(state_bytes, 0, state_bytes.Length);
            //fs.Write(Context_bytes, state_bytes.Length, Context_bytes.Length);
            //fs.Close();
        }
    }
}
