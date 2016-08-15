using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace LSMachine {
	
	public class RandomLsm : DialectModel<dynamic> { 
		
		public RandomLsm (){
		}

		/// <summary>
		/// Moves the current state to a pseudo-randomly chosen next state
		/// Chainable
		/// </summary>
		/// <returns> The new state </returns>
		public override State GoToNextState () {

			// Pseudo-random generator
			var randomEngine = new Random((int)DateTime.Now.ToBinary());
			int adjacentCount = CurrentState.AdjacentCount;
			int nextStateIndex = randomEngine.Next() % adjacentCount;

			CurrentState = CurrentState.NextStates.ElementAt(nextStateIndex);
			return CurrentState;
			
		}

		public IList<string> Generate () {

			throw new NotImplementedException();

		}

	}

}

