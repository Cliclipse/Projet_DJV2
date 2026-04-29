using Level;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

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
        
        private UnityEvent<EnemyController> _onReachCastle = new();
        private UnityEvent<EnemyController> _onDeath = new();

        /// <summary>
        /// La cible de l'ennemi
        /// </summary>
        public Transform Target
        {
            get => _target;
            set => _target = value;
        }
        
        /// <summary>
        /// Données de l'ennemi
        /// </summary>
        public EnemyData EnemyData => enemyData;

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
            _health.AddDeathListener(HandleOnDeath);
        }

        /// <summary>
        /// Défini la cible de l'ennemi pour qu'il suive le chemin du level
        /// </summary>
        private void Update()
        {
            if (!_target || Vector3.Distance(_target.position, transform.position) <= 0.1f)
            {
                if (_levelController.IsLastPathPoint(_target))
                {
                    ReachCastle();
                    _target = null;
                }
                else
                {
                    _target = _levelController.GetNextPathPoint(_target);
                }
            }
            
            if (_target) _mover.Target(_target.position);
        }

        /// <summary>
        /// L'ennemi a atteint le château
        /// </summary>
        private void ReachCastle()
        {
            _onReachCastle?.Invoke(this);
            Destroy(gameObject);
        }
        
        /// <summary>
        /// L'ennemi a été tué
        /// </summary>
        private void HandleOnDeath()
        {
            _onDeath?.Invoke(this);
            Destroy(gameObject);
        }

        public void AddOnReachCastleListener(UnityAction<EnemyController> listener) => _onReachCastle.AddListener(listener);
        public void RemoveOnReachCastleListener(UnityAction<EnemyController> listener) => _onReachCastle.RemoveListener(listener);
        public void AddOnDeathListener(UnityAction<EnemyController> listener) => _onDeath.AddListener(listener);
        public void RemoveOnDeathListener(UnityAction<EnemyController> listener) => _onDeath.RemoveListener(listener);
    }
}
