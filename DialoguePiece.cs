using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePiece
{
   
    [Header("DialogueContext")]
    
    public Sprite faceImage;
    public bool isPlayer;
    public bool isCucumber;
    public bool isPirate;
    public bool isWhale;
    [TextArea]
    public string dialogueText;
    public bool isDone;
    
}
