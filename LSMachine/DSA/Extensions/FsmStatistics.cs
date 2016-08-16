using System;

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

		public static int StateNumber<TKey, TValue>
		(this FiniteStateMachine<TKey, TValue> Fsm) where TKey : class {

			return Fsm.AllStates.Count;

		}

		public static int PathNumber<TKey, TValue>
		(this FiniteStateMachine<TKey, TValue> Fsm) where TKey : class {
			throw new NotImplementedException();
		}

		public static int Diameter<TKey, TValue>
		(this FiniteStateMachine<TKey, TValue> Fsm) where TKey : class {
			throw new NotImplementedException();
		}

		public static string CharSet<TKey, TValue>
		(this FiniteStateMachine<TKey, TValue> Fsm) where TKey : class {
			throw new NotImplementedException();
		}

	}
}

