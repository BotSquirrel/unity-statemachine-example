using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StateMachine
{
    public class CharacterCtrl : StateRunner<CharacterCtrl>
    {
        public BoxCollider2D boxCollider;
        public LayerMask jumpableGround;
        public bool altInput;

        protected override void Awake()
        {
            base.Awake();
        }

        public bool IsColliderGroundend(BoxCollider2D boxCollider)
        {
            return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        }
    }
}
