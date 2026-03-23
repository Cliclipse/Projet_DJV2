using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BowTower" , menuName = "ScriptableObjet/TowerData/BowTower" , order = 0)]
public class BowTowerData : TowerData
{
    public float slowTime;
    public float slowSpeed;

    public float poisonTime;
    public float poisonDamages;
}
