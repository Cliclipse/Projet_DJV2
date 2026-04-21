using System.Collections;
using UnityEngine;

namespace Effects
{
    public class Effect : MonoBehaviour
    {
        protected float duration;

        public void SetDuration(float newDuration)
        {
            duration = newDuration;
        }
    
    
        protected virtual IEnumerator DestructionCooldown()
        {
            yield return new WaitForSeconds(duration);
            Destroy(gameObject);
        }
    }
}
