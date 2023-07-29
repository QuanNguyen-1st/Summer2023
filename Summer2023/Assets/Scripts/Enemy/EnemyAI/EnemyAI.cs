using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> _steeringBehaviours;

    [SerializeField]
    private List<Detector> _detectors;

    [SerializeField]
    private AIData _aiData;

    [SerializeField]
    private float _detectionDelay = 0.05f, _aiUpdateDelay = 0.06f, _attackDelay = 1f;

    [SerializeField]
    private float _attackDistance = 0.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    private Vector2 _movementInput;

    [SerializeField]
    private ContextSolver _movementDirectionSolver;
    [SerializeField]
    private EnemyStatSO _enemyData;

    private bool _following = false;
    [Header("Action")]
    public bool AllowAttack;
    public bool AllowPointer;
    public bool AllowMove;
    

    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, _detectionDelay);
        _attackDelay = _enemyData.RestTime;
        _attackDistance = _enemyData.AttackDistance;
    }

    private void PerformDetection()
    {
        foreach (Detector detector in _detectors)
        {
            detector.Detect(_aiData);
        }
    }

    private void Update()
    {
        if (_aiData.CurrentTarget != null) //Enemy AI movement based on Target availability
        {
            //Looking at the Target
            if (AllowPointer) OnPointerInput?.Invoke(_aiData.CurrentTarget.position);
            if (_following == false)
            {
                _following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (_aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            _aiData.CurrentTarget = _aiData.Targets[0];
        }
        //Moving the Agent
        if (AllowMove) OnMovementInput?.Invoke(_movementInput);
        
    }

    private IEnumerator ChaseAndAttack()
    {
        if (_aiData.CurrentTarget == null)
        {
            //Stopping Logic
            Debug.Log("Stopping");
            _movementInput = Vector2.zero;
            _following = false;
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(_aiData.CurrentTarget.position, transform.position);

            if (distance < _attackDistance)
            {
                //Attack logic
                _movementInput = Vector2.zero;
                if (AllowAttack) OnAttackPressed?.Invoke();
                yield return new WaitForSeconds(_attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                _movementInput = _movementDirectionSolver.GetDirectionToMove(_steeringBehaviours, _aiData);
                yield return new WaitForSeconds(_aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }

        }

    }
}
