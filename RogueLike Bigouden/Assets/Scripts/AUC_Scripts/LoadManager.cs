using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class LoadManager : MonoBehaviour
{
    public static LoadManager LoadManagerInstance;

    #region Variables
    [Header("Variables indicatives sur la salle actuelle et les salles visités")]
    public int currentRoom;
    public int numberOfRoomToCreate;
    public int roomBeforeBoss;
    public float shopApparitionValue;
    
    public int smallRoomCounter;
    public int mediumRoomCounter;
    public int largeRoomCounter;
    
    [Space(10)]
    [Header("Valeur d'apparition des différant types de salles")]
    public float smallRoomApparitionValue = 7;
    public float mediumRoomApparitionValue = 2;
    public float largeRoomApparitionValue = 1;
    
    /*
    [Space(10)]
    [Header("Apparition Value Multiplicator")]
    [SerializeField] public float smallRoomMultiplicator;
    [SerializeField] private float mediumRoomMultiplicator;
    [SerializeField] private float largeRoomMultiplicator;
    public float multiplicatorSizeIndicator = 4f;
    */
    
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

    [Header("Animator Duration")]
    public Animator launchAnimator;
    public float transitionDuration;
    
    public const int RoomLevel1BeforeBoss = 10;
    public const int RoomLevel2BeforeBoss = 20;
    
    private float x;
    private float y;
    private float z;

    private string lastRoom;
    #endregion
    
    public void Awake()
    {
        if (LoadManagerInstance == null)
            LoadManagerInstance = this;
        else
            Destroy(this);
        
        DontDestroyOnLoad(this.gameObject);
        currentRoom = 0;
    }

     public void Start()
     {
         isLevel1 = true;
         AlgoCompliquer();
     }

     private void Update()
     {
         if (Input.GetKeyUp(KeyCode.Space))
         {
             ChangeRoom();
         }
     }
     
     public void ChangeRoom()
     {
         SceneManager.LoadSceneAsync(finalList[currentRoom]);
         currentRoom++;

         SoundManager.instance.StartMusic();
             
     }

     public void ResetProcedural()
     {
         finalList = new List<string>();
         currentRoom = 0;
         
         smallRoomApparitionValue = 6;
         mediumRoomApparitionValue = 3f;
         smallRoomApparitionValue = 1f;
         shopApparitionValue = 0;
         
         smallRoomCounter = 0;
         mediumRoomCounter = 0;
         largeRoomCounter = 0;
         isLevel1 = true;
         AlgoCompliquer();
     }

     private void AddRoomLevel1()
     {
         isLevel1 = true;
         
         if (finalList.Count == 0)
         {
             finalList.Add(roomLevel1Small[0]);
             return;
         }
         
         if (finalList.Count == RoomLevel1BeforeBoss)
         {
             if (shopApparitionValue != 0)
             {  
                 finalList.Add(storeRoom[0]);
             }

             finalList.Add(bossRoom[0]);
             isLevel1 = false;
         }
         else if (finalList.Count != RoomLevel1BeforeBoss && finalList.Count >= 1)
         {
             LaMoulinette(smallRoomApparitionValue, mediumRoomApparitionValue, largeRoomApparitionValue);
         }
     }

     private void AddRoomLevel2()
     {
         isLevel1 = false;
         
         if (finalList.Count == RoomLevel2BeforeBoss) // Add le shop avant le boss
         {
             if (shopApparitionValue != 0)
             {
                 finalList.Add(storeRoom[0]);
             }
             finalList.Add(bossRoom[1]);
         }
         else if (finalList.Count != RoomLevel2BeforeBoss && finalList.Count > 0)
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

             if (i == 4 || i == 13)
             {
                 shopApparitionValue = 3;
             }
             
            CheckShop(); // Check si on met un shop ou pas
         
            string rAllCharacter = finalList[finalList.Count - 1];
            char rLastCharacter = rAllCharacter[rAllCharacter.Length - 1];
            
            if (rLastCharacter == 'S') // Si Petite Salle tirée
            {
                smallRoomCounter++;
                
                smallRoomApparitionValue --;    
             
                // Valeur Apparition Salle Moyenne
                mediumRoomApparitionValue += 0.75f;
                
                // Valeur Apparition Salle Grande
                largeRoomApparitionValue += 0.25f;
            }
            else if (rLastCharacter == 'M') // Si moyenne salle tirée 
            {
                mediumRoomCounter++;
                
                // Valeur Apparition Salle Petite
                smallRoomApparitionValue += 0.5f;
             
                // Valeur Apparition Salle Moyenne
                mediumRoomApparitionValue --;
             
                // Valeur Apparition Salle Grande
                largeRoomApparitionValue += 0.5f;
            }
            else if(rLastCharacter == 'L') // Si grande salle tirée
            {
                largeRoomCounter++;
                // Valeur Apparition Salle Petite
                smallRoomApparitionValue += 0.25f;
             
                // Valeur Apparition Salle Moyenne
                mediumRoomApparitionValue += 0.75f;
             
                // Valeur Apparition Salle Grande
                largeRoomApparitionValue --;
            }
            else if (finalList[finalList.Count - 1] == "Store")
                shopApparitionValue = 0;
            
            if (smallRoomApparitionValue <= 0)
                smallRoomApparitionValue = 2;
            else if (mediumRoomApparitionValue <= 0)
                mediumRoomApparitionValue = 2;
            else if (largeRoomApparitionValue <= 0)
                largeRoomApparitionValue = 2;
            
         }

         CheckAlgo();
     }

     private void CheckAlgo()
     {
         if (smallRoomCounter >= 8 || mediumRoomCounter >= 8 || largeRoomCounter >= 8 || finalList.Count > 22 || finalList[finalList.Count - 1] != "DN_Boss")
         {
             ResetProcedural();
         }
     }
     
     private int i;
     
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
         lastRoom = String.Empty;
         lastRoom = finalList[finalList.Count - 1];

         x = sValue;
         y = x + mValue; 
         z = y + lValue;
         
         var rand = Random.Range(0, z);
         if (isLevel1)
         {
             if (rand <= x && roomLevel1Small != null)
             {
                 finalList.Add(roomLevel1Small[Random.Range(0, roomLevel1Small.Count)]);
             }
             else if ( rand > x && rand <= y && roomLevel1Medium != null)
             {
                 finalList.Add(roomLevel1Medium[Random.Range(0, roomLevel1Medium.Count)]);
             }
             else if (rand > y && roomLevel1Large != null)
             {
                 finalList.Add(roomLevel1Large[Random.Range(0, roomLevel1Large.Count)]);
             }
         }
         else
         {
             if (rand <= x && roomLevel2Small != null)
             {
                 finalList.Add(roomLevel2Small[Random.Range(0, roomLevel2Small.Count)]);
             }
             else if ( rand > x && rand <= y && roomLevel2Medium != null)
             {
                 finalList.Add(roomLevel2Medium[Random.Range(0, roomLevel2Medium.Count)]);
             }
             else if (rand > y && roomLevel2Large != null)
             {
                 finalList.Add(roomLevel2Large[Random.Range(0, roomLevel2Large.Count)]);
             }
         }
         
         if (finalList[finalList.Count - 1] == lastRoom)
         {
             int b = finalList.Count;
             var a = finalList[finalList.Count - 1];
             var c = new List<String>(finalList);
             c.RemoveAt(b - 1);
             finalList = c;
             LaMoulinette(x, y ,z);
         }
     }
}
