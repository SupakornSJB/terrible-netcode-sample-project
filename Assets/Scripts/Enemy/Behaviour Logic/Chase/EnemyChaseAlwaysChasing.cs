using UnityEngine;

[CreateAssetMenu(fileName = "Always Chasing", menuName = "Enemy Logic/Chase Logic")]
public class EnemyChaseAlwaysChasing : EnemyChaseSOBase
{
    [SerializeField] private float _speed = 5.0f;

    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Entering Chase State");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        Debug.Log("Exiting Chase State");
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        transform.LookAt(playerTransform);
        transform.Translate(transform.forward * (_speed * Time.deltaTime));
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
