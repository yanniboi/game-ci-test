using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CardInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject _iLeft;
    [SerializeField]
    private GameObject _iRight;
    [SerializeField]
    private GameObject _iUp;
    [SerializeField]
    private GameObject _iDown;
    [SerializeField]
    private Animator _pickup;
    private AudioSource _gameAudio;

    public float cardWaitTime = 2f;
    
    private CardItem _card;
    public event Action<CardInteractable> OnInteraction;

    [SerializeField]
    private GameObject _alertPopup;
    [SerializeField]
    private GameObject _keyHintSpace;

    private bool _needsKeyHint = true;
    private bool _isInteracting => Time.timeScale == 0;
    private static readonly int ShowItem = Animator.StringToHash("showItem");

    private void OnEnable()
    {
        _gameAudio = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        if (!_isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                ClearColliders();
                _iDown.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                ClearColliders();
                _iUp.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                ClearColliders();
                _iLeft.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ClearColliders();
                _iRight.SetActive(true);
            }
        }


        
        if (_card != default)
        {
            if (!_isInteracting && Input.GetKeyUp(KeyCode.Space))
            {
                Interaction interaction = new Interaction((CardInteractable) _card);
                interaction.Interact();
                if (interaction.IsInteracting)
                {
                    HideAlert(true);
                    if (!_card.Collected)
                    {
                        Time.timeScale = 0;
                        _pickup.SetBool(ShowItem, true);
                        _gameAudio.Pause();
                        _card.Found();
                        StartCoroutine(HidePickup());
                    }
                    else
                    {
                        TriggerInteraction();
                    }
                }
            }
        }
    }


    void HideAlert(bool hintLearned = false)
    {
        if (_alertPopup.activeSelf)
        {
            _alertPopup.SetActive(false);

            if (_keyHintSpace.activeSelf)
            {
                if (hintLearned)
                    _needsKeyHint = false;
                HideSpacebarHint();
            }

        }
    }
    
    IEnumerator HidePickup()
    {
        yield return StartCoroutine( WaitForRealSeconds( cardWaitTime));
        _pickup.SetBool(ShowItem, false);
        TriggerInteraction();
        Time.timeScale = 1;
        if (!_gameAudio.isPlaying)
        {
            _gameAudio.Play();
        }
    }

    void TriggerInteraction()
    {
        if (OnInteraction != null)
        {
            OnInteraction((CardInteractable) _card);
            _card.ShowCard();
            ClearCard();
        }
    }
    
    public void SetCard(CardItem card)
    {
        _card = card;
        
        if (!_alertPopup.activeSelf)
        {
            _alertPopup.SetActive(true);

            if (_needsKeyHint)
            {
                _keyHintSpace.SetActive(true);
            }
        }
    }
    
    public static IEnumerator WaitForRealSeconds( float delay )
    {
        float start = Time.realtimeSinceStartup;
        while( Time.realtimeSinceStartup < start + delay  )
        {
            yield return null;
        }
    }
    
    public void ClearCard()
    {
        _card = default;
        HideAlert();
    }

    private void ClearColliders()
    {
        _iLeft.SetActive(false);
        _iRight.SetActive(false);
        _iUp.SetActive(false);
        _iDown.SetActive(false);
    }
    
    private void HideSpacebarHint()
    {
        Animator spaceAnimator = _keyHintSpace.GetComponent<Animator>();
        spaceAnimator.SetTrigger("Hide");
        StartCoroutine(WaitToHideSpacebar());
    }

    private IEnumerator WaitToHideSpacebar()
    {
        yield return new WaitForSeconds(1f);
        _keyHintSpace.SetActive(false);
    }
}
