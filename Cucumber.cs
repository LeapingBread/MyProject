using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy,IDamagable
{
    
    public override void SpecialAttack()
    {
        base.SpecialAttack();
        
    }
    public override void Attack()
    {
        base.Attack();
    }
    public void BlowOff()//animation event
    {
        if (targetPoint.GetComponent<Bomb>() != null)
        {
            targetPoint.GetComponent<Bomb>().isOff = true;
            targetPoint.gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
    }

}
