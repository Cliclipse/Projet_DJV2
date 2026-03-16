using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;
    
    private UnityEvent deathEvent = new();

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }
    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
    }
    public void SetCurrentHealth(float currentHealth)
    {
        _currentHealth = currentHealth;
    }

    private void Death()
    {
        deathEvent.Invoke();
    }
    
    public void AddDeathListener(UnityAction deathListener) => deathEvent.AddListener(deathListener);
    public void RemoveDeathListener(UnityAction deathListener) => deathEvent.RemoveListener(deathListener);
    
    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0) Death();
    }
    
    
    
}
