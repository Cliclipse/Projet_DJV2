using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Mover))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        
        private Mover _mover;

        private void Awake()
        {
            _mover =  GetComponent<Mover>();
            _mover.SetSpeed(enemyData.speed);
        }
    }
}
