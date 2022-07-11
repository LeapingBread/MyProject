using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtalStatus : EnemyStatus
{
    public override void StartStatus(Enemy enemy)
    {
        enemy.SwitchPoint();
        enemy.isRun = false;
    }
    public override void UpdateStatus(Enemy enemy)
    {
        if (!enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            enemy.isRun = true;
            enemy.EnemyMover();
        }
        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01)
        {
           
            enemy.StatusChange(enemy.ProtalStatus);
        }
        if(enemy.targetList.Count>0)
        {
            enemy.StatusChange(enemy.AttackStatus);
        }
    }
}
