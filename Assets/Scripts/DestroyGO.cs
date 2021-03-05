using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyGO : MonoBehaviour
{
    public bool onDrag = false;
    public void DestroyParent(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(0);
            child.parent = null;
        }
    }

    public void OnBackground(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            child.parent = null;
        }
    }
}
