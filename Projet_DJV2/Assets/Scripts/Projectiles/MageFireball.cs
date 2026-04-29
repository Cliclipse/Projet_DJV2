using System.Linq;
using Enemies;
using UnityEngine;

namespace Projectiles
{
    public class Fireball : Projectile
    {
        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private float explosionRange;
    
        void Start()
        {
            _mover = GetComponent<Mover>();
            _mover.SetSpeed(_speed);

            _targetAlive = true;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDirection();
            MoveToTarget();
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy")) Boum();
        }


        //Je peux pas juste hit tous les ennemis car on va trigger plusieurs colliders de chaque ennemi

        private void AOE(Collider[] hits)
        {
            GameObject[] alreadyHits = new GameObject[hits.Length];
            foreach (Collider col in hits)
            {
                GameObject go = col.GetComponentInParent<EnemyController>().gameObject;
                if (!alreadyHits.Contains(go))
                {
                    go.GetComponent<Health>().TakeDamage(_damage);
                    alreadyHits.Append(go);
                }
            }
        
        }
        protected override void Boum()
        {
            //
        
        
            HitSound();
            Instantiate(_explosion , transform.position , Quaternion.identity);

            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRange, LayerMask.GetMask("Enemy"));
            AOE(hits);

            PutBackInPool();
            //Implémenter l'overlap de l'aoe (je crois c fait)
        }
    }
}
