using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowShootManager : ShootManager
{
    [SerializeField] private CrossbowBolt projectile;
    
    private void Shoot()
    {
        if (_hasTarget && !_inCooldown)
        {
            Debug.Log("Projectile Instancié");
            CrossbowBolt lastBolt = Instantiate(projectile , transform.position , Quaternion.identity);
            lastBolt.SetSpeed(_projectilSpeed);
            lastBolt.SetDamage(_projectileDamages);
            lastBolt.SetDamage(_projectileDamages);
            lastBolt.SetTarget(_target);
            
            StartCoroutine(ShootingCooldownCoroutine());
        }

    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _inCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        TargetSelection();
        Shoot();
    }
}
