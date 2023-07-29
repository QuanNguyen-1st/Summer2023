using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private readonly List<Vector2> _points = new List<Vector2>();
    public float SelfDestructionTime;
    public List<GameObject> EnemiesInteracting;
    // Start is called before the first frame update
    void Start()
    {
        _collider.transform.position -= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        if (SelfDestructionTime >= 0) StartCoroutine(SelfDestroy()); 
    }

    IEnumerator SelfDestroy() {
        yield return new WaitForSeconds(SelfDestructionTime);
        KillLine();
    }

    public void SetPosition(Vector2 pos) {
        if (!CanAppend(pos)) return;

        _points.Add(pos);

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos);

        _collider.points = _points.ToArray();
    }

    private bool CanAppend(Vector2 pos) {
        if (_renderer.positionCount == 0) {
            return true;
        }
        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    public void KillLine() {
        foreach (GameObject enemy in EnemiesInteracting) {
            enemy.GetComponent<EnemyBehaviour>().ResetCircle();
        }
        Destroy(this.gameObject);
    }
}
