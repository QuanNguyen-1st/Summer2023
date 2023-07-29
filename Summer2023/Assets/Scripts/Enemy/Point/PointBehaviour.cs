using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehaviour : MonoBehaviour
{
    private EnemyBehaviour _parentEnemy;
    [SerializeField] private GameObject _outlineSprite;
    // Start is called before the first frame update
    void Start()
    {
        _parentEnemy = transform.parent?.gameObject.GetComponent<EnemyBehaviour>();
        // _outlineSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Line")) {
            _parentEnemy.SetInteractingLine(other.gameObject);
            _parentEnemy.UpdateCircle();
        }
    }

    public void TriggerOutline(bool trigger) {
        _outlineSprite.SetActive(trigger);
    }
}
