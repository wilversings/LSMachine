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
		public IEnumerable<State> FinishStates {
			get {
				return (from s in StateMap.Values
				        where s.IsFinishState == true
				        select s).ToList();
			}
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

		public void Transpose () {

			ICollection<State> futureFinishStates = StartState.AllAdjacent.ToList();
			StartState.CutAll();

			IEnumerable<State> currentFinishStates = FinishStates.ToList();

			foreach (State currentFinishState in currentFinishStates) {
				currentFinishState.IsFinishState = false;
			}
			foreach (State finishState in futureFinishStates) {
				finishState.IsFinishState = true;
			}

			var reverseLinks = new Dictionary<TKey, List<TKey>>();

			foreach (State state in AllStates) {

				if (state.Key == StartState.Key)
					continue;

				foreach (State neighbour in state) {
					if (reverseLinks.ContainsKey(neighbour.Key)) {
						reverseLinks[neighbour.Key].Add(state.Key);
					} else {
						var newList = new List<TKey>();
						newList.Add(state.Key);
						reverseLinks.Add(neighbour.Key, newList);
					}
				}
				state.CutAll();
			}

			foreach (var statePair in reverseLinks) {
				foreach (var valueState in statePair.Value) {
					this[statePair.Key].Link(this[valueState]);
				}
			}

			foreach (State startState in currentFinishStates) {
				StartState.Link(startState);
			}

		}

		public abstract State GetNextState (TKey StateKey);

	}
}

