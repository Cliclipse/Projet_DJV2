using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData" , menuName = "ScriptableObjet/LevelData" , order = 2)]
public class LevelData : ScriptableObject
{
    public int initialGold;
    public int intialLife;
    public WaveData[] waves;
}
