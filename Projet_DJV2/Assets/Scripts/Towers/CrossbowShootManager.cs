using UnityEngine;

namespace Towers
{
    public class CrossbowShootManager : ShootManager
    {
    
        private void Shoot()
        {
            if (_hasTarget && !_inCooldown)
            {
                SpawnProjectile();
                StartCoroutine(ShootingCooldownCoroutine());
            }
        }
        

        // Update is called once per frame
        void Update()
        {
            TargetSelection();
            Shoot();
        }


    }
}
