using System;
using System.Collections.Generic;

namespace LSMachine
{
	public abstract partial class FiniteStateMachine <T>
	{
		public class Node {

			public ICollection<Node> NextStates { get; set; }
			public bool IsFinishState { get; set; }
			public T Data { get; set; }

			public Node Associate (Node Second) {

				NextStates.Add(Second);

				return this;
			}

		}
	}
}

