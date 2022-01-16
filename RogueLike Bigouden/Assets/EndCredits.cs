using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    // Start is called before the first frame update
    public void Restart()
    {
        SceneManager.LoadScene("Splashscreen");
        Destroy(FindObjectOfType<test>().gameObject);
    }
}
