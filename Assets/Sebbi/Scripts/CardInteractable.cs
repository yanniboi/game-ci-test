
using UI;
using UnityEngine;

public class CardInteractable : Identity
{
    private CardInteractable(string name = null, string text = null, Sprite image = null, int fontSize = 50) : base(name, text, image, fontSize)
    {
    }

    public static implicit operator CardInteractable(CardItem cardItem)
    {
        CardInteractable card = new CardInteractable(cardItem.GetName(), cardItem.GetText(), cardItem.GetImage(), cardItem.GetFontSize());
        return card;
    }
}
