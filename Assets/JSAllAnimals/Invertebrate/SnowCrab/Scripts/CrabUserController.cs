using UnityEngine;
using System.Collections;

public class CrabUserController : MonoBehaviour {
	CrabCharacter crabCharacter;
	public float crouchSpeed=1f;
	
	void Start () {
		crabCharacter = GetComponent < CrabCharacter> ();
	}

	private void FixedUpdate()
	{
		if (Input.GetButtonDown ("Fire1")) {
			crabCharacter.Attack();
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			crabCharacter.Hit();
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			crabCharacter.Defense(true);
		}

		if (Input.GetKeyUp (KeyCode.Z)) {
			crabCharacter.Defense(false);
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			crabCharacter.AimDefense(true);
		}
		
		if (Input.GetKeyUp (KeyCode.X)) {
			crabCharacter.AimDefense(false);
		}

		if (Input.GetButtonDown ("Jump")) {
			crabCharacter.Jump();
		}

		if (Input.GetKey (KeyCode.N)) {
			crabCharacter.upDown=Mathf.Clamp(crabCharacter.upDown-Time.deltaTime*crouchSpeed,-1f,1f);
		}
		if (Input.GetKey (KeyCode.U)) {
			crabCharacter.upDown=Mathf.Clamp(crabCharacter.upDown+Time.deltaTime*crouchSpeed,-1f,1f);
		}

		if (Input.GetKeyDown (KeyCode.O)) {
			crabCharacter.Open();
		}

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		crabCharacter.turn = h;
		crabCharacter.forward = v;
	}
}
