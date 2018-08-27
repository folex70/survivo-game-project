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
	public int chop;
	public List<Item> Inventory = new List<Item> ();
	
	// Use this for initialization
	void Start (){
		life = 100;
		food = 100;
		foodTime = 0;
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
		} else if (col.gameObject.tag == "fruit") {
			//pick fruit
			ableToPick = true;
		} else {
			ableToPick = true;
		}

		if(Input.GetButtonDown("Fire1")){
			print("pick item or action");

			if (ableToChopTree) {
				print ("chopping tree");
				chop = 1;
			}
			if(ableToPick){
				print ("pick something");
				print ("inventory"+ Inventory.Count);
				if (Inventory.Count < 9) {
					Inventory.Add (new Item (Inventory.Count + 1, col.gameObject.tag));	
					col.gameObject.SendMessage ("DesactiveMaterial");
				} else {
					print ("inventory full");
				}
			}
		}

	}

	void OnCollisionExit2D(Collision2D col){
		print (col.gameObject.tag);
		ableToChopTree = false;
		ableToPick = false;
	}

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
