using UnityEngine;
using System.Collections;

public class HelpGUI : MonoBehaviour {

	public GUISkin skin;
	public GUISkin bulbSkin;
	public GUISkin gameSkin;
	public Texture2D bulbOn;
	int currentInfo = 0;
	public MonoBehaviour activateOnQuit;
	private string[] texts = 
	{"Welcome to Worldly Wise, a virtual world where you will learn more about the physical sciences.",
		@"To play the game use the WASD keys to move.
		W/S to move forward/back
		A/D to move strafe left/right.
		You have a 360 degree viewing. Click the middle mouse button and drag to rotate the view.

		Interactions in the game are also using the mouse.
		Interactive objects can interacted with by the mouse click and, in some case, using drag and drop technique.
		Always, when an object is interactive (at any point) the cursor color will change to green",
		@"Notifications about important events appear at the top left of the screen.
		The main menu is always accessible during the game by clicking the light icon on the top right.",

		"Use Teleportation to get from your home (laboratory) to the last point of save. Clicking on the cone " + 
		"structures (with polka dots) in the game will reassign the away target. The home target cannot be changed " + 
		"and will always point to the laboratory."
	};

	void OnGUI()
	{
			if(currentInfo == 2)
				GameGUI ();
			GUI.skin = gameSkin;
			GUI.Window(0, new Rect(Screen.width/2 - 320, Screen.height/2 - 240, 640, 480), ShowMessage, "");
	}

	void GameGUI()
	{
		GUI.skin = skin;
		GUI.TextField(new Rect(10,10,400,40), "Notifications");
		GUI.BeginGroup(new Rect(Screen.width - 235, 35, 200, 200));
		GUI.Button (new Rect(0,0, 200,40), "Main Menu");
		GUI.Button (new Rect(0,50, 200,40), "Controls");
		GUI.Button (new Rect(0,100, 200,40), "Help");
		GUI.EndGroup();
		GUI.skin = bulbSkin;
		GUI.Button (new Rect(Screen.width - 60, 10, 50, 100), bulbOn);
	}

	void ShowMessage(int id)
	{
		GUI.Label (new Rect(30,30, 580, 300), texts[currentInfo]);
		if(currentInfo > 0)
			if(GUI.Button (new Rect(110,350, 200,40), "BACK"))
				currentInfo--;
		if(currentInfo < texts.Length - 1)
			if(GUI.Button (new Rect(330,350, 200,40), "NEXT"))
				currentInfo++;
		if(GUI.Button (new Rect(220,410, 200,40), "OK"))
		{
			Destroy (this);
			if(activateOnQuit != null)
				activateOnQuit.enabled = true;
		}
	}
}
