using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraTargetMover : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float zoomSensibility = 3000f;

    [SerializeField] private Transform target;
    
    [SerializeField] private Vector3 initialPosition= Vector3.zero;
    [SerializeField] private float initialZoom = 55f;


    [SerializeField] private float maxX = 50f;
    [SerializeField] private float maxZ = 50f;
    [SerializeField] private float minX = -50f;
    [SerializeField] private float minZ = -50f;
    
    [SerializeField] private float maxZoom = 80f;
    [SerializeField] private float minZoom = 10f;

    private CinemachineVirtualCamera _virtualCamera;
    void Update()
    {
        target.transform.position += speed * Time.deltaTime * new Vector3( Input.GetAxis("Horizontal") , 0, Input.GetAxis("Vertical")).normalized;
        _virtualCamera.m_Lens.FieldOfView += zoomSensibility * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * -1;

        float positionX = Mathf.Clamp(target.transform.position.x, minX, maxX);
        float positionZ = Mathf.Clamp(target.transform.position.z, minZ, maxZ); //Clamp fait comme si je foustais dans la fonction min et max à la suite pour que àa dépasse pas
        
        target.transform.position= new Vector3(  positionX  , target.transform.position.y, positionZ);
        _virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(_virtualCamera.m_Lens.FieldOfView, minZoom, maxZoom);
    }

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        target.transform.position = initialPosition;
        _virtualCamera.m_Lens.FieldOfView = initialZoom;
        
    }
}
