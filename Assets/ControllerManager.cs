using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using Assets.Scripts;
using UniRx;

public class ControllerManager : ContollerBaseMonoBehavior
{
    public float MovingSpeed = 6;
    public float JumpingForce = 20000;
    public LayerMask[] groundLayers = new LayerMask[0];

    private Vector2 _stepsToMove;
    private Rigidbody2D _playerRigidBody;
    private SpriteRenderer _playerSpriteRenderer;
    private bool _isPlayerRotated = false;

    override protected void Start()
    {
        base.Start();
        AddKeyboardEvents();
        _playerRigidBody = Player.GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
    }

    public void OnJumpButtonClick()
    {
        if (CanJump()) _playerRigidBody.AddForce(new Vector2(0, JumpingForce));
    }

    private bool CanJump()
    {
        return groundLayers.Any(layer => _playerCollider.IsTouchingLayers(layer.value));
    }

    private Vector2 GenerateForwardVector(bool isReleased)
    {
        return new Vector2(isReleased ? 0 : MovingSpeed, 0);
    }

    public void OnLeftButtonClick(bool isReleased)
    {
        FlipPlayerSprite(true);
        _stepsToMove = GenerateForwardVector(isReleased);
    }

    public void OnRightButtonClick(bool isReleased)
    {
        FlipPlayerSprite(false);
        _stepsToMove = GenerateForwardVector(isReleased);
    }

    void Update()
    {
        MovePlayer(_stepsToMove);
    }
    
    private void FlipPlayerSprite(bool isFliped)
    {
        Player.transform.Rotate(new Vector3(0, _isPlayerRotated != isFliped ? 180 : 0, 0));
        _isPlayerRotated = isFliped;
    }

    private void AddKeyboardEvents()
    {
        // Character Jump
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown("space"))
            .Throttle(TimeSpan.FromMilliseconds(20))
            .Subscribe(_ => OnJumpButtonClick());

        // Character fire
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown("left ctrl"))
            .Select(_ => GetComponent<FireButtonController>())
            .Where(fireController => fireController != null)
            .Subscribe(fireController => fireController.OnFireButtonClick());

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            .Do(_ => FlipPlayerSprite(false))
            .Subscribe(_ => MovePlayer(GenerateForwardVector(false)));

        Observable.EveryUpdate()
            .Where(_ => Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            .Do(_ => FlipPlayerSprite(true))
            .Subscribe(_ => MovePlayer(GenerateForwardVector(false)));

    }

    private void MovePlayer(Vector2 pMovement)
    {
        Player.transform.Translate(new Vector3(pMovement.x, 0, 0));
    }

    
}
