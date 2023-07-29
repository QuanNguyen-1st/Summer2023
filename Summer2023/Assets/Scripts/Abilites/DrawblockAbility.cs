using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AbilitySO", menuName = "Summer2023/Ability Data/Draw Block", order = 0)]
public class DrawblockAbility : AbilitySO
{
    private bool _deactivated = false;
    public override void  Activate() {
        _deactivated = false;
        Time.timeScale = Extra.SlowTimeScale;
        TriggerDrawManager("Draw Block Manager");
        
        // if (!_deactivated) {
        //     float timeAllowed = 1.5f;
        //     while (timeAllowed != 0 && !_deactivated) timeAllowed -= Time.timeScale * (1 / Extra.SlowTimeScale);
        //     Time.timeScale = 1;
        //     TriggerDrawManager("Draw Manager");
        // } 
    }

    public override void Deactivate()
    {
        _deactivated = true;
        Time.timeScale = 1;
        TriggerDrawManager("Draw Manager");
    }

    private void TriggerDrawManager(string objName) {
        DrawManager[] objects = FindObjectsOfType<DrawManager>();
        foreach (DrawManager obj in objects) {
            if (obj.gameObject.name == objName) {
                obj.enabled = true;
            }
            else {
                obj.enabled = false;
            }
        }
    }
}
