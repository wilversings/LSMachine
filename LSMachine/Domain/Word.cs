using System;

namespace LSMachine {
	
	public class Word {

		private string Reprezentation { get; set; }
		// TODO Culture info field

		public Word (string Word) {
			Reprezentation = Word;
		}
			
		public static uint operator - (Word First, Word Second) {

			var dTable = new uint[First.Reprezentation.Length, Second.Reprezentation.Length];
			string first = First.Reprezentation,
				   second = Second.Reprezentation;
			int fLen = First.Reprezentation.Length, 
				 sLen = Second.Reprezentation.Length;

			dTable [0, 0] = second [0] == first [0] ? 0u : 1u;
			for (int i = 1; i < fLen; ++i)
				dTable [i, 0] = dTable [i - 1, 0] + (second[0] == first[i] ? 0u : 1u);
			for (int i = 1; i < sLen; ++i)
				dTable [0, i] = dTable [0, i - 1] +  (first[0] == second[i] ? 0u : 1u);

			for (int i = 1; i < fLen; ++i)
				for (int j = 1; j < sLen; ++j)
					if (first [i] == second [j])
						dTable [i, j] = dTable [i - 1, j - 1];
					else 
						dTable [i, j] = Math.Min (dTable [i - 1, j] + 1, dTable [i, j - 1] + 1);
					

			return dTable [fLen - 1, sLen - 1];

		}

		public static Word operator + (Word First, Word Second) {
			throw new NotImplementedException ();
		}

	}

}

