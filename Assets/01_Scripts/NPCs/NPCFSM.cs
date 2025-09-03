using System;
using UnityEngine;

public class NPCFSM : MonoBehaviour
{
    enum FSM_State
    {
        Empty,
        Arrival,
        Waiting,
        Eating,
        LeaveHappy,
        LeaveUnhappy,
        Exiting
    }

    private NPCAI _npcAI;
    private FSM_State _currentState = FSM_State.Empty;
    
    //Starts off as one to avoid directly changing state
    private float _distanceToGoal=1f;
    
    [SerializeField] private SpriteRenderer NPCVisual;
    [SerializeField] private Transform movePoint;

    private void Start()
    {
        if(TryGetComponent(out _npcAI)){}
        
        SetState(FSM_State.Arrival);

        movePoint.parent = null;
    }

    private void FixedUpdate()
    {
        CheckTransitions(_currentState);
        OnStateUpdate(_currentState);
    }
    private void CheckTransitions(FSM_State state)
    {
        switch (state)
        {
            case FSM_State.Arrival:
                if (_distanceToGoal <= 0.15f)
                    SetState(FSM_State.Waiting);
                break;
            
            case FSM_State.Waiting:
                if(_npcAI.waitTimer<=0 || _npcAI.servedBad)
                    SetState(FSM_State.LeaveUnhappy);
                else if(_npcAI.servedGood)
                    SetState(FSM_State.Eating);
                break;
                
            case FSM_State.Eating:
                if(_npcAI.eatTimer<=0)
                    SetState(FSM_State.LeaveHappy);
                break;
                
            case FSM_State.LeaveHappy:
                if (_distanceToGoal <= 0.15f)
                    SetState(FSM_State.Exiting);
                break;
                
            case FSM_State.LeaveUnhappy:
                if(_distanceToGoal <= 0.15f)
                    SetState(FSM_State.Exiting);
                break;
                
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }


    private void OnStateEnter(FSM_State state)
    {
        //Debug.Log($"OnEnter : {state}");
        
        switch (state)
        {
            case FSM_State.Arrival:
                NPCVisual.color = Color.yellow;
                _npcAI.ArrivalFactor = 1f;
                break;
            case FSM_State.Waiting:
                NPCVisual.color = Color.blue;
                _npcAI.WaitFactor = 1f;
                break;
            case FSM_State.Eating:
                NPCVisual.color = Color.green;
                _npcAI.EatFactor = 1f;
                break;
            case FSM_State.LeaveHappy:
                NPCVisual.color = Color.cyan;
                _npcAI.aiPath.maxSpeed = 3f;
                _npcAI.LeaveHappyFactor = 1f;
                break;
            case FSM_State.LeaveUnhappy:
                NPCVisual.color = Color.red;
                StreakManager.NegativeStreakIncrease();
                _npcAI.aiPath.maxSpeed = 3f;
                _npcAI.LeaveUnhappyFactor = 1f;
                break;
            case FSM_State.Exiting:
                _npcAI.ExitFactor = 1f;
                break;
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

    }
    private void OnStateExit(FSM_State state)
    {
        //Debug.Log($"OnExit : {state}");
        
        switch (state)
        {
            case FSM_State.Arrival:
                _npcAI.ArrivalFactor = 0f;
                _npcAI.aiPath.maxSpeed = 0;
                break;
            case FSM_State.Waiting:
                _npcAI.WaitFactor = 0f;
                _npcAI.ClientUIDisabled();
                break;
            case FSM_State.Eating:
                _npcAI.EatFactor = 0f;
                break;
            case FSM_State.LeaveHappy:
                _npcAI.LeaveHappyFactor = 0f;
                break;
            case FSM_State.LeaveUnhappy:
                _npcAI.LeaveUnhappyFactor = 0f;
                break;
            case FSM_State.Exiting:
                _npcAI.ExitFactor = 0f;
                break;
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    private void OnStateUpdate(FSM_State state)
    {
        //Debug.Log($"OnUpdate : {state}");

        switch (state)
        {
            case FSM_State.Arrival:
            case FSM_State.LeaveHappy:
            case FSM_State.LeaveUnhappy:
                _distanceToGoal = Vector2.Distance(transform.position, _npcAI.aiPath.destination);
                break;
            case FSM_State.Waiting:
                _npcAI.waitTimer -= Time.deltaTime;
                break;
            case FSM_State.Eating:
                _npcAI.eatTimer -= Time.deltaTime;
                break;
            case FSM_State.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void SetState(FSM_State newState)
    {
        if (newState == FSM_State.Empty) return;
        if(_currentState != FSM_State.Empty) OnStateExit(_currentState);
        
        _currentState = newState;
        OnStateEnter(_currentState);
    }
}