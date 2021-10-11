using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    // Scene State
    public bool IsRoomClear;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
            Debug.Log("Collider");
        
        if (IsRoomClear)
        {
            Debug.Log("Oh");
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("J'ai le droit de vivre");
                LoadManager.LoadManagerInstance.ChangeRoom();
                Debug.Log("un peu ?");
            }
                
        }
    }
}