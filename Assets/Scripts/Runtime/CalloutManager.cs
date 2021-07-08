using NPC;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class CalloutManager : MonoBehaviour
{

    private PlayerInteraction _player; 
    private Animator _animator;
    private Text _nameText;
    private Text _textText;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerInteraction>();
        if (_player)
        {
            _player.OnInteraction += OpenInteraction;
        }
        _animator = GetComponent<Animator>();
        
        foreach (Text textComponent in gameObject.GetComponentsInChildren<Text>())
        {
            if (textComponent.gameObject.name == "Name")
            {
                _nameText = textComponent;
            }
            if (textComponent.gameObject.name == "Text")
            {
                _textText = textComponent;
            }
        }
    }

    
    public void CloseInteraction()
    {
        // Hide the dialogue.
        _animator.SetBool("isInteracting", false);
        
        // Resume gameplay.
        Time.timeScale = 1;
    }

    public void OpenInteraction(ICalloutData identity = null)
    {
        if (identity != null)
        {
            _nameText.text = identity.GetName();
            _textText.text = identity.GetText();
        }
        
        // Stop time in game.
        Time.timeScale = 0;
                
        // Show dialogue callout.
        _animator.SetBool("isInteracting", true);
    }

    private void OnDestroy()
    {
        if (_player)
        {
            _player.OnInteraction -= OpenInteraction;
        }
    }
}