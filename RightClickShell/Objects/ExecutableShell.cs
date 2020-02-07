﻿using System;
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
        
        private String command;
        public String Command { get => command; set =>command = value; }

        public ExecutableShell()
        {
            this.type = RightClickShellType.ExecutableShell;
        }
        public ExecutableShell(RegistryKey registryKey)
        {
            this.name = registryKey.Name.Split('\\')[registryKey.Name.Split('\\').Length - 1];
            command = (String)registryKey.OpenSubKey("command").GetValue("");
        }
        ExecutableShell(SerializationInfo info, StreamingContext context)
        {
            this.name = info.GetString("Name");
            this.type = RightClickShellType.ExecutableShell;
            this.HaveIcon = info.GetString("HaveIcon");
            command = info.GetString("Command");
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info,context);
            info.AddValue("Command", command, command.GetType());
        }
    }
}
