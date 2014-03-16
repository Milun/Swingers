/**
 * Base class of all player controlled characters?
 */

using UnityEngine;
using System.Collections;

// Prints an error if there is no CharacterController for this object.
[RequireComponent( typeof(CharacterController))]

public class Character : MonoBehaviour {

	public float moveSpeed = 10.0f;		// Maximum speed the character reaches while running.

	private float m_direction = 1;		// Can equal 1 or -1. 1 = Facing right.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Temporary: Move the character forard.
		CharacterController controller = GetComponent<CharacterController>();
		Vector3 forward = new Vector3 (1.0f * m_direction, 0.0f, 0.0f);
		controller.SimpleMove (forward * moveSpeed * Time.deltaTime);

		// Temporary: Show when the character is on the ground.
		if (controller.isGrounded)
			renderer.material.color = new Color(0.5f,0.0f,0.5f);
		else
			renderer.material.color = new Color(1.0f,1.0f,1.0f);
	}
}
