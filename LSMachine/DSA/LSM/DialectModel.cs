using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine
{
	public abstract class DialectModel<TData> : FiniteStateMachine<string, TData> {

		protected string Alphabet;
		protected Dictionary<string, FiniteStateMachine<string, TData>.State> StateMap;

		public int Coherence { get; set; }

		public DialectModel () {
		}

		public bool Validate (string Sentance) {

			string[] words = Sentance.Split(new char[] { ' ' });
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

		public ICollection<string> Generate () {

			var ans = new List<string>();
			var originalCurrentState = CurrentState;

			string[] currentKeyWords = null;

			do {
				GoToNextState();
				currentKeyWords = CurrentState.Key.Split(new char[] { ' ' });
				ans.Add(currentKeyWords.First());
			} while (!CurrentState.IsFinishState);
			foreach (string word in currentKeyWords.Skip(1)) {
				ans.Add(word);
			}

			CurrentState = originalCurrentState;
			return ans;

		}

	}
}

