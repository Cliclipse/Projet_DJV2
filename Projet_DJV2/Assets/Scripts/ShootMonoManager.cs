using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonoManager : ShootManager
{
    [SerializeField] private Projectile projectile;
    
    private void Shoot()
    {
        if (_hasTarget && !_inCooldown)
        {
            Debug.Log("Projectile Instancié");
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
