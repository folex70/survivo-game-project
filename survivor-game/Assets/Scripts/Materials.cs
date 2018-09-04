using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour {

	public string type;
	public int life = 3;
	public GameObject materialPrefab;
	public GameObject materialPrefab2;
	public float materialTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		materialTime += Time.deltaTime;
		if(life < 0){
			DropMaterial ();
			DesactiveMaterial ();
		}
		if(this.gameObject.tag == "tree"){
			if (materialTime > 20) {
				int r = Random.Range (1, 999);
				if(r == 10){
					Instantiate(materialPrefab2, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0), Quaternion.identity);	
				}
				materialTime = 0;
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
}
