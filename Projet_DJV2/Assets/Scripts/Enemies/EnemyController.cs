using System;
using Level;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Mover))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        
        private Transform _target;
        private Health _health;
        private Mover _mover;
        private LevelController _levelController;

        public Transform Target
        {
            get => _target;
            set => _target = value;
        }

        private void Awake()
        {
            _mover =  GetComponent<Mover>();
            _health = GetComponent<Health>();
            _mover.SetSpeed(enemyData.speed);
            
            _health.SetMaxHealth(enemyData.maxHealth);
            _health.SetCurrentHealth(enemyData.maxHealth);
            
            _levelController = FindAnyObjectByType<LevelController>(); 
            if (_levelController == null)
            {
                Debug.Log("No level controller found by the ennemy");
            }
        }

        private void Start()
        {
            _health.AddDeathListener(OnDeath);
        }
        
        private void OnDeath()
        {
            if (_levelController != null) _levelController.gold += enemyData.reward;
            Destroy(gameObject); //moyen d'override ou de modif si on veut faire des ennemis particuliers du type slime qui se sépare ou jsp
        }

        private void Update()
        {
            if (!_target || Vector3.Distance(_target.position, transform.position) <= 0.1f)
            {
                _target = _levelController.GetNextPathPoint(_target);
            }
            
            _mover.Target(_target.position);
        }
    }
}
