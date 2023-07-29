using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerDataSO _playerData;
    private int _maxHealth;
    private int _currentHealth;
    [Header("iFrame")]
    [SerializeField] private int flashesAmount;
    private float _iFrameDuration;
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _renderer;
    [Header("UI")]
    [SerializeField] private Image _healthBarCurrentImage;
    [SerializeField] private Image _healthBarFullImage;
    [SerializeField] private ResultMenu _resultMenu;
    private PlayerExperience _playerExperience;
    [SerializeField] private AudioSource _increaseHealthAudio;
    [SerializeField] private AudioSource _decreaseHealthAudio;
    private void Awake() {
        _maxHealth = _playerData.InitHealth;
        _iFrameDuration = _playerData.InvincibilityTime;
        _currentHealth = _maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        // if (HealthManager.Instance != null) HealthManager.Instance.OnHealthChange += ChangeHealthEvent; 
        _playerExperience = GetComponent<PlayerExperience>();
        _healthBarFullImage.fillAmount = _maxHealth / 10f;
        // HealthManager.Instance.OnHealthChange += ChangeHealthEvent; 
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthBarCurrentImage != null)
            _healthBarCurrentImage.fillAmount = _currentHealth / 10f;
    }
    private void OnEnable() {
        StartCoroutine(WaitAdd()); 
    }
    private IEnumerator WaitAdd() {
        yield return new WaitUntil(()=> HealthManager.Instance != null);
        HealthManager.Instance.OnHealthChange += ChangeHealthEvent; 
    }
    private void OnDisable() {
        StartCoroutine(WaitSub()); 
    }
    private IEnumerator WaitSub() {
        yield return new WaitUntil(()=> HealthManager.Instance != null);
        HealthManager.Instance.OnHealthChange -= ChangeHealthEvent;
    }
    private void ChangeHealthEvent(int changeValue) {
        _currentHealth = Mathf.Clamp(_currentHealth + changeValue, 0, _maxHealth);
        if(changeValue < 0) // Lose Health
        {
            _decreaseHealthAudio.Play();
            if (_currentHealth > 0) {
                _animator.SetTrigger("IsHurt");
                StartCoroutine(Invulnerability());
            }
            else {
                _animator.SetTrigger("IsDead");
                _resultMenu.LoseGame(_playerExperience.CurrentExperience);
                // Debug.Log("Death");
            }  
        }
        else if (changeValue > 0) // Add Health
        {
            _increaseHealthAudio.Play();
        }
    }
    private IEnumerator Invulnerability() {
        HealthManager.IsInvulnerable = true;
        Physics2D.IgnoreLayerCollision(6, 10, true);
        for (int i = 0; i < flashesAmount; i++) {
            _renderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(_iFrameDuration / (2 * flashesAmount));
            _renderer.color = Color.white;
            yield return new WaitForSeconds(_iFrameDuration / (2 * flashesAmount));
        }
        HealthManager.IsInvulnerable = false;
        Physics2D.IgnoreLayerCollision(6, 10, false);
    }
}
