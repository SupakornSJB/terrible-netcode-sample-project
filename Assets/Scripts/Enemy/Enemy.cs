using Unity.Netcode;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(NetworkObject))]
    public class Enemy : NetworkBehaviour
    {
        [SerializeField] private NetworkVariable<bool> isMoving = new NetworkVariable<bool>();
        [SerializeField] public NetworkVariable<bool> isReady = new NetworkVariable<bool>(false);
        private readonly NetworkVariable<int> enemyConfigId = new NetworkVariable<int>(-1); // crucial must be set to non positive 
        public EnemyScriptableObject enemyConfig;
        public GameObject followingPlayer;
        private bool hasFoundPlayer;

        private void Update()
        {
            if (!isMoving.Value || !isReady.Value) return;
            var direction = (followingPlayer.transform.position - transform.position).normalized;
            direction.y = 0f;
            transform.Translate(transform.TransformDirection(direction) * (enemyConfig.movementSpdStat * Time.deltaTime));
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            enemyConfigId.OnValueChanged += (_, current) =>
            {
                Initialize();
            };
        }

        public void InitializeConfigId(int id)
        {
            if (IsServer)
            {
                enemyConfigId.Value = id;
            }
        }

        GameObject FindClosestPlayer()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            GameObject closestPlayer = null;
            foreach (var player in players)
            {
                var distance = transform.position - player.transform.position;
                if (closestPlayer == null || closestPlayer.transform.position.sqrMagnitude > distance.sqrMagnitude)
                {
                    closestPlayer = player;
                }
            }

            return closestPlayer;
        }

        private void Initialize()
        {
            followingPlayer = FindClosestPlayer();
            enemyConfig = EnemySpawnManager.Instance.GetEnemyConfigById(enemyConfigId.Value);
            GetComponent<Renderer>().material = enemyConfig.mat;

            if (IsServer)
            {
                isMoving.Value = followingPlayer != null;
            }

            if (enemyConfig != null && followingPlayer != null)
            {
                isReady.Value = true;
            }
        }
    }
}
