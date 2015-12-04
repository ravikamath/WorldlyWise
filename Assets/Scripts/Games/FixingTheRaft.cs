using UnityEngine;
using System.Collections.Generic;
using WorldlyWise.Inventory;

namespace WorldlyWise.Games
{
	public class FixingTheRaft : GameManager
	{
		// an enum to represent the "game" as a state machine
		public enum State { Start, RaftClicked, PumpClicked, RaftChecked, BearingMade, RaftDeflated, RaftInflated, Finish}

		// Physical elements
		public GameClicker raft;
		public GameClicker pump;
		public Transform targetLocation;
		public BearingGame bearingGame;

		// Sounds
		public AudioClip foundClip;
		public AudioClip leverClip;
		public AudioClip successClip;

		// Puzzle parts
		public State state = State.Start;
		private Dictionary<State, Interactions> interactions;
		private GameGUI gameGUI;

		public override void Initialize ()
		{
			interactions = new Dictionary<State, Interactions>();
			interactions[State.Start] = StartInteractions;
			interactions[State.RaftClicked] = RaftClickedInteractions;
			interactions[State.PumpClicked] = PumpClickedInteractions;
			interactions[State.RaftChecked] = RaftCheckedInteractions;
			interactions[State.BearingMade] = BearingMadeInteractions;
			interactions[State.RaftDeflated] = RaftDeflatedInteractions;
			interactions[State.RaftInflated] = RaftInflatedInteractions;
			interactions[State.Finish] = FinishInteractions;

			raft.manager = this;
			pump.manager = this;
			bearingGame.parentManager = this;

			gameGUI = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameGUI>();
			gameGUI.message = "Excellent! So you have reached the shore. Use the raft to get to the other side of the river";

		}
		
		public override void Interact(Clicker clicker)
		{
			interactions[state](clicker);
		}

		private void StartInteractions(Clicker clicker)
		{
			if(clicker.gameObject == raft.gameObject)
			{
				state = State.RaftClicked;
				gameGUI.message = "It appears the raft has lost much of its air. There is a pump nearby. Maybe you can pump some air in.";
			}
		}
		private void RaftClickedInteractions(Clicker clicker)
		{
			if(clicker.gameObject == pump.gameObject)
			{

				pump.gameObject.animation.Play ("Pump");
				AnimateDelayed component = raft.gameObject.AddComponent<AnimateDelayed>();
				component.time = 1;
				component.clip = "InflateDeflate";
				gameGUI.message = "Awesome! The pump inflated the raft, but it has soon deflated again. Perhaps something is wrong with the raft";
				state = State.PumpClicked;
				AudioSource.PlayClipAtPoint(leverClip, pump.transform.position);
			}

		}
		private void PumpClickedInteractions(Clicker clicker)
		{
			if(clicker.gameObject == raft.gameObject)
			{
				gameGUI.message = "A closer inspection reveals that the ball bearing assembly (that keeps the air from going out) " +
									"has been damaged. You will need to create one. Perhaps you can make use of the items available on " +
									"the table";
				state = State.RaftChecked;
				bearingGame.Initialize();
			}
		}
		private void RaftCheckedInteractions(Clicker clicker)
		{
			if(clicker.name == "Bearing")
			{
				state = State.BearingMade;
				(clicker as Item).target = raft.collider;
				(clicker as Item).manager = this;
				AudioSource.PlayClipAtPoint(foundClip, raft.transform.position);
				GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>().Interact(clicker as Item);
				gameGUI.message = 
					"Well done!\n" + 
					"You managed to create a ball bearing of the appropriate size. The shiny new ball has been added to your inventory. " +
						"We should be able to fix the raft using it.";
			}
		}
		private void BearingMadeInteractions(Clicker clicker)
		{
			if(clicker.name == "Bearing")
			{
				state = State.RaftDeflated;
				gameGUI.message = "Almost there. Now just fill up the raft with air and sail forward";
			}

		}
		private void RaftDeflatedInteractions(Clicker clicker)
		{
			if(clicker.gameObject == pump.gameObject)
			{
				pump.animation.Play ("Pump");
				AnimateDelayed component = raft.gameObject.AddComponent<AnimateDelayed>();
				component.time = 1;
				component.clip = "Inflate";
				state = State.RaftInflated;
				AudioSource.PlayClipAtPoint(leverClip, pump.transform.position);
			}
		}
		private void RaftInflatedInteractions(Clicker clicker)
		{
			if(clicker.gameObject == raft.gameObject)
			{
				GameObject.FindGameObjectWithTag("Player").transform.position = targetLocation.position;
				GameObject.FindGameObjectWithTag("Player").transform.rotation = targetLocation.rotation;
				gameGUI.message = "Quest completed! You were able to successfully apply Archimedes' principle to fix your raft. Great job.\n\n<-- End of Game -->";
				state = State.Finish;
				AudioSource.PlayClipAtPoint(successClip, targetLocation.position);
			}
		}
		private void FinishInteractions(Clicker clicker)
		{
		}
	}
}