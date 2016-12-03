using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine {
	
	public abstract partial class FiniteStateMachine <TKey, TData> 
		where TKey : class {

		/// <summary>
		/// This class encapsulates the concept of state in a FSM
		/// This is basically a node, with a key which is unique in the
		/// owner FSM.
		/// </summary>
		public partial class State :
			IEnumerable<State> {

			public Dictionary<TKey, State> NextStates { get; set; }
			public bool IsFinishing { get; set; }
			public TData Data { get; set; }
			public TKey Key { get; set; }
			public FiniteStateMachine<TKey, TData> Fsm;

			public State (FiniteStateMachine<TKey, TData> MachineOwner, TKey Key) {
				NextStates = new Dictionary<TKey, State> ();
				Fsm = MachineOwner;
				this.Key = Key;
			}

			/// <summary>
			/// Associates `this` with `Second` node in the finite state machine
			/// This is a chainable method
			/// </summary>
			/// <param name="Second"> The node to asociate `this` with </param>
			/// <returns> `this` for the chainable property </returns>
			public State Link (State Other) {

				NextStates[Other.Key] = Other;

				return this;
			}

			public State Link (TKey Key) {

				NextStates[Key] = Fsm[Key];

				return this;
			}

			public bool Cut (TKey Key) {
				return NextStates.Remove(Key);
			}
			public void CutAll() {
				NextStates.Clear();
			}

			/// <summary>
			/// Returns the number of possible next states of this state
			/// </summary>
			/// <value> The adjacent count </value>
			public int AdjacentCount {
				get { return NextStates.Count; }
			}

			/// <summary>
			/// Gets the adjacent state with the specified Key.
			/// </summary>
			/// <param name="Key">Key.</param>
			public State this[TKey Key] {
				get {
					return NextStates[Key];
				}
			}

			public ICollection<State> AllAdjacent {
				get {
					return NextStates.Values;
				}
			}

			public IEnumerator<State> GetEnumerator () {
				return NextStates.Values.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
				return NextStates.Values.GetEnumerator();
			}
		}
	}
}

