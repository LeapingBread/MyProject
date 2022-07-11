using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy State")]
    public  int health;
    
    public bool isBoss;
    [Header("Movement")]
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected float speed;
    [Header("Attack")]
    [SerializeField] protected float specialAttackRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackCD;
    float attackCountDown;
    [Tooltip ("DO NOT SET VALUE IN UNITY")]
    public Transform targetPoint;
    GameObject alarmSign;
    protected bool enemyDead;
    
    Animator animator;
    public Animator Animator { get { return animator; } }
    protected Collider2D collider2D;
    protected Rigidbody2D rigidbody2D;
    [Tooltip("DO NOT SET VALUE IN UNITY")]
    public bool isRun;
    [Tooltip("DO NOT SET VALUE IN UNITY")]
    public bool isAttack;
    [Tooltip("DO NOT SET VALUE IN UNITY")]
    public bool hasBomb;

    [Tooltip("DO NOT SET VALUE IN UNITY")]
    public List<Collider2D> targetList = new List<Collider2D>();//for debug use
    
    [Header("FSM")]
    EnemyStatus currentStatus;
    ProtalStatus protalStatus = new ProtalStatus();
    AttackStatus attackStatus = new AttackStatus();
    [Header("SoundFX")]
    [SerializeField] protected SoundName attackSound;
    [SerializeField] protected SoundName diedSound;
    [SerializeField] protected SoundName specialSkillSound;

    public ProtalStatus ProtalStatus { get { return protalStatus; } }
    public AttackStatus AttackStatus { get { return attackStatus; } }

    protected virtual void Awake()
    {
        
        attackCountDown = 0f;
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        alarmSign = transform.GetChild(0).gameObject;
        
    }
    private void Start()
    {
        
        StatusChange(protalStatus);
        GameManager.Instance.AddEnemy(this);
    }
    protected virtual void Update()
    {
        SwitchAnimation();
        if (isBoss)
        {
            UIManager.Instance.UpdateBossHealthBar(health);
            UIManager.Instance.TrunOffBossHealthBar(health);
        }
        if (enemyDead)
        {
            GameManager.Instance.RemoveEnemy(this);
            return;
        }
        attackCountDown += Time.deltaTime;
        currentStatus.UpdateStatus(this);
        
    }
    private void OnEnable()
    {
        EventSystem.AfterSceneLoaded += OnAfterSceneLoaded;
    }
    private void OnDisable()
    {
        EventSystem.AfterSceneLoaded -= OnAfterSceneLoaded;
    }
    void OnAfterSceneLoaded()
    {
       // if (isBoss)
           // UIManager.Instance.ShowBossHealthBar(health);
    }
    public void StatusChange(EnemyStatus status)
    {
        currentStatus = status;
        currentStatus.StartStatus(this);
    }
    public void EnemyMover()
    {
        FlipeEnemy();
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed*Time.deltaTime);
        
        

    }
    void FlipeEnemy()
    {
        if (targetPoint.position.x > transform.position.x)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    public void SwitchPoint()
    {
        if (Mathf.Abs(transform.position.x - pointA.position.x) > Mathf.Abs(transform.position.x - pointB.position.x))
            targetPoint = pointA;
        else
            targetPoint = pointB;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!targetList.Contains(collision)&& !hasBomb && !GameManager.Instance.GameOver)
        {
            targetList.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        targetList.Remove(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!enemyDead&& !GameManager.Instance.GameOver)
        StartCoroutine(AlarmSign()); 
    }
    void SwitchAnimation()
    {
        animator.SetBool("IsRun", isRun);
        animator.SetBool("IsAttack", isAttack);
        animator.SetBool("IsDead", enemyDead);
    }
    public virtual void SpecialAttack()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < specialAttackRange)
        {
            if (attackCD < attackCountDown)
            {
                animator.SetTrigger("SpecialAttack");
                attackCountDown = 0;
                if (specialSkillSound != SoundName.None)
                {
                    EventSystem.CallPlaySoundEvent(specialSkillSound);
                    
                }
            }
        }
       
    }
    public virtual void Attack()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (attackCD < attackCountDown)
            {
                animator.SetTrigger("Attack");
                attackCountDown = 0;
                if (attackSound != SoundName.None)
                {
                    EventSystem.CallPlaySoundEvent(attackSound);
                    
                }
            }
            
        }
    }
    public virtual void GetHit(int damage)
    {
        if (!Animator.GetCurrentAnimatorStateInfo(1).IsName("GetHit"))
            health -= damage;
        if (health < 1)
        {
            health = 0;
            enemyDead = true;
            if(diedSound != SoundName.None)
             EventSystem.CallPlaySoundEvent(diedSound);
        }
        Animator.SetTrigger("GetHit");
    }
    IEnumerator AlarmSign()
    {
        alarmSign.SetActive(true);
        yield return new WaitForSeconds(animator.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        alarmSign.SetActive(false);
    }
    public void DestoryThisEnemy()//Animation event
    {
        Destroy(gameObject);
    }
}
