using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatingTextEnnemi : MonoBehaviour
{
    public float destroyTime = 1f;

    public Vector3 offset = new Vector3(0.38f, 0.69f, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }
}