using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	public GameObject p;

	public bool openInvetory;

	//public GameObject [] Slots;

	// Use this for initialization
	void Start () {
		p = GameObject.Find ("player");
		//print (p.Inventory);

	}
	
	// Update is called once per frame
	void Update () {
		//foreach (p.Item item in p.Inventory) {
		//	print (item);
		//}
	}

	public void Invetory(){
		
		if (openInvetory) {

			this.gameObject.SetActive (true);
			openInvetory = false;

		} else {
			this.gameObject.SetActive (false);
			openInvetory = true;
		}

	}
}
