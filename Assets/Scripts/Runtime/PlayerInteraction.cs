using System;
using System.Collections;
using System.Collections.Generic;
using NPC;
using UI;
using UnityEngine;
using Utility;

public class PlayerInteraction : MonoBehaviour
{
    public event Action<Identity> OnInteraction;
    private List<Identity> _npcsInRange = new List<Identity>();
    private Selection _npcSelection;
    
    [SerializeField]
    private GameObject _alertPopup;
    [SerializeField]
    private GameObject _keyHintSpace;
    private bool _needsKeyHint = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NpcIdentity npcIdentity = other.gameObject.GetComponentInParent<NpcIdentity>();
        if (npcIdentity)
        {
            Debug.Log("Enter" + npcIdentity.Name);
            Identity identity = npcIdentity.Identity;
            _npcsInRange.Add(identity);

            if (!_alertPopup.activeSelf && _npcsInRange.Count > 0)
            {
                _alertPopup.SetActive(true);

                if (_needsKeyHint)
                {
                    _keyHintSpace.SetActive(true);
                }
            }
        }
        // else
        // {
        //     Debug.Log("Enter" + other.gameObject);
        // }
        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        NpcIdentity npcIdentity = other.gameObject.GetComponentInParent<NpcIdentity>();
        if (npcIdentity)
        {
            Identity identity = npcIdentity.Identity;
            _npcsInRange.Remove(identity);
            Debug.Log("Exit" + npcIdentity.Name);
            
            if (_alertPopup.activeSelf && _npcsInRange.Count == 0)
            {
                _alertPopup.SetActive(false);

                if (_keyHintSpace.activeSelf)
                {
                    HideSpacebarHint();
                }

            }
        }
        // else
        // {
        //     Debug.Log("Exit" + other.gameObject);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interaction interaction = new Interaction(_npcsInRange);
            interaction.Interact();
            if (interaction.IsInteracting)
            {
                Identity identity = _npcsInRange[0];
                HideSpacebarHint();
                _needsKeyHint = false;
                
                if (OnInteraction != null)
                {
                    OnInteraction(identity);
                }
            }
            if (_npcsInRange.Count > 1)
            {
                if (_npcSelection == default)
                {
                    Time.timeScale = 0;
                    _npcSelection = new Selection(_npcsInRange.ToArray());
                    HideSpacebarHint();
                    _needsKeyHint = false;
                }
                else
                {
                    ISelectable selected = _npcSelection.MakeSelection();

                    foreach (Identity npc in _npcsInRange)
                    {
                        if (selected == npc && OnInteraction != null)
                        {
                            OnInteraction(npc);
                        }
                    }

                    _npcSelection = default;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_npcSelection != default)
            {
                _npcSelection.Scroll();
            }

        }
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