using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public enum EAbilityState {
        Ready,
        Active,
        Cooldown
    }
    [SerializeField] private AbilitySO _abilitieData;
    public float _activeTime;
    public EAbilityState _state = EAbilityState.Ready;
    private float _cooldownTime = 0f;
    public bool IsUnlocked;
    [SerializeField] private Image _abilityImageBW;
    [SerializeField] private Image _abilityActiveImage;

    // Update is called once per frame
    void Update()
    {
        if (IsUnlocked && !PauseMenu.IsPaused && !ResultMenu.IsDone) {
            CheckAbility();
        }
    }
    void CheckAbility() {
        switch (_state) {
            case EAbilityState.Ready:
                if (Input.GetKeyDown(_abilitieData.Key) && !Extra.Activating) {
                    _abilityImageBW.fillAmount = 1;
                    _abilitieData.Activate();
                    Extra.Activating = true;
                    _activeTime = _abilitieData.ActiveTime;
                    _state = EAbilityState.Active;
                }
            break;
            case EAbilityState.Active:
                if (_activeTime > 0) {
                    _abilityActiveImage.fillAmount = _activeTime / _abilitieData.ActiveTime;
                    _activeTime -= (Time.deltaTime * (1 / Extra.SlowTimeScale));
                }
                else {
                    Deactivate();
                }
            break;
            case EAbilityState.Cooldown:
                if (_cooldownTime > 0) {
                    _cooldownTime -= Time.deltaTime;
                    _abilityImageBW.fillAmount = _cooldownTime / _abilitieData.CooldownTime;
                }
                else {
                    _abilityImageBW.fillAmount = 0;
                    _state = EAbilityState.Ready;
                }
            break;
        }
    }

    public void Deactivate() {
        _activeTime = 0;
        _abilityActiveImage.fillAmount = 0;
        _abilitieData.Deactivate();
        Extra.Activating = false;
        _state = EAbilityState.Cooldown;
        _cooldownTime = _abilitieData.CooldownTime;
    }
}
