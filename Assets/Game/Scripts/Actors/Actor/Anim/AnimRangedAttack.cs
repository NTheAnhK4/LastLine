
using UnityEngine;

public class AnimRangedAttack : MonoBehaviour
{
    public void HandeRangedAttack()
    {
        RangedAttack rangedAttack = transform.parent.GetComponentInChildren<RangedAttack>();
        if (rangedAttack == null)
        {
            Debug.LogWarning("Ranged attack is null");
            return;
        }
        
        if(rangedAttack != null) rangedAttack.Shoot();
    }
}
