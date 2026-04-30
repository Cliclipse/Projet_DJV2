using UnityEngine.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;

    void Start()
    {
        playableDirector.Play();
        playableDirector.stopped += OnCinematicFinished;
    }

    private void OnCinematicFinished(PlayableDirector director)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
