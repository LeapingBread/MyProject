using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuy : Enemy, IDamagable
{
    
    [SerializeField] Transform pickUpPoint;
    [SerializeField] float throwPower;
    public override void SpecialAttack()
    {
        base.SpecialAttack();

    }
    public override void Attack()
    {
        base.Attack();
    }

    
    public void PickUpBomb()//animation event
    {
        if(targetPoint.CompareTag("Bomb")&& !hasBomb)
        {
            targetPoint.gameObject.GetComponent<Bomb>().countTime = 0;
            targetPoint.gameObject.transform.position = pickUpPoint.position;
            targetPoint.SetParent(pickUpPoint);
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            hasBomb = true;
        }
    }
    public void ThrowBomb()//animation event
    {
        if(hasBomb)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            targetPoint.SetParent(transform.parent.parent);
            if (FindObjectOfType<PlayerMovement>().gameObject.transform.position.x - transform.position.x < 0)
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * throwPower, ForceMode2D.Impulse);
            else
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * throwPower, ForceMode2D.Impulse);
        }
        hasBomb = false;
    }
}
