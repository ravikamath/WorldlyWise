using UnityEngine;
using System.Collections;

public class InstructionsGUI : MonoBehaviour {

	public GUISkin skin;
	int currentInfo = 0;
	public MonoBehaviour activateOnQuit;
	private string[] texts = 
	{@"In Worldly Wise, you play the role of a young apprentice. You seek the one who knows everything,
		and he is somewhere in the realm. But you need to prove your worth first by solving the
		puzzles. Thankfully, he has left you a laboratory (where you start) and you can conduct
		experiments to learn concepts (not implemented)."
	};

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Window(0, new Rect(Screen.width/2 - 320, Screen.height/2 - 240, 640, 480), ShowMessage, "");
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
