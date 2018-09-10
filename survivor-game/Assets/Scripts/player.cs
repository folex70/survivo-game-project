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
	public int playerDamage;
	public string status; //starved - loose 2 hp per 10 seconds; freeze - loose 1 hp per 2 seconds, stay close to fire; normal; good - cures 1 hp per 10 seconds
	public SpriteRenderer mySprite;
	//-------------------------------
	public float statusTime; 
	public float foodTime; 
	public bool ableToChopTree;
	public bool ableToMine;
	public bool ableToPick;
	public bool ableToOpenTentMenu;
	public bool ableToOpenCampFireMenu;
	public bool ableToFishing;
	public bool ableToAttack;
	public bool usedTent;
	public bool nextAFire;
	public bool poisoned;
	//-------------------------------
	public GameObject UIManager;
	//-------------------------------
	public int selectedItem;
	//-------------------------------
	public int chop;
	public List<Item> Inventory = new List<Item> ();
	public GameObject [] Slots;
	public Sprite [] Sprites; //0 - fruit 1 - wood 2 - woodpile 3 - fish 4 - string 5 - rod 6 - grass
	//------------------------------- 
	//materials for create
	public GameObject [] Prefabs;//woodPilePrefab
	//-------------------------------
	// Use this for initialization
	void Start (){
		life = 100;
		food = 100;
		playerDamage = 5;
		foodTime = 0;
		usedTent = false;
		nextAFire = false;
		base.Start();
		status = "good";
	}
	
	// Update is called once per frame
	protected override void Update (){
		GetInput();
		base.Update(); //load superclass update method
		foodTime += Time.deltaTime;
		statusTime += Time.deltaTime;
		//-------------------------------------------------------
		//status bar update
		barLife.size = life/100;
		barFood.size = food/100;
		//-------------------------------------------------------
		//hungry count
		if(foodTime > 10){
			if(food > 0){
				food -= 5;
				status = "normal";
			}
			foodTime = 0;
			//if no food, decreaces health per time
			if(food < 1){
				status = "starved";
				//life -= 2;
				//if life = 0 {gameOver();}
			}
			//if food > 80% cure player
			else if(food > 80 && life < 100){
				status = "good";
				//life += 1;
			}
		}
		//-------------------------------------------------------
		mySprite.color = new Color(1,1,1,1);
		//-------------------------------------------------------
		//freeze check
		if(Gaia.treeSeasons == 4 && !nextAFire){
			mySprite.color = new Color(0.4392157f,0.7843137f,1f,1f);
			status = "freeze";
		}
		//poison check
		if(poisoned){
			mySprite.color = new Color(16,0,16);
			status = "poison";
		}
		//-------------------------------------------------------
		if (statusTime > 10){
			statusCheck(status);
			statusTime = 0;
		}
		//-------------------------------------------------------
		LoadInventory();
	}
	
	public void statusCheck(string s){
		print (s);
		switch (s) {

			case "good":
				life = life + 1; 
			break;
			case "freeze":
				life = life - 10;
			break;

			case "normal":

			break;
			
			case "dead":
				//call game over
			break;

			case "starved":
				life = life - 2;
			break;		

			case "poison":
				life = life - 5;
			break;				
		}
	}

	public void LoadInventory(){
		//load inventory
		foreach (Item slot in Inventory) {
			//para não sobrepor imagens
			print (Inventory.IndexOf(slot));
			print("itens no inventorio: "+slot.name+""+slot.idItem);
			if (slot.name == "fruit") {
				//Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [0];
				Slots [Inventory.IndexOf(slot)].GetComponent<Image> ().sprite = Sprites [0];
			} else if (slot.name == "wood_pile" || slot.name == "woodPile") {
				//Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [2];
				Slots [Inventory.IndexOf(slot)].GetComponent<Image> ().sprite = Sprites [2];
			} else if (slot.name == "wood") {
				//Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [1];
				Slots [Inventory.IndexOf(slot)].GetComponent<Image> ().sprite = Sprites [1];
			}
			else if (slot.name == "fish") {
				//Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [3];
				Slots [Inventory.IndexOf(slot)].GetComponent<Image> ().sprite = Sprites [3];
			}
			else if (slot.name == "string") {
				//Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [4];
				Slots [Inventory.IndexOf(slot)].GetComponent<Image> ().sprite = Sprites [4];
			}
			else if (slot.name == "rod") {
				//Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [5];
				Slots [Inventory.IndexOf(slot)].GetComponent<Image> ().sprite = Sprites [5];
			}
			else if (slot.name == "grass") {
				//Slots [Inventory.IndexOf(slot)+1].GetComponent<Image> ().sprite = Sprites [6];
				Slots [Inventory.IndexOf(slot)].GetComponent<Image> ().sprite = Sprites [6];
			} 
		}		
	}

	public void ClearInventory(){
		for (int i = 0; i <= 8; i++)
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
			ableToFishing = false;
			ableToAttack = false;
		} else if (col.gameObject.tag == "fruit" || col.gameObject.tag == "wood" || col.gameObject.tag == "woodPile" ||
		           col.gameObject.tag == "string" || col.gameObject.tag == "grass" || col.gameObject.tag == "rod" || col.gameObject.tag == "fish") {
			//pick fruit
			ableToPick = true;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = false;
			ableToFishing = false;
			ableToAttack = false;
		} else if (col.gameObject.tag == "tent") {
			ableToPick = false;
			ableToOpenTentMenu = true;
			ableToOpenCampFireMenu = false;
			ableToFishing = false;
			ableToAttack = false;
			if (usedTent) {
				col.gameObject.SetActive (false);
				usedTent = false;
				UIManager.SendMessage ("closeTent");
			}
		} else if (col.gameObject.tag == "campFire" || col.gameObject.tag == "campfire") {
			ableToPick = false;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = true;
			nextAFire = true;
			ableToFishing = false;
			ableToAttack = false;
		} else if (col.gameObject.tag == "water") {
			ableToPick = false;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = false;
			ableToFishing = true;
			ableToAttack = false;
		} else if (col.gameObject.tag == "snake") {
			ableToPick = false;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = false;
			ableToFishing = false;
			ableToAttack = true;
		}
		else {
			//@TODO corrigir essa parte, pois está pegando qq coisa!!
			ableToPick = false;
			ableToOpenTentMenu = false;
			ableToOpenCampFireMenu = false;
			ableToFishing = false;
			ableToAttack = false;
		}

		if(Input.GetButtonDown("Fire1")){
			if (ableToChopTree) {
				chop = 1;
			} 
			if (ableToPick) {
				if (Inventory.Count < 9) {
					//Inventory.Add (new Item (Inventory.Count + 1, col.gameObject.tag));	
					Inventory.Add (new Item (Inventory.Count, col.gameObject.tag));	
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
			if(ableToFishing){
				UIManager.SendMessage ("FishingMenu");
			}
			if(ableToAttack){
				col.gameObject.SendMessage ("damage",playerDamage);
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		//print (col.gameObject.tag);
		ableToChopTree = false;
		ableToPick = false;
		ableToOpenTentMenu = false;
		ableToOpenCampFireMenu = false;
		ableToFishing = false;
		nextAFire = false;
		UIManager.SendMessage ("closeTent");
		UIManager.SendMessage ("closeCampFire");
		UIManager.SendMessage ("closeFishingMenu");
	}

	//public void Create(string name, int prefabCode ,string material, int qtd){
	public void Create(string name){	
		string material = "";
		string material2 = "";
		int prefabCode = 0; //0 - wood Pile 1- tent 2 -campfire
		int qtd = 0;
		int qtd2 = 0;
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
		} 
		else if(name == "fish") {
			material = "rod";
			prefabCode = 3;
			qtd = 1;
		}
		else if(name == "string") {
			material = "grass";
			prefabCode = 4;
			qtd = 3;
		}
		else if(name == "rod") {
			material  = "wood";
			material2 = "string";
			prefabCode = 5;
			qtd  = 1;
			qtd2 = 1;
		}
		else if(name == "cookedFish") {
			material = "fish";
			prefabCode = 6;
			qtd = 1;
		} 
		else if(name == "cookedMeat") {
			material = "meat";
			prefabCode = 7;
			qtd = 1;
		}
		//--------------------------------------------------------------------
		//count materials for create
		int countInInvetory = 0;		
		int countInInvetory2 = 0;		
		foreach (Item slot in Inventory.ToArray()){
			print (material);
			if(slot.name == material){
				countInInvetory++;
				print ("i have "+countInInvetory+" nubember of" +material);
			}
			print ("i have "+countInInvetory+" nubember of" +material);
		}
		if(qtd2 >0){
			foreach (Item slot in Inventory.ToArray()){
				if(slot.name == material2){
					countInInvetory2++;
				}
			}
		}
		//requeres "qtd" "material" for create a "name" (name of material)
		if (countInInvetory >= qtd && countInInvetory2 >= qtd2) {
			Instantiate (Prefabs [prefabCode], new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);	
			int countRemovals = 0;
			int countRemovals2 = 0;
			foreach (Item slot in Inventory.ToArray()) {
				if (slot.name == material) {
					//Slots [slot.idItem].GetComponent<Image> ().sprite = null;
					Inventory.Remove (slot); 
					countRemovals++;
					if (countRemovals >= qtd) {
						ClearInventory ();
						break;
					}
				}
			}
			foreach (Item slot in Inventory.ToArray()) {
				if (slot.name == material2) {
					//Slots [slot.idItem].GetComponent<Image> ().sprite = null;
					Inventory.Remove (slot); 
					countRemovals2++;
					if (countRemovals2 >= qtd) {
						ClearInventory ();
						break;
					}
				}
			}				
		} else {
			print("Not enough material. Needs: "+qtd+" "+material+" and "+qtd2+" material "+material2);
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
	public void snakeDamage(){
		life = life - 10;
		int randPoison = UnityEngine.Random.Range (1,10);
		if(randPoison == 5){
			poisoned = true;
		}
	}
	//--------------------------------------------------------------------
	public void UseItem(int selectedItem){
		foreach (Item slot in Inventory.ToArray()){
			//if (Inventory.IndexOf (slot) + 1 == selectedItem){
			if (Inventory.IndexOf (slot) == selectedItem){
				print ("selecionei usar o item" + selectedItem + " econtrei o item" + Inventory [Inventory.IndexOf (slot)].name+" item de index "+Inventory.IndexOf (slot));
				if(Inventory [Inventory.IndexOf (slot)].name == "fruit"){
					eat (8);
					Inventory.Remove (slot); 
				}else if(Inventory [Inventory.IndexOf (slot)].name == "fish"){
					eat (15);
					Inventory.Remove (slot); 
				}
				else if(Inventory [Inventory.IndexOf (slot)].name == "meat"){
					eat (25);
					Inventory.Remove (slot); 
				}
				else if(Inventory [Inventory.IndexOf (slot)].name == "cookedFish"){
					eat (50);
					Inventory.Remove (slot); 
				}
				else if(Inventory [Inventory.IndexOf (slot)].name == "cookedMeat"){
					eat (75);
					Inventory.Remove (slot); 
				}
				else if(Inventory [Inventory.IndexOf (slot)].name == "antidote"){
					poisoned = false;
				}		
				ClearInventory ();
				break;
			} else {
				print ("nenhum item aqui");
			}
		}
		ClearInventory ();
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
