using System.Collections;
using UnityEngine;

namespace Effects
{
    public class Poison : Effect
    {
        private int _damagePerSecond;
        private Health _health;
        private bool _onCD;

        public void SetDamagePerSecond(int newDamagePerSecond)
        {
            _damagePerSecond = newDamagePerSecond;
        }

        private IEnumerator DoPoisonDamages()
        {
            if (!_onCD)
            {
                _health.TakeDamage(_damagePerSecond);
                _onCD = true;
            }
            else
            {
                yield return new WaitForSeconds(1);
                _onCD = false;
            }
        }

        void Start()
        {
            if (!TryGetComponent<Health>(out _health)) Debug.Log("Poison infligé à une entité sans composant Health");
            StartCoroutine(DestructionCooldown());
            StartCoroutine(DoPoisonDamages());
        }

    }
}
