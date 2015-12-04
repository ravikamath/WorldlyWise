using UnityEngine;
using System.Collections;

namespace WorldlyWise.Inventory
{
	public class Interactor : Clicker {
		public Texture2D inventoryIcon;
		public GameObject worldIcon;

		protected override void OnMouseUp ()
		{
			GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>().Interact(this);
			base.OnMouseUp ();
		}

		public virtual bool Use()
		{
			if(manager != null)
				manager.Interact(this);
			Destroy(gameObject);
			return true;
		}
	}
}
