using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    
    [SerializeField] private MonoProjectilePool[] pools;

    private Dictionary<EnumTower.Tower, MonoProjectilePool> _hashMapTowerTypePool = new();
    private int _nbPoolReady;

        void Awake()
        {
            Instance = this;
            _nbPoolReady = 0;
            for (int i = 0; i < pools.Length; i++)
            {
                MonoProjectilePool instance = Instantiate(pools[i], transform);
                pools[i] = instance;
                instance.AddStateChangedListener(PoolFinish);
            }
            // A refaire avec un truc qui parcourt l'enum et tout mais + tard, un peu moche mais fonctionnel
            _hashMapTowerTypePool[EnumTower.Tower.Crossbow] = pools[0];
            _hashMapTowerTypePool[EnumTower.Tower.Mage] = pools[1];
            _hashMapTowerTypePool[EnumTower.Tower.Bow] = pools[2];

        }

    public MonoProjectilePool GetPool(EnumTower.Tower type) => _hashMapTowerTypePool[type];
    
    
    private void PoolFinish()
    {
        _nbPoolReady++;
        if (_nbPoolReady == pools.Length)
        {
            Debug.Log("Toutes les pools sont prêtes rajoute le truc pour envoyer ça à ton système de chargement ici");
        }
    }
}


