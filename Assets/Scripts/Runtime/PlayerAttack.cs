using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    private BoxCollider2D _attackLeftCollider;
    [SerializeField]
    private BoxCollider2D _attackRightCollider;
    [SerializeField]
    private BoxCollider2D _attackUpCollider;
    [SerializeField]
    private BoxCollider2D _attackDownCollider;
    [SerializeField]
    private Animator _playerAnimator;
    
    private bool IsMovingHorizontal => Mathf.Abs(_playerAnimator.GetFloat("Horizontal")) > 0;
    private bool IsMovingVertical => Mathf.Abs(_playerAnimator.GetFloat("Vertical")) > 0;
    
    private string _currentDirection = "down";
    private BoxCollider2D _activeCollider;

    void Update()
    {
        if (_activeCollider)
        {
            DisableCollider();
        }
        string direction = FindPlayerDirection();

        if (direction != _currentDirection)
        {
            _currentDirection = direction;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
          EnableCollider();
        }
    }

    private void EnableCollider()
    {
        Debug.Log(_currentDirection);
        switch (_currentDirection)
        {
            case "up":
                _activeCollider = _attackUpCollider;
                break;
            case "down":
                _activeCollider = _attackDownCollider;
                break;
            case "left":
                _activeCollider = _attackLeftCollider;
                break;
            case "right":
                _activeCollider = _attackRightCollider;
                break;
        }
        Debug.Log(_activeCollider.gameObject.activeSelf);
        _activeCollider.gameObject.SetActive(true);
        Debug.Log(_activeCollider.gameObject.activeSelf);

    }
    
    private void DisableCollider()
    {
        _activeCollider.gameObject.SetActive(false);
    }

    private string FindPlayerDirection()
    {
        string direction = "";
        if (IsMovingHorizontal)
        {
            direction = _playerAnimator.GetFloat("Horizontal") > 0 ? "right" : "left";
        } 
        else if (IsMovingVertical)
        {
            direction = _playerAnimator.GetFloat("Vertical") > 0 ? "up" : "down";
        }
        
        return direction == "" ? _currentDirection : direction;
    }
}
