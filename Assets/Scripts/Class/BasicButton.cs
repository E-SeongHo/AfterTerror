using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicButton
{
    public GameObject button;
    public int index;

    public BasicButton(GameObject button_prefab, int idx)
    {
        button = button_prefab;
        index = idx;
    }
}

public class SpecialButton
{
    public GameObject button;
    public int index;

    // 0: 연타, 1: 지속, 2: 타이밍
    public int type; 
    public int times;
    public int during;

    public SpecialButton(GameObject button_prefab, int idx, int type_idx)
    {
        button = button_prefab;
        index = idx;
        type = type_idx;
        times = Random.Range(3, 6);
        during = Random.Range(1, 3);
    }
}
