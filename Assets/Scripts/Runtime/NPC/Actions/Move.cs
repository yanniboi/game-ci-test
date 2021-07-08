using UnityEngine;

namespace NPC.Actions
{
    public class Move : ActionBase
    {

        private readonly Rigidbody2D _rb;
        private readonly Animator _animator;
        private readonly float _speed;
    
        private Vector2 _currentPosition;
        private Vector2 _targetPosition;
        private Vector2 _direction;
    
        private GameObject _trolleyUp;
        private GameObject _trolleyDown;
        private GameObject _trolleyLeft;
        private GameObject _trolleyRight;
    
        public Move(NpcMovement movement, Vector2 direction)
        {
            _rb = movement.GetComponent<Rigidbody2D>();
            _animator = movement.GetComponent<Animator>();
            _currentPosition = movement.transform.position;
            _speed = movement.GetSpeed();
            _direction = direction;

            // @todo Clean up trolley stuff.
            FindTrolleyObjects(movement);
        }

        public override void Prepare()
        {
            // Update target vector.
            _targetPosition = _currentPosition + _direction;
      
            if (CheckNpcCollision())
            {
                // Stop animation if we are making them wait.
                _animator.SetFloat("Speed", 0f);
                return;
            }
        
            // Set animation.
            _animator.SetFloat("Vertical", _direction.y);
            _animator.SetFloat("Horizontal", _direction.x);
            _animator.SetFloat("Speed", _direction.sqrMagnitude);

            // @todo Clean up trolley stuff.
            TrolleyAnimation();
        
            // Start processing immediately.
            base.Prepare();
            Process();
        }

        public override void Process()
        { 
            // Update current position from the rigid body.
            _currentPosition = _rb.position;
        
            // Make the movement happen.
            Vector2 movement = Vector2.MoveTowards(_currentPosition, _targetPosition, _speed);
            _rb.MovePosition(movement);

            // If we have arrived, complete the action.
            if (_currentPosition == _targetPosition)
            {
                Complete();
            }
        }

        public override void Complete()
        {
            base.Complete();
        }
    
        private bool CheckNpcCollision()
        {
            int layer1 = 11;
            int layermask1 = 1 << layer1;
        
            RaycastHit2D hit = Physics2D.Raycast(_currentPosition + (_direction * 0.8f), _direction, 3f, layermask1);

            if (hit.collider != null)
            {
                // Collision imminent!!
                return true;
            }        

        
            return false;
        }

        private void FindTrolleyObjects(NpcMovement movement)
        {
            for (int i = 0; i < movement.transform.childCount; i++)
            {
                GameObject child = movement.transform.GetChild(i).gameObject;
                switch (child.name)
                {
                    case "TrolleyDown":
                    {
                        _trolleyDown = child;
                        break;
                    }
                    case "TrolleyUp":
                    {
                        _trolleyUp = child;
                        break;
                    }
                    case "TrolleyLeft":
                    {
                        _trolleyLeft = child;
                        break;
                    }
                    case "TrolleyRight":
                    {
                        _trolleyRight = child;
                        break;
                    }
                }
            }
        }
        private void TrolleyAnimation()
        {
            // @todo Clean this up a bit.
            _trolleyUp.SetActive(false);
            _trolleyDown.SetActive(false);
            _trolleyLeft.SetActive(false);
            _trolleyRight.SetActive(false);
            
            if (_animator.GetBool("HasTrolley"))
            {
                if (Mathf.Abs(_direction.y) > 0f)
                {
                    if (_direction.y > 0f)
                    {
                        _trolleyUp.SetActive(true);
                    }
                    else
                    {
                        _trolleyDown.SetActive(true);
                    }
                }
                if (Mathf.Abs(_direction.x) > 0f)
                {
                    if (_direction.x > 0f)
                    {
                        _trolleyRight.SetActive(true);
                    }
                    else
                    {
                        _trolleyLeft.SetActive(true);
                    }
                }
            
            }
            else
            {
                _trolleyUp.SetActive(false);
                _trolleyDown.SetActive(false);
                _trolleyLeft.SetActive(false);
                _trolleyRight.SetActive(false);

            }
        }
    }
}
