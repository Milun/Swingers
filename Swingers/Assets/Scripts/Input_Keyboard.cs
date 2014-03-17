using UnityEngine;
using System.Collections;

// Prints an error if there is no CharacterController for this object.
[RequireComponent( typeof(CharacterMovement))]

public class Input_Keyboard : MonoBehaviour
{

	KeyCode moveLeft = KeyCode.LeftArrow;
	KeyCode moveRight = KeyCode.RightArrow;
	KeyCode jump = KeyCode.Z;

	CharacterMovement charMovement;

	// Use this for initialization
	void Start ()
	{
		charMovement = GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 moveDirection = Vector3.zero;

		if(Input.GetKey (moveLeft))
		{
			charMovement.SetXSpeed(-1.0f);
		}
		else if(Input.GetKey (moveRight))
		{
			charMovement.SetXSpeed(1.0f);
		}

		if(Input.GetKey (jump))
		{
			charMovement.SetYSpeed(10.0f);
		}
		
	}
}