using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsIndicatorsManager : MonoBehaviour
{
   [SerializeField] private StatIndicator[] statIndicators;

   public void UpdateStats(params float[] stats) //Fonction à n param en général ca devrait être 3
   {
      for (int i = 0; i < stats.Length; i++)
      {
         statIndicators[i].UpdateStat(stats[i]);
      }
   }
}
