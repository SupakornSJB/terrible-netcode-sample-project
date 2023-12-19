using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : NetworkBehaviour, IDamageable, ITriggerCheckable
{
    // TODO: Find a way to make everything works in netcode => This works partially
    // TODO: Remove unnecessary stuff (or make a decision about) such as attack list in enemy base
    public float maxHealth { get; set; }
    public NetworkVariable<float> currentHealth { get; set; } = new NetworkVariable<float>(0); // NetworkVariable must be initialized
    public Rigidbody rigidBody { get; set; }

    #region State ScriptableObject Variable

    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }

    #endregion

    #region State Machine Variable

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool isAggroed { get; set; }
    public bool isWithinStrikingDistance { get; set; }

    #endregion

    public void Start()
    {
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

    public override void OnNetworkSpawn()
    {
        currentHealth.Value = maxHealth;
    }

    #region Enemy Damage and Die Logic

    public void Damage(float damageAmount)
    {
        if (!IsServer) return;
        currentHealth.Value -= damageAmount;
        if (currentHealth.Value <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        StateMachine.ChangeState(IdleState);
        currentHealth.Value = maxHealth;

        var enemyNetworkObject = GetComponent<NetworkObject>();
        enemyNetworkObject.Despawn();
        // BUG (FATAL): gameObject can not be used in the ReturnNetworkObject function, must reference actual prefab
        NetworkObjectPool.Singleton.ReturnNetworkObject(enemyNetworkObject, gameObject);
    }

    #endregion

    #region Animation

    private void AnimationTrigger(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTrigger(triggerType);
    }

    #endregion

    #region Trigger Check 

    public void SetAggroedState(bool isAggroed)
    {
        this.isAggroed = isAggroed;
    }

    public void SetStrikingDistancBool(bool isWithinStrikingDistance)
    {
        this.isWithinStrikingDistance = isWithinStrikingDistance;
    }

    #endregion

    #region Attack

    public void PerformAttack(EnemyAttack attack)
    {
        attack.PerformAttack();
    }

    public void PerformCoroutine(IEnumerator ienumerator)
    {
        StartCoroutine(ienumerator);
    }

    public void PerformStopCoroutine(IEnumerator ienumerator)
    {
        StopCoroutine(ienumerator);
    }

    #endregion

    // Test animation trigger type - May not really be used
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSounds
    }
}
