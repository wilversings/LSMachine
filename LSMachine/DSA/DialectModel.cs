﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LSMachine
{
	public abstract class DialectModel<TData> : FiniteStateMachine<string, TData> {

		protected string Alphabet;
		protected Dictionary<string, FiniteStateMachine<string, TData>.State> StateMap;

		public int Coherence { get; set; }

		public DialectModel () {
		}

		public bool Validate (string Sentance) {

			string[] words = Sentance.Split(new char[] { ' ' });
			int wordLen = words.Length;

			State currentState = StartState;

			for (int ind = 0; ind <= wordLen - Coherence + 1; ++ind) {
				string[] currentStateWords = words.Skip(ind).Take(Coherence).ToArray();

				currentState = currentState.Next(string.Join(" ", currentStateWords));
				if (currentState == null)
					return false;
			}

			return true;

		}
	}
}
