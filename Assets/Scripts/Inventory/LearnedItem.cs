using UnityEngine;
using System.Collections;

namespace WorldlyWise.Inventory
{
	public class LearnedItem : Interactor
	{
		public string message = null;

		public override bool Use()
		{
			if(manager != null)
				manager.Interact(this);
			if(message != null && message.Length > 0)
				GameObject.FindGameObjectWithTag("GameController").GetComponent<GameGUI>().message = message;
			return false;
		}
	}
}
