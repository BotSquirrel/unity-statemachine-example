using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Character/Idle")]
    public class IdleState : State<CharacterCtrl>
    {
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private float _yInput;
        private float _xInput;

        public override void Init(CharacterCtrl parent)
        {
            base.Init(parent);
            if (_rigidbody == null) _rigidbody = parent.GetComponent<Rigidbody2D>();
            if (_boxCollider == null) _boxCollider = parent.GetComponent<BoxCollider2D>();
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

        public override void ChangeState()
        {
            if (_runner.IsColliderGroundend(_boxCollider) && _yInput > 0f)
            {
                _runner.SetState(typeof(JumpState));
            }
            if (_xInput != 0f)
            {
                _runner.SetState(typeof(WalkState));
            }
        }

        public override void Update() {}

        public override void FixedUpdate() {}

        public override void Exit() {}
    }

}