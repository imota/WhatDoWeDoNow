﻿using UnityEngine;
using System.Collections;

public class BatteriesHelper : MonoBehaviour 
{
	public static BatteriesHelper Instance;

	public float period = 60f;

	public Texture texture;
	public float scale = 0.225f;
	public float x = -5.9f;
	public float y = -4.5f;
	private float heightScale = 2f;
	private float widthScale = 0.25f;

	private float elapsedTime = 0f;

	public float ElapsedTime
	{
		get {return elapsedTime;}
	}

	public int startingBatteries = 3;
	public int maximumBatteries = 5;

	private int power;
	public int Power
	{
		get
		{
			return power;
		}
		private set
		{
			if (value < 0)
				power = 0;
			else if (value >= maximumBatteries)
				power = maximumBatteries;
			else
				power = value;
		}
	}

	void Awake()
	{
		if (Instance != null)
			Debug.LogError("More than one instance of BatteriesHelper!");

		Instance = this;
		Power = startingBatteries;
	}

	public void DecreasePower()
	{
		if (Power != 0)
			--Power;
		Debug.Log("Battery power: " + Power);
	}

	public void IncreasePower()
	{
		++Power;
		Debug.Log("Battery power: " + Power);
	}

	// Draw batteries with respect to the player position
	void OnGUI()
	{
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

		if ((playerObject != null) && (GameStateHelper.Instance.currentState == GameStates.PLAYING))
		{
			Vector3 playerPosition = playerObject.transform.position;
			playerPosition.x += x;
			playerPosition.y -= y;
			Vector3 newPosition = Camera.main.WorldToScreenPoint(playerPosition);
			
			Rect position = new Rect(newPosition.x, newPosition.y, scale*widthScale*Screen.width, -scale*heightScale*Screen.height/5f*Power);
			Rect size = new Rect(0, 0, 1.0f, 1.0f/5f*Power);
			GUI.DrawTextureWithTexCoords(position, texture, size);
		}
	}
	
	void Update()
	{
		elapsedTime += Time.deltaTime;
		
		if (elapsedTime > period)
		{
			elapsedTime = 0;
			BatteriesHelper.Instance.DecreasePower();
		}
	}
}
