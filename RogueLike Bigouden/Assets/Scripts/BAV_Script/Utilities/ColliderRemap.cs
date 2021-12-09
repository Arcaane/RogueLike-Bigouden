using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderRemap : MonoBehaviour
{
    public EdgeCollider2D edge;
    public BoxCollider2D box;
    public Vector4 boxSize_Offset;

    // Start is called before the first frame update
    void Start()
    {
        edge = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetCollider();
    }

    void ResetCollider()
    {
    }
}
/*
void ResetCollider()
{
    Vector2[] points = edge.points;
    points.SetValue(new Vector2(-1f, -0.1f), 0);
    points.SetValue(new Vector2(-1f, -0.1f), 1);
    points.SetValue(new Vector2(-1f, -0.1f), 2);
    points.SetValue(new Vector2(-1f, -0.1f), 3);
    points.SetValue(new Vector2(-1f, -0.1f), 4);
    points.SetValue(new Vector2(-1f, -0.1f), 5);
    points.SetValue(new Vector2(-1f, -0.1f), 6);
    points.SetValue(new Vector2(-1f, -0.1f), 7);
    points.SetValue(new Vector2(-1f, -0.1f), 8);
    points.SetValue(new Vector2(-1f, -0.1f), 9);
    points.SetValue(new Vector2(-1f, -0.1f), 10);
    points.SetValue(new Vector2(-1f, -0.1f), 11);
    points.SetValue(new Vector2(-1f, -0.1f), 12);
    points.SetValue(new Vector2(-1f, -0.1f), 13);
    points.SetValue(new Vector2(-1f, -0.1f), 14);
    points.SetValue(new Vector2(-1f, -0.1f), 15);
    points.SetValue(new Vector2(-1f, -0.1f), 16);
    points.SetValue(new Vector2(-1f, -0.1f), 17);
    points.SetValue(new Vector2(-1f, -0.1f), 18);
    points.SetValue(new Vector2(-1f, -0.1f), 19);
    edge.points = points;
}*/