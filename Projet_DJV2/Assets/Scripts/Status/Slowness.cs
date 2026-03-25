using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slowness : Status
{
    private float slowedSpeed;
    void Start()
    {
        if (!TryGetComponent<NavMeshAgent>(out NavMeshAgent navMeshAgent))
        {
            Debug.Log("Verif avec la vitesse et l'acceleration ");
            StartCoroutine(DestructionCooldown(navMeshAgent));
        }
        else
        {
            Debug.Log("Effet de Slow infligé mais pas de Nav mesh Agent sur l'ennemi");
        }
    }
    
    
    protected IEnumerator DestructionCooldown(NavMeshAgent navMeshAgent)
    {
        navMeshAgent.speed *= slowedSpeed;
        yield return new WaitForSeconds(duration);
        navMeshAgent.speed /= slowedSpeed;
        Destroy(gameObject);
    }
}
