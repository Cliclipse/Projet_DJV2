using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimatorManager : MonoBehaviour
{
    // Je fais une machine à état ultra simple. 4 états avec des prio:
    //Dead(defeat) > Spawn/Upgrade > Attacking > Idle
    //Ensuite je pense je rajouterai des particules à l'update
    [SerializeField] private Animator animator;
    [SerializeField] private Transform meshTransform;
    
    private Vector3 _directionToTarget = Vector3.zero;

    public void SetDirectionToTarget(Vector3 directionToTarget)
    {
        Debug.Log(directionToTarget);
        _directionToTarget = directionToTarget;
    }

    public void SetDeadState(bool isDead)
    {
        animator.SetBool("Defeat", isDead);
    }
    
    public void SetUpdatedState(bool updated)
    {
        animator.SetBool("Updated", updated);
        StartCoroutine(UpdateCoroutine());
    }
    
    public void SetAttackingState(bool attacking)
    {
        animator.SetBool("Attacking", attacking);
    }

    private IEnumerator UpdateCoroutine()
    {
        yield return  new WaitForSeconds(1.09f);
        animator.SetBool("Updated", false);
    }

    void Update()
    {
        if (_directionToTarget != Vector3.zero)
        {
            meshTransform.rotation = Quaternion.LookRotation(_directionToTarget);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
    
    
}
