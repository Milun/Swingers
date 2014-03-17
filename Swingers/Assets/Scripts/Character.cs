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

public class Character : MonoBehaviour
{

	public float 	moveSpeed = 1.0f;			// Maximum speed the character reaches while running.
	public float	m_gravity = 0.4f;			// Gets added every frame to y-speed when falling.
	private float	m_gravityCurrent = 0.0f;	// Current fall speed to be added to movement vector.
	public float	m_gravityMax = 2.0f;		// A character will not exceed this speed when in free fall.

	private bool	m_isAnchored = false;		// Becomes true for the first frame when controller.isGrounded == true. 
	private float	m_anchor = 0.0f;			// Applies downward force to keep the character on the platform it's standing on.

	private float 	m_direction = 1;			// Can equal 1 or -1. 1 = Facing right.
	private Vector3 m_colNormal = new Vector3(-0.01f, 0.0f, 0.0f);

	private CharacterController controller;

	// Use this for initialization
	void Awake ()
	{
		controller = GetComponent<CharacterController>();
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (controller.isGrounded)
		{
			m_colNormal = hit.normal;
		}
	}

	/**
	 * Sets the value of m_gravityCurrent, which in turn gets added to the downward velocity of the character.
	 */
	private void Gravity()
	{
		// If the character is not on the ground, increase its gravity.
		if (!controller.isGrounded)
		{
			if (m_gravityCurrent < m_gravityMax)
			{
				m_gravityCurrent += m_gravity;
			}
			else
			{
				m_gravityCurrent = m_gravityMax;
			}
		}
		// Otherwise, reset the gravity to 0.
		else
		{
			m_gravityCurrent = 0.0f;
		}
	}

	/**
	 * Adds to the downward velocity of the character, to keep them grounded on downward slopes.
	 * 
	 * MUST be called after Move().
	 */
	private void Anchor()
	{
		// If grounded, have the character constantly be pushed towards the ground.
		if (controller.isGrounded)
		{
			m_isAnchored = true;

			m_anchor = -2.0f;
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

		Vector3 forward = new Vector3(1.0f, m_anchor, 0.0f)*moveSpeed + new Vector3(0.0f, -m_gravityCurrent, 0.0f);
		controller.Move ( forward*Time.deltaTime );

		Anchor();
	}

	// Update is called once per frame
	void Update ()
	{
		Move ();

		// Temporary: Show when the character is on the ground.
		if (controller.isGrounded)
			renderer.material.color = new Color(0.5f,0.0f,0.5f);
		else
		{
			//UnityEditor.EditorApplication.isPaused = true;
			renderer.material.color = new Color(1.0f,1.0f,1.0f);
		}
	}
}
