using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace LSMachine {
	
	public class RandomLsm : DialectModel<dynamic> { 

		protected Random RandomEngine;

		public RandomLsm (){
			RandomEngine = new Random((int)DateTime.Now.ToBinary());
		}

		/// <summary>
		/// Moves the current state to a pseudo-randomly chosen next state
		/// Chainable
		/// </summary>
		/// <returns> The new state </returns>
		public override State GoToNextState () {

			// Pseudo-random generator
			int adjacentCount = CurrentState.AdjacentCount;
			int nextStateIndex = RandomEngine.Next() % adjacentCount;

			CurrentState = CurrentState.NextStates.ElementAt(nextStateIndex);
			return CurrentState;

		}

		public override ICollection<string> Generate () {

			var ans = new List<string>();
			string[] currentKeyWords = null;

			do {
				GoToNextState();
				currentKeyWords = CurrentState.Key.Split(new char[] { ' ' });
				ans.Add(currentKeyWords.First());
			} while (!CurrentState.IsFinishState);
			foreach (string word in currentKeyWords.Skip(1)) {
				ans.Add(word);
			}

			Reset();
			return ans;

		}

	}

}

