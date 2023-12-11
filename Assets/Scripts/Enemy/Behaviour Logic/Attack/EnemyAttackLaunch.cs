using UnityEngine;

[CreateAssetMenu(fileName = "Launch", menuName = "Enemy Logic/Attack Logic")]
public class EnemyAttackLaunch : EnemyAttackSOBase
{
    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Entering Attack Launch State");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        Debug.Log("Exiting Attack Launch State");
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
