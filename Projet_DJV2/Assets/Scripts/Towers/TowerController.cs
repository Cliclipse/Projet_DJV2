using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private TowerData towerData;
    
    private int _cost;


    private ShootManager _shootManager;
    
    private 
    // Start is called before the first frame update
    void Start()
    {
        _shootManager = GetComponent<ShootManager>();
        
        _shootManager.SetProjectilsShot(towerData.projectilsShot);
        _shootManager.SetProjectileSpeed(towerData.shotCooldown);
        _shootManager.SetProjectileDamages(towerData.projectileDamages);
        _shootManager.SetShotCooldown(towerData.shotCooldown);
        
        
        _cost = towerData.cost;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
