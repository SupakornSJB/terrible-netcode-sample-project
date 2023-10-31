using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;
    private GameObject player;
    private bool isReady = false;

    void Start()
    {
        enemy = GetComponent<Enemy>();

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

    void Update()
    {
        if (isReady)
        {
            transform.LookAt(player.transform);
        }
    }
}