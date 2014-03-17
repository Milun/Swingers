using UnityEngine;
using System.Collections;
using XInputDotNetPure;

[RequireComponent( typeof(CharacterMovement))]

public class Input_Controller : MonoBehaviour
{
	private PlayerIndex playerIndex;
	private GamePadState state;
	private GamePadState prevState;

	CharacterMovement charMovement;

	// Use this for initialization
	void Start()
	{
		playerIndex = (PlayerIndex)0;

		charMovement = GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = GamePad.GetState(playerIndex);

		if(state.IsConnected)
		{
			charMovement.SetXSpeed (state.ThumbSticks.Left.X);

			print (state.ThumbSticks.Left.X);

			if(state.Buttons.A == ButtonState.Pressed)
			{
				charMovement.SetYSpeed (10.0f);
			}

			prevState = state;
		}
		else
		{
			print("No Controller Detected");
		}
	}
}
