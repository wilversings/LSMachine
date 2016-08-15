using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine
{
	public abstract partial class FiniteStateMachine <TKey, TData> 
		where TKey : class {
		public partial class State :
			IEnumerable<State> {

			public ICollection<State> NextStates { get; set; }
			public bool IsFinishState { get; set; }
			public TData Data { get; set; }
			public TKey Key { get; set; }
			public FiniteStateMachine<TKey, TData> Machine;

			public State (FiniteStateMachine<TKey, TData> MachineOwner) {
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

			/// <summary>
			/// Returns the next associated state which matches
			/// the specified key
			/// </summary>
			/// <param name="Key"> The key to match the next state</param>
			public State Next (TKey Key) {
				
				var nextStates = 
					from s in NextStates
					where s.Key.Equals(Key)
					select s;

				return nextStates.FirstOrDefault();

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

