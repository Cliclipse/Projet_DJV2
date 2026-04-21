using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TowerDataLevel" , menuName = "ScriptableObjet/TowerDataLevel" , order = 4)]
    public class TowerDataLevel : ScriptableObject
    {
        public TowerData[] towerDatas;
    }
}
