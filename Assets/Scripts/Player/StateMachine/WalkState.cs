using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Character/Walk")]
    public class WalkState : State<CharacterCtrl>
    {
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private float _yInput;
        private float _xInput;

        [SerializeField] public float WalkVelocity;

        public override void Init(CharacterCtrl parent)
        {
            base.Init(parent);
            if (_rigidbody == null) _rigidbody = parent.GetComponent<Rigidbody2D>();
            if (_boxCollider == null) _boxCollider = parent.boxCollider;
        }

        public override void CaptureInput()
        {

            if (!_runner.altInput)
            {
                _xInput = Input.GetAxisRaw("Horizontal");
                _yInput = Input.GetAxisRaw("Vertical");
            } 
            else
            {
                _xInput = Input.GetAxisRaw("Horizontal_Alt");
                _yInput = Input.GetAxisRaw("Vertical_Alt");
            }
        }

        public override void Update() 
        {
            _rigidbody.velocity =
                    new Vector2(WalkVelocity * _xInput, _rigidbody.velocity.y);
        }

        public override void FixedUpdate() {}

        public override void ChangeState()
        {
            if (_runner.IsColliderGroundend(_boxCollider) && _yInput > 0f)
            {
                _runner.SetState(typeof(JumpState));
            }

            if (_xInput == 0f && _yInput == 0f)
            {
                _runner.SetState(typeof(IdleState));
            }
        }

        public override void Exit() { }
    }

}