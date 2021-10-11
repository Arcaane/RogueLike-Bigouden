using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    public float moveSpeed = 100f;

    public GameObject spawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.Z))
            GetComponent<Rigidbody2D>().velocity= Vector2.up * 5f;
        else
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.Q))
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
    }
}