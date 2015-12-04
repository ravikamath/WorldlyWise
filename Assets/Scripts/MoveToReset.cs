using UnityEngine;
using System.Collections;

public class MoveToReset : MonoBehaviour {

	public Vector3 start;
	public Vector3 end;
	public float time = 1;

	private float startTime;

	void Start()
	{
		startTime = Time.time;
		if(rigidbody != null)
			rigidbody.useGravity = false;
	}


	void FixedUpdate()
	{
		float t = Mathf.Min ((Time.time - startTime) / time, 1);
		float cosT = (Mathf.Cos (t*Mathf.PI) + 1) / 2;
		transform.position = start * cosT + end * (1-cosT);
		if (t >= 1)
		{
			Destroy (this);
			if(rigidbody != null)
				rigidbody.useGravity = true;
		}

	}
}
