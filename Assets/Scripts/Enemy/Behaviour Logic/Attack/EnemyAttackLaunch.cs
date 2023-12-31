using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Launch", menuName = "Enemy Logic/Attack Logic/Launch")]
public class EnemyAttackLaunch : EnemyAttackSOBase
{
    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Entering Random Attack State");
        var randomAtk = Random.Range(0, allAttack.Count);
        allAttack[randomAtk].PerformAttack();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        Debug.Log("Exiting Random Attack State");
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
