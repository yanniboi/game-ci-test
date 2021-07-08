using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string Name;
    public string Message;
    public int FontSize = 50;
    public Sprite Image;
}
