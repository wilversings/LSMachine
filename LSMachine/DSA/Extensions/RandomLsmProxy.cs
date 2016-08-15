using System;
using LSMachine.Utils;
using System.Linq;

namespace LSMachine {

	/// <summary>
	/// A collection of extension methods for the RandomLsm
	/// which helps populating the FSM with data from various sources
	/// </summary>
	public static class RandomLsmProxy {

		/// <summary>
		/// Loads the data from a file in which the sentances are
		/// separated by new line (UNIX or DOS like) and the words
		/// are separated by space
		/// </summary>
		/// <param name="Lsm">`this` instance of the Lsm</param>
		/// <param name="filePath">Raw data file path</param>
		/// <param name="Coherence">The coherence of the Lsm</param>
		public static void LoadFromRawData(
			this RandomLsm Lsm, 
			int Coherence,
			string FilePath = null
		) {
			
			if (FilePath == null)
				FilePath = Settings.GetSetting("RawDataPath");

			string currentLine;
			using (var inStream = new System.IO.StreamReader(FilePath)) {
					
				while ((currentLine = inStream.ReadLine()) != null) {
					string[] words = currentLine.Split(new char[] { ' ' });
					var cursorState = Lsm.StartState;

					for (int ind = 0; ind < words.Length - Coherence + 1; ++ind) {
						string currentKey = string.Join(" ", words.Skip(ind).Take(Coherence));
						var foundState = Lsm[currentKey];
						if (foundState == null) 
							foundState = Lsm.CreateNewState(currentKey);
						cursorState.Associate(foundState);
						cursorState = foundState;
					}
					cursorState.IsFinishState = true;
				}
			}

		}

		/// <summary>
		/// Loads the data from a XML file, where the coherence is already
		/// specified
		/// </summary>
		/// <param name="Lsm">Lsm.</param>
		/// <param name="filePath">`this` instance of the Lsm</param>
		public static void LoadFromXml (this RandomLsm Lsm, string filePath) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads from SQL
		/// Unknown signature
		/// </summary>
		/// <param name="Lsm">`this` instance of the Lsm</param>
		public static void LoadFromSql (this RandomLsm Lsm) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads from NoSQL
		/// Unknown singature
		/// </summary>
		/// <param name="Lsm">`this` instance of the Lsm</param>
		public static void LoadFromNoSql (this RandomLsm Lsm) {
			throw new NotImplementedException();
		}

	}
}

