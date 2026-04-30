using UnityEngine.Playables;
using UnityEngine;

public class PlayTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playableDirector.Play();
        }
    }
}
