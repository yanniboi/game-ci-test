using NPC;
using UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CardManager : MonoBehaviour
{

    private CardInteraction _interaction; 
    private Animator _animator;
    private Text _nameText;
    private Text _textText;
    private Image _profileImage;
    private ButtonKey _closeButton;
    
    void Start()
    {
        _interaction = FindObjectOfType<CardInteraction>();
        Debug.Log("OnInteraction registered");

        _interaction.OnInteraction += OpenInteraction;
        _animator = GetComponent<Animator>();
        
        foreach (Text textComponent in gameObject.GetComponentsInChildren<Text>())
        {
            if (textComponent.gameObject.name == "Name")
            {
                _nameText = textComponent;
            }
            else if (textComponent.gameObject.name == "Text")
            {
                _textText = textComponent;
            }
        }
   
        foreach (Image imageComponent in gameObject.GetComponentsInChildren<Image>())
        {
            if (imageComponent.gameObject.name == "Profile")
            {
                _profileImage = imageComponent;
            }
        }
        
        foreach (ButtonKey buttonComponent in gameObject.GetComponentsInChildren<ButtonKey>())
        {
            if (buttonComponent.gameObject.name == "Button")
            {
                _closeButton = buttonComponent;
            }
        }
    }

    
    public void CloseInteraction()
    {
        Debug.Log("CloseInteraction started");
        _closeButton.IsShowing = false;

        // Hide the dialogue.
        _animator.SetBool("isInteracting", false);
        
        // Resume gameplay.
        Time.timeScale = 1;
    }

    public void OpenInteraction(ICalloutData identity = null)
    {

        _closeButton.IsShowing = true;
        
        if (identity != null)
        {
            _nameText.text = identity.GetName();
            _textText.text = identity.GetText();
            _textText.fontSize = identity.GetFontSize();
            _profileImage.sprite = identity.GetImage();
        }
        
        // Stop time in game.
        Time.timeScale = 0;
                
        // Show dialogue callout.
        _animator.SetBool("isInteracting", true);
    }

    private void OnDestroy()
    {
        _interaction.OnInteraction -= OpenInteraction;
    }
}