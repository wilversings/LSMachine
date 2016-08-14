using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LSMachine.Utils {
	public static class Settings {

		private static XDocument SettingsXml;
		private static string SettingsPath = "../../app.config";

		// <summary>
		// Loads the settings from the SettingsPath specified variable
		// </summary>
		public static void Load () {
			SettingsXml = XDocument.Load(SettingsPath);
		}

		// <summary>
		// Gets the setting with the specified name
		// </summary>
		// <param name = "Setting"> The name of the setting </param>
		public static string GetSetting (string Setting) {

			IEnumerable<string> settingList = 
				from s in SettingsXml.Element("configuration").Elements()
				where s.Attribute("name").Value == Setting
				select s.Attribute("value").Value;

			return settingList.FirstOrDefault();

		}
			
	}
}

