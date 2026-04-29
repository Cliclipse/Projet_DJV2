using Enum;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected EnumTower.Tower projectileType;
        
        [SerializeField] private Transform mesh;
        [SerializeField] private SoundMaker soundMaker;

        protected Mover _mover;

    
    
        protected float _speed;
        protected float _damage;
    
        protected Transform _target;
        private Vector3 lastPlaceOfTarget;
        protected Vector3 _direction;
    
        protected bool _targetAlive;

        public bool isReturned; //Pour éviter le problème des ennemis superposé qui font retourner plusieurs fois le projectile


        protected void PutBackInPool()
        {
            if (!isReturned)
            {
                isReturned = true;
                PoolManager.Instance.GetPool(projectileType).PutBackAProjectile(this);
            }
        }

        protected void HitSound()
        {
            Instantiate(soundMaker, transform.position, transform.rotation);
        }

        protected virtual void Boum()
        {
            if (_targetAlive)
            {
                Health health = _target.gameObject.GetComponentInParent<Health>();
                health.TakeDamage(_damage);
            }
            HitSound();
            PutBackInPool();
        }

        //Me faut une fonction pour check que l'ennemi est pas mort d'une autre tour sinon ca va buguer sur le refresh Update
        protected void CheckTargetStillAlive()
        {
            _targetAlive = (_target != null);
        }
    
        protected void MoveToTarget()
        {
            _mover.Move(_direction);
            //_mover.Orienting(_direction, this); Fais des trucs étranges
            if (!_targetAlive && transform.position == lastPlaceOfTarget) Destroy(gameObject); //Je détruis le projectile quand il arrive où devait être sa cible

        }

        protected void UpdateDirection()
        {
            CheckTargetStillAlive();
            if (_targetAlive)
            {
                _direction = (_target.position - transform.position).normalized;
                lastPlaceOfTarget = _target.position;
            }

        }
    
    
        public void SetTarget(Transform target)
        {
            //Ici je pourrais ajouter une condition pour check qu'il a bien le tag ennemi ensuite
            _target = target;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
    
        public void SetDamage(float damage)
        {
            _damage = damage;
        }
    
        public float GetDamage()
        {
            return _damage;
        }
    
    }
}
