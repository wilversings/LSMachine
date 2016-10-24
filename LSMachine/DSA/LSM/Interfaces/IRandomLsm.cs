using System;

namespace LSMachine {
	public interface IRandomLsm {
		FiniteStateMachine<string, object>.State GetNextState (string StateKey);
	}
}

