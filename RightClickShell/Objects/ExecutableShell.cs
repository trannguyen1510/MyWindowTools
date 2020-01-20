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
    }
}
