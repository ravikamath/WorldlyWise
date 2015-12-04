using UnityEngine;
using System.Collections;

public class AnimateDelayed : MonoBehaviour {
	public float time; // time to wait
	public string clip; // clip to play
	private float startTime;
	// Use this for initialization
	void Awake () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > time)
		{
			animation.Play(clip);
			Destroy(this);
		}
	}
}
