using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaia : MonoBehaviour {

	public float gaiaTime;
	public int seasons; //1 spring; 2 summer;3 fall; 4 winter;
	public static int treeSeasons;
	public Sprite [] floorSprites; //0 - spring floor; 1 - summer floor; 2 - summer floor; 3 - fall floor ; 4 - winter floor1; 5 - winter floor2;

	// Use this for initialization
	void Start () {
		seasons = 1;
	}
	
	// Update is called once per frame
	void Update () {
		gaiaTime += Time.deltaTime;
		seasonControll ();
		treeSeasons = seasons;
	}

	public void changeFloor(int s){	
		
		switch (s) {
			
			case 1:
				//spring
				//ob.GetComponent<Sprite>().sprite = floorSprites [0];
				gameObject.GetComponent<SpriteRenderer>().sprite = floorSprites [0];
			break;

			case 2:
				//summer
				int r = Random.Range (1, 4); 
				if (r == 1) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = floorSprites [1];	
				}
				else if (r == 2) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = floorSprites [0];	
				}
				else {
					gameObject.GetComponent<SpriteRenderer>().sprite = floorSprites [2];
				}
			break;

			case 3:
				//fall
				gameObject.GetComponent<SpriteRenderer>().sprite = floorSprites [3];
				break;

			case 4:
				//winter
				int r2 = Random.Range (1, 3); 
				if (r2 == 1) {
					gameObject.GetComponent<SpriteRenderer> ().sprite = floorSprites [4];	
				} else {
					gameObject.GetComponent<SpriteRenderer> ().sprite = floorSprites [5];
				}
			break;
		}
	}

	public void seasonControll(){
		//if (gaiaTime > 6000) {
		if (gaiaTime>100) {
			print ("troca estação");
			seasons ++;

			if (seasons > 4) {
				seasons = 1;
			}
			changeFloor (seasons);
			gaiaTime = 0;
		}		
	}

}
