using System;
using System.Collections.Generic;
using Utility;
using NPC.Actions;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace NPC
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class NpcMovement : MonoBehaviour, IStateController
    {
        [SerializeField] private bool isDebug = false;
        [SerializeField] private float npcSpeed = 3f;
        [SerializeField] private float npcChaseMultiplier = 5f;

        private Rigidbody2D _rb;
        private Animator _anim;
        
        private CapsuleCollider2D _npcCollider;
        private StateMachine _stateMachine;

        private Vector2 _target;
        private Vector2 _newDirection;
        private Vector2 _moveDirection = Vector2.right;
        private bool _hit;
        private bool _atTarget => _target.Equals(_rb.transform.position);
        private bool _isNewTarget => _moveDirection != _newDirection;

        private Vector2 _collisionAreaMin;
        private Vector2 _collisionAreaMax;
        private Vector2 _collisionDirection;

        // Action processing
        private Queue<ActionBase> _actionQueue = new Queue<ActionBase>();
        private ActionBase _currentAction;

        public Vector2 GetTarget()
        {
            return _target;
        }
        
        public Vector2 GetDirection()
        {
            return _moveDirection;
        }
        
        public void SetTarget(Vector2 target)
        {
            _target = target;
        }

        public Vector2 FindNewTarget()
        {
            return _rb.position + _moveDirection;
        }

        public float GetSpeed()
        {
            return npcSpeed * Time.fixedDeltaTime;
        }

        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _anim = gameObject.GetComponent<Animator>();
            _npcCollider = gameObject.GetComponent<CapsuleCollider2D>();
            _moveDirection = Vector2.right;
            
            
            _stateMachine = new StateMachine();

            IState idle = new NpcIdle(this);
            IState walk = new Walk(this, _rb, _anim);
            IState flee = new Walk(this, _rb, _anim);
            
            _stateMachine.AddTransition(idle, walk, CanWalk());
            _stateMachine.AddTransition(walk, idle, IsBlocked());
            
            _stateMachine.AddAnyTransition(flee, IsFleeing());

            _target = _rb.position + _moveDirection;
            _stateMachine.SetState(walk);

            Func<bool> CanWalk() => () => (!_atTarget && !CheckNpcCollision());
            Func<bool> IsBlocked() => () => (CheckNpcCollision());
            Func<bool> IsFleeing() => () => (_hit);
        }

        private bool CheckNpcCollision()
        {
            int npcLayer = 11;
            int npcLayerMask = 1 << npcLayer;
            int playerLayer = 12;
            int playerLayerMask = 1 << playerLayer;
            int finalmask = npcLayerMask | playerLayerMask;
            
            RaycastHit2D hit = Physics2D.Raycast((Vector2)_rb.transform.position + (_moveDirection * 0.8f), _moveDirection, 1f, finalmask);

            if (hit.collider != null)
            {
                
                // Collision imminent!!
                return true;
            }        

        
            return false;
        }

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("NpcMovement"))
            {
                // Track the bounds of the collider so we can make sure the NPC is fully inside before changing direction.
                _collisionAreaMin = GetCollisionArea(other );
                _collisionAreaMax = GetCollisionArea(other, false);
        
                // Getting the allowed directions from trigger and choosing one at random.
                NpcDirection npcDirection = other.GetComponent<NpcDirection>();
                List<Vector2> allowed = npcDirection.GetAllowedDirections();
                int i = Random.Range(0, allowed.Count);
                _collisionDirection = allowed[i];
            }
    
            // Destroy the NPC when they leave the scene.
            else if (other.CompareTag("NpcDespawn"))
            {
                // Invoke observers.
                DespawnScript despawn = other.gameObject.GetComponent<DespawnScript>();

                despawn.NpcDespawned();
                Destroy(this.gameObject);
            }

            if (other.gameObject.GetComponentInParent<PlayerAttack>())
            {
                npcSpeed *= npcChaseMultiplier;
                _hit = true;
            }
        }

        private Vector2 GetCollisionArea(Collider2D colliderObject, bool isMin = true)
        {
            // Calculate exact coordinates of the box collider based on size and offset.
            BoxCollider2D boxCol = colliderObject.GetComponent<BoxCollider2D>();
            Vector2 size = boxCol.size;
            Vector2 offset = boxCol.offset;
            Vector2 colTrans = boxCol.transform.position;

            // Get either max or min point.
            Vector2 colArea;
            if (isMin)
            {
                colArea = colTrans + offset - (size / 2);
            }
            else
            {
                colArea = colTrans + offset + (size / 2);

            }

            return colArea;
        }

        private void Update()
        {
            if (_atTarget && _newDirection != Vector2.zero && _isNewTarget)
            {
                _moveDirection = _newDirection;
            }
            else if (_atTarget)
            {
                _target = _target + _moveDirection;
            }
            if (_collisionDirection != default)
            {
                // Check the position of the NPCs feet (not center).
                Vector2 colliderSizeY = _npcCollider.size;
                colliderSizeY.x = 0;
                Vector2 npcFeet = _rb.position + _npcCollider.offset - (colliderSizeY / 2);
       
        
                if (npcFeet.x > _collisionAreaMin.x 
                    && npcFeet.y > _collisionAreaMin.y
                    && npcFeet.x < _collisionAreaMax.x
                    && npcFeet.y < _collisionAreaMax.y)
                {
                    _newDirection = _collisionDirection;
                    _collisionDirection = default;
                    _collisionAreaMin = default;
                    _collisionAreaMax = default;
                }
            }
        }

        private void FixedUpdate()
        {
            _stateMachine.Tick();
        }

        private void MakeDecision()
        {
            // Check if we have a new target from a movement floor tile.
            if (_collisionDirection != default)
            {
                // Check the position of the NPCs feet (not center).
                Vector2 colliderSizeY = _npcCollider.size;
                colliderSizeY.x = 0;
                Vector2 npcFeet = _rb.position + _npcCollider.offset - (colliderSizeY / 2);
       
        
                if (npcFeet.x > _collisionAreaMin.x 
                    && npcFeet.y > _collisionAreaMin.y
                    && npcFeet.x < _collisionAreaMax.x
                    && npcFeet.y < _collisionAreaMax.y)
                {
                    _moveDirection = _collisionDirection;
                    _collisionDirection = default;
                    _collisionAreaMin = default;
                    _collisionAreaMax = default;
                }
            }
    
            Move move = new Move(this, _moveDirection);
            _actionQueue.Enqueue(move);
        }

        private void Debug(string message)
        {
            if (isDebug)
            {
                UnityEngine.Debug.Log(message);
            }
        }

    }
}
