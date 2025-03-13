
using System;
using UnityEngine;

public class AnimHandler : ComponentBehavior
{
    [SerializeField] private Animator anim;
   

    public enum State
    {
        Idle,
        Move,
        
        Dead,
        Attack,
        Upgrade
    }

    public State currentState;
    public State previousState;
    private static readonly int IsMove = Animator.StringToHash("isMove");
    private static readonly int OnDead = Animator.StringToHash("onDead");
    private static readonly int OnAttack = Animator.StringToHash("onAttack");
    private static readonly int OnUpgrade = Animator.StringToHash("onUpgrade");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (anim == null) anim = transform.GetComponent<Animator>();
    }

    public void RotateAnim(bool isFacingRight)
    {
        float scaleX = Mathf.Abs(transform.localScale.x) * (isFacingRight ? 1 : -1);
        var localScale = transform.localScale;
        localScale = new Vector3(scaleX, localScale.y, localScale.z);
        transform.localScale = localScale;
    }
    

    public void SetAnim(State newState)
    {
        
        if(currentState != newState) previousState = currentState;
        switch (newState)
        {
            case State.Move:
                if(currentState == State.Idle) anim.SetBool(IsIdle,false);
                anim.SetBool(IsMove,true);
                currentState = State.Move;
                
                break;
            case State.Dead:
                anim.SetTrigger(OnDead);
                currentState = State.Dead;
                break;
            case State.Attack:
                anim.SetTrigger(OnAttack);
                currentState = State.Attack;
                break;
            case State.Upgrade:
                
                anim.SetTrigger(OnUpgrade);
                currentState = State.Upgrade;
                break;
            case State.Idle:
                if(currentState == State.Move) anim.SetBool(IsMove,false);
                anim.SetBool(IsIdle, true);
                currentState = State.Idle;
                break;
        }
    }

    public void SetInt(string animName, int value)
    {
        
        anim.SetInteger(animName,value);
    }

    public void RevertToPreviousAnim()
    {
        if(previousState == State.Upgrade) SetAnim(State.Idle);
        SetAnim(previousState);
    }

    

    
}
