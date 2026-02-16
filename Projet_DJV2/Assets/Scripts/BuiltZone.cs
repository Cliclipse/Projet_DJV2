using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiltZone : MonoBehaviour
{
    
    protected Camera _mainCamera;
    
    protected LevelController levelController;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        _mainCamera = camObj.GetComponent<Camera>();
    }

    protected void ClickManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Transform clicked = hit.collider.gameObject.transform.parent;
                if (transform == clicked)
                {
                    levelController.OpenShop(this);
                }
                else
                {
                    levelController.CloseShop();
                }
            }
        } 
    }
    
    // Update is called once per frame
    void Update()
    {
        ClickManager();
    }
}
