using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour {

	public string type;
	public int life = 3;
	public GameObject materialPrefab;
	public GameObject materialPrefab2;
	public float materialTime;
	public float seasonChangeTime;
	public int r;
	public Sprite [] treeSprite; //0 - spring and summer tree; 1 - fall tree; 2 - winter tree; 3 - winter tree2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		materialTime += Time.deltaTime;
		seasonChangeTime += Time.deltaTime;
		if (life < 0) {
			DropMaterial ();
			DesactiveMaterial ();
		} else {
			if(this.gameObject.tag == "tree"){
				treeBehavior ();
			}
		}
	}

	void DropMaterial (){
		//quando a arvore for destruida dropa madeira
		if(materialPrefab){
			Instantiate(materialPrefab, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0), Quaternion.identity);	
		}
	}

	public void DamageMaterial(){
		life = life - 1;
	}

	public void DesactiveMaterial(){
		gameObject.SetActive (false);
	}

	public void treeBehavior(){
		//spawn fruits
		if (materialTime > 20) {
			
			if(Gaia.treeSeasons == 1 || Gaia.treeSeasons == 2){
				r = Random.Range (1, 999);
				if(r == 10){
					Instantiate(materialPrefab2, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0), Quaternion.identity);	
				}
			}


			materialTime = 0;
		}
		if (seasonChangeTime > 300) {
			changeTreeSprite (Gaia.treeSeasons);
			seasonChangeTime = 0;
		}
	}

	public void changeTreeSprite(int s){	

		switch (s) {

		case 1:
		case 2:
			//spring & summer
			gameObject.GetComponent<SpriteRenderer>().sprite = treeSprite [0];
		break;

		case 3:
			//fall
			int r = Random.Range (1, 3); 
			if (r == 1) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = treeSprite [0];	
			} else {
				gameObject.GetComponent<SpriteRenderer> ().sprite = treeSprite [1];
			}
			break;

		case 4:
			//winter
			int r2 = Random.Range (1, 3); 
			if (r2 == 1) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = treeSprite [2];	
			} else {
				gameObject.GetComponent<SpriteRenderer> ().sprite = treeSprite [3];
			}
			break;
		}
	}
}
