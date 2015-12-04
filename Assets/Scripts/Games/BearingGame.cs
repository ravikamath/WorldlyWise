using UnityEngine;
using System.Collections.Generic;
using WorldlyWise.Inventory;

namespace WorldlyWise.Games
{
	public class BearingGame : GameManager
	{
		public Shard shard;
		public GameClicker furnace;
		public GameObject beaker;
		public int correctVolume;
		public int currentVolume;
		public Transform shardPosition;
		private Shard[] shards;
		List<Shard> selectedShards;
		public GameManager parentManager;
		public Item ballBearing;
		public AudioClip wrongClip;

		public override void Initialize ()
		{
			MakeGrid();
			correctVolume = Random.Range(10, 15);
			// distribute the volume across the shards
			for(int tempVolume = correctVolume; tempVolume > 0;  tempVolume--)
				shards[Random.Range(0,6)].volume++;
			for(int i = 6; i < 16; i++)
				shards[i].volume = Random.Range(1,5);
			for(int i = 0; i < 16; i++)
			{
				int x = Random.Range(0,16);
				int y = Random.Range(0,16);
				int swap = shards[x].volume;
				shards[x].volume = shards[y].volume;
				shards[y].volume = swap;
			}
			selectedShards = new List<Shard>();
			beaker.transform.Find("Beaker").Find("Wall").renderer.materials[1].mainTextureScale = new Vector2(1.0f - correctVolume/40f, 10);
			furnace.manager = this;
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
				beaker.transform.Find("Water").localScale = new Vector3 (1,1, Mathf.Min (1+currentVolume/20f, 2));
			}
			else if (clicker == furnace)
			{
				if(currentVolume == correctVolume)
				{
					Item ball = Instantiate(ballBearing, Vector3.zero, Quaternion.identity) as Item;
					ball.name = "Bearing";
					parentManager.Interact(ball);
					furnace.manager = null;
					foreach(Shard shard in shards)
						Destroy (shard.gameObject);
					Destroy (this);
				}
				else
				{
					GameObject.FindGameObjectWithTag("GameController").GetComponent<GameGUI>().message = "Not the correct volume. Try again.";
					AudioSource.PlayClipAtPoint(wrongClip, beaker.transform.position);
				}
			}
		}

		void MakeGrid() {
			shards = new Shard[16];
			for(int i = 0; i < 4; i = i+1)
				for(int j = 0; j < 4; j = j + 1)
				{
					shards[i*4+j] = Instantiate(shard, shardPosition.position + new Vector3(i*0.2f,0, j*0.2f), shard.transform.rotation) as Shard;
					shards[i*4+j].manager = this;
					shards[i*4+j].name = "Metal Shard";
				}
		}

	}
}