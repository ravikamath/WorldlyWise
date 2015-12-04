using UnityEngine;
using System.Collections;

namespace WorldlyWise.Inventory
{
	public class Pickup : Interactor
	{
		protected override void OnMouseUp()
		{
			Use ();
		}
	}
}
