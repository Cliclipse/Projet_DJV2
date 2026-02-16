using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        TargetSelection();
        Shoot();
    }


}
