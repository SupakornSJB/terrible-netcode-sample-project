using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot", menuName = "Enemy Logic/Attack Logic/Shoot")]
public class EnemyAttackShoot : EnemyAttackSOBase
{
    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Enter Attack Shoot State");
        if (allAttack.Count <= 0)
        {
            Debug.LogError("Enemy has no attack set");
            return;
        }
        allAttack[0].PerformAttack();
        enemy.StateMachine.ChangeState(enemy.IdleState);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        Debug.Log("Exit Attack Shoot State");
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void ResetValue()
    {
        base.ResetValue();
    }
}
