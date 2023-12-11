using UnityEngine;

public class EnemyAggroedCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    public EnemyBase _enemy;

    public void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.setAggroedState(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.setAggroedState(false);
        }
    }
}
