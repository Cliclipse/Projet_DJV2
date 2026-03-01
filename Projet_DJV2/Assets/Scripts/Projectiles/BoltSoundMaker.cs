using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Je fais un soundMaker un peu different car je veux plusieurs cris dfifferent vu que le crossbowtire à la chaine
public class BoltSoundMaker : SoundMaker
{
    [SerializeField] AudioClip[] audioClips;
    // Start is called before the first frame update
    
    
    protected override  IEnumerator PlayCoroutine()
    {        
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
    
}
