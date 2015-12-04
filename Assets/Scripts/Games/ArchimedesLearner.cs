using UnityEngine;
using System.Collections.Generic;
using WorldlyWise.Inventory;

namespace WorldlyWise.Games
{
	public class ArchimedesLearner : Learner
	{
		public Shard shard;
		public GameObject beaker;
		public int currentVolume;
		public Transform shardPosition;
		public LearnedItem learnedItem;
		private Shard[] shards;
		List<Shard> selectedShards;

		public AudioClip successClip;

		public void Start()
		{
			MakeGrid();
			for(int i = 0; i < 9; i++)
				shards[i].volume = Random.Range(1,3);
			selectedShards = new List<Shard>();
		}

		public override void Interact (Clicker clicker)
		{
			base.Interact (clicker);
			if(clicker as Shard)
			{
				RaycastHit info;
				beaker.collider.enabled = true;
				if(beaker.collider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info, Camera.main.farClipPlane))
				{
					if(!selectedShards.Contains(clicker as Shard))
						selectedShards.Add (clicker as Shard);
					clicker.transform.position = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(info.distance+0.14f);
					if(!learned)
					{
						LearnedItem item = Instantiate(learnedItem) as LearnedItem;
						item.message =  
							"Eureka!\n\nYou just learned Archimedes' principle for displacement of volume.\n\n" + 
							"Archimedes was a Greek philosophist who lived during the third century BCE. Perhaps he is best known " +
							"for proposing a method for computing the volume of arbitrarily shaped bodies through displacement. In this " +
								"experiment, observe how the water level changes as you put more items into the flask.";
						GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>().Interact(item);
						learned = true;
						AudioSource.PlayClipAtPoint(successClip,beaker.transform.position);
					}

				}
				else
				{
					selectedShards.Remove(clicker as Shard);
					MoveToReset component = clicker.gameObject.AddComponent<MoveToReset>();
					component.end = clicker.startPosition;
					component.start = clicker.transform.position;
					component.time = 0.5f;
				}
				beaker.collider.enabled = false;
				currentVolume = 0;
				foreach(Shard shard in selectedShards)
					currentVolume += shard.volume;
				beaker.transform.Find("Water").localScale = new Vector3 (1,1, 1+currentVolume/20f);
			}
		}

		void MakeGrid() {
			shards = new Shard[9];
			for(int i = 0; i < 3; i = i+1)
				for(int j = 0; j < 3; j = j + 1)
				{
					shards[i*3+j] = Instantiate(shard, shardPosition.position + new Vector3(i*0.2f,0, j*0.2f), shard.transform.rotation) as Shard;
					shards[i*3+j].manager = this;
				}
		}

	}
}