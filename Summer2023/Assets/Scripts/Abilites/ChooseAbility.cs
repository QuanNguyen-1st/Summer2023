using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Summer2023/Ability Data/Choose Enemy", order = 0)]
public class ChooseAbility : AbilitySO
{
    private float _timeAllowed;
    // private bool _deactivated = false;
    // [SerializeField] private GameObject _pointAbility;
    public override void  Activate() {
        // _deactivated = false;
        TriggerEnemyEvent(true);
        
        Time.timeScale = Extra.SlowTimeScale;
        // while (AbilityHolderForChoose.ChosenType == null && !_deactivated);
        
        // Debug.Log("A");
        // TriggerEnemyEvent(false);
        // if (!_deactivated) {
        //     EnemyStatSO typeChosen = AbilityHolderForChoose.ChosenType.EnemyData;
        
        //     List<GameObject> points = ShowPoints(typeChosen.NumberOfPoints);
            
        //     // while (AbilityHolderForChoose.CurrentNumberOfPoints != typeChosen.NumberOfPoints);
        //     MonoInstance.Instance.StartCoroutine(WaitForCond(AbilityHolderForChoose.CurrentNumberOfPoints, typeChosen.NumberOfPoints));
        //     GoodByeEnemies(typeChosen);
        // }
        
    }
    public override void Deactivate()
    {
        // _deactivated = true;
        TriggerEnemyEvent(false);
        // AbilityHolderForChoose.ChosenType = null;
        Time.timeScale = 1;  
    }
    private List<GameObject> ShowPoints(int numberOfPoints)
    {
        List<GameObject> points = new List<GameObject>();
        for (int i = 0; i < numberOfPoints; i++) {
            float screenX = UnityEngine.Random.Range(0.0f, Camera.main.pixelWidth);
            float screenY = UnityEngine.Random.Range(0.0f, Camera.main.pixelHeight);
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, 0));
            // GameObject point = Instantiate(_pointAbility, pos, Quaternion.identity);
            // points.Add(point);
        }
        return points;
    }
    private void GoodByeEnemies(EnemyStatSO typeChosen) {
        PointerOver[] objects = FindObjectsOfType<PointerOver>();
        foreach (PointerOver obj in objects) {
            if (obj.EnemyData == typeChosen) { Destroy(obj.gameObject); }
        }
    }
    private void TriggerEnemyEvent(bool enable) {
        PointerOver[] objects = FindObjectsOfType<PointerOver>();
        foreach (PointerOver obj in objects) {
            obj.enabled = enable;
        }
    }
}
