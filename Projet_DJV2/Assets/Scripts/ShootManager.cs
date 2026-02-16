using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public  class ShootManager : MonoBehaviour
{
    protected int _projectilsShot;
    protected float _shotCooldown;
    protected float _projectilSpeed;
    protected float _projectileDamages;

    [SerializeField] protected Transform _target; //temporairement en serialize

    protected bool _hasTarget ;
    protected bool _inCooldown ;


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
        _hasTarget = (_target != null); // à améliorer en prenant en compte la range

    }
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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



}
