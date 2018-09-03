using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	public bool openInvetory;
	public bool openCreateMenu;
	public bool openTentMenu;
	public bool openCampFireMenu;

	public GameObject InvetoryMenu;
	public GameObject createMenu;
	public GameObject tentMenu;
	public GameObject campFireMenu;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Invetory(){
		
		if (openInvetory) {

			InvetoryMenu.gameObject.SetActive (true);
			openInvetory = false;

		} else {
			InvetoryMenu.gameObject.SetActive (false);
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
	
	public void Tent(){
	
		if (openTentMenu) {

			tentMenu.SetActive (true);
			openTentMenu = false;

		} 
		//else {
		//	tentMenu.SetActive (false);
		//	openTentMenu = true;
		//}

	}
	
	public void closeTent(){
			tentMenu.SetActive (false);
			openTentMenu = true;
		
	}
	
	public void CampFire(){

		if (openCampFireMenu) {

			campFireMenu.SetActive (true);
			openCampFireMenu = false;

		} 
	}

	public void closeCampFire(){
		campFireMenu.SetActive (false);
		openCampFireMenu = true;
	}
	
	//fazer um menu generico que alimenta um frame apenas
}
