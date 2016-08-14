using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine
{
	public abstract partial class FiniteStateMachine <T> {

		protected ICollection<State> States;

		public State StartState { get; set; }
		public State CurrentState { get; set; }

		public State CreateNewState () {
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

		public IEnumerable<State> FinishStates () {
			return from s in States
			       where s.IsFinishState == true
			       select s;
		}

		public abstract State GoToNextState ();

	}
}

