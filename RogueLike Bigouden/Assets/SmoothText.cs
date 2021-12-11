using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SmoothText : MonoBehaviour
{
    private TextMeshProUGUI m_textMeshPro;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        m_textMeshPro.gameObject.GetComponent<TextMeshProUGUI>();

        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            m_textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {

                yield return new WaitForSeconds(1.0f);
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
