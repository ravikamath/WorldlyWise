using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

	public float rotateSpeed = 22.5f;


	// Update is called once per frame
	void Update () {
		transform.Rotate(0,rotateSpeed * Time.deltaTime, 0);	
	}
}
