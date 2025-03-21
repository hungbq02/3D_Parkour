using System.Security.Claims;
using Unity.VisualScripting;
using UnityEngine;

public class Grounded : BaseState
{
    protected MovementSM sm;

    float gravityValue;
    bool jump;
    bool climb;
    Vector3 curVelocity;
    bool isGrounded;
    float playerSpeed;

    float inputMagnitude;

    Vector3 cVelocity;
    public Grounded(PlayerController playerController, MovementSM stateMachine) : base(playerController, stateMachine)
    {
        sm = (MovementSM)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        jump = false;
        climb = false;
        velocity = Vector3.zero;
        gravityVelocity.y = 0.0f;

        playerSpeed = sm.speed;
        // isGrounded = playerController.IsGrounded();
        isGrounded = playerController.Controller.isGrounded;

        gravityValue = playerController.gravityValue;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (playerController.Input.jump)
        {
            jump = true;
        }
        if (playerController.onStair && playerController.Input.move.y > 0)
        {
            climb = true;
        }
        velocity = new Vector3(playerController.Input.move.x, 0.0f, playerController.Input.move.y);

        //normalize the velocity
        inputMagnitude = Mathf.Clamp01(velocity.magnitude);

        playerController.Animator.SetFloat("InputMagnitude", inputMagnitude);

        velocity = velocity.x * playerController.cameraTransform.right + velocity.z * playerController.cameraTransform.forward.normalized;
        velocity.y = 0.0f;

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (jump)
        {
            sm.ChangeState(sm.jumpingState);
        }
        if(climb)
        {
           sm.ChangeState(sm.climbState);
        }

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        gravityVelocity.y += gravityValue * Time.deltaTime;

        //isGrounded = playerController.IsGrounded();
        isGrounded = playerController.Controller.isGrounded;

        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        curVelocity = Vector3.SmoothDamp(curVelocity, velocity, ref cVelocity, 0.1f);
        playerController.Controller.Move(curVelocity * playerSpeed * Time.deltaTime + gravityVelocity * Time.deltaTime + playerController.windForce * Time.deltaTime);

        //rotate player
        if (velocity.sqrMagnitude > 0)
        {
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, Quaternion.LookRotation(velocity), playerController.rotationDampTime);
        }
    }
}
