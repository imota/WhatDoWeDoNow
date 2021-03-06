﻿using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
	// Movement variables
	public Vector2 speed = new Vector2(10, 10);
	public Vector2 direction = new Vector2(1, 0);
	private Vector2 movement;

	private Vector2 endPos;

	// Get input from keyboard
	Vector2 GetInput()
	{
		float input_x = Input.GetAxis("Horizontal");
		float input_y = Input.GetAxis("Vertical");
		Vector2 input = new Vector2(input_x, input_y);
		
		return input;
	}
	
	// Calculate movement vector based on speed and input keys
	void CalculateMovement()
	{
		Vector2 input = GetInput();
		Vector2 playerSpeed = speed;

		if (BatteriesHelper.Instance.Power == 1)
		{
			playerSpeed.x *= 1.5f;
			playerSpeed.y *= 1.5f;
		}

		if (input.x == 0 || input.y == 0)
			movement = new Vector2(playerSpeed.x*input.x, playerSpeed.y*input.y);
		else 
			movement = new Vector2(playerSpeed.x*input.x/Mathf.Sqrt(2), playerSpeed.y*input.y/Mathf.Sqrt(2));
	}
	
	// Move rigid body
	void Move()
	{
		rigidbody2D.velocity = movement;
	}
	
	void Update()
	{
		if (GameStateHelper.Instance.currentState == GameStates.PLAYING)
		{
			CalculateMovement();
			endPos = transform.position;
		}
		else
		{
			transform.position = endPos;
			transform.rigidbody2D.velocity = new Vector2(0f,0f);
		}
	}
	
	void FixedUpdate()
	{
		Move();
	}
}