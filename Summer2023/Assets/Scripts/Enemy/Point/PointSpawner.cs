using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    [SerializeField] private EnemyStatSO _enemyStat;
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private GameObject _pointParent;
    void Start() {
        SpawnPoint();
    }
    private void SpawnPoint() {
        float deg = Random.Range(0, 180);
        float y = Mathf.Sin(deg * Mathf.Deg2Rad) * _enemyStat.PointRadius;
        float x = Mathf.Cos(deg * Mathf.Deg2Rad) * _enemyStat.PointRadius;

        int numberOfPoints = _enemyStat.NumberOfPoints;

        float degLeft = 360 / numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++) {
            deg += degLeft;
            float newY = Mathf.Sin(deg * Mathf.Deg2Rad) * _enemyStat.PointRadius;
            float newX = Mathf.Cos(deg * Mathf.Deg2Rad) * _enemyStat.PointRadius;
            GameObject point = Instantiate(_pointPrefab, new Vector3(newX, newY) + _pointParent.transform.position, Quaternion.identity);
            point.transform.parent = _pointParent.transform;
        }
    }
}
