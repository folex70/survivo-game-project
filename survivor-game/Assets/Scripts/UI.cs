using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	public bool openInvetory;
	public bool openCreateMenu;

	public GameObject createMenu;

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
			createMenu.SetActive (false);
			openCreateMenu = true;
			openInvetory = true;
		}

	}

	public void Create(){

		if (openCreateMenu) {

			createMenu.SetActive (true);
			openCreateMenu = false;

		} else {
			createMenu.SetActive (false);
			openCreateMenu = true;
		}

	}
}
