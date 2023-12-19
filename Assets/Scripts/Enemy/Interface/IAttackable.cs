using System.Collections;
using System.Collections.Generic;

public interface IAttackable 
{
    List<EnemyAttack> AllowableAttackList { get; set; }
    void PerformAttack(EnemyAttack attack);
}
