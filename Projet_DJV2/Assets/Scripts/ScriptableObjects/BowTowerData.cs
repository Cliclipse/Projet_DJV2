using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BowTower" , menuName = "ScriptableObjet/TowerData/BowTower" , order = 0)]
    public class BowTowerData : TowerData
    {
        public bool HasSlow = true;
        public float slowTime;
        public float slowSpeed;
    
        public bool hasPoison = false;
    
        public float poisonTime;
        public float poisonDamages;
    }
}
