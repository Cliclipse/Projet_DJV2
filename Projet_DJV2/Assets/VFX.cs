using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    private float _time;
    ParticleSystem _particle;
    
    // Start is called before the first frame update
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _time = _particle.main.duration;
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }

}
