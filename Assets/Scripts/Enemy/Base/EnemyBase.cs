using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMovable, ITriggerCheckable
{
    [SerializeField] public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public Rigidbody rigidBody { get; set; }

    public void Start()
    {
        currentHealth = maxHealth;
        rigidBody = GetComponent<Rigidbody>();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    public void Awake()
    {
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    public void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }


    #region ScriptableObject Variable

    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }

    #endregion

    #region State Machine 

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool isAggroed { get; set; }
    public bool isWithinStrikingDistance { get; set; }

    #endregion

    #region Enemy Damage and Die

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Enemy Movement 

    public void MoveEnemy(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
    }

    #endregion

    #region Animation

    private void AnimationTrigger(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTrigger(triggerType);
    }

    #endregion

    #region Trigger Check 

    public void setAggroedState(bool isAggroed)
    {
        this.isAggroed = isAggroed;
    }

    public void setStrikingDistancBool(bool isWithinStrikingDistance)
    {
        this.isWithinStrikingDistance = isWithinStrikingDistance;
    }

    #endregion

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSounds
    }

}
