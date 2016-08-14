using System;
using System.Collections.Generic;

namespace LSMachine
{
	public abstract partial class FiniteStateMachine <T> {
		public partial class Node :
			IEnumerable<Node> {

			public ICollection<Node> NextStates { get; set; }
			public bool IsFinishState { get; set; }
			public T Data { get; set; }

			// <summary>
			// Associates `this` with `Second` node in the finite state machine
			// </summary>
			// <param name="Second"> The node to asociate `this` with </param>

			public Node () {
				NextStates = new List<Node> ();
			}

			public Node Associate (Node Second) {

				NextStates.Add(Second);

				return this;
			}

			public IEnumerator<Node> GetEnumerator ()
			{
				return NextStates.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
			{
				return NextStates.GetEnumerator();
			}
		}
	}
}

