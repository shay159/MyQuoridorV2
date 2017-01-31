using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Programmer           : Shay Itzhakey
// Date                 : 31/01/2017

public class WallAlignment : MonoBehaviour
{

    // Use this for initialization
    public bool used = false;
    public bool isDragged = false;

    public float oldX;
    public float oldZ;
    public bool isHoriontal = true;
    // Update is called once per frame
    void Start()
    {
        oldX = this.transform.position.x;
        oldZ = this.transform.position.z;
    }

    void Update()
    {
        if (isDragged)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 18f;
            transform.position = Camera.main.ScreenToWorldPoint(pos);
			if (Input.GetKeyUp(KeyCode.Space))
			{
				isHoriontal = !isHoriontal;
				if (!isHoriontal) 
				{
					this.transform.localScale = new Vector3 (0.5f, 2.0f, 2.5f);
				} 
				else 
				{
					this.transform.localScale = new Vector3 (2.5f, 2.0f, 0.5f);
				}
			}
        }
        else
        {
            this.transform.position = new Vector3(oldX, 1.0f, oldZ);
        }
    }


    void OnMouseDown()
    {
        if (!used)
        {
            oldX = this.transform.position.x;
            oldZ = this.transform.position.z;
            isDragged = !isDragged;
        }
    }

    void OnMouseUp()
    {
        if (!used && isDragged)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 18f;
            transform.position = Camera.main.ScreenToWorldPoint(pos);
            isDragged = !isDragged;
            used = !used;
            Debug.Log(this.transform.position);
            if (isHoriontal)
            {
                if (!toAlignmentHoriz())
                {
                    this.transform.position = new Vector3(oldX, 1.0f, oldZ);
					isHoriontal = true;
					this.transform.localScale = new Vector3 (2.5f, 2.0f, 0.5f);

                }
                else
                {
                    oldX = this.transform.position.x;
                    oldZ = this.transform.position.z;
					BoardBuilder.TakenSpots [BoardBuilder.Spotindex++] = new Boardcordinates (oldX, oldZ);
                }
            }
            else if (!toAlignmentVertc())
            {
                this.transform.position = new Vector3(oldX, 1.0f, oldZ);
				isHoriontal = true;
				this.transform.localScale = new Vector3 (2.5f, 2.0f, 0.5f);
            }
            else
            {
                oldX = this.transform.position.x;
                oldZ = this.transform.position.z;
				BoardBuilder.TakenSpots [BoardBuilder.Spotindex++] = new Boardcordinates (oldX, oldZ);
            }
        }
	}


        bool toAlignmentHoriz()
        {
            float x = this.transform.position.x;
            float z = this.transform.position.z;
            bool flag = true;
            int i;
            int j = 0;
            Boardcordinates point = new Boardcordinates(x, z);
            for (i = 0; i < 8 && flag; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if ((x > BoardBuilder.Positions[i, j].x && x < BoardBuilder.Positions[i, j + 1].x) &&
                        (BoardBuilder.Positions[i, j].inRange(point) || BoardBuilder.Positions[i, j + 1].inRange(point)))
                    {
                        flag = !flag;
                        break;
                    }
                }
            }

            if (!flag)
            {
                x = (BoardBuilder.Positions[i, j].x + BoardBuilder.Positions[i, j + 1].x) / 2.0f;
				if (z > BoardBuilder.Positions [i, j].z)
					z = BoardBuilder.Positions [i, j].z + 0.750f;
				else
                	z = BoardBuilder.Positions[i, j].z - 0.750f;
             	this.transform.position = new Vector3(x, 1.0f, z);
            }
            else
            {
                used = !used;
            }
            return !flag;
        }
			
        bool toAlignmentVertc()
        {
            float x = this.transform.position.x;
            float z = this.transform.position.z;
            bool flag = true;
            int i;
            int j = 0;
            Boardcordinates point = new Boardcordinates(x, z);
            for (i = 0; i < 8 && flag; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if ((z > BoardBuilder.Positions[j, i].z && z < BoardBuilder.Positions[j + 1 , i].z) &&
                        (BoardBuilder.Positions[j, i].inRange(point) || BoardBuilder.Positions[j + 1, i].inRange(point)))
                    {
                        flag = !flag;
                        break;
                    }
                }
            }

            if (!flag)
            {
                z = (BoardBuilder.Positions[j, i].z + BoardBuilder.Positions[j + 1, i].z) / 2.0f;
                if (x >= BoardBuilder.Positions[j, i].x)
                    x = BoardBuilder.Positions[j, i].x + 0.750f;
                else
                    x = BoardBuilder.Positions[j, i].x - 0.750f;
                this.transform.position = new Vector3(x, 1.0f, z);
            }
            else
            {
                used = !used;
            }

            return !flag;
        }


	bool isAvailable(Boardcordinates source, Boardcordinates check, bool sourceState)
	{
		bool OkFlag = true;
		if (source.inRange (check))
			OkFlag = false;
		if (sourceState == isHoriontal) 
		{
			if (isHoriontal) 
			{
				if (source.x + 0.25f == check.x - 1.25f || source.z - 0.25f == check.x + 1.25f)
					OkFlag = false;
			}
			else 
			{
				if (source.z + 0.25f == check.z - 1.25f || source.z - 0.25f == check.z + 1.25f)
					OkFlag = false;
			}
		}

		return true;
	}
 }

