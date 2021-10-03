using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // 0 = LR , 1 = LRB, 2 = LRT, 3 = LRTB
    public List<GameObject> lastRoomInstantiate;

    private int direction;
    public float moveAmount = 5;
    
    private float timeBetweenRooms;
    public float startTimeBetweenRooms = 0.25f;

    public float minX;
    public float maxX;
    public float maxY;
    private bool stopGeneration;
    public int upCounter;
    
    [SerializeField]
    private LayerMask room;

    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        lastRoomInstantiate.Add(rooms[0]);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBetweenRooms <= 0 && !stopGeneration)
        {
            Move();
            timeBetweenRooms = startTimeBetweenRooms;
        }
        else
        {
            timeBetweenRooms -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // Move Right
        {
            if (transform.position.x < maxX)
            {
                upCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                lastRoomInstantiate.Add(rooms[rand]);
                
                
                direction = Random.Range(1, 6);
                if (direction == 3)
                    direction = 2;
                else if (direction == 4)
                    direction = 5;
            }
            else
            {
                direction = 5;
            }
        }
        else if(direction == 3 || direction == 4) // Move Left
        {
            if (transform.position.x > minX)
            {
                upCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                lastRoomInstantiate.Add(rooms[rand]);

                direction = Random.Range(3, 6);
            }
            else {
                direction = 5;
            }
        }
        
        
        else if (direction == 5) { // Move UP
            upCounter++;
            if (transform.position.y < maxY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                
                if (roomDetection.GetComponent<RoomType>().type == 3 &&
                    roomDetection.GetComponent<RoomType>().type == 0)
                {
                    if (upCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[1], transform.position, Quaternion.identity);
                        lastRoomInstantiate.Add(rooms[1]);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        
                        var randTopRoom = Random.Range(1, 4);
                        if (randTopRoom == 2)
                            randTopRoom = 1;

                        Instantiate(rooms[randTopRoom], transform.position, Quaternion.identity);
                        lastRoomInstantiate.Add(rooms[randTopRoom]);
                        
                    }
                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
                transform.position = newPos;
                
                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                lastRoomInstantiate.Add(rooms[rand]);
                
                direction = Random.Range(1, 6);
            }
            
            else {
                // Stop level generation
                stopGeneration = true;
            } 
        }
    }
}
