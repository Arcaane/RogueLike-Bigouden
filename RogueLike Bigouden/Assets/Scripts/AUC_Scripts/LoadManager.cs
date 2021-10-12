using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadManager : MonoBehaviour
{
    public static LoadManager LoadManagerInstance;
    public int numberOfRoom;
    public List<int> randomIndex = new List<int>();
    
    [Header("Liste pour la Génération des Salles")]
    public List<string> roomLevel1 = new List<string>(); 
    // public List<string> roomS2 = new List<string>();
    public List<string> utilityRoom = new List<string>();
    public List<string> bossRoom = new List<string>();
        
    [Header("Liste des Salles pour cette partie")]
    public List<string> finalList = new List<string>(); 
        
    public void Awake()
     { 
         DontDestroyOnLoad(this.gameObject);
         numberOfRoom = 0;

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
         finalList.Add(roomLevel1[randomIndex[0]]); // RandomRoom5
         finalList.Add(roomLevel1[randomIndex[2]]); // RandomRoom5
         finalList.Add(roomLevel1[randomIndex[0]]); // RandomRoom5
         finalList.Add(roomLevel1[randomIndex[1]]); // RandomRoom5
         
         finalList.Add(utilityRoom[0]); // UtilityRoom1
         
         finalList.Add(roomLevel1[randomIndex[2]]); // RandomRoom5
         finalList.Add(roomLevel1[randomIndex[1]]); // RandomRoom5
         
         finalList.Add(bossRoom[0]);
     }

     public void ChangeRoom()
     {
         Debug.Log("Oui");
         SceneManager.LoadSceneAsync(finalList[numberOfRoom]);
         Debug.Log("Oui");
         numberOfRoom++;
         Debug.Log("Oui");

         if (numberOfRoom == 11)
         {
             Debug.Log("Oui");
             ResetProcedural();
         }
         Debug.Log("Oui");
         //SetPlayerPosition();
         Debug.Log("Oui");
     }

     void ResetProcedural()
     {
         finalList = new List<string>();
         randomIndex = new List<int>();
         numberOfRoom = 0;
        
         GetRandomNumber();
         CreateFinalList();
     }

     private void SetPlayerPosition()
     {
         GameObject spawnPoint = GameObject.Find("SpawnPoint");
         GameObject player = GameObject.FindGameObjectWithTag("Player");
         player.transform.position = spawnPoint.transform.position;
     }
}
