using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine {
	
	public abstract class DialectModel<TData> : FiniteStateMachine<string, TData> {

		//public string Alphabet;

		public int Coherence { get; set; }
		public string Alphabet { get; private set; }

		public DialectModel () : base("__init_key") {
		}

		/// <summary>
		/// Checks whether the Sentance is accepted by the FSM
		/// </summary>
		/// <param name="Sentance">The Sentance as a string</param>
		public virtual bool Validate (string Sentance) {

			string[] words = Sentance.Split(' ');
			int wordLen = words.Length;

			var currentState = StartState;

			for (int ind = 0; ind < wordLen - Coherence + 1; ++ind) {
				string[] currentStateWords = words.Skip(ind).Take(Coherence).ToArray();

				currentState = currentState[string.Join(" ", currentStateWords)];
				if (currentState == null)
					return false;
			}

			return true;

		}

		/// <summary>
		/// Generates a collection of strings (FSM Keys) which
		/// is accepted by the FSM
		/// </summary>
		public virtual ICollection<string> Generate () {

			var ans = new List<string>();

			string[] currentKeyWords = null;
			var cursorState = GetNextState(CurrentState.Key);
	
			while (!cursorState.IsFinishing) {
				currentKeyWords = cursorState.Key.Split(' ');
				ans.Add(currentKeyWords.First());
				cursorState = GetNextState(cursorState.Key);
			}

			foreach (string word in currentKeyWords.Skip(1)) {
				ans.Add(word);
			}
				
			return ans;

		}

	}
}

