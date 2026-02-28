using UnityEngine;

[CreateAssetMenu(fileName = "TowerData" , menuName = "ScriptableObjet/TowerData" , order = 1)]
public class TowerData : ScriptableObject
{
    public int cost;
    public int projectilsShot;
    public float shotCooldown;
    public float projectilSpeed;
    public float projectileDamages;
    
    public AudioClip castSound;

    //Ensuite faut voir comment on renseigne les améliorations, genere en met tout dans une tableau et la case 0 c'est lvl 1 etc ou si on fait un autre truc
}