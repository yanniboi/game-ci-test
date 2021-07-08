using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{

    private CardInteraction parent;
    
    void Start()
    {
        parent = GetComponentInParent<CardInteraction>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger started");
        CardItem card = other.gameObject.GetComponent<CardItem>();
        if (card)
        {
            parent.SetCard(card);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("trigger ended");

        CardItem card = other.gameObject.GetComponent<CardItem>();
        if (card)
        {
            parent.ClearCard();
        }
    }
}
