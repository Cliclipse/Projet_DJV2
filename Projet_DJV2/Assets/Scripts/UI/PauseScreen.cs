using System.Collections;
using System.Collections.Generic;
using Level;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private readonly LevelController _levelController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
