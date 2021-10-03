using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudstorageEditorV3
{

    public class EpicInstallLocations
    {
        [JsonProperty("InstallationList")]
        public List<Installation> InstallationList { get; set; }
    }

    public class Installation
    {
        [JsonProperty("InstallLocation")]
        public string InstallLocation { get; set; }

        [JsonProperty("AppName")]
        public string AppName { get; set; }
    }
    class CloudstorageEditor
    {
        public void Launch()
        {
            var FortnitePath = GetFortnitePath();

            if (File.Exists(FortnitePath + "SSL.dll"))
                File.Delete(FortnitePath + "SSL.dll");

            File.Copy("SSL.dll", FortnitePath + "SSL.dll");

            if (File.Exists(FortnitePath + "NoKick.dll"))
                File.Delete(FortnitePath + "NoKick.dll");

            File.Copy("NoKick.dll", FortnitePath + "NoKick.dll");

            File.Delete(FortnitePath + "FortniteClient-Win64-Shipping_EAC.exe");
            File.Delete(FortnitePath + "FortniteClient-Win64-Shipping_BE.exe");

            File.Copy("CloudstorageEditorV3.exe", FortnitePath + "FortniteClient-Win64-Shipping_EAC.exe");
            File.Copy("CloudstorageEditorV3.exe", FortnitePath + "FortniteClient-Win64-Shipping_BE.exe");

            Process.Start("com.epicgames.launcher://apps/Fortnite?action=launch&silent=true");

            new Listener().Start().GetAwaiter().GetResult();
        }

        string GetFortnitePath()
        {
            var ConfigData = File.ReadAllText("C:\\ProgramData\\Epic\\UnrealEngineLauncher\\LauncherInstalled.dat");
            var InstallationList = JsonConvert.DeserializeObject<EpicInstallLocations>(ConfigData);
            foreach (var Installation in InstallationList.InstallationList)
            {
                if (Installation.AppName == "Fortnite")
                {
                    return Installation.InstallLocation + "\\FortniteGame\\Binaries\\Win64\\";
                }
            }
            return "";
        }
    }
}
