using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadManager : MonoBehaviour
{
    public static LoadManager LoadManagerInstance;

    #region Variables
    [Header("Variables indicatives sur la salle actuelle et les salles visités")]
    public int currentRoom;
    public int numberOfRoomToCreate;
    public int numberOfLevel;
    public List<string> roomVisited = new List<string>();
    
    public float visitedRooms;
    public int roomBeforeBoss;
    public float shopApparitionValue;
    
    [Space(10)]
    [Header("Valeur d'apparition des différant types de salles")]
    public float smallRoomApparitionValue = 75;
    public float mediumRoomApparitionValue = 22.5f;
    public float largeRoomApparitionValue = 7.5f;
    
    [Space(10)]
    [Header("Apparition Value Multiplicator")]
    public float smallRoomMultiplicator;
    [SerializeField] private float mediumRoomMultiplicator;
    [SerializeField] private float largeRoomMultiplicator;
    public float multiplicatorSizeIndicator = 4f;
    
    private bool isLevel1; // Level 1 == t / Level 2 == f

    
    [Space(10)]
    [Header("Liste  des salles pour la génération du niveau 1")]
    public List<string> roomLevel1Small = new List<string>(); 
    public List<string> roomLevel1Medium = new List<string>(); 
    public List<string> roomLevel1Large = new List<string>(); 
    
    [Space(10)]
    [Header("Liste des salles pour la génération du niveau 1")]
    public List<string> roomLevel2Small = new List<string>();
    public List<string> roomLevel2Medium = new List<string>();
    public List<string> roomLevel2Large = new List<string>();
    
    [Space(10)]
    [Header("StoreRoom / BossRoom")]
    public List<string> defaultRoom = new List<string>();
    public List<string> storeRoom = new List<string>();
    public List<string> bossRoom = new List<string>();
    
    [Space(10)]
    [Header("Liste des Salles pour cette partie")]
    public List<string> finalList = new List<string>();

    private float x;
    private float y;
    private float z;
    #endregion
    
    public void Awake()
    {
        if (LoadManagerInstance == null)
            LoadManagerInstance = this;
        else
            Destroy(this);
        
        isLevel1 = true;
        DontDestroyOnLoad(this.gameObject);
        currentRoom = 0;
    }

     public void Start()
     {
         isLevel1 = true;
         mediumRoomMultiplicator = smallRoomMultiplicator + (smallRoomMultiplicator / multiplicatorSizeIndicator);
         largeRoomMultiplicator = smallRoomMultiplicator + (smallRoomMultiplicator / ( mediumRoomMultiplicator / 2 )) - 1;
         AlgoCompliquer();
     }

     private void Update()
     {
         if (Input.GetKeyUp(KeyCode.Space))
         {
             ChangeRoom();
         }
     }

     /*
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
         
         finalList.Add(roomLevel1[randomIndex[2]]); // RandomRoom
         finalList.Add(roomLevel1[randomIndex[1]]); // RandomRoom
         
         finalList.Add(bossRoom[0]);
     }
    */
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
         //randomIndex = new List<int>();
         
         finalList = new List<string>();
         currentRoom = 0;
         AlgoCompliquer();
        
         //GetRandomNumber();
         //CreateFinalList();
     }

     private void SetPlayerPosition()
     {
         GameObject spawnPoint = GameObject.Find("SpawnPoint");
         GameObject player = GameObject.FindGameObjectWithTag("Player");
         player.transform.position = spawnPoint.transform.position;
     }

     private void AddRoomLevel1()
     {
         isLevel1 = true;
         
         if (finalList.Count == 0)
         {
             finalList.Add(roomLevel1Small[0]);
             return;
         }

         Debug.Log("Oui ?");
         
         if (finalList.Count == 9)
         {
             if (shopApparitionValue != 0)
             {
                 finalList.Add(storeRoom[0]);
             }

             finalList.Add(bossRoom[0]);
             isLevel1 = false;
         }
         else if (finalList.Count != 9 && finalList.Count > 0)
          // Add une salle normalement selon l'algo. La vache j'ai cho
         {
             LaMoulinette(smallRoomApparitionValue, mediumRoomApparitionValue, largeRoomApparitionValue);
         }
     }

     private void AddRoomLevel2()
     {
         Debug.Log("Level2");
         if (finalList.Count == 19) // Add le shop avant le boss
         {
             if (shopApparitionValue != 0)
             {
                 finalList.Add(storeRoom[0]);
             }
             finalList.Add(bossRoom[1]);
         }
         else // Add une salle normalement selon l'algo. La vache j'ai cho
         {
             LaMoulinette(smallRoomApparitionValue, mediumRoomApparitionValue, largeRoomApparitionValue);
         }
     }

     private void AlgoCompliquer()
     {
         for (int i = 0; i < numberOfRoomToCreate; i++)
         {
             if (isLevel1)
                 AddRoomLevel1();
             else
                 AddRoomLevel2();

             if (numberOfRoomToCreate == 3 || numberOfRoomToCreate == 13)
             {
                 shopApparitionValue = 3;
             }
             
            CheckShop(); // Check si on met un shop ou pas
         
            string rAllCharacter = finalList[finalList.Count - 1];
            Debug.Log(rAllCharacter);
            char rLastCharacter = rAllCharacter[rAllCharacter.Length - 1];
            
            
            if (rLastCharacter == 'S') // Si Petite Salle tirée
            {
                Debug.Log("Last Letter : S");
                // Valeur Apparition Salle Petite
                //smallRoomApparitionValue *= smallRoomMultiplicator;     // TRY
                smallRoomApparitionValue --;    
             
                // Valeur Apparition Salle Moyenne
                //mediumRoomApparitionValue /= mediumRoomMultiplicator;   // TRY
                mediumRoomApparitionValue += 0.75f;
                
                // Valeur Apparition Salle Grande
                //largeRoomApparitionValue *= largeRoomMultiplicator;     // TRY
                largeRoomApparitionValue += 0.25f;
            }
            else if (rLastCharacter == 'M') // Si moyenne salle tirée 
            {
                Debug.Log("Last Letter : M");
                // Valeur Apparition Salle Petite
                // smallRoomApparitionValue /= smallRoomMultiplicator;     // TRY
                smallRoomApparitionValue += 0.5f;
             
                // Valeur Apparition Salle Moyenne
                // mediumRoomApparitionValue *= mediumRoomMultiplicator;     // TRY
                mediumRoomApparitionValue --;
             
                // Valeur Apparition Salle Grande
                // largeRoomApparitionValue *= largeRoomMultiplicator;     // TRY
                largeRoomApparitionValue += 0.5f;
            }
            else if(rLastCharacter == 'L') // Si grande salle tirée
            {
                Debug.Log("Last Letter : L");
                // Valeur Apparition Salle Petite
                //smallRoomApparitionValue /= smallRoomMultiplicator;      // TRY
                smallRoomApparitionValue += 0.25f;
             
                // Valeur Apparition Salle Moyenne
                //mediumRoomApparitionValue /= mediumRoomMultiplicator;    // TRY
                mediumRoomApparitionValue += 0.75f;
             
                // Valeur Apparition Salle Grande
                //largeRoomApparitionValue /= largeRoomMultiplicator;      // TRY
                largeRoomApparitionValue --;
            }
            else if (finalList[finalList.Count - 1] == "Store")
                shopApparitionValue = 0;
         }
         
         if (smallRoomApparitionValue <= 0)
             smallRoomApparitionValue = 1;
         else if (mediumRoomApparitionValue <= 0)
             mediumRoomApparitionValue = 1;
         else if (largeRoomApparitionValue <= 0)
             largeRoomApparitionValue = 1;
         
         Debug.Log("Shop apparition value : " + shopApparitionValue);
     }

     private void CheckShop()
     {
         if (shopApparitionValue == 0)
         {
             return;
         }

         if (finalList.Count >= 3 && finalList.Count <= roomBeforeBoss)
         {
             int rand = Random.Range(0, 10);
             if (rand <= shopApparitionValue)
             {
                 finalList.Add(storeRoom[0]);
                 shopApparitionValue = 0;
             }
             else
                 shopApparitionValue++;
         }
     }

     private void LaMoulinette(float sValue, float mValue, float lValue)
     {
         
         x = sValue;
         y = sValue + mValue; 
         z = y + lValue + 1;
         
         Debug.Log("X : " + x);
         Debug.Log("Y : " + y);
         Debug.Log("Z : " + z);
         
         var rand = Random.Range(0, z);
         Debug.Log(rand);
         if (isLevel1)
         {
             if (rand <= x && roomLevel1Small != null)
             {
                 finalList.Add(roomLevel1Small[Random.Range(0, roomLevel1Small.Count - 1)]);
                 Debug.Log("Salle choisie : Small");
             }
             else if ( rand > x && rand <= y && roomLevel1Medium != null)
             {
                 finalList.Add(roomLevel1Medium[Random.Range(0, roomLevel1Medium.Count - 1)]);
                 Debug.Log("Salle choisie : Medium");
             }
             else if (rand >= z && roomLevel1Large != null)
             {
                 finalList.Add(roomLevel1Large[Random.Range(0, roomLevel1Large.Count - 1)]);
                 Debug.Log("Salle choisie : Large");
             }
         }
         else
         {
             if (rand <= x && roomLevel2Small != null)
             {
                 finalList.Add(roomLevel1Small[Random.Range(0, roomLevel2Small.Count - 1)]);
                 Debug.Log("Salle choisie : Small");
             }
             else if ( rand > x && rand <= y && roomLevel2Medium != null)
             {
                 finalList.Add(roomLevel1Medium[Random.Range(0, roomLevel2Medium.Count - 1)]);
                 Debug.Log("Salle choisie : Medium");
             }
             else if (rand >= z && roomLevel2Large != null)
             {
                 finalList.Add(roomLevel2Large[Random.Range(0, roomLevel2Large.Count - 1)]);
                 Debug.Log("Salle choisie : Large");
             }
         }
     }
}
