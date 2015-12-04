using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public Color color;
	public Texture2D texture;

	void Start()
	{
		Screen.showCursor = false;
		color = Color.white;
	}

	void Update()
	{
		transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	}

	void OnGUI()
	{
		GUI.depth = 0;
		GUI.color = color;
		GUI.DrawTexture(new Rect(Input.mousePosition.x - 16, Screen.height - Input.mousePosition.y - 16, 32, 32), texture);
	}

	void OnDestroy()
	{
		Screen.showCursor = true;
	}
}
