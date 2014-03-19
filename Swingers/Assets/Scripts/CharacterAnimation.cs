using UnityEngine;
using System.Collections;

[RequireComponent( typeof(CharacterCommon))]
[RequireComponent( typeof(CharacterPhysics))]
public class CharacterAnimation : MonoBehaviour {

	CharacterCommon 	charCommon;
	CharacterPhysics 	charPhysics;

	// Use this for initialization
	void Awake () {
		charCommon 	= GetComponent<CharacterCommon>();
		charPhysics = GetComponent<CharacterPhysics>();
	}

	/**
	 * Handles walking AND running
	 **/
	void AnimWalk() {

		// Speed from 0 - 1, where 1 is the maximum running speed.
		float speed = Mathf.Abs(charPhysics.GetXSpeed()) / charPhysics.GetMaxSpeed();

		// Synch the walking animation and running animation (as if they were playing simultaneously.
		// This allows the two animations to be transitioned between smoothly.

		// Note: With the current knight animation, this doesnt work as his run and walk cycles are inverted.
		// Whoops.
		if (!animation.IsPlaying("anim_walk") && animation.IsPlaying("anim_run"))
		{
			float animRunPercent = animation["anim_run"].time/animation["anim_run"].length;
			animation["anim_walk"].time = animRunPercent*animation["anim_walk"].length;
		}
		else if (!animation.IsPlaying("anim_run") && animation.IsPlaying("anim_walk"))
		{
			float animWalkPercent = animation["anim_walk"].time/animation["anim_walk"].length;
			animation["anim_run"].time = animWalkPercent*animation["anim_run"].length;
		}

		// Check if the speed is low enough for the character to walk.
		if (speed < 0.7f)
		{
			animation["anim_walk"].speed = speed*4;

			// Smoothly transitions towards this animation (takes half a second).
			animation.CrossFade("anim_walk", 0.4f);
		}
		else
		{
			animation["anim_run"].speed = speed;

			// Smoothly transitions towards this animation (takes half a second).
			animation.CrossFade("anim_run", 0.4f);
		}

		if (charCommon.GetDirection() == -1)
		{
			transform.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
		}
		else
		{
			transform.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		AnimWalk();
	}
}
