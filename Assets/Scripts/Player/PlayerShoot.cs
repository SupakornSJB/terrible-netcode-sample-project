using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Serialization;

public class PlayerShoot : NetworkBehaviour
{
    [FormerlySerializedAs("_bulletPrefab")] [SerializeField] GameObject bulletPrefab;
    private Transform bulletSpawn;

    public void ShootBullet()
    {
        bulletSpawn = gameObject.transform.Find("BulletSpawn");
        if (bulletPrefab != null && IsClient && IsOwner)
        {
            SpawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpawn.transform.position);
        }
    }

    [ServerRpc]
    private void SpawnBulletServerRpc(ulong localId, Vector3 bulletSpawnPosition)
    {
        var bulletObj = Instantiate(bulletPrefab, bulletSpawnPosition, transform.rotation);
        var networkBulletObj = bulletObj.GetComponent<NetworkObject>();
        networkBulletObj.SpawnWithOwnership(localId);
        bulletObj.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.VelocityChange);
        StartCoroutine(DespawnBullet(networkBulletObj));
    }

    private static IEnumerator DespawnBullet(NetworkObject bulletObj)
    {
        yield return new WaitForSeconds(10.0f);
        bulletObj.Despawn(true);
    }
}
