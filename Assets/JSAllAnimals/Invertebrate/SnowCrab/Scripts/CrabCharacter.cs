using UnityEngine;
using System.Collections;

public class CrabCharacter : MonoBehaviour {
	Animator crabAnimator;
	public float upDown=0f;
	public float turn=0f;
	public float forward=0f;

	void Start () {
		crabAnimator = GetComponent<Animator> ();
	}
	
	void Update () {
		Move ();
	}
	
	public void Attack(){
		crabAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		crabAnimator.SetTrigger("Hit");
	}
	
	public void Defense(bool tf){
		crabAnimator.SetBool ("Defense",tf);
	}

	public void AimDefense(bool tf){
		crabAnimator.SetBool ("AimDefense",tf);
	}
	
	public void Jump(){
		crabAnimator.SetTrigger("Jump");
	}

	public void Open(){
		crabAnimator.SetTrigger("Open");
	}

	public void Move(){
		crabAnimator.SetFloat ("Forward", forward);
		crabAnimator.SetFloat ("Turn", turn);
		crabAnimator.SetFloat ("UpDown", upDown);
	}
}
