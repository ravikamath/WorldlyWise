using UnityEngine;
using System.Collections;

public class Enabler : MonoBehaviour {

	public GameObject[] objects;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			foreach(GameObject obj in objects)
				obj.SetActive(true);
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
			foreach(GameObject obj in objects)
				obj.SetActive(false);
	}
}
