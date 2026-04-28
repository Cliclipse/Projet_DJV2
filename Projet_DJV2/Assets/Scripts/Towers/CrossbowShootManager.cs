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
    
    
    
        // Start is called before the first frame update
        void Start()
        {
            _inCooldown = false;
            towerAnimatorManager = GetComponent<TowerAnimatorManager>();
        }

        // Update is called once per frame
        void Update()
        {
            TargetSelection();
            Shoot();
        }


    }
}
