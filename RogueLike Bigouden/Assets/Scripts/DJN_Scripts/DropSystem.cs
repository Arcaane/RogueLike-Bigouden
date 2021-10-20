using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DropSystem : MonoBehaviour
{
    private ItemsManager itemManager;
    private GameObject gameManager;

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
       
    }

 
}
