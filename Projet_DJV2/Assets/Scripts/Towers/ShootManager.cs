using System.Collections;
using Enemies;
using UnityEngine;

public  class ShootManager : MonoBehaviour
{
    [SerializeField] protected AudioSource castSound; 
    protected int _projectilsShot;
    protected float _shotCooldown;
    protected float _projectilSpeed;
    protected float _projectileDamages;
    protected float _range;
    

    [SerializeField] protected Transform _target; //temporairement en serialize

    protected bool _hasTarget ;
    protected bool _inCooldown ;

    [SerializeField] private Projectile projectile;

    protected void SpawnProjectile()
    {
        if (castSound != null) castSound.Play();
        Projectile lastProjectile = Instantiate(projectile , transform.position , Quaternion.identity);
        lastProjectile.SetSpeed(_projectilSpeed);
        lastProjectile.SetDamage(_projectileDamages);
        lastProjectile.SetTarget(_target);
    }
    

    protected IEnumerator ShootingCooldownCoroutine()
    {
        _inCooldown = true;
        yield return new WaitForSeconds(_shotCooldown);
        _inCooldown = false;
    }
    
    
    /*
 je vais avoir un prob c'est que si la tour a un transforme en tant que target
 , quand le mob meurt, elle va plus rien trouvé.
    Donc en vrai je ferais bien un listener qui lorsqu'un mob meurt
    en cherche un nouveau ?
 */
    protected void TargetSelection()
    {
        _hasTarget = (_target != null); //je regarde si ma target actuelle est morte ou si j'en ai plus'
        //Je peux pas faire un ou car si pas de target, target est false et donc on peut pas le rechercher et je suis obligé de faire ce truc bizarre
        if (_hasTarget)
        {
            if (Vector3.Magnitude(_target.transform.position - transform.position) > _range * _range)// Si l'ennemi est plus dans la range
            {
                Debug.Log("FindNewTarget");
                FindNewTarget();
            }
        }
        else
        {
            FindNewTarget();
        }
    }


    private void FindNewTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _range, LayerMask.GetMask("Enemy"));
        if (hits.Length == 0)
        {
            _target = null;
            _hasTarget = false;
            return;
        }

        Transform closest = null;
        float minMagn = Mathf.Infinity;

        foreach (Collider hit in hits)
        {

            EnemyController ec = hit.GetComponentInParent<EnemyController>();
            if (ec == null)
            { 
                Debug.Log("Problème dans la scène, l'objet suivant" + hit.gameObject.name+ " est sur le layer ennemy sans posséder d'EnnemyController dans ses parents");
            }
            Transform target = ec.target;
            float magn =  Vector3.Magnitude(target.transform.position - transform.position);
            if (magn < minMagn)
            {
                minMagn = magn;
                closest = target;
            }
        }

        if (closest != null)
        {
            _target = closest;
            _hasTarget = true;  
        }
    }
    
    
    
    
    
    public void SetProjectilsShot(int projectilsShot)
    {
        _projectilsShot = projectilsShot;
    }
    public void SetShotCooldown(float shotCooldown)
    {
        _shotCooldown = shotCooldown;
    }
    public void SetProjectileSpeed(float projectileSpeed)
    {
        _projectilSpeed = projectileSpeed;
    }
    public void SetProjectileDamages(float damages)
    {
        _projectileDamages = damages;
    }

    public void SetRange(float range)
    {
        _range = range;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }


}
