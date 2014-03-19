using UnityEngine;
using System.Collections;
using XInputDotNetPure;

[RequireComponent( typeof(Character))]

public class Input_Controller : MonoBehaviour
{
	private PlayerIndex playerIndex;
	private GamePadState state;
	private GamePadState prevState;

	Character character;

	// Use this for initialization
	void Awake()
	{
		playerIndex = (PlayerIndex)0;

		character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = GamePad.GetState(playerIndex);

		if(state.IsConnected)
		{
			character.PressHorizontal(state.ThumbSticks.Left.X);

			print (state.ThumbSticks.Left.X);

			if(state.Buttons.A == ButtonState.Pressed)
			{
				character.PressUp();
			}

			prevState = state;
		}
		else
		{
			print("No Controller Detected");
		}
	}
}
