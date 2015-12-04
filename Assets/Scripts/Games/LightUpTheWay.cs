using UnityEngine;
using System.Collections.Generic;
using WorldlyWise.Inventory;

namespace WorldlyWise.Games
{
	public class LightUpTheWay : GameManager
	{
		public enum State { Start, Finish}
		
		// Puzzle parts
		public State state = State.Start;
		private Dictionary<State, Interactions> interactions;
		
		void Start()
		{
			interactions = new Dictionary<State, Interactions>();
			interactions[State.Start] = StartInteractions;
			interactions[State.Finish] = FinishInteractions;
		}

		public override void Interact(Clicker clicker)
		{
			interactions[state](clicker);
		}

		private void StartInteractions (Clicker clicker)
		{
		}
		
		private void FinishInteractions (Clicker clicker)
		{
		}
	}
}
