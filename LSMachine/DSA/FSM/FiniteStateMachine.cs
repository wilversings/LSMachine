using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine
{
	public abstract partial class FiniteStateMachine <TKey, TData> {

		protected ICollection<State> States;

		public State StartState { get; set; }
		public State CurrentState { get; set; }

		/// <summary>
		/// Creates the new state with the current object owner
		/// and add its to the machine
		/// </summary>
		/// <returns> The newly created state </returns>
		public State CreateNewState () {
			States = new List<State>();
			var newState = new State(this);
			States.Add(newState);
			return new State(this);
		}

		public FiniteStateMachine () {
			var startState = CreateNewState();
			var endState = CreateNewState();

			StartState = startState;
			endState.IsFinishState = true;

		}

		/// <summary>
		/// Does a query for all the finish states in the machine
		/// </summary>
		/// <returns> All the finish states in the machine</returns>
		public IEnumerable<State> FinishStates () {
			return from s in States
			       where s.IsFinishState == true
			       select s;
		}

		public abstract State GoToNextState ();

	}
}

