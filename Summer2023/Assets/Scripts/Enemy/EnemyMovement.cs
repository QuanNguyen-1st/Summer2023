using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private EnemyStatSO _enemyData;
	[SerializeField] private bool _momentum;
	[SerializeField] Animator _anim;
	[SerializeField] private SpriteRenderer _spriteRenderer;
    public void HandleMovement(Vector2 movementInput) {
		if (!PauseMenu.IsPaused && !ResultMenu.IsDone) {
			SpriteControl(movementInput);
		}
		float targetSpeedX = movementInput.x * _enemyData.RunMaxSpeed;
		float targetSpeedY = movementInput.y * _enemyData.RunMaxSpeed;
		
		if (_momentum) {
			#region Calculate AccelRate
			float accelRateX = Mathf.Abs(targetSpeedX) > 0.01f ? _enemyData.RunAcceleration : _enemyData.RunDeceleration;
			float accelRateY = Mathf.Abs(targetSpeedY) > 0.01f ? _enemyData.RunAcceleration : _enemyData.RunDeceleration;
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
			_rb.MovePosition(_rb.position + new Vector2(targetSpeedX, targetSpeedY) * Time.fixedDeltaTime);
		}
    }
	private void SpriteControl(Vector2 movementInput) {
		if (movementInput.x != 0 || movementInput.y != 0) {
			_anim.SetFloat("DirX", movementInput.x * Time.timeScale);
			_anim.SetFloat("DirY", movementInput.y * Time.timeScale);
			_anim.SetBool("IsRunning", true);
			if (movementInput.x > 0) {
				_spriteRenderer.flipX = false;
			} 
			else if (movementInput.x < 0) {
				_spriteRenderer.flipX = true;
			}
		} else {
			_anim.SetBool("IsRunning", false);
		}
	}
}
