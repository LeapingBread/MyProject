using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueUI : MonoBehaviour
{
    public Text text;
    public GameObject dialoguePanle;
    public Image playerFace;
    public Image pirateFace;
    public Image cucumberFace;
    public Image whaleFace;
    public GameObject continueButton;

    private void OnEnable()
    {
        EventSystem.ShowDialogueUIEvent += OnShowDialogueUIEvent;
    }
    private void OnDisable()
    {
        EventSystem.ShowDialogueUIEvent -= OnShowDialogueUIEvent;
    }
    void OnShowDialogueUIEvent(DialoguePiece dialoguePiece)
    {
        StartCoroutine(DialogueUIRoutine(dialoguePiece));
    }
    IEnumerator DialogueUIRoutine(DialoguePiece dialoguePiece)
    {
        if (dialoguePiece != null)
        {
            dialoguePiece.isDone = false;
            dialoguePanle.SetActive(true);
            continueButton.SetActive(true);
            text.text = string.Empty;
            if (dialoguePiece.isPlayer)
            {
                playerFace.gameObject.SetActive(true);
                pirateFace.gameObject.SetActive(false);
                cucumberFace.gameObject.SetActive(false);
                whaleFace.gameObject.SetActive(false);
                playerFace.sprite = dialoguePiece.faceImage;
                playerFace.SetNativeSize();
            }
            if (dialoguePiece.isPirate)
            {
                playerFace.gameObject.SetActive(false);
                pirateFace.gameObject.SetActive(true);
                cucumberFace.gameObject.SetActive(false);
                whaleFace.gameObject.SetActive(false);
                pirateFace.sprite = dialoguePiece.faceImage;
                pirateFace.SetNativeSize();
            }
            if (dialoguePiece.isCucumber)
            {
                playerFace.gameObject.SetActive(false);
                pirateFace.gameObject.SetActive(false);
                cucumberFace.gameObject.SetActive(true);
                whaleFace.gameObject.SetActive(false);
                cucumberFace.sprite = dialoguePiece.faceImage;
                cucumberFace.SetNativeSize();
            }
            if (dialoguePiece.isWhale)
            {
                playerFace.gameObject.SetActive(false);
                pirateFace.gameObject.SetActive(false);
                cucumberFace.gameObject.SetActive(false);
                whaleFace.gameObject.SetActive(true);
                whaleFace.sprite = dialoguePiece.faceImage;
                whaleFace.SetNativeSize();
            }
            yield return text.DOText(dialoguePiece.dialogueText, 1f).WaitForCompletion();
            dialoguePiece.isDone = true;
        }
        else
            dialoguePanle.SetActive(false);
        yield break;
    }
   public void DialogueButtonEvent()
    {
        EventSystem.CallDialogueButtonEvent();
    }
}
