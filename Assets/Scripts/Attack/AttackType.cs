using UnityEngine;

namespace Attack
{
    [CreateAssetMenu(fileName="New Attack Type", menuName="Scriptable Object /Attack type")]
    public class AttackType : ScriptableObject 
    {
        public int movingSpeed;
        public int damage;
        public Material mat;
    }
}
