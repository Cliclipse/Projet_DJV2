using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;
    
    private UnityEvent<float> _onUpdate = new();
    private UnityEvent deathEvent = new();
    
    public float HealthPercentage => (float)_currentHealth / _maxHealth;

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _onUpdate?.Invoke(damage);
    }
    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        _onUpdate?.Invoke(healAmount);
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
    
    public void AddUpdateListener(UnityAction<float> action) => _onUpdate.AddListener(action);
    public void RemoveUpdateListener(UnityAction<float> action) => _onUpdate.RemoveListener(action);
    
    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0) Death();
    }
    
    
    
}
