using UnityEngine;
using System.Collections;

using WorldlyWise.Inventory;

public class UpdateTeleporter : MonoBehaviour {

	void OnMouseUp()
	{
		GameObject.Find("Away").GetComponent<TeleportItem>().target = transform;
	}
}
