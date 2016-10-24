using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine {
	
	public static class LsmStatistics {

		private static List<string> data = null;

		public static int Redundancy<TValue>
		(this DialectModel<TValue> Lsm, int CaseNumber) {

			int redundancy = 0;

			while (CaseNumber-- != 0) {
				if (IsRendundant(Lsm.Generate())) {
					++redundancy;
				}
			}

			return redundancy;

		}

		public static bool IsRendundant (ICollection<string> Output) {

			if (data == null)
				data = System.IO.File.ReadAllLines(Utils.Settings.GetSetting("RawDataPath")).ToList();

			string outputString = string.Join(" ", Output);
			foreach (string line in data) {
				if (line.Contains(outputString)) {
					return true;
				}
			}

			return false;

		}
			
	}
}

