using Common;
using UnityEngine;

namespace Projectiles
{
    public class CrossbowBolt : Projectile
    {
        // Start is called before the first frame update
    
        void Start()
        {
            _mover = GetComponent<Mover>();
            _mover.SetSpeed(_speed);

            _targetAlive = true;
            _collisioned = false;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDirection();
            MoveToTarget();
        }

    }
}
