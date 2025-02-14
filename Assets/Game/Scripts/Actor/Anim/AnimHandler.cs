
using System;
using UnityEngine;

public class AnimHandler : ComponentBehavior
{
    [SerializeField] private Animator anim;
    [SerializeField] private string preAnimName;

    public enum State
    {
        Move,
        Idle,
        Dead,
        Attack
    }

    public State currentState;
    private static readonly int IsMove = Animator.StringToHash("isMove");
    private static readonly int OnDead = Animator.StringToHash("onDead");
    private static readonly int OnAttack = Animator.StringToHash("onAttack");   
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (anim == null) anim = transform.GetComponent<Animator>();
    }

    public void Init(string animName)
    {
        preAnimName = animName;
        if(preAnimName != string.Empty) SetAnim(animName);
    }

    public void SetAnim(string animName)
    {
        switch (animName)
        {
            case "Move":
                anim.SetBool(IsMove,true);
                currentState = State.Move;
                break;
            case "Dead":
                anim.SetTrigger(OnDead);
                currentState = State.Dead;
                break;
            case "Attack":
                anim.SetTrigger(OnAttack);
                currentState = State.Attack;
                break;
        }
    }

    public void SetInt(string animName, int value)
    {
        anim.SetInteger(animName,value);
    }

    public void SetPreAnim()
    {
        SetAnim(preAnimName);
    }
}
