using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using Assets.Scripts;

public class ControllerManager : ContollerBaseMonoBehavior
{
    public float MovingSpeed = 2;
    public float JumpingForce = 20000;
    public LayerMask[] groundLayers = new LayerMask[0];

    private Vector2 _stepsToMove;
    private Rigidbody2D _playerRigidBody;
    
    override protected void Start()
    {
        base.Start();
        _playerRigidBody = Player.GetComponent<Rigidbody2D>();
    }

    public void OnJumpButtonClick()
    {
        if (CanJump()) _playerRigidBody.AddForce(new Vector2(0, JumpingForce));
    }

    private bool CanJump()
    {
        return groundLayers.Any(layer => _playerCollider.IsTouchingLayers(layer.value));
    }

    public void OnLeftButtonClick(bool isReleased)
    {
        _stepsToMove = new Vector2(isReleased ? 0: -MovingSpeed, 0);
    }

    public void OnRightButtonClick(bool isReleased)
    {
        _stepsToMove = new Vector2(isReleased ? 0 : MovingSpeed, 0);
    }

    void Update()
    {
        MovePlayer(_stepsToMove);
    }

    private void MovePlayer(Vector2 pMovement)
    {
        Player.transform.position = new Vector3(Player.transform.position.x + pMovement.x, Player.transform.position.y, Player.transform.position.z);
    }

    
}
