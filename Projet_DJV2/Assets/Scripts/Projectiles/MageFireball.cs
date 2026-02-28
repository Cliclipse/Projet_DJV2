using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    // Start is called before the first frame update
    
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


    protected override void Boum()
    {
        HitSound();
        Destroy(gameObject);
        //Implémenter l'overlap de l'aoe
    }
    
}
