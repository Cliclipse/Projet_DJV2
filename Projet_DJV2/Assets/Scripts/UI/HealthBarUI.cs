using System;
using UnityEngine;

namespace UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private RectTransform healthBar;

        private Camera _camera;
        private Health _health;
        
        private void Awake()
        {
            _health = GetComponentInParent<Health>();
            _health.AddUpdateListener(OnUpdate);
        }
        
        private void OnEnable()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            transform.rotation = Quaternion.Euler(_camera.transform.rotation.eulerAngles.x, 0, 0);
        }

        private void OnUpdate(float damage)
        {
            healthBar.anchorMax = new Vector2(_health.HealthPercentage, 1);
        }
    }
}
