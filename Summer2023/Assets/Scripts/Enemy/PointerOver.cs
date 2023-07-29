using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerOver : MonoBehaviour
{
    public SpriteRenderer Renderer;
    public EnemyStatSO EnemyData;
    public PointerOver[] Objects;
    private void Update() {
        
    }
    private void OnEnable() {
        Objects = FindObjectsOfType<PointerOver>();
    }
    private void OnDisable() {
        Objects = null;
    }
}
