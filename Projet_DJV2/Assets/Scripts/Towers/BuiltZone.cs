using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enum;
using Towers;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BuiltZone : MonoBehaviour
{
    
    public void Construct(EnumTower.Tower towerType , Dictionary< EnumTower.Tower, TowerController> towersHashMap)
    {
        Instantiate( towersHashMap[towerType], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
