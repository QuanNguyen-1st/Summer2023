using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Summer2023/Ability Data/Slowdown Time", order = 0)]
public class SlowtimeAbility : AbilitySO
{
    public override void  Activate() {
        Time.timeScale = Extra.SlowTimeScale;
    }

    public override void Deactivate()
    {
        Time.timeScale = 1;
    }
}
