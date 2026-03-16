using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Mover))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        public Transform target;
            
        private Health _health;
        private Mover _mover;

        private void Awake()
        {
            _mover =  GetComponent<Mover>();
            _health = GetComponent<Health>();
            _mover.SetSpeed(enemyData.speed);
            
            _health.SetMaxHealth(enemyData.maxHealth);
            _health.SetCurrentHealth(enemyData.maxHealth);

        }

        private void Start()
        {
            _health.AddDeathListener(OnDeath);
        }
        private void OnDeath()
        {
            Debug.Log("Ennemi Tué");
            Destroy(gameObject); //moyen d'override ou de modif si on veut faire des ennemis particuliers du type slime qui se sépare ou jsp
        }


    }
}
