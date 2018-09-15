using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boar : MonoBehaviour {

	public int hp;
	public int myDamage;
	public float speed;
	public int rand;
	public float boarTime;
	public float boarTimeAttack;
	public SpriteRenderer sprite;
	public bool attacking;
	//public Animator boarAnim;
	public GameObject player;
	public GameObject[] Drops;
	
	// Use this for initialization
	void Start () {
		rand = Random.Range(1,4);
		speed =2f;
		myDamage = 20;
		hp = 100;
		attacking = false;
	}
	
	// Update is called once per frame
	void Update () {
		boarTime += Time.deltaTime;
		boarTimeAttack += Time.deltaTime;
		
		if(boarTime > 1){
			rand = Random.Range(0,5);
			boarTime = 0;
		}
		
		if(!attacking){
			move(rand);
		}		

		if(hp <= 0){
			int r = Random.Range (1, 3);
			if (r == 2) {
				Instantiate (Drops [0], new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);		
			}
			
			gameObject.SetActive (false);	
		}
	}
	
	public void move(int dir){		
		switch(dir){
			case 1 :
				transform.Translate (Vector2.left * speed * Time.deltaTime);
				sprite.flipX = true;
			break;
			case 2 :
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				
				sprite.flipX = false;	
			
			break;
			case 3 :
				transform.Translate (Vector2.up * speed * Time.deltaTime);
			
			break;
			case 4 :
				transform.Translate (Vector2.down * speed * Time.deltaTime);
			
			break;
			default :
				//transform.Translate (Vector2.down * speed * Time.deltaTime);
			break;
		}		
	}

	public void damage(int d){
		hp = hp - d;
	}
	
	void OnCollisionStay2D(Collision2D col){		
		if (col.gameObject.tag == "Player") {
			print("atacar o  player!");
			//boarAnim.Play ("attack");
			attacking = true;
			if(boarTimeAttack > 2){				
				player.SendMessage ("boarDamage",myDamage);				
				boarTimeAttack = 0;
			}			
		}		
	}
	
	void OnCollisionExit2D(Collision2D col){
		attacking = false;
	}
	
}
