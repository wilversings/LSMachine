using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine {

	/// <summary>
	/// Finite State Machine
	/// <see href="https://en.wikipedia.org/wiki/Finite-state_machine"/> 
	/// </summary>
	public abstract partial class FiniteStateMachine <TKey, TData> {

		protected Dictionary<TKey, State> StateMap;

		public State StartState { get; set; }
		public State CurrentState { get; set; }

		/// <summary>
		/// Creates the new state with the current object owner
		/// and add its to the machine
		/// </summary>
		/// <returns> The newly created state </returns>
		public State CreateNewState (TKey Key) {
			var newState = new State(this, Key);
			StateMap.Add(Key, newState);
			return newState;
		}

		public FiniteStateMachine (TKey StartStateKey) {

			StateMap = new Dictionary<TKey, State>();
			StartState = CurrentState = new State(this, null);
			StartState.Key = StartStateKey;
			StateMap.Add(StartStateKey, StartState);

		}

		/// <summary>
		/// Does a query for all the finish states in the machine
		/// </summary>
		/// <returns> All the finish states in the machine</returns>
		public IEnumerable<State> FinishStates () {
			return from s in StateMap.Values
			       where s.IsFinishState == true
			       select s;
		}

		/// <summary>
		/// Resets the machine (current state) to the start state
		/// </summary>
		public void Reset () {
			CurrentState = StartState;
		}

		/// <summary>
		/// Gets or sets the <see cref="LSMachine.FiniteStateMachine`2"/> with the specified Key.
		/// </summary>
		/// <param name="Key">Key.</param>
		public State this[TKey Key] {
			get {
				if (StateMap.ContainsKey(Key))
					return StateMap[Key];
				return null;
			}
			set {
				StateMap[Key] = value;
			}
		}

		public State GoToNextState () {
			CurrentState = GetNextState(CurrentState.Key);
			return CurrentState;
		}

		public ICollection<State> AllStates {
			get {
				return StateMap.Values;
			}
		}

		public abstract State GetNextState (TKey StateKey);

	}
}

