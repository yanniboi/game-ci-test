using System;
using NPC;
using NPC.Actions;
using UnityEngine;
using Utility;

public class PlayerSmImplementation  : MonoBehaviour
{

    private StateMachine _stateMachine;
    private Rigidbody2D _rb;

    public bool IsBlocked = true;
    public Vector2 Target;
    

    private void Awake()
    {
        Target = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        _stateMachine = new StateMachine();

        // IState idle = new Idle(this);
        // IState wait = new Wait(this);
        // IState walk = new Walk(this, _rb, _anim);
            
        // _stateMachine.AddTransition(idle, wait, (IsBlocked()));
        // _stateMachine.AddTransition(wait, idle, (IsNotBlocked()));
        // _stateMachine.AddAnyTransition(walk, (HasDirection()));

        // _stateMachine.SetState(idle);

        Func<bool> IsBlocked() => () => this.IsBlocked;
        Func<bool> IsNotBlocked() => () => !this.IsBlocked;
        Func<bool> HasDirection() => () => (!transform.position.Equals(Target) && !this.IsBlocked);

    }

    public Vector2 GetTarget()
    {
        return Target;
    }
    
    private void Update()
    {
      _stateMachine.Tick();
    }
}