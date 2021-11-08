using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueText", order = 2, menuName = "ScriptableObject/Dialogue Editor")]
public class DialogueSO : ScriptableObject
{
    public string[] dialogueLine;
}
