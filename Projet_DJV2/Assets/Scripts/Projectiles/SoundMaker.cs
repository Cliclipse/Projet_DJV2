using System.Collections;
using UnityEngine;

namespace Projectiles
{
    public class SoundMaker : MonoBehaviour
    {
        public AudioSource audioSource;


        protected virtual IEnumerator PlayCoroutine()
        {        
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            Destroy(gameObject);
        }
        // Start is called before the first frame update
        protected void Start()
        {
            StartCoroutine(PlayCoroutine());
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
