/**
 * Base class of all player controlled characters?
 * 
 * Author: Milton Plotkin
 * Date: 17 March 2013
 */

using UnityEngine;
using System.Collections;

// Prints an error if there is no CharacterController for this object.
[RequireComponent( typeof(CharacterController))]

public class CharacterPhysics : MonoBehaviour
{

	public float 	moveSpeed = 7.0f;			// Maximum speed the character reaches while running.
	public float	m_gravity = 0.4f;			// Gets added every frame to y-speed when falling.
	public float	m_gravityMax = 10.0f;		// A character will not exceed this speed when in free fall.

	private bool	m_isAnchored = false;		// Becomes true for the first frame when controller.isGrounded == true. 
	private float	m_anchor = 0.0f;			// Applies downward force to keep the character on the platform it's standing on.
	private float	m_anchorSize = 5.0f;		// The larger the anchor, the steeper the slopes your character will stick to.
	
	public float 	m_maxSpeed = 1.0f;

	public float 	m_xSpeed, m_ySpeed = 0.0f;	//Two speeds represented on a scale of -1 to 1
	private float 	friction = 0.99f;

	CharacterController controller;

	// Use this for initialization
	void Awake ()
	{
		controller = GetComponent<CharacterController>();
	}

	/**
	 * Sets the value of m_gravityCurrent, which in turn gets added to the downward velocity of the character.
	 */
	private void Gravity()
	{
		// If the character is not on the ground, increase its gravity.
		if (!IsGrounded())
		{
			if (-m_ySpeed < m_gravityMax)
			{
				m_ySpeed -= m_gravity;
			}
			else
			{
				m_ySpeed = -m_gravityMax;
			}
		}
	}

	/**
	 * Adds to the downward velocity of the character, to keep them grounded on downward slopes.
	 * 
	 * MUST be called after Move().
	 */
	private void Anchor()
	{
		if(m_ySpeed > 0.0f)
		{
			m_isAnchored = false;
			m_anchor = 0.0f;
			return;
		}

		// If grounded, have the character constantly be pushed towards the ground.
		if (controller.isGrounded)
		{
			m_isAnchored = true;

			m_anchor = -m_anchorSize;
		}
		// If not grounded, do not apply downward force.
		else if (!controller.isGrounded && m_isAnchored)
		{
			m_isAnchored = false;

			// Fixes the jitter that occurs in the first frame that the character is not on the ground.
			controller.Move ( (new Vector3(0.0f, -m_anchor*2.0f, 0.0f)) * Time.deltaTime );

			m_anchor = 0.0f;
		}
	}

	/**
	 * Moves the character to the right (temporary).
	 * 
	 * Also calls Anchor().
	 */
	private void Move()
	{
		Gravity ();

		Vector3 movementDir = new Vector3(m_xSpeed * moveSpeed, m_ySpeed + (m_anchor), 0.0f);
		controller.Move (movementDir * Time.deltaTime);

		Anchor();
	}

	public void SetXSpeed(float newX)
	{
		if(newX > 1)
		{
			newX = 1;
		}
		else if(newX < -1)
		{
			newX = -1;
		}

		m_xSpeed = newX;
	}

	public void SetYSpeed(float newY)
	{
		m_ySpeed = newY;
	}

	public float GetXSpeed()
	{
		return m_xSpeed;
	}

	public float GetMaxSpeed()
	{
		return m_maxSpeed;
	}

	public float GetYSpeed()
	{
		return m_ySpeed;
	}

	public bool IsGrounded()
	{
		return controller.isGrounded;
	}

	// Update is called once per frame
	void Update ()
	{
		m_xSpeed *= friction;
		if(Mathf.Abs (m_xSpeed) <= 0.05)
		{
			m_xSpeed = 0.0f;
		}

		Move ();

	}
}