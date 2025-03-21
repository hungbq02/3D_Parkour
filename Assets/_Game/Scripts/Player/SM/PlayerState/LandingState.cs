using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : BaseState
{
    MovementSM sm;
    float timePassed;
    float landingTime; //Delay from jumpend -> move
    public LandingState(PlayerController playerController, MovementSM stateMachine) : base(playerController, stateMachine)
    {
        sm = (MovementSM)this.stateMachine;
    }

    public override void Enter()
    {
        timePassed = 0f;
        //anim landing
        landingTime = 0.15f;

    }

    public override void UpdateLogic()
    {
        if (timePassed > landingTime)
        {
            //trigger back move
            stateMachine.ChangeState(sm.groundedState);
        }
        timePassed += Time.deltaTime;
    }
}


