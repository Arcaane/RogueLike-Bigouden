using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 1f;

    public Vector3 offset1 = new Vector3(0.38f, 0.69f, 0);
    public Vector3 offset2 = new Vector3(-0.097f, 0.719f, 0);
    public Vector3 offset3 = new Vector3(0.047f, 0.962f, 0);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        int rand = Random.Range(0, 3);
        if (rand == 0)
            transform.localPosition += offset1;
        else if (rand == 1)
            transform.localPosition += offset2;
        else
            transform.localPosition += offset3;
    }
}
