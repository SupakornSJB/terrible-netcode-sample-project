using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cooldown", menuName = "Enemy Logic/Idle Logic/Cooldown")]
public class EnemyIdleCooldown : EnemyIdleSOBase
{
    [SerializeField] private float cooldownTime = 3.0f;

    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Entering Cooldown State");
        enemy.PerformCoroutine(CoolDown());
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        Debug.Log("Exiting Cooldown State");
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

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(cooldownTime);
        // TODO: Maybe find a way to change the state at certain condition 
        // rather than change it after the cooldown ends rightaway
        enemy.StateMachine.ChangeState(enemy.ChaseState);
    }
}
