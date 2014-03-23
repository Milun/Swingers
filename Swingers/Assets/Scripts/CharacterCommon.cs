﻿using UnityEngine;
using System.Collections;

[RequireComponent( typeof(CharacterPhysics))]
public class CharacterCommon : MonoBehaviour
{
	public GameObject grapplePointPrefab;
	private GameObject currentGrapplePoint = null;

	public int		m_jumpCount = 0;			// How many times you've jumped already.
	public int		m_jumpMax = 2;				// Maximum amount of times you can jump.
	private float	m_jumpHeight = 10.0f;		// How high you jump each time.

	public float	runAcc = 4.0f;				// How fast you gain speed while starting a run.
	public float 	runSpeed = 9.0f;			// Maximum speed the character reaches while running.

	private float 	m_facing = 1;				// Can equal 1 or -1. 1 = Facing right.
	private Vector3 m_aim = Vector3.zero;		// Where the character is aiming their grappling hook
	private Vector3 m_grappleDirection;
	
	CharacterPhysics charPhysics;

	// Use this for initialization
	void Awake ()
	{
		charPhysics = GetComponent<CharacterPhysics>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		// Allow the character to jump again after landing.
		// The ySpeed check prevents this from triggering when the player jumps again.
		if (charPhysics.isGrounded)
		{
			// Only do this once.
			if (m_jumpCount != 0)
			{
				m_jumpCount = 0;
			}
			else
			{
				// Be SURE to reset yspeed or they'll fastfall after going off again!
				charPhysics.ySpeed = 0.0f;
			}


		}

		if(currentGrapplePoint != null)
		{
			if(currentGrapplePoint.GetComponent<GrappleLogic>().IsSet())
			{
				//He can talk? He can talk? He can talk? I CAN SWING!
				
				//Milton: Ryan, this was brutally raping the poor Knight when it activated. I'm dissabling it for now.
				transform.position = Vector3.Lerp(transform.position, currentGrapplePoint.transform.position + new Vector3(6,-1,0), Time.deltaTime * 5.0f);
			}
		}
	}
	
	// Makes the character jump and also handles multiple jumps.
	public void Jump()
	{
		// If the slope is PERFECTLY VERTICAL, you can wall jump off of it.
		if (!charPhysics.isGrounded && Mathf.Abs(charPhysics.wallNormal.x) >= 1.0f)
		{
			charPhysics.ySpeed = m_jumpHeight;
			charPhysics.xSpeed = charPhysics.wallNormal.x*runSpeed*2.0f;

			charPhysics.isGrounded = false;
		}
		else if(m_jumpCount < m_jumpMax)
		{
			charPhysics.ySpeed = m_jumpHeight;

			// If you're in the air when you jump for the firt time, you lose one of your jumps.
			if (m_jumpCount == 0 && !charPhysics.isGrounded)
				m_jumpCount++;

			m_jumpCount++;
		}
	}

	/**
	 * Makes the character accelerate in speed along the ground while walking/running.
	 * Note: If the player is already moving at a speed faster than their run speed, this will NOT
	 * restrict that, and instead the characters natural friction will. This is intentional. It allows moonwalking.
	 * 
	 * @param speed From negative to positive values. Negative accelerates the character left. Range[-1, 1].
	 */
	public void Run(float speed)
	{
		// Accelerate in speed. Do not accelerate if you're going faster than runSpeed.
		if (
				(speed > 0.0f && charPhysics.xSpeed < runSpeed) ||
				(speed < 0.0f && charPhysics.xSpeed > -runSpeed)
		   )
		{
			charPhysics.xSpeed = charPhysics.xSpeed + speed*runAcc;
		}

		// Change direction variable.
		if (charPhysics.isGrounded)
		{
			if (speed < 0)
			{
				m_facing = -1;
			}
			else if (speed > 0)
			{
				m_facing = 1;
			}
		}
	}

	public float facing
	{
		get
		{
			return m_facing;
		}
	}

	public void Aim(Vector3 newAim)
	{
		m_aim = newAim;
	}

	public void FireGrapple()
	{
		if(currentGrapplePoint == null)
		{
			currentGrapplePoint = Instantiate (grapplePointPrefab, transform.position,transform.rotation) as GameObject;
			Destroy(currentGrapplePoint,100);

			GrappleLogic grappleLogic = currentGrapplePoint.GetComponent<GrappleLogic>();

			if(m_aim == Vector3.zero)
			{
				grappleLogic.SetDirection(new Vector3(1,1,0));
			}
			else
			{
				grappleLogic.SetDirection(m_aim);
			}
		}
	}
}
