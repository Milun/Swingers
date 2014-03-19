using UnityEngine;
using System.Collections;

[RequireComponent( typeof(CharacterPhysics))]
public class CharacterCommon : MonoBehaviour {

	public int		m_jumpCount = 0;			// How many times you've jumped already.
	public int		m_jumpMax = 2;				// Maximum amount of times you can jump.
	private float	m_jumpHeight = 10.0f;		// How high you jump each time.
	private float 	m_direction = 1;			// Can equal 1 or -1. 1 = Facing right.

	CharacterPhysics charPhysics;

	// Use this for initialization
	void Awake () {
		charPhysics = GetComponent<CharacterPhysics>();
	}
	
	// Update is called once per frame
	void Update () {

		// Allow the character to jump again after landing.
		// The ySpeed check prevents this from triggering when the player jumps again.
		if (charPhysics.IsGrounded())
		{
			// Only do this once.
			if (m_jumpCount != 0)
			{
				m_jumpCount = 0;
			}
			else
			{
				// Be SURE to reset yspeed or they'll fastfall after going off again!
				charPhysics.SetYSpeed(0.0f);
			}
		}

	}
	
	// Makes the character jump and also handles multiple jumps.
	public void Jump()
	{
		if(m_jumpCount < m_jumpMax)
		{
			charPhysics.SetYSpeed(m_jumpHeight);

			// If you're in the air when you jump for the firt time, you lose one of your jumps.
			if (m_jumpCount == 0 && !charPhysics.IsGrounded())
				m_jumpCount++;

			m_jumpCount++;
		}
	}

	/**
	 * Temporary. Moves the character along the ground. Changes direction only when on the ground.
	 * 
	 * @param speed From negative to positive values. Negative moves the character left.
	 */
	public void Walk(float speed)
	{
		charPhysics.SetXSpeed(speed);

		// Change direction variable.
		if (charPhysics.IsGrounded())
		{
			if (speed < 0)
			{
				m_direction = -1;
			}
			else if (speed > 0)
			{
				m_direction = 1;
			}
		}
	}

	public float GetDirection()
	{
		return m_direction;
	}
}
