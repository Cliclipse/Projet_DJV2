using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private float _speed; 
    public float angularSpeed = 300f;

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetAngularSpeed(float angularSpeed)
    {
        this.angularSpeed = angularSpeed;
    }
    
    public void Move(Vector3 direction)
    {
        transform.Translate(direction * _speed);   
    }

    public void Orienting(Vector3 direction , Transform mesh)
    {
        mesh.rotation = Quaternion.RotateTowards(mesh.rotation, Quaternion.LookRotation(direction), angularSpeed * Time.deltaTime);
    }
}