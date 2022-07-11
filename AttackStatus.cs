using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStatus : EnemyStatus
{
    public override void StartStatus(Enemy enemy)
    {
        enemy.isAttack = true;
        enemy.targetPoint = enemy.targetList[0].transform;
    }
    public override void UpdateStatus(Enemy enemy)
    {
        if (enemy.hasBomb)
            return;
       if(enemy.targetList.Count == 0)
        {
            enemy.isAttack = false;
            enemy.StatusChange(enemy.ProtalStatus);
        }
       if(enemy.targetList.Count>1)
        {
            for (int i = 0; i < enemy.targetList.Count; i++)
            {
                if (Mathf.Abs(enemy.transform.position.x - enemy.targetList[i].transform.position.x)<Mathf.Abs(enemy.transform.position.x- enemy.targetPoint.position.x))
                {
                    enemy.targetPoint = enemy.targetList[i].transform;
                }
            }
        }
       if(enemy.targetList.Count ==1)
        {
            enemy.targetPoint = enemy.targetList[0].transform;
        }
        if(enemy.targetPoint.CompareTag("Player"))
        {
            enemy.Attack();
        }
        else if(enemy.targetPoint.CompareTag("Bomb"))
        {
            enemy.SpecialAttack();
        }
        enemy.EnemyMover();

    }
}
