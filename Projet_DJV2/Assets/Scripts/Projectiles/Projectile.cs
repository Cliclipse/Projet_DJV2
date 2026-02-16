using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    [SerializeField] private Transform mesh;
    protected Mover _mover;

    
    
    protected float _speed;
    protected float _damage;
    
    protected Transform _target;
    protected Vector3 _direction;
    
    protected bool _targetAlive;





    protected void Boum()
    {
        if (_targetAlive)
        {
            Debug.Log("Boum");
        }
        else
        {
            Debug.Log("Boum mais il était déjà mort de toute façon");
        }
        Destroy(gameObject);
    }

    //Me faut une fonction pour check que l'ennemi est pas mort d'une autre tour sinon ca va buguer sur le refresh Update
    protected void CheckTargetStillAlive()
    {
        _targetAlive = (_target != null);
    }
    
    protected void MoveToTarget()
    {
        _mover.Move(_direction);
        _mover.Orienting(_direction, mesh);
        if (!_targetAlive && transform.position == _target.position) Destroy(gameObject); //Je détruis le projectile quand il arrive où devait être sa cible

    }

    protected void UpdateDirection()
    {
        CheckTargetStillAlive();
        if (_targetAlive) _direction = (_target.position - transform.position).normalized;
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
