using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Programmer           : Shay Itzhakey
// Date                 : 31/01/2017

public class BoardBuilder : MonoBehaviour {
	public Rigidbody pos;
	public Rigidbody block;
	public Rigidbody Border;
	public Rigidbody wall;
	public static Boardcordinates[,] Positions;
	public static string[,] PositionsTag;
	public static Boardcordinates[] TakenSpots;
	public static int Spotindex;
	public static bool[] areHorizontal;
	//public Rigidbody[,] Positions;
	// Use this for initialization
	void Start () {
		//Positions = new Rigidbody[9, 9];
		PositionsTag = new string[9,9];
		Positions = new Boardcordinates[9, 9];
		TakenSpots = new Boardcordinates[20];
		areHorizontal = new bool[20];
		float z = -6.0f;
		float x;
		int counter = 1;
        // Positions
		for (int i = 0; i < 9; i++) {
			x = -6.0f;
			for (int j = 0; j < 9; j++) {
				Instantiate (pos);
				pos.transform.position = new Vector3 (x, 0.5f, z);
				pos.tag = counter.ToString ();
				PositionsTag [i, j] = counter.ToString ();
				Positions [i, j] = new Boardcordinates (x, z);
				counter++;
				x += 1.5f;
			}
			z += 1.5f;
		}

		z = -7.5f;
		x = -8.25f;
        // Blocks
		for (int i = 0; i < 11; i++) {
			Instantiate(block);
			block.transform.position = new Vector3(x, 0.5f, z);
			z += 1.5f;
		}
		z = -7.5f;
		x = 8.25f;
		for (int i = 0; i < 11; i ++)
		{
			Instantiate(block);
			block.transform.position = new Vector3 (x, 0.5f, z);
			z += 1.5f;
		}

        // Borders
		Instantiate(Border);
		Border.transform.position = new Vector3 (-6.75f, 0.5f, 0.0f);
		Instantiate(Border);
		Border.transform.position = new Vector3 (6.75f, 0.5f, 0.0f);

		z = -7.0f;
		for (int i = 0; i < 10; i++) {
			Instantiate (wall);
			wall.transform.position = new Vector3 (x , 1.0f, z);
			wall.freezeRotation = true;
			wall.tag = "Player1";
			z += 1.5f;
		}

        // Walls
		x = -8.25f;
		z = -7.0f;
		for (int i = 0; i < 10; i++) {
			Instantiate (wall);
			wall.freezeRotation = true;
			wall.transform.position = new Vector3 (x, 1.0f, z);
			wall.tag = "Player2";
			z += 1.5f;
		}
	}
}

public class Boardcordinates
{
	public float x;
	public float z;

	public bool inRange(Boardcordinates a)
	{
		float result = Mathf.Pow(a.x - this.x, 2.0f)+ Mathf.Pow(a.z - this.z, 2.0f);
		return Mathf.Sqrt(result) <= 1.0f;
	}


	public Boardcordinates(float x, float z)
	{
		this.x = x;
		this.z = z;
	}
		
}