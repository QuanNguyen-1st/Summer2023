using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float _targetReachedThreshold = 0.5f;

    [SerializeField]
    private bool _showGizmo = true;

    private bool _reachedLastTarget = true;
    [SerializeField] private EnemyStatSO _enemyData;

    //gizmo parameters
    public Vector2 _targetPositionCached;
    private float[] _interestsTemp;
    private void Start() {
        _targetReachedThreshold = _enemyData.AttackDistance;
    }
    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        //if we don't have a target stop seeking
        //else set a new target
        if (_reachedLastTarget)
        {
            if (aiData.Targets == null || aiData.Targets.Count <= 0)
            {
                aiData.CurrentTarget = null;
                return (danger, interest);
            }
            else
            {
                _reachedLastTarget = false;
                aiData.CurrentTarget = aiData.Targets.OrderBy
                    (target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }

        }

        //cache the last position only if we still see the target (if the targets collection is not empty)
        if (aiData.CurrentTarget != null && aiData.Targets != null && aiData.Targets.Contains(aiData.CurrentTarget))
            _targetPositionCached = aiData.CurrentTarget.position;

        //First check if we have reached the target
        if (Vector2.Distance(transform.position, _targetPositionCached) <= _targetReachedThreshold)
        {
            _reachedLastTarget = true;
            aiData.CurrentTarget = null;
            return (danger, interest);
        }

        //If we havent yet reached the target do the main logic of finding the interest directions
        Vector2 directionToTarget = (_targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            //accept only directions at the less than 90 degrees to the target direction
            if (result > 0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }

            }
        }
        _interestsTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {

        if (_showGizmo == false)
            return;
        Gizmos.DrawSphere(_targetPositionCached, 0.2f);

        if (Application.isPlaying && _interestsTemp != null)
        {
            if (_interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < _interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * _interestsTemp[i]*2);
                }
                if (_reachedLastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(_targetPositionCached, 0.1f);
                }
            }
        }
    }
}
