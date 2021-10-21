using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DropSystem : MonoBehaviour
{
    private ItemsManager itemManager;
    private GameObject gameManager;
    public int roll;
    public int commonValue = 100;
    public int rareValue = 40;
    public int epicValue = 10;
    public Items itemSelect; //assigné un item dans la liste de l'ItemManager en fonction de son dropRate.

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        itemManager = gameManager.GetComponent<ItemsManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            roll = UnityEngine.Random.Range(0, 100);
            Debug.Log(roll);
            
            if (roll < 10)
            {
                itemSelect = itemManager.epicItems[UnityEngine.Random.Range(0, itemManager.epicItems.Count)];
                Debug.Log("It will be epic");
            }

            if (roll > 10 && roll < 40)
            {
                itemSelect = itemManager.rareItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be rare");
            }

            if (roll > 40 && roll < 100)
            {
                itemSelect = itemManager.commonItems[UnityEngine.Random.Range(0, itemManager.rareItems.Count)];
                Debug.Log("It will be common");
            }
            
        }
        
        
    }

 
}
