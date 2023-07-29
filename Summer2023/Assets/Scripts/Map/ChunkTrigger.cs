using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController _mapController;
    [SerializeField] private GameObject _targetMap;
    // Start is called before the first frame update
    void Start()
    {
        _mapController = FindObjectOfType<MapController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _mapController.CurrentChunk = _targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (_mapController.CurrentChunk == _targetMap) {
                _mapController.CurrentChunk = null;
            }
        }
    }
}
