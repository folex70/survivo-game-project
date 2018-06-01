using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class character : MonoBehaviour {

	[SerializeField]
	private float speed;
	protected Vector2 direction;
	
	private Animator animator;

	// Use this for initialization
	protected virtual void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		Move();
		Animate(direction);
	}	
		
	public void Move(){
		transform.Translate(direction*speed*Time.deltaTime);
	}
	
	public void Animate(Vector2 direction){
		animator.SetFloat("x", direction.x);
		animator.SetFloat("y", direction.y);
	}
}
