using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackHandler
{
    public override void Attack(Transform enemy)
    {
        base.Attack(enemy);
        Debug.Log("Do rangedAttack");
    }
}
