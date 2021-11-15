using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadManager : MonoBehaviour
{
    public static LoadManager LoadManagerInstance;
    // Variables 
    public int currentRoom;
    public int numberOfRoomToCreate;
    public int numberOfLevel;
    public List<string> roomVisited = new List<string>();
    public float visitedRooms;
    
    public float variationMultiplicator;
    public float valeurVariationApparition;
    
    public int roomBeforeBoss;
    public float shopApparitionValue;
    public float smallRoomApparitionValue;
    public float mediumRoomApparitionValue;
    public float largeRoomApparitionValue;
    public float numberofEnemiesBase;
    public float enemyMultiplicator;
    private bool isLevel1; // Level 1 == t / Level 2 == f
    
    


    public List<int> randomIndex = new List<int>();
    
    [Header("Liste pour la Génération des Salles")]
    public List<string> roomLevel1 = new List<string>(); 
    public List<string> roomLevel2 = new List<string>();
    public List<string> utilityRoom = new List<string>();
    public List<string> bossRoom = new List<string>();
        
    [Header("Liste des Salles pour cette partie")]
    public List<string> finalList = new List<string>(); 
        
    
    
    public void Awake()
    {
        isLevel1 = true;
        DontDestroyOnLoad(this.gameObject);
        currentRoom = 0;

        if (LoadManagerInstance == null)
             LoadManagerInstance = this;
        else
             Destroy(this);
    }

     public void Start()
     {
         GetRandomNumber();
         CreateFinalList();
     }

     private void Update()
     {
         if (Input.GetKeyUp(KeyCode.Space))
         {
             ChangeRoom();
         }
     }

     void GetRandomNumber()
     {
         for (int i = 0; i < 3; i++)
         {
             int index = Random.Range(0, roomLevel1.Count);
             if (!randomIndex.Contains(index))
             {
                 randomIndex.Add(index);
             }
             else
             {
                 i--;
             }
         }
     }

     void CreateFinalList()
     {
         
         finalList.Add(roomLevel1[randomIndex[0]]); // RandomRoom
         finalList.Add(roomLevel1[randomIndex[2]]); // RandomRoom
         finalList.Add(roomLevel1[randomIndex[0]]); // RandomRoom
         finalList.Add(roomLevel1[randomIndex[1]]); // RandomRoom
         
         finalList.Add(utilityRoom[0]); // UtilityRoom1
         
         finalList.Add(roomLevel2[randomIndex[2]]); // RandomRoom
         finalList.Add(roomLevel2[randomIndex[1]]); // RandomRoom
         
         finalList.Add(bossRoom[0]);
     }

     public void ChangeRoom()
     {
         SceneManager.LoadSceneAsync(finalList[currentRoom]);
         currentRoom++;

         if (currentRoom == 11)
         {
             ResetProcedural();
         }
     }

     void ResetProcedural()
     {
         finalList = new List<string>();
         randomIndex = new List<int>();
         currentRoom = 0;
        
         GetRandomNumber();
         CreateFinalList();
     }

     private void SetPlayerPosition()
     {
         GameObject spawnPoint = GameObject.Find("SpawnPoint");
         GameObject player = GameObject.FindGameObjectWithTag("Player");
         player.transform.position = spawnPoint.transform.position;
     }
     
     private void CreatingRoomPath()
     {
         AlgoCompliqué();
     }
     
     private void AddRoomLevel1()
     {
         isLevel1 = true;
         if (finalList.Count == numberOfRoomToCreate)
         {
             if (shopApparitionValue != 0)
             {
                 finalList.Add(utilityRoom[0]);
             }
             finalList.Add(bossRoom[0]);
             isLevel1 = false;
         }
         else
         {
             int randRoom = Random.Range(0, roomLevel1.Count - 1);
             finalList.Add(roomLevel1[randRoom]);
             roomLevel1.Remove(roomLevel1[randRoom]);
         }
     }

     private void AddRoomLevel2()
     {
         
         if (finalList.Count == numberOfRoomToCreate)
         {
             if (shopApparitionValue != 0)
             {
                 finalList.Add(utilityRoom[0]);
             }
             finalList.Add(bossRoom[1]);
         }
         else
         {
             int randRoom = Random.Range(0, roomLevel2.Count - 1);
             finalList.Add(roomLevel2[randRoom]);
             roomLevel1.Remove(roomLevel1[randRoom]);
         }
     }

     private void AlgoCompliqué()
     {
         for (int i = 0; i < numberOfRoomToCreate * 2; i++)
         { 
             if (isLevel1)
                 AddRoomLevel1();
             else
                 AddRoomLevel2();
         
         
            CheckShop(); // Check si on met un shop ou pas
         
            string rAllCharacter = finalList[finalList.Count - 1];
            Debug.Log(rAllCharacter);
            char rLastCharacter = rAllCharacter[rAllCharacter.Length - 1];

         
            if (rLastCharacter == 'S') // Si Petite Salle tirée
            {
             // Valeur Apparition Salle Petite
             smallRoomApparitionValue -= valeurVariationApparition;
             smallRoomApparitionValue /= variationMultiplicator;
             
             // Valeur Apparition Salle Moyenne
             mediumRoomApparitionValue += valeurVariationApparition;
             mediumRoomApparitionValue /= variationMultiplicator;
             
             // Valeur Apparition Salle Grande
             largeRoomApparitionValue += valeurVariationApparition;
             largeRoomApparitionValue /= variationMultiplicator;

            }
            else if (rLastCharacter == 'M') // Si moyenne salle tirée 
            {
             // Valeur Apparition Salle Petite
             smallRoomApparitionValue += valeurVariationApparition;
             smallRoomApparitionValue /= variationMultiplicator;
             
             // Valeur Apparition Salle Moyenne
             mediumRoomApparitionValue -= valeurVariationApparition;
             mediumRoomApparitionValue /= variationMultiplicator;
             
             // Valeur Apparition Salle Grande
             largeRoomApparitionValue += valeurVariationApparition;
             largeRoomApparitionValue /= variationMultiplicator;
            }
            else if(rLastCharacter == 'L') // Si grande salle tirée
            {
             // Valeur Apparition Salle Petite
             smallRoomApparitionValue += valeurVariationApparition;
             smallRoomApparitionValue /= variationMultiplicator;
             
             // Valeur Apparition Salle Moyenne
             mediumRoomApparitionValue += valeurVariationApparition;
             mediumRoomApparitionValue /= variationMultiplicator;
             
             // Valeur Apparition Salle Grande
             largeRoomApparitionValue -= valeurVariationApparition;
             largeRoomApparitionValue /= variationMultiplicator;
            }
            else if (finalList[finalList.Count - 1] == "Store")
            { shopApparitionValue = 0; }
         }
     }

     private void CheckShop()
     {
         if (shopApparitionValue == 0)
         {
             return;
         }

         if (finalList.Count >= 2 && finalList.Count <= roomBeforeBoss)
         {
             if (shopApparitionValue == 0)
             {
                return;
             }
             else
             {
                 shopApparitionValue += valeurVariationApparition;
             }
         }
     }
}
