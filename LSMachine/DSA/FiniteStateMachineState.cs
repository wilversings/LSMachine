using System;
using System.Collections.Generic;

namespace LSMachine
{
	public abstract partial class FiniteStateMachine <T> {
		public partial class State :
			IEnumerable<State> {

			public ICollection<State> NextStates { get; set; }
			public bool IsFinishState { get; set; }
			public T Data { get; set; }
			public FiniteStateMachine<T> Machine;

			public State (FiniteStateMachine<T> MachineOwner) {
				NextStates = new List<State> ();
				Machine = MachineOwner;
			}

			/// <summary>
			/// Associates `this` with `Second` node in the finite state machine
			/// This is a chainable method
			/// </summary>
			/// <param name="Second"> The node to asociate `this` with </param>
			/// <returns> `this` for the chainable property </returns>
			public State Associate (State Second) {

				NextStates.Add(Second);

				return this;
			}

			/// <summary>
			/// Returns the number of possible next states of this state
			/// </summary>
			/// <value> The adjacent count </value>
			public uint AdjacentCount {
				get { return (uint)NextStates.Count; }
			}
				
			public IEnumerator<State> GetEnumerator () {
				return NextStates.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
				return NextStates.GetEnumerator();
			}
		}
	}
}

