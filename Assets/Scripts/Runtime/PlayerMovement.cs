using System.Collections;
using UnityEngine;
using Utility;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public partial class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rb;
    private Animator _animator;
    private IInputHelper _inputHelper = new InputHelper();
    public float playerSpeed = 3f;
    private Vector2 _directionOfTravel;

    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private Vector2 CurrentPosition => _rb.position;
    private float PlayerSpeed => playerSpeed * Time.deltaTime;
    private bool IsMovingHorizontal => Mathf.Abs(_directionOfTravel.x) > 0;
    private bool IsMovingVertical => Mathf.Abs(_directionOfTravel.y) > 0;
    private bool IsAttacking => _animator.GetCurrentAnimatorStateInfo(0).IsName("Base.Attack");
    private bool PressAttack => Input.GetKeyDown(KeyCode.E);

    [SerializeField]
    private GameObject _keyHintWasd;
    private bool _hintLearned = false;
    
    private void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        _animator = this.gameObject.GetComponent<Animator>();
        
        StartCoroutine(WaitToShowKeyHints());
    }

    void Update()
    {
        if (IsAttacking)
        {
            _directionOfTravel = Vector2.zero;
            return;
        }

        GetInput();
        AnimatePlayerMovement();
        
        if (PressAttack)
        {
            _animator.SetTrigger(Attack);
        }
    }

    private void AnimatePlayerMovement()
    {
        if (IsMovingHorizontal)
        {
            _animator.SetFloat(Horizontal, _directionOfTravel.x);
            _animator.SetFloat(Vertical, 0);
            
        }
        else if (IsMovingVertical)
        {
            _animator.SetFloat(Vertical, _directionOfTravel.y);
            _animator.SetFloat(Horizontal, 0);
            
        }
        _animator.SetFloat(Speed, _directionOfTravel.sqrMagnitude);

        if (_keyHintWasd && !_hintLearned && _directionOfTravel.sqrMagnitude > 0)
        {
            _hintLearned = true;
            HideWasdHint();
        }
    }

    private void GetInput()
    {
        _directionOfTravel = _inputHelper.GetDirectionOfTravel();
    }

    private void FixedUpdate()
    {
        // @todo Move to physics based movement.
        _rb.MovePosition(CurrentPosition + (_directionOfTravel * PlayerSpeed));
    }

    public void SetInput(IInputHelper input)
    {
        _inputHelper = input;
    }
    
    private void HideWasdHint()
    {
        Animator spaceAnimator = _keyHintWasd.GetComponent<Animator>();
        spaceAnimator.SetTrigger("Hide");
        StartCoroutine(WaitToHideKeyHints());
    }

    private IEnumerator WaitToHideKeyHints()
    {
        yield return new WaitForSeconds(1f);
        _keyHintWasd.SetActive(false);
    }
    
    private IEnumerator WaitToShowKeyHints()
    {
        yield return new WaitForSeconds(1f);
        _keyHintWasd.SetActive(true);
    }
}