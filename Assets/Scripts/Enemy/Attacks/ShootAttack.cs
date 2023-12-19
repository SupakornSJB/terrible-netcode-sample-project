using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Attack", menuName = "Enemy Logic/Attack Pattern/Shoot")]
public class ShootAttack : EnemyAttack
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletAmount = 1;
    [SerializeField] private float bulletSpeed = 1000.0f;
    [SerializeField] private float bulletDelay = 0.1f;
    private Rigidbody bulletRB;
    private GameObject bulletSpawn;

    public override void Initialize(GameObject targetPlayer, GameObject enemyGameObject)
    {
        base.Initialize(targetPlayer, enemyGameObject);
        bulletRB = bulletPrefab.GetComponent<Rigidbody>();
        bulletSpawn = enemy.transform.Find("BulletSpawn")?.gameObject;
        if (bulletRB == null)
            Debug.LogError("Bullet prefab does not have rigidbody");
        if (bulletSpawn == null)
            Debug.LogError("Bullet spawn is not found");
    }

    public override void PerformAttack()
    {
        enemy.PerformCoroutine(ShootAttackCoroutine());
    }

    private IEnumerator ShootAttackCoroutine()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            var newBullet = NetworkObjectPool.Singleton.GetNetworkObject(bulletPrefab, bulletSpawn.transform.position, enemy.transform.rotation);
            enemy.transform.LookAt(targetPlayer.transform);
            newBullet.Spawn();
            // Expensive Method Invocation
            newBullet.gameObject.GetComponent<Rigidbody>()?.AddForce(bulletSpeed * enemy.transform.forward, ForceMode.VelocityChange);
            yield return new WaitForSeconds(bulletDelay);
        }
    }
}
