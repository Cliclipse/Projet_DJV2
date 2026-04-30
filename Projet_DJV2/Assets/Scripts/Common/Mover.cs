using UnityEngine;
using UnityEngine.AI;

namespace Common
{
    public class Mover : MonoBehaviour
    {
        private float _speed;
        public float angularSpeed = 300f;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

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
            if (_agent)
            {
                _agent.velocity = direction * _speed;
            }
            else
            {
                transform.Translate(direction * _speed);
            }
        }
    
        public void Target(Vector3 target)
        {
            if (_agent)
            {
                _agent.speed = _speed;
                _agent.angularSpeed = angularSpeed;
                _agent.SetDestination(target);
            }
        }

        public void Orienting(Vector3 direction , Transform mesh)
        {
            mesh.rotation = Quaternion.RotateTowards(mesh.rotation, Quaternion.LookRotation(direction), angularSpeed * Time.deltaTime);
        }
    }
}