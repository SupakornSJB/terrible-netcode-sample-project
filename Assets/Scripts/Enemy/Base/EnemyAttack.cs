using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

// <summary>
// Enemy Attack is the base of all the attack patterns of the enemy
// It is NOT the object that is attached to the enemy prefab
// </summary>
public abstract class EnemyAttack : ScriptableObject 
{
    protected GameObject targetPlayer;
    protected EnemyBase enemy;
    protected GameObject enemyGameObject;

    public virtual void Initialize(GameObject targetPlayer, GameObject enemyGameObject)
    {
        this.targetPlayer = targetPlayer;
        this.enemy = enemyGameObject.GetComponent<EnemyBase>();
        this.enemyGameObject = enemyGameObject;
    }

    public abstract void PerformAttack();
}
