using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector : Detector
{
    [SerializeField]
    private float _detectionRadius = 2;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private bool _showGizmos = true;

    private Collider2D[] _colliders;

    public override void Detect(AIData aiData)
    {
        _colliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _layerMask);
        aiData.Obstacles = _colliders;
    }

    private void OnDrawGizmos()
    {
        if (_showGizmos == false)
            return;
        if (Application.isPlaying && _colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D obstacleCollider in _colliders)
            {
                Gizmos.DrawSphere(obstacleCollider.transform.position, 0.2f);
            }
        }
    }
}
