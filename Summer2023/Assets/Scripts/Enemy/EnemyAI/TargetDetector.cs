using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField]
    private float _targetDetectionRange = 5;

    [SerializeField]
    private LayerMask _obstaclesLayerMask, _playerLayerMask;

    [SerializeField]
    private bool _showGizmos = false;

    //gizmo parameters
    private List<Transform> _colliders;

    public override void Detect(AIData aiData)
    {
        //Find out if player is near
        Collider2D playerCollider = 
            Physics2D.OverlapCircle(transform.position, _targetDetectionRange, _playerLayerMask);
        if (playerCollider != null)
        {
            //Check if you see the player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = 
                Physics2D.Raycast(transform.position, direction, _targetDetectionRange, _obstaclesLayerMask);
            //Make sure that the collider we see is on the "Player" layer
            if (hit.collider != null && (_playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * _targetDetectionRange, Color.magenta);
                _colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                _colliders = null;
            }
        }
        else
        {
            //Enemy doesn't see the player
            _colliders = null;
        }
        aiData.Targets = _colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (_showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, _targetDetectionRange);

        if (_colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach (var item in _colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
