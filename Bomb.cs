using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float checkRediu;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float exploadTime;
    [SerializeField] float exploadForce;
    public float countTime;
    [SerializeField] float destoryTimeAfterBlow;
    float destorytime;
    [SerializeField] int damageForPlayer;
    [SerializeField] int damageForEnemy;
    public bool isOff;
    Collider2D collider;
    Rigidbody2D rigidbody2D;
    Animator animator;
    [SerializeField] SoundName explosionSound;
    [SerializeField] SoundName idelSound;
    

    private void Awake()
    {
        
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        destorytime = 0;
        
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
        Destroy(gameObject);
    }
    private void Update()
    {
        
        countTime += Time.deltaTime;
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Bomb_Off"))
        animator.SetBool("Expload", countTime >= exploadTime);
        animator.SetBool("IsOff", isOff);
    }
    public void Explotion()//animation event
    {
        collider.enabled = false;
        rigidbody2D.gravityScale = 0;
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position,checkRediu,targetLayer);
        foreach(var item in aroundObjects)
        {
            Vector2 pos = item.transform.position - transform.position;
            item.GetComponent<Rigidbody2D>().AddForce(pos * exploadForce * Vector3.up, ForceMode2D.Impulse);
            if(item.CompareTag("Bomb")&& item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bomb_Off"))
            {
                item.GetComponent<Animator>().Play("Bomb_Explode");  
            }
            if(item.CompareTag("Player"))
            {
                item.GetComponent<IDamagable>().GetHit(damageForPlayer);
            }
            if(item.CompareTag("Enemy"))
            {
                item.GetComponent<IDamagable>().GetHit(damageForEnemy);
            }
        }
       if(explosionSound != SoundName.None)
        {
            EventSystem.CallPlaySoundEvent(explosionSound);
        }
    }
    public void DestoryThis()//animation event
    {
        Destroy(gameObject);
    }



    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRediu);
    }
   
    public void IdelSoundFX()//Animation Soundfx
    {
        if (idelSound != SoundName.None)
            EventSystem.CallPlaySoundEvent(idelSound);
        
    }
}
