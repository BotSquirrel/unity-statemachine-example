using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    [CreateAssetMenu(menuName = "States/Character/Jump")]
    public class JumpState : State<CharacterCtrl>
    {
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private bool _airborne;
        private float _xInput;

        [SerializeField] private WalkState _walkState;
        [SerializeField] private float _jumpVelocity;

        public override void Init(CharacterCtrl parent)
        {
            base.Init(parent);
            if (_rigidbody == null) _rigidbody = parent.GetComponent<Rigidbody2D>();
            if (_boxCollider == null) _boxCollider = parent.boxCollider;
            _airborne = false;
        }

        public override void CaptureInput() {
            if (!_runner.altInput)
            {
                _xInput = Input.GetAxisRaw("Horizontal");
            } else
            {
                _xInput = Input.GetAxisRaw("Horizontal_Alt");
            }
        }

        public override void Update() 
        {
            if (!_airborne)
            {
                _rigidbody.velocity =
                    new Vector2(_walkState.WalkVelocity * _xInput, _jumpVelocity);
            }

            if (_airborne)
            {
                _rigidbody.velocity =
                    new Vector2(_walkState.WalkVelocity * _xInput, _rigidbody.velocity.y);
            }

            if (!_runner.IsColliderGroundend(_boxCollider))
            {
                _airborne = true;
            }
        }

        public override void FixedUpdate() {}

        public override void ChangeState()
        {
            if (_airborne && _runner.IsColliderGroundend(_boxCollider))
            {
                _runner.SetState(typeof(WalkState));
            }
        }

        public override void Exit() {}
    }
}
