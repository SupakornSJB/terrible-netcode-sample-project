using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private global::Enemy.Enemy enemy;
        private GameObject player;
        private bool isReady = false;

        private void Start()
        {
            enemy = GetComponent<global::Enemy.Enemy>();

            if (!enemy.isReady.Value)
            {
                enemy.isReady.OnValueChanged += (_, current) =>
                {
                    if (!current)
                    {
                        return;
                    }

                    player = enemy.followingPlayer;
                    isReady = true;
                };
            }
            else
            {
                player = enemy.followingPlayer;
                isReady = true;
            }
        }

        private void Update()
        {
            if (isReady)
            {
                transform.LookAt(player.transform);
            }
        }
    }
}