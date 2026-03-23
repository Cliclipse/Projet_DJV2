using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    protected float duration;

    public void SetDuration(float newDuration)
    {
        duration = newDuration;
    }
    
    
    protected IEnumerator DestructionCooldown()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
