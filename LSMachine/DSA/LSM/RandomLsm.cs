using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace LSMachine {

	/// <summary>
	/// The implementation of a LSM which randomly selects
	/// the new state
	/// </summary>
	public class RandomLsm : DialectModel<object>, IRandomLsm { 

		protected Random RandomEngine;

		public RandomLsm () {
			RandomEngine = new Random((int)DateTime.Now.ToBinary());
		}

		/// <summary>
		/// Moves the current state to a pseudo-randomly chosen next state
		/// Chainable
		/// </summary>
		/// <returns> The new state </returns>
		public override State GetNextState (string StateKey) {

			// Pseudo-random generator
			var st = this[StateKey];
			int adjacentCount = st.AdjacentCount;
			int nextStateIndex = RandomEngine.Next() % adjacentCount;

			return st.NextStates.Values.ElementAt(nextStateIndex);

		}

	}

}

