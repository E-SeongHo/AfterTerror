using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicButton
{
    public GameObject button;
    public int index;

    public BasicButton(GameObject buttonPrefab, int idx)
    {
        button = buttonPrefab;
        idx = index;
    }
}
