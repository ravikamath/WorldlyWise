using UnityEngine;
using System.Collections;

namespace WorldlyWise.Inventory
{
	public class GameClickerDragDrop : GameClicker
	{
		private float distance = -1;
		private int originalLayer = 0;

		protected override void Start ()
		{
			base.Start ();
			originalLayer = gameObject.layer;
		}
		
		protected virtual void OnMouseDown()
		{
			distance = Vector3.Dot(transform.position - Camera.main.transform.position, Camera.main.transform.forward);
			gameObject.layer = 8;
		}
		
		protected virtual void OnMouseDrag()
		{
			Vector3 direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
			transform.position = Camera.main.transform.position + direction * distance / Vector3.Dot(Camera.main.transform.forward, direction);
		}

		protected override void OnMouseUp ()
		{
			base.OnMouseUp ();
			gameObject.layer = originalLayer;
		}

	}
}
