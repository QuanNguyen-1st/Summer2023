using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _propSpawnPoints;
    [SerializeField] private List<GameObject> _propPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        SpawnProp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnProp() {
        if (_propSpawnPoints.Count == 0 || _propPrefabs.Count == 0) return;
        foreach (GameObject sp in _propSpawnPoints) {
            int rand = Random.Range(0, _propPrefabs.Count);
            GameObject prop = Instantiate(_propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
