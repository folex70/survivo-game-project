using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campFire : MonoBehaviour {

	public float fireTime;
	public Animator animFire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		fireTime += Time.deltaTime;
		if (fireTime > 30) {
			animFire.Play ("stopFire");
			//print (" apagar essa fogueira");
			Destroy (this.gameObject, 7f);
		}	
	}
}
