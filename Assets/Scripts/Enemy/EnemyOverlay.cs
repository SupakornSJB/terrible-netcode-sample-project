using TMPro;
using UnityEngine;

namespace Enemy
{
    public class EnemyOverlay : MonoBehaviour
    {
        private TextMeshProUGUI enemyOverlayText;
        private EnemyHealth health;

        private void Start()
        {
            enemyOverlayText = GetComponentInChildren<TextMeshProUGUI>();
            health = GetComponent<EnemyHealth>();

            if (enemyOverlayText != null && health != null) return;
            enabled = false;
        }

        private void Update()
        {
            enemyOverlayText.text = "HP: " + health.hpStat.Value;
        }
    }
}