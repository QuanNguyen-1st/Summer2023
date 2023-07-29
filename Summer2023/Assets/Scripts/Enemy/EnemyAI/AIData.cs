using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    [HideInInspector] public List<Transform> Targets;
    [HideInInspector] public Collider2D[] Obstacles = null;

    [HideInInspector] public Transform CurrentTarget;

    public int GetTargetsCount() => Targets == null ? 0 : Targets.Count;
}
