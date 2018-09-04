using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : character {
	
	public Scrollbar barLife;
	public Scrollbar barFood;
	public float life;
	public float food;
	public float globalTime; 
	public float foodTime; 
	public bool ableToChopTree;
	public bool ableToMine;
	public bool ableToPick;
	public bool ableToOpenTentMenu;
	public bool ableToOpenCampFireMenu;
	public bool usedTent;
	//-------------------------------
	public GameObject UIManager;
	//-------------------------------
	public int selectedItem;
	//-------------------------------
	public int chop;
	public List<Item> Inventory = new List<Item> ();
	public GameObject [] Slots;
	public Sprite [] Sprites; //0 - fruit 1 - wood
	//------------------------------- 
	//materials for create
	public GameObject [] Prefabs;//woodPilePrefab
	
	// Use this for initialization
	void Start (){
		life = 100;
		food = 100;
		foodTime = 0;
		usedTent = false;
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update (){
		GetInput();
		base.Update(); //load superclass update method
		foodTime += Time.deltaTime;
		
		//status bar update
		barLife.size = life/100;
		barFood.size = food/100;
		
		//hungry count
		if(foodTime > 10){
			if(food > 0){
				food -= 5;
			}
			foodTime = 0;
			//if no food, decreaces health per time
			if(food < 1){
				life -= 2;
				//if life = 0 {gameOver();}
			}
			//if food > 80% cure player
			else if(food > 80 && life < 100){
				life += 1;
			}
		}
		//-------------------------------------------------------
		LoadInventory();
	}

	public void LoadInventory(){
		//load inventory
		foreach (Item slot in Inventory) {
			//para não sobrepor imagens
			Slots [slot.idItem].GetComponent<Image> ().sprite = null;
			print (Inventory.IndexOf(slot));
			print("itens no inventorio: "+slot.name+""+slot.idItem);
			if (slot.name == "fruit") {
				//Slots [slot.idItem].GetComponent<Image> ().sprite = Sprites [0];
				Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [0];
			} else if (slot.name == "wood_pile" || slot.name == "woodPile") {
				//Slots [slot.idItem].GetComponent<Image> ().sprite = Sprites [2];
				Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [2];
			} else if (slot.name == "wood") {
				//Slots [slot.idItem].GetComponent<Image> ().sprite = Sprites [1];
				Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [1];
			} else {
				print("empty nunca cai aqui");
				//Slots [slot.idItem].GetComponent<Image> ().sprite = null;
				Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = null;
			}						
		}		
	}

	public void ClearInventory(){
		for (int i = 0; i < 9; i++)
		{
			if (Slots [i] != null) {
				Slots[i].GetComponent<Image> ().sprite = null;
			}
		}
	}

	private void GetInput(){
		direction = Vector2.zero;
		
		if (Input.GetKey(KeyCode.W)){
			direction += Vector2.up;
		}
		if (Input.GetKey(KeyCode.A)){
			direction += Vector2.left;
		}
		if (Input.GetKey(KeyCode.S)){
			direction += Vector2.down;
		}
		if (Input.GetKey(KeyCode.D)){
			direction += Vector2.right;
		}
		//if (Input.GetKey(KeyCode.I)){
		//	UIManager.SendMessage ("Invetory");
		//}
		if(Input.GetButtonDown("i")){
			
			UIManager.SendMessage ("Invetory");	
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		print (col.gameObject.tag);
	}

	void OnCollisionStay2D(Collision2D col){
		
		if (col.gameObject.tag == "tree") {
			ableToChopTree = true;

			if (chop > 0) {
				chop = 0;
				col.gameObject.SendMessage ("DamageMaterial");
			}
			ableToPick = false;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = false;
		} else if (col.gameObject.tag == "fruit" || col.gameObject.tag =="wood" || col.gameObject.tag == "woodPile") {
			//pick fruit
			ableToPick = true;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = false;
		} else if (col.gameObject.tag == "tent"){
			ableToPick = false;
			ableToOpenTentMenu = true;
			ableToOpenCampFireMenu = false;
			if(usedTent){
					col.gameObject.SetActive (false);
					usedTent = false;
					UIManager.SendMessage ("closeTent");
			}
		}
		else if (col.gameObject.tag =="campFire" || col.gameObject.tag =="campfire"){
			ableToPick = false;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = true;
		}		
		else {
			//@TODO corrigir essa parte, pois está pegando qq coisa!!
			ableToPick = true;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = false;
		}

		if(Input.GetButtonDown("Fire1")){
			//print("pick item or action");
			if (ableToChopTree) {
				//print ("chopping tree");
				chop = 1;
			} 
			if (ableToPick) {
				//print ("pick something");
				//print ("inventory"+ Inventory.Count);
				//Inventory.IndexOf(slot)
				if (Inventory.Count < 9) {
					Inventory.Add (new Item (Inventory.Count + 1, col.gameObject.tag));	
					//Inventory.Sort ();
					col.gameObject.SendMessage ("DesactiveMaterial");
				} else {
					print ("inventory full");
				}
			} 
			if(ableToOpenTentMenu){
				UIManager.SendMessage ("Tent");				
			}
			if(ableToOpenCampFireMenu){
				UIManager.SendMessage ("CampFire");
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		//print (col.gameObject.tag);
		ableToChopTree = false;
		ableToPick = false;
		ableToOpenTentMenu = false;
		ableToOpenCampFireMenu = false;
		UIManager.SendMessage ("closeTent");
		UIManager.SendMessage ("closeCampFire");
	}

	//public void Create(string name, int prefabCode ,string material, int qtd){
	public void Create(string name){	
		string material = "";
		int prefabCode = 0; //0 - wood Pile 1- tent 2 -campfire
		int qtd = 0;

		if (name == "woodPile") {
			material = "wood";
			prefabCode = 0;
			qtd = 3;
		} else if (name == "tent") {
			material = "woodPile";
			prefabCode = 1;
			qtd = 2;
		} else if (name == "campFire") {
			material = "woodPile";
			prefabCode = 2;
			qtd = 1;
		} else if(name == "cookedFish") {
			material = "fish";
			prefabCode = 3;
			qtd = 1;
		} else if(name == "cookedMeat") {
			material = "meat";
			prefabCode = 4;
			qtd = 1;
		}
		//--------------------------------------------------------------------
		//count woods for create
		int countInInvetory = 0;		
		foreach (Item slot in Inventory.ToArray()) {
			print (material);
			if(slot.name == material){
				countInInvetory++;
				print ("i have "+countInInvetory+" nubember of" +material);
			}
			print ("i have "+countInInvetory+" nubember of" +material);
		}
			//requeres "qtd" "material" for create a woodPile
		if (countInInvetory >= qtd) {
			Instantiate (Prefabs [prefabCode], new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);	
			int countRemovals = 0;
			foreach (Item slot in Inventory.ToArray()) {
				if (slot.name == material) {
					Slots [slot.idItem].GetComponent<Image> ().sprite = null;
					Inventory.Remove (slot); 
					countRemovals++;
					if (countRemovals >= qtd) {
						ClearInventory ();
						break;
					}
				}
			}	
		} else {
			print("Not enough material. Needs: "+qtd+" "+material);
		}
		//}
	}
	//--------------------------------------------------------------------
	public void eat(int recover){
		print("eat something, restores hp: "+recover);
		food = food + recover;
		if(food > 100){food = 100;}	
	}
	//--------------------------------------------------------------------
	public void rest(int recover){
		print("recover "+recover);
		usedTent = true;
		life = life + recover;
		if(life > 100){life = 100;}		
	}
	//--------------------------------------------------------------------
	public void UseItem(int selectedItem){
		foreach (Item slot in Inventory.ToArray()) {
			if (Inventory.IndexOf (slot) + 1 == selectedItem){
				print ("selecionei usar o item" + selectedItem + " econtrei o item" + Inventory [Inventory.IndexOf (slot)].name);
				if(Inventory [Inventory.IndexOf (slot)].name == "fruit"){
					eat (10);
					Slots [slot.idItem].GetComponent<Image> ().sprite = null;
					Inventory.Remove (slot); 
				}
				break;
			} else {
				print ("nenhum item aqui");
			}
		}
	}
	//--------------------------------------------------------------------
}

public class Item: IComparable<Item>{
	public int 		idItem;
	public string 	name;

	public Item(int newidItem, string newName){
		idItem  = newidItem;
		name 	= newName;	
	}

	public int CompareTo(Item other){
		if (other == null) {
			return 1;
		}
		if(other.idItem > this.idItem){
			return other.idItem;
		}
			return this.idItem;
	}
}
