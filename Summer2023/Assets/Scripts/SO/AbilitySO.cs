using System;
using UnityEngine;

public class AbilitySO : ScriptableObject {
    public string AbilityName;
    public float CooldownTime;
    public float ActiveTime;
    public KeyCode Key;
    public virtual void Activate() {}
    public virtual void Deactivate() {}
}
