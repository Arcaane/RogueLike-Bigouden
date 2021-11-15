using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFeedback : MonoBehaviour
{
    public Vector2 floatY;
    public float originalY;
    public float effectLength;

    public float effectSpeed;
    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time * effectSpeed) * effectLength);
        transform.position = floatY;

    }
}
