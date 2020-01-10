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
namespace RightClickShell
{
    [XmlInclude(typeof(Directory))]
    [Serializable]
    public class ExecuteAble : RightClickShell,ISerializable
    {
        
        public String Command;
        public ExecuteAble()
        {
            this.type = RightClickShellType.Executable;
        }
        public ExecuteAble(RegistryKey registryKey)
        {
            this.type = GetTypeOfRegistryKey(registryKey);
            Command = (String)registryKey.OpenSubKey("command").GetValue("");
            
        }
        ExecuteAble(SerializationInfo info, StreamingContext context):base(info,context)
        {
            this.type = RightClickShellType.Executable;
            Command = info.GetString("Command");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info,context);
            info.AddValue("Command", Command, Command.GetType());
        }

    }
}
