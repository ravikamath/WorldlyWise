using UnityEngine;
using System.Collections;

namespace WorldlyWise.Inventory
{
	public class TeleportItem : Interactor
	{
		private Transform _target;
		public Transform target
		{
			get{ return _target; }
			set
			{
				if((_target = value) != null)
					OnMouseUp();
			}
		}
		
		public override bool Use()
		{
			if(manager != null)
				manager.Interact(this);
			if(target != null)
				GameObject.FindGameObjectWithTag("Player").transform.position = target.position + Vector3.one + Vector3.up*2;
			return false;
		}
	}
}
