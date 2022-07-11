using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Captain : Enemy,IDamagable
{
    SpriteRenderer spriteRenderer;
    
    bool isFinished;
    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
   
    protected override void Update()
    {
        base.Update();
        if (!isAttack)
            spriteRenderer.flipX = false;
        if(enemyDead&& !isFinished)
        {
            isFinished = true;
            EventSystem.CallShowGameFinishedEvent(isFinished);
            
        }
       
    }
    public override void SpecialAttack()
    {
        base.SpecialAttack();
        if(Animator.GetCurrentAnimatorStateInfo(1).IsName("Special Skill"))
        {
            spriteRenderer.flipX = true;
            if(transform.position.x > targetPoint.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, speed * 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * 2 * Time.deltaTime);
            }
        }
        else
        spriteRenderer.flipX = false;

    }
    public override void Attack()
    {
        base.Attack();
    }

}
