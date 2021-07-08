using UnityEngine;

namespace NPC.Actions
{
    public class Walk : IState
    {
        private readonly IStateController _controller;
        private readonly Rigidbody2D _rb;
        private readonly Animator _anim;
        
        private Vector2 _currentPosition;
        private Vector2 _targetPosition;
        private float _speed;

        public Walk(IStateController controller, Rigidbody2D rb, Animator anim)
        {
            _rb = rb;
            _anim = anim;
            _controller = controller;

        }

        public void Tick()
        {
            _currentPosition = _rb.position;
            if (_currentPosition == _targetPosition)
            {
                _targetPosition = _controller.GetTarget();
                UpdateAnimation();
            }
            
            Vector2 movement = Vector2.MoveTowards(_currentPosition, _targetPosition, _speed);
            _rb.MovePosition(movement);
        }

        private void UpdateAnimation()
        {
            _anim.SetFloat("Vertical", _controller.GetDirection().y);
            _anim.SetFloat("Horizontal", _controller.GetDirection().x);
            _anim.SetFloat("Speed", _controller.GetDirection().sqrMagnitude);
        }

        public void OnEnter()
        {
            _currentPosition = _rb.position;
            _speed = _controller.GetSpeed();
            _targetPosition = _controller.GetTarget();
            
            UpdateAnimation();
        }

        public void OnExit()
        {
            _anim.SetFloat("Speed", 0f);
        }
    }
}