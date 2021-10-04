using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    public int Size;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RoomDestruction()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
