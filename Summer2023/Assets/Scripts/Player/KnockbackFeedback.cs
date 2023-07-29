using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _strength = 16, _delay = 0.15f;
    public UnityEvent OnBegin, OnDone;
    public void PlayerFeedback(GameObject sender) {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 dir = (transform.position - sender.transform.position).normalized;
        _rb.AddForce(dir * _strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private IEnumerator Reset() {
        yield return new WaitForSeconds(_delay);
        _rb.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
