using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	public GUISkin skin;
	public GUISkin gameSkin;
	public GUISkin bulbSkin;
	public Texture2D bulb;

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.BeginGroup(new Rect(50, Screen.height/2 - 100, 200, 200));
		if(GUI.Button (new Rect(0,0, 200,40), "Play"))
			Application.LoadLevel("Game");
		if(GUI.Button (new Rect(0,50, 200,40), "Instructions"))
		{
			InstructionsGUI help = gameObject.AddComponent<InstructionsGUI>();
			help.skin = gameSkin;
			help.activateOnQuit = this;
			enabled = false;
		}
		if(GUI.Button (new Rect(0,100, 200,40), "Help"))
		{
			HelpGUI help = gameObject.AddComponent<HelpGUI>();
			help.skin = skin;
			help.gameSkin = gameSkin;
			help.bulbSkin = bulbSkin;
			help.activateOnQuit = this;
			help.bulbOn = bulb;
			enabled = false;
		}
		if(Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
		   if(GUI.Button (new Rect(0,150, 200,40), "Quit"))
				Application.Quit();
		GUI.EndGroup();
		GUI.DrawTexture(new Rect(Screen.width*0.5f, Screen.height * 0.25f, Screen.height * bulb.width * 0.5f / bulb.height, Screen.height * 0.5f), bulb);
	}
}
