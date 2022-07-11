using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]

public class DialogueController : Singleton<DialogueController>
{
    [SerializeField] Enemy enemy;
    public List<DialoguePiece> dialougeList = new List<DialoguePiece> ();
    public Stack<DialoguePiece> dialogueStack;
   bool isTalking;
    public UnityEvent afterTalkingEvent;
    bool afterTalking;
    

    private void Awake()
    {
        FillingStack();
        
    }
    private void OnEnable()
    {
        EventSystem.DialogueButtonEvent += OnDialogueButtonEvent;
    }
    private void OnDisable()
    {
        EventSystem.DialogueButtonEvent -= OnDialogueButtonEvent;
    }
    void OnDialogueButtonEvent()
    {
        StartCoroutine(TalkRoutine());
    }

    void FillingStack()
    {
        dialogueStack = new Stack<DialoguePiece>();
        for(int i = dialougeList.Count-1;i>-1;i--)
        {
            dialougeList[i].isDone = false;
            dialogueStack.Push(dialougeList[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.enemies.Count <= 1)
        {
            StartCoroutine(TalkRoutine());
            afterTalking = true;
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemy.isBoss && afterTalking)
        {
            UIManager.Instance.ShowBossHealthBar(enemy.health);
            Destroy(gameObject);
        }
    }
    public void ContinueTalk()
    {
        StartCoroutine(TalkRoutine());
    }
    IEnumerator TalkRoutine()
    {
        isTalking = true;
        if(dialogueStack.TryPop(out DialoguePiece result))
        {
            
            EventSystem.CallShowDialogueUIEvent(result);
            EventSystem.CallGameStateEvent(GameState.Pause);
            yield return new WaitUntil(() => result.isDone);
            isTalking = false;
        }
        else
        {
            EventSystem.CallShowDialogueUIEvent(null);
            EventSystem.CallGameStateEvent(GameState.Start);
            if(afterTalkingEvent != null)
                afterTalkingEvent.Invoke();
            
        }
    }
}
