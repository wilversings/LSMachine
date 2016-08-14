using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LSMachine.Utils {
	public static class Settings {

		private static XDocument SettingsXml;
		private static string SettingsPath = "../../app.config";

		public static bool AutoSync { get; set; } = false;

		// <summary>
		// Loads the settings from the SettingsPath specified variable
		// </summary>
		public static void Load () {
			SettingsXml = XDocument.Load(SettingsPath);
		}

		// <summary>
		// Saves the current state of the Settings Xml tree to the
		// SettingsPath path
		// </summary>
		public static void Sync () {
			SettingsXml.Save(SettingsPath);
		}

		// <summary>
		// Gets the setting with the specified name
		// </summary>
		// <param name = "Setting"> The name of the setting </param>
		public static string GetSetting (string Name) {

			IEnumerable<string> settingList = 
				from s in SettingsXml.Element("configuration").Elements()
				where s.Attribute("name").Value == Name
				select s.Attribute("value").Value;

			return settingList.FirstOrDefault();

		}

		// <summary>
		// Sets the setting with the specified name, at the specified value
		// Syncs with file if AutoSync is true
		// </summary>
		// <param name="Name"> The name of the setting </param>
		// <param value="Value"> The value of the setting </param>
		public static void SetSetting (string Name, string Value) {

			var configurationNode = SettingsXml.Element("configuration");

			IEnumerable<XElement> settingList = 
				from s in configurationNode.Elements()
				where s.Attribute("name").Value == Name
				select s;

			if (settingList.Count() != 0) {
				settingList.First().SetAttributeValue("value", Value);
			} else {
				var newXmlNode = new XElement("setting");
				newXmlNode.SetAttributeValue("name", Name);
				newXmlNode.SetAttributeValue("value", Value);
				configurationNode.Add(newXmlNode);
			}
			if (AutoSync) {
				Sync();
			}

		}
			
	}
}

