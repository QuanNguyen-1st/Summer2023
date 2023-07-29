using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _projectileRange;
    [SerializeField] private Rigidbody2D _rb;
    private Vector2 _startingDistance;
    // Start is called before the first frame update
    void Start()
    {
        _startingDistance = transform.position;
        MoveProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, _startingDistance) >= _projectileRange) {
            Destroy(this.gameObject);
        }
    }

    public void UpdateMoveSpeed(float moveSpeed) {
        _moveSpeed = moveSpeed;
    }

    public void UpdateProjectileRange(float projectileRange) {
        _projectileRange = projectileRange;
    }

    private void MoveProjectile() {
        _rb.AddForce(transform.right * _moveSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<KnockbackFeedback>().PlayerFeedback(this.gameObject);
            HealthManager.Instance?.AddHealth(-1);
            Destroy(this.gameObject);
        }
    }
}
