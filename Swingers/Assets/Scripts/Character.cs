using UnityEngine;
using System.Collections;

[RequireComponent( typeof(CharacterPhysics))]
[RequireComponent( typeof(CharacterCommon))]

public class Character : MonoBehaviour
{

	CharacterCommon 	charCommon;
	CharacterPhysics 	charPhysics;

	// Use this for initialization
	void Awake ()
	{
		charCommon 	= GetComponent<CharacterCommon>();
		charPhysics = GetComponent<CharacterPhysics>();
	}

	public void PressUp()
	{
		charCommon.Jump();
	}

	public void PressHorizontal(float val)
	{
		charCommon.Walk (val);
	}

	public void Aim(Vector3 newAim)
	{
		charCommon.Aim(newAim);
	}

	
	public void FireGrapple()
	{
		charCommon.FireGrapple();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
