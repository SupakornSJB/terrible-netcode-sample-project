using UnityEngine;

public interface IEnemyMovable 
{
    Rigidbody rigidBody { get; set; }
    
    void MoveEnemy(Vector3 velocity);
}
