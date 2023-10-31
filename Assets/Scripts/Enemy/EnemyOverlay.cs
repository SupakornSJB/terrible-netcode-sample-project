using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyOverlay : MonoBehaviour
{
    private TextMeshProUGUI enemyOverlayText;
    private EnemyHealth health;

    void Start()
    {
        enemyOverlayText = GetComponentInChildren<TextMeshProUGUI>();
        health = GetComponent<EnemyHealth>();

        if (enemyOverlayText == null || health == null)
        {
            Debug.LogError("Overlay text or enemy is null");
            enabled = false;
        }
    }

    void Update()
    {
        enemyOverlayText.text = "HP: " + health.HP_stat.Value;
    }
}