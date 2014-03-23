using UnityEngine;
using System.Collections;
using XInputDotNetPure;

[RequireComponent( typeof(Character))]

public class Input_Controller : MonoBehaviour
{
	private PlayerIndex playerIndex;
	private GamePadState state;
	private GamePadState prevState;

	private ButtonState jumpButton;
	private ButtonState toggleGrapple;

	Character character;

	// Use this for initialization
	void Awake()
	{
		playerIndex = (PlayerIndex)0;

		jumpButton = state.Buttons.A;
		toggleGrapple = state.Buttons.RightShoulder;

		character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = GamePad.GetState(playerIndex);

		if(state.IsConnected)
		{
			print (state.ThumbSticks.Left.X);

			character.PressHorizontal(state.ThumbSticks.Left.X);

			Vector3 currentAim = new Vector3(state.ThumbSticks.Left.X,state.ThumbSticks.Left.Y,0);
			if(currentAim == Vector3.zero)
			{
				currentAim = new Vector3(1,1,0);
			}
			character.Aim(currentAim);

			if(jumpButton == ButtonState.Pressed)
			{
				character.PressUp();
			}

			if(toggleGrapple == ButtonState.Pressed)
			{
				character.FireGrapple();
			}

			prevState = state;
		}
		else
		{
			//THERE IS NO CONTROLLER, CHAOS ENSUES
		}
	}
}
