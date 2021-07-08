using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

// public class CardItem : MonoBehaviour, ICalloutData
public class CardItem : MonoBehaviour
{
    [SerializeField]
    private Card _card;
    private SpriteRenderer _cardSprite;

    private ParticleSystem _particles;
    private AudioSource _audio;
    public bool Collected = false;
    
    public event Action OnCardFound;

    public string GetName()
    {
        return _card.Name;
    }

    public string GetText()
    {
        return _card.Message;

    }

    public Sprite GetImage()
    {
        return _card.Image;
    }
    
    public int GetFontSize()
    {
        return _card.FontSize;
    }

    private void Start()
    {
        float start = Random.Range(3, 8);
        float interval = Random.Range(5, 10);
        _particles = GetComponentInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        InvokeRepeating ("TriggerParticles", start, interval);
        _cardSprite = GetComponent<SpriteRenderer>();
    }

    private void TriggerParticles()
    {
        if (Collected)
            return;
        
        int count = Random.Range(1, 3);
        _particles.Emit(count);
    }

    public void Found()
    {
        Collected = true;
        _audio.Play();
        OnCardFound?.Invoke();
    }

    public void ShowCard()
    {
        _cardSprite.enabled = true;
    }
}
