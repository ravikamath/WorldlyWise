using UnityEngine;
using System.Collections;
using WorldlyWise.Games;

public class Initiator : MonoBehaviour {

	public GameManager manager;
	public Learner[] learners;

	void OnTriggerEnter(Collider other)
	{
		foreach(Learner learner in learners)
			if(!learner.learned)
				return;
		manager.Initialize();
		Destroy (gameObject);
	}
}
