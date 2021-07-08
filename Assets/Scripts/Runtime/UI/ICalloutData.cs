using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICalloutData
{
    string GetName();

    string GetText();

    Sprite GetImage();
    
    int GetFontSize();

}
