using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;
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
            Debug.Log("Salle à droite");
            if (transform.position.x < maxX)
            {
                upCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); // Random de la salle générée
                if (rand == 0 || rand == 1)
                {
                    rand = 3;
                    Instantiate(rooms[rand], transform.position, Quaternion.identity); // Instansiate de la salle
                    lastRoomInstantiate.Add(rooms[rand]);
                    //Debug.Log("Salle instansiate : ")
                }
                else if (rand == 2 || rand == 5)
                {
                    rand = 4;
                    Instantiate(rooms[rand], transform.position, Quaternion.identity); // Instansiate de la salle
                    lastRoomInstantiate.Add(rooms[rand]);
                }
                else
                {
                    Instantiate(rooms[rand], transform.position, Quaternion.identity); // Instansiate de la salle
                    lastRoomInstantiate.Add(rooms[rand]);
                }

                direction = Random.Range(1, 6); // Random pour la prochaine direction : Si gauche -> droite ou haut 
                if (direction == 3)
                    direction = 2;
                else if (direction == 4)
                    direction = 5;
                Debug.Log("Prochaine direction : " + direction);
            }
            else // va vers le haut si pas possible d'aller à droite
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) // Move Left
        {
            if (transform.position.x > minX)
            {
                upCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(4, 7); // Random pour une salle compatible avec le mouvement à gauche (ouverte à droite)
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                lastRoomInstantiate.Add(rooms[rand]);

                direction = Random.Range(3, 6); // Random pour la prochaine direction : Pas de droite
                Debug.Log("Prochaine direction : " + direction);
            }
            else
            {
                direction = 5; // va vers le haut si pas possible d'aller à gauche
                Debug.Log("Prochaine direction : " + direction);
            }
        }
        else if (direction == 5)
        { 
            Debug.Log("Salle haut");
            // Move UP
            upCounter++;
            if (transform.position.y < maxY)
            {
                if (lastRoomInstantiate[lastRoomInstantiate.Count - 1].GetComponent<RoomType>().type == 0
                    || lastRoomInstantiate[lastRoomInstantiate.Count - 1].GetComponent<RoomType>().type == 3
                    || lastRoomInstantiate[lastRoomInstantiate.Count - 1].GetComponent<RoomType>().type == 5
                    || lastRoomInstantiate[lastRoomInstantiate.Count - 1].GetComponent<RoomType>().type == 6)
                {
                    Debug.Log("Salle précédente valide");
                    if (upCounter >= 2)
                    {
                        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
                        transform.position = newPos;
                    
                        if (transform.position.x != maxX && transform.position.x != minX)
                        {
                            int rand = Random.Range(0,3);
                            Instantiate(rooms[rand], transform.position, Quaternion.identity);
                            lastRoomInstantiate.Add(rooms[rand]);
                        }
                        else if (transform.position.x == minX) // Tout a gauche
                        {
                            int rand = Random.Range(0,3);
                            while (rand == 1)
                            {
                                rand = Random.Range(0, 3);
                            }
                            Instantiate(rooms[rand], transform.position, Quaternion.identity);
                            lastRoomInstantiate.Add(rooms[rand]);
                        }
                        else if (transform.position.x == maxX) // Tout a droite
                        {
                            int rand = Random.Range(0,2);
                            Instantiate(rooms[rand], transform.position, Quaternion.identity);
                            lastRoomInstantiate.Add(rooms[rand]);
                        }
                        direction = Random.Range(1, 6);
                        Debug.Log("Prochaine direction : " + direction);
                    }
                }
                else
                {
                    direction = Random.Range(1, 5);
                    Debug.Log("Prochaine direction : " + direction);
                }
            }
            else
            {
                // Stop level generation
                stopGeneration = true;
            }
        }
    }
}