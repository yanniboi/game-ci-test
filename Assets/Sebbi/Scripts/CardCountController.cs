using System;
using System.Collections.Generic;
using NPC;
using TMPro;
using UI;
using UnityEngine;

public class CardCountController : MonoBehaviour
{
    public static event Action OnAllCardsFound; 
    
    private ShopperCounter _counter;  
    private int _currentCount = 0;
    private int _totalCount = 0;
    private CardItem[] _cardItems;

    [SerializeField] private TextMeshProUGUI _currentCountText;
    [SerializeField] private TextMeshProUGUI _totalCountText;

    // Start is called before the first frame update
    void Start()
    {
        _counter = new ShopperCounter();
        _currentCount = _counter.Count;
        _totalCount = _cardItems.Length;

        UpdateUI();
    }

    public void CheckFoundAllCards()
    {
        if (_currentCount == _totalCount)
        {
            OnAllCardsFound?.Invoke();
        }
    }
    
    private void IncrementCounter()
    {
        _counter.Increase();
        _currentCount = _counter.Count;
        UpdateUI();
    }
    
    private void DescreaseCounter()
    {
        _counter.Decrease();
        _currentCount = _counter.Count;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _currentCountText.text = _currentCount.ToString();
        _totalCountText.text = _totalCount.ToString();
    }

    private void OnEnable()
    {
        _cardItems = FindObjectsOfType<CardItem>();
        foreach (CardItem card in _cardItems)
        {
            card.OnCardFound += IncrementCounter;
        }
    }

    private void OnDisable()
    {
        foreach (CardItem card in _cardItems)
        {
            card.OnCardFound -= IncrementCounter;
        }
    }
}
