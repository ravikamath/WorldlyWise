using UnityEngine;
using System.Collections;
using WorldlyWise.Games;

namespace WorldlyWise.Inventory
{
	public class Clicker : MonoBehaviour 
	{
		public Vector3 startPosition { get; set; }
		public Quaternion startRotation { get; set; }
		public string information;
		public GameManager manager;
		
		protected virtual void Start()
		{
			// Remember the starting position and orientation
			startPosition = transform.position;
			startRotation = transform.rotation;
			if(information == null) information = name;
		}
		
		protected virtual void OnMouseUp()
		{
			// OnMouseExit();
		}

		void OnMouseEnter()
		{
			GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>().color = Color.green;
			if(information != null & information.Length > 0)
				GameObject.FindGameObjectWithTag("GameController").GetComponent<GameGUI>().notification = information;
		}
		
		void OnMouseExit()
		{
			GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>().color = Color.white;
		}
	}
}