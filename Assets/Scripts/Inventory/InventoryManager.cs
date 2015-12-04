using UnityEngine;
using System.Collections.Generic;

namespace WorldlyWise.Inventory
{
	public class InventoryManager : MonoBehaviour 
	{
		public GUISkin skin;
		public int itemSize = 64;
		private int page = 0;
		public bool showInventory = false;
		public Cursor cursor;
		private float animationSpeed = 0;
		private float currentStep;
		private List<Interactor> items;

		private Interactor _selectedItem;
		public Interactor selectedItem
		{
			get { return _selectedItem;}
			set
			{
				if(_selectedItem != null && _selectedItem.worldIcon != null)
					_selectedItem.worldIcon.SetActive(false);
				if((_selectedItem = value) != null && _selectedItem.worldIcon != null)
					_selectedItem.worldIcon.SetActive(true);
			}
		}

		// Use this for initialization
		void Start ()
		{
			items = new List<Interactor>();
			selectedItem = null;
			if(cursor == null)
				GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
		}
		
		void Update()
		{
			if(selectedItem != null)
				if(selectedItem as Item)
				{
					selectedItem.worldIcon.transform.position =
						Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(1);
					if(Input.GetMouseButtonDown(0))
						UseItem(selectedItem);
					else if(Input.GetMouseButtonDown(1))
						selectedItem = null;
				}
				else
					UseItemImmediate(selectedItem);

			currentStep = Mathf.Clamp(currentStep + Time.deltaTime * animationSpeed, 0, 1);
			if(currentStep < 0.001f) { showInventory = false; animationSpeed = 0;}
		}
		
		void OnGUI()
		{
			GUI.skin = skin;
			int itemsPerPage = Screen.width / itemSize - 1;
			GUI.BeginGroup(new Rect(0,Screen.height - itemSize * currentStep - 16,Screen.width, itemSize * currentStep + 16));
			if(GUI.Button (new Rect(0,0, Screen.width, 16), showInventory ? "HIDE" : "SHOW"))
			{
				if(showInventory)
					animationSpeed = -1;
				else
				{
					showInventory = true;
					animationSpeed = 1;
				}
			}
			if(showInventory)
			{
				float offset = (Screen.width - itemsPerPage * itemSize)/2;
				if(GUI.Button (new Rect(0,16,offset,itemSize), "<"))
					page = Mathf.Max(0, page - 1);
				for(int i = 0; i < itemsPerPage; i++)
				{
					if(page * itemsPerPage + i < items.Count)
					{
						if(GUI.Button (new Rect(offset + i * itemSize, 16, itemSize, itemSize), items[page * itemsPerPage + i].inventoryIcon))
							selectedItem = items[page * itemsPerPage + i];
					}
					else
						GUI.Button (new Rect(offset + i * itemSize, 16, itemSize, itemSize), "");
				}
				if(GUI.Button (new Rect(Screen.width - offset,16, offset,itemSize), ">"))
					page = Mathf.Min(items.Count / itemsPerPage, page + 1);
			}
			GUI.EndGroup();
		}
		
		public void Interact(Interactor item)
		{
			if(items.Contains(item))
				return;
			items.Add (item);
			if(item.worldIcon == null && item.gameObject.renderer != null)
			{
				item.worldIcon = item.gameObject;
				item.worldIcon.transform.localScale *= 0.2f;
			}
			if(item.worldIcon != null)
			{
				item.worldIcon.SetActive(false);
				item.worldIcon.layer = 8;
			}
			animationSpeed = 1;
			showInventory = true;
		}
		
		public void UseItem(Interactor item)
		{
			if(item.Use ())
			{
				items.Remove (item);
				selectedItem = null;
			}
		}

		public void UseItemImmediate(Interactor item)
		{
			if(item.Use ())
				items.Remove (item);
			selectedItem = null;
		}
	}
}