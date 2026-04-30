using System.Collections;
using System.Collections.Generic;
using Projectiles;
using UnityEngine;
using UnityEngine.Events;

public class MonoProjectilePool : MonoBehaviour
{
    
    [SerializeField] private int projectilesPoolSize = 150;
    [SerializeField] private Projectile projectilePrefabModel;
    

    private int _projectilesAvailable;

    public bool isPoolReady; // Sera utile quand on fera une sorte de barre de chargement
    
    private Projectile[] _projectilesPool; 
    
    //========= Unity event pour déclarer qu'il est prêt ==============//
    private UnityEvent _readyEvent = new();
    public void AddStateChangedListener(UnityAction listener) => _readyEvent.AddListener(listener);
    public void RemoveStateChangedListener(UnityAction listener) => _readyEvent.RemoveListener(listener);
    //================================================================//


    public Projectile GetAProjectile(Vector3 position, Quaternion rotation)
    {
        if (_projectilesAvailable > 0)
        {
            _projectilesAvailable--;
            Projectile projectileFourni = _projectilesPool[_projectilesAvailable];
            projectileFourni.transform.position = position;
            projectileFourni.transform.rotation = rotation;
            projectileFourni.gameObject.SetActive(true);
            projectileFourni._collisioned = false;
            return projectileFourni;
        }
            //Pool de ce projectile vide, augmentation automatique de la pool pas encore implémentée;
            return null;
    }
    public void PutBackAProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        _projectilesAvailable++;
        projectile.transform.position = Vector3.zero;
        projectile.transform.rotation = Quaternion.identity;
        
    }
    
    void Start()
    {
        
        isPoolReady = false;
        _projectilesPool = new Projectile[projectilesPoolSize];
        for (int i = 0; i < projectilesPoolSize; i++)
        {
            Projectile lastProj = Instantiate(projectilePrefabModel , transform);
            lastProj.gameObject.SetActive(false);
            _projectilesPool[i] = lastProj;
        }
        _projectilesAvailable = projectilesPoolSize;
        isPoolReady = true;
        _readyEvent.Invoke();
    }
}
