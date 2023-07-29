using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public const float RESOLUTION = .1f;
    private Camera _camera;
    [SerializeField] private Line _linePrefab;
    private Line _previousLine;
    private Line _currentLine;
    [SerializeField] private bool _selfKill;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        }
        if (Input.GetMouseButton(0)) {
            if (_currentLine != null) _currentLine.SetPosition(mousePos);
        }
        if (Input.GetMouseButtonUp(0) && _selfKill) {
            if (_currentLine != null) _currentLine.KillLine();
        }
    }
}
