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
				SettingsXml.Save(SettingsPath);
			}

		}
			
	}
}

