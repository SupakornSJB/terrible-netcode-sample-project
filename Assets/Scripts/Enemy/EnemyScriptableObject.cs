using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Object/Enemy Type")]
    public class EnemyScriptableObject : ScriptableObject 
    {
        public new string name;
        public string description;
        [FormerlySerializedAs("HP_stat")] public int hpStat;
        [FormerlySerializedAs("ATK_stat")] public int atkStat;
        [FormerlySerializedAs("DEF_stat")] public int defStat;
        [FormerlySerializedAs("Movement_SPD_stat")] public int movementSpdStat;
        public Material mat;
        public bool isFlying = false;
        public bool isMelee = false;
        public float normalRangeFromPlayer;
    }
}
