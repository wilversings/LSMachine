using System;

namespace LSMachine {
	public static class LsmStatistics {

		public static int Redundancy<TValue>
		(this DialectModel<TValue> Lsm, int CasesRan) {

			string data = System.IO.File.ReadAllText(Utils.Settings.GetSetting("RawDataPath"));
			int redundancy = 0;

			while (CasesRan-- != 0) {
				string generated = string.Join(" ", Lsm.Generate());
				if (data.Contains(generated)) {
					++redundancy;
				}
			}

			return redundancy;

		}

	}
}

