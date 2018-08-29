using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	public bool openInvetory;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

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
