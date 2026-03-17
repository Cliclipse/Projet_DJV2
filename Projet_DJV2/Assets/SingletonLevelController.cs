using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLevelController : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectOfType<SingletonLevelController>() != this)
        {
            Debug.Log("Impossible de mettre en place le nouveau LevelController car il y'a déjà un LeveleController dans la scène");
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
