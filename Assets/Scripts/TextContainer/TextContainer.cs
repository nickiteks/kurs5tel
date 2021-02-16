using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewContainerTexts", menuName = "TextContainer")]
public class TextContainer : ScriptableObject
{
    [SerializeField]
    [TextArea(2, 8)]
    private string[] texts;

    public string this[int index]
    {
        get
        {
            return texts[index];
        }
    }

    public int Count { get { return texts.Length; } }
}
