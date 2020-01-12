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
namespace RightClickShells
{
    //[XmlInclude(typeof(Directory))]
    [Serializable]
    public class ExecuteAbleShell : RightClickShell,ISerializable
    {
        
        public String Command;
        public ExecuteAbleShell()
        {
            this.type = RightClickShellType.DirectoryShell;
        }
        public ExecuteAbleShell(RegistryKey registryKey)
        {
            this.type = RightClickShellType.ExecutableShell;
            Command = (String)registryKey.OpenSubKey("command").GetValue("");
        }
        ExecuteAbleShell(SerializationInfo info, StreamingContext context):base(info,context)
        {
            this.type = RightClickShellType.ExecutableShell;
            Command = info.GetString("Command");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info,context);
            info.AddValue("Command", Command, Command.GetType());
        }

    }
}
