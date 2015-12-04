using UnityEngine;
using System.Collections;

namespace WorldlyWise.Inventory
{
	public class Shard : GameClickerDragDrop
	{
		public int _volume;
		public int volume
		{
			get {return _volume; }
			set 
			{
				_volume = value;
				int x = Mathf.Max(1, Random.Range(_volume / 3, (_volume * 2) / 3));
				transform.localScale = new Vector3(0.1f * x, 0.03f, 0.05f * Mathf.Max (_volume / x, 1));
			}
		}

		protected override void OnMouseDown ()
		{
			base.OnMouseDown ();
			rigidbody.useGravity = false;
		}

		protected override void OnMouseUp ()
		{
			base.OnMouseUp ();
			rigidbody.useGravity = true;
		}
	}
}