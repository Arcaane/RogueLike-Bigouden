using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    //Point de Spawn des Joueurs
    [SerializeField] private Transform[] PlayerSpawns;
    //Prefab du Player
    [SerializeField] private GameObject playerPrefab;
    //Nombre de joueurs dans la scene
    [SerializeField] private List<GameObject> prefabInstantiate;

    // Start is called before the first frame update
    void LateUpdate()
    {
        if (PlayerConfigurationManager.Instance.launchGame)
        {
            var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
            for (int i = 0; i < playerConfigs.Length; i++)
            {
                GameObject player = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation,
                    gameObject.transform);
                player.name = ("Player " + i);
                player.GetComponent<PlayerInput_Final>().InitializePlayer(playerConfigs[i]);
                prefabInstantiate.Add(player);
                PlayerConfigurationManager.Instance.launchGame = false;
                if (PlayerConfigurationManager.Instance.launchGame == false)
                {
                    UIManager.instance.SearchPlayer(player);
                    PlayerConfigurationManager.Instance.MainLayout.SetActive(false);
                }
            }
        }
 
   }
}