using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine
{
	public abstract partial class FiniteStateMachine <TKey, TData> {

		protected Dictionary<TKey, State> States;

		public State StartState { get; set; }
		public State CurrentState { get; set; }

		/// <summary>
		/// Creates the new state with the current object owner
		/// and add its to the machine
		/// </summary>
		/// <returns> The newly created state </returns>
		public State CreateNewState (TKey Key) {
			var newState = new State(this, Key);
			States.Add(Key, newState);
			return newState;
		}

		public FiniteStateMachine () {

			States = new Dictionary<TKey, State>();

			StartState = CurrentState = new State(this, null);

		}

		/// <summary>
		/// Does a query for all the finish states in the machine
		/// </summary>
		/// <returns> All the finish states in the machine</returns>
		public IEnumerable<State> FinishStates () {
			return from s in States.Values
			       where s.IsFinishState == true
			       select s;
		}

		public void Reset () {
			CurrentState = StartState;
		}

		public State GetState (TKey Key) {
			if (States.ContainsKey(Key))
				return States[Key];
			return null;
		}

		public abstract State GoToNextState ();

	}
}

