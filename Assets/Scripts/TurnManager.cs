using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twitch {
	public class TurnManager {
		public delegate void VoidDelegate();
		public event VoidDelegate OnTurnPassed;

		public void OnTurnPass() {
			if(OnTurnPassed != null) {
				OnTurnPassed();
			}
		}
	}
}