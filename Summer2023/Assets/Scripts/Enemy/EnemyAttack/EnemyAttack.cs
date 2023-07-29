using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [HideInInspector] public Vector2 PointerInput;
    public abstract void Attack();
    public virtual void SetPointerInput(Vector2 pointerInput) {
        PointerInput = pointerInput;
    }
}
