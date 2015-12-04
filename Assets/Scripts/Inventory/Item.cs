using UnityEngine;
using System.Collections;

namespace WorldlyWise.Inventory
{
	public class Item : Interactor 
	{
		public Collider target;
		
		public override bool Use()
		{
			RaycastHit info;
			if(target.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
				out info, Camera.main.farClipPlane))
			{
				Consume ();
				Destroy (gameObject);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Consume this instance.
		/// </summary>
		public virtual void Consume()
		{
			if(manager != null)
				manager.Interact(this);
		}
	}
}