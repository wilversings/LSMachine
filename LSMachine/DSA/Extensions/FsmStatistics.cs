using System;
using System.Collections;
using System.Collections.Generic;

namespace LSMachine{
	public static class FsmStatistics {
		
		public static double AdjacentNumberAverage<TKey, TValue> 
		(this FiniteStateMachine<TKey, TValue> Fsm) where TKey : class {

			var allStates = Fsm.AllStates;
			int stateNumber = allStates.Count;
			double average = 0;

			foreach (var state in allStates) {
				average += state.AdjacentCount;
			}

			return average / stateNumber;

		}

	}
}

