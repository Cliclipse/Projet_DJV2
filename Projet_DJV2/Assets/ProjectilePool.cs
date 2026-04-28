using System.Collections;
using System.Collections.Generic;
using Projectiles;
using UnityEngine;

public class MonoProjectilePool : MonoBehaviour
{
    
    [SerializeField] private int projectilesPoolSize = 150;
    [SerializeField] private Projectile projectilePrefabModel;
    

    private int _projectilesAvailable;

    public bool isPoolReady; // Sera utile quand on fera une sorte de barre de chargement
    
    private Projectile[] _projectilesPool;

    public Projectile GetAProjectile(Vector3 position, Quaternion rotation)
    {
        if (_projectilesAvailable > 0)
        {
            _projectilesAvailable--;
            Projectile projectileFourni = _projectilesPool[_projectilesAvailable];
            projectileFourni.transform.position = position;
            projectileFourni.transform.rotation = rotation;
            projectileFourni.gameObject.SetActive(true);
            return projectileFourni;
        }
            Debug.Log("Pool de ce projectile vide, augmentation automatique de la pool pas encore implémentée");
            return null;
    }
    public void PutBackAProjectile(Projectile projectile)
    {
        if (_projectilesAvailable > 0)
        {
            projectile.gameObject.SetActive(false);
            _projectilesAvailable++;

        }
    }

    
    void Start()
    {
        isPoolReady = false;
        for (int i = 0; i < projectilesPoolSize; i++)
        {
            Instantiate(projectilePrefabModel);
            projectilePrefabModel.gameObject.SetActive(false);
            _projectilesPool[i] = projectilePrefabModel;

        }
        _projectilesAvailable = projectilesPoolSize;
        isPoolReady = true;
    }
}
