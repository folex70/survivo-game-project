using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour {

	public string type;
	public int life = 3;
	public GameObject materialPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(life < 0){
			DropMaterial ();
			DesactiveMaterial ();
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
