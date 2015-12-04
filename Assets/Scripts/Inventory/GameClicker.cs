using UnityEngine;
using System.Collections;
using WorldlyWise.Games;

namespace WorldlyWise.Inventory
{
	public class GameClicker : Clicker
	{
		protected override void OnMouseUp ()
		{
			base.OnMouseUp ();
			if(manager != null)
				manager.Interact(this);
		}

	}
}
