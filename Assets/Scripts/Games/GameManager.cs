using UnityEngine;
using System.Collections;
using WorldlyWise.Inventory;

namespace WorldlyWise.Games
{
	public class GameManager : MonoBehaviour
	{
		protected delegate void Interactions(Clicker clicker);
		public virtual void Interact(Clicker clicker)
		{
		}

		public virtual void Initialize()
		{
		}
	}
}