using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [HideInInspector] public Vector2 MoveInput;
	[SerializeField] private PlayerDataSO _playerData;
	[Header("Animation")]
	[SerializeField] private Animator _anim;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	private bool _isFacingRight;
	[SerializeField] private bool _momentum;
	[Header("Audio")]
	[SerializeField] private AudioSource _playerMoveAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!PauseMenu.IsPaused && !ResultMenu.IsDone) {
			MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
			if (_rb.velocity.magnitude <= 0.1f) _rb.velocity = Vector2.zero;
			SpriteControl();
		}
		AudioControl();
    }
    void FixedUpdate() {
        HandleMovement();
    }
    private void HandleMovement()
	{
		
		float targetSpeedX = MoveInput.x * _playerData.RunMaxSpeed;
		float targetSpeedY = MoveInput.y * _playerData.RunMaxSpeed;

		if (_momentum) {
			#region Calculate AccelRate
			float accelRateX = Mathf.Abs(targetSpeedX) > 0.01f ? _playerData.RunAcceleration : _playerData.RunDeceleration;
			float accelRateY = Mathf.Abs(targetSpeedY) > 0.01f ? _playerData.RunAcceleration : _playerData.RunDeceleration;
			#endregion
			
			#region Conserve Momentum
			if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(targetSpeedX) && Mathf.Sign(_rb.velocity.x) == Mathf.Sign(targetSpeedX) && Mathf.Abs(targetSpeedX) > 0.01f) {
				accelRateX = 0;
			}
			if (Mathf.Abs(_rb.velocity.y) > Mathf.Abs(targetSpeedY) && Mathf.Sign(_rb.velocity.y) == Mathf.Sign(targetSpeedY) && Mathf.Abs(targetSpeedY) > 0.01f) {
				accelRateY = 0;
			}
			#endregion

			//Calculate difference between current velocity and desired velocity
			float speedDifX = targetSpeedX - _rb.velocity.x;
			float speedDifY = targetSpeedY - _rb.velocity.y; 

			//Calculate force along x-axis to apply to thr player
			float movementX = speedDifX * accelRateX;
			float movementY = speedDifY * accelRateY; // Calculate the movement in the Y direction

			//Convert this to a vector and apply to rigidbody
			_rb.AddForce(new Vector2(movementX, movementY), ForceMode2D.Force);
		} 
		else {
			// _rb.MovePosition(_rb.position + new Vector2(targetSpeedX, targetSpeedY) * Time.fixedDeltaTime);
			_rb.velocity = new Vector2(targetSpeedX, targetSpeedY);
		}
	}
	private void SpriteControl() {
		
		if (MoveInput.x != 0 || MoveInput.y != 0) {
			_anim.SetFloat("DirX", MoveInput.x * Time.timeScale);
			_anim.SetFloat("DirY", MoveInput.y * Time.timeScale);
			_anim.SetBool("IsRunning", true);
			if (MoveInput.x > 0) {
				_spriteRenderer.flipX = false;
			} 
			else if (MoveInput.x < 0) {
				_spriteRenderer.flipX = true;
			}
		} else {
			_anim.SetBool("IsRunning", false);
		}
	}
	private void AudioControl() {
		if ((MoveInput.x != 0 || MoveInput.y != 0) && Time.timeScale == 1 && !PauseMenu.IsPaused && !ResultMenu.IsDone) {
			_playerMoveAudio.enabled = true;
		}
		else {
			_playerMoveAudio.enabled = false;
		}
	}
}
