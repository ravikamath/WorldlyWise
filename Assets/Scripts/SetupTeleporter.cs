using UnityEngine;
using System.Collections;
using WorldlyWise.Inventory;

public class SetupTeleporter : MonoBehaviour {

	public GameObject home;
	public Texture2D homeIcon;
	public Texture2D awayIcon;

	// Use this for initialization
	void Update () {
		GameObject homeObj = new GameObject();
		TeleportItem item = homeObj.AddComponent<TeleportItem>();
		item.inventoryIcon = homeIcon;
		item.target = home.transform;

		GameObject awayObj = new GameObject();
		item = awayObj.AddComponent<TeleportItem>();
		awayObj.name = "Away";
		item.inventoryIcon = awayIcon;
		item.target = home.transform;

		Destroy (this);
	}
}
