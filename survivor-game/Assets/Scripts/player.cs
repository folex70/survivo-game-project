using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : character {
	
	public Scrollbar barLife;
	public Scrollbar barFood;
	public float life;
	public float food;
	public float globalTime; 
	public float foodTime; 
	
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
		if(Input.GetKey(KeyCode.E)){
			print("pick item or action");
		}
		
	}
}
