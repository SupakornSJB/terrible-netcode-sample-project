using UnityEngine;
using System.Collections;

public interface IEnemyMovable 
{
    Rigidbody rigidBody { get; set; }
    void MoveEnemy(Vector3 velocity);
    IEnumerator MoveEnemyCoroutine();
}
