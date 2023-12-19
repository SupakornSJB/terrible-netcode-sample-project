using UnityEngine;

[CreateAssetMenu(fileName = "Standing Still", menuName = "Enemy Logic/Idle Logic/Standing Still")]
public class EnemyIdleStandingStill : EnemyIdleSOBase
{
    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Entering Idle State");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        Debug.Log("Exiting Idle State");
    }

    public override void DoFrameUpdateLogic()
    {
        if (enemy.isAggroed)
        {
            Debug.Log("Enemy is aggroed!!");
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
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
