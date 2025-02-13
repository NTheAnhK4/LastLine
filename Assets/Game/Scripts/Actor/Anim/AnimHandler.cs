
using UnityEngine;

public class AnimHandler : ComponentBehavior
{
    [SerializeField] private Animator anim;

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

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (anim == null) anim = transform.GetComponent<Animator>();
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
        }
    }
}
