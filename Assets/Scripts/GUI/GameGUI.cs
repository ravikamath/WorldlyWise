using UnityEngine;
using System.Collections.Generic;

public class GameGUI : MonoBehaviour {

	public GUISkin skin;
	public GUISkin bulbSkin;
	public GUISkin gameSkin;
	public Texture2D bulbOff;
	public Texture2D bulbOn;
	List<MonoBehaviour> interactiveComponents;

	private string _notification;
	private float notificationTimer = 0;
	public string notification
	{
		get { return _notification; }
		set
		{ 
			_notification = value;
			notificationTimer = 2;
		}
	}
	public bool isMenuVisible = false;

	private string _message;
	public string message
	{
		get { return _message;}
		set
		{
			if((_message = value) != null)
			{
				foreach(MonoBehaviour component in interactiveComponents)
					component.enabled = false;
			}
			else
				foreach(MonoBehaviour component in interactiveComponents)
					component.enabled = true;
		}
	}

	void Start()
	{
		interactiveComponents = new List<MonoBehaviour>();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		interactiveComponents.Add (player.GetComponent<MouseLookRestricted>());
		interactiveComponents.Add (player.GetComponent("CharacterMotor") as MonoBehaviour);
		interactiveComponents.Add(player.transform.Find("Main Camera").gameObject.GetComponent<MouseLookRestricted>());

		message = "Welcome, my young paduwan. Right now you are standing in your laboratory. Look at it carefully - for you will " +
			"spend much of your time learning here. You need to discover the concepts, and I am not going to guide you. When you " +
			"learn something new, you will know.";
	}

	void Update()
	{
		if (notificationTimer > 0)
			notificationTimer = Mathf.Max (0, notificationTimer - Time.deltaTime);
	}

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.color = Color.white;
		if(isMenuVisible)
		{
			GUI.BeginGroup(new Rect(Screen.width - 235, 35, 200, 200));
			if(GUI.Button (new Rect(0,0, 200,40), "Main Menu"))
				Application.LoadLevel(0);
			if(GUI.Button (new Rect(0,50, 200,40), "Instructions"))
			{
				InstructionsGUI help = gameObject.AddComponent<InstructionsGUI>();
				help.skin = gameSkin;
				help.activateOnQuit = this;
				enabled = false;
			}
			if (GUI.Button (new Rect(0,100, 200,40), "Help"))
			{
				isMenuVisible = false;
				HelpGUI help = gameObject.AddComponent<HelpGUI>();
				help.skin = skin;
				help.bulbSkin = bulbSkin;
				help.gameSkin = gameSkin;
				help.bulbOn = bulbOn;
				help.activateOnQuit = this;
				enabled = false;
			}
			GUI.EndGroup();
		}
		GUI.skin = bulbSkin;
		if(GUI.Button (new Rect(Screen.width - 60, 10, 50, 100), isMenuVisible ? bulbOn : bulbOff))
			isMenuVisible = ! isMenuVisible;
		GUI.skin =gameSkin;
		if(message != null)
			GUI.Window(0, new Rect(Screen.width/2 - 320, Screen.height/2 - 240, 640, 480), ShowMessage, "");
		if(notificationTimer > 0)
		{
			GUI.color = new Color(1,1,1,notificationTimer < 1 ? notificationTimer : 1);
			GUI.TextField(new Rect(10,10,400,40), notification);
		}
	}

	private void ShowMessage(int id)
	{
		GUI.Label (new Rect(30,30, 580, 360), message);
		if(GUI.Button (new Rect(220,410, 200,40), "OK"))
			message = null;
	}
}
