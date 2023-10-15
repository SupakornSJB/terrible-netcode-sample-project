using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    private Transform bulletSpawn;

    public void ShootBullet()
    {
        bulletSpawn = gameObject.transform.Find("BulletSpawn");
        if (_bulletPrefab != null && IsClient && IsOwner)
        {
            SpawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpawn.transform.position);
        }
    }

    [ServerRpc]
    private void SpawnBulletServerRpc(ulong localId, Vector3 bulletSpawnPosition)
    {
        GameObject bulletObj = Instantiate(_bulletPrefab, bulletSpawnPosition, transform.rotation);
        NetworkObject networkBulletObj = bulletObj.GetComponent<NetworkObject>();
        networkBulletObj.SpawnWithOwnership(localId);
        bulletObj.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.VelocityChange);
        StartCoroutine(DespawnBullet(networkBulletObj));
    }

    private IEnumerator DespawnBullet(NetworkObject bulletObj)
    {
        yield return new WaitForSeconds(10.0f);
        bulletObj.Despawn(true);
    }
}
