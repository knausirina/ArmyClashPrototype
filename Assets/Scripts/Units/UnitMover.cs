using UnityEngine;
using UnityEngine.AI;

public class UnitMover 
{
    private readonly NavMeshAgent _agent;
    private readonly Transform _transform;
    
    private readonly float RotationSpeed = 10f;

    public UnitMover(NavMeshAgent agent, Transform transform, float speed)
    {
        _agent = agent;
        _transform = transform;
        
        if (_agent != null)
            _agent.speed = speed;
    }

    public void MoveTo(Vector3 target)
    {
        if (!_agent.enabled)
            return;
        _agent.isStopped = false;
        _agent.SetDestination(target);
    }

    public void Stop()
    {
        if (_agent.enabled)
            _agent.isStopped = true;
    }

    public void Resume()
    {
        if (_agent.enabled)
            _agent.isStopped = false;
    }

    public void RotateTo(Vector3 targetPosition)
    {
        var direction = (targetPosition - _transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(
                _transform.rotation, 
                targetRotation, 
                Time.deltaTime * RotationSpeed
            );
        }
    }

    public void Disable()
    {
        if (!_agent.enabled)
            return;
        _agent.isStopped = true;
        _agent.enabled = false;
    }

    public void Enable()
    {
        if (_agent.enabled)
            return;
        
        _agent.enabled = true;
        _agent.isStopped = false;
        
    }
}
