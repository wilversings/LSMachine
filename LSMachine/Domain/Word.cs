using System;

namespace LSMachine {
	
	public class Word {

		private string Reprezentation { get; set; }
		// TODO Culture info field

		public Word (string Word) {
			Reprezentation = Word;
		}
			
		public static int operator - (Word First, Word Second) {
			throw new NotImplementedException ();
		}

		public static Word operator + (Word First, Word Second) {
			throw new NotImplementedException ();
		}

	}

}

