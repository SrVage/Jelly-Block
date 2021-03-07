using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyGO : MonoBehaviour
{
    private GameObject obj = null;
    private bool _onBlock = false;
    public bool onDrag = false;
    public void DestroyParent(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(0);
            child.transform.tag = "StayBlock";
            child.parent = null;
        }
    }

    public bool OnBackground(int num)
    {
        bool _back = false;
        int _count = 0;
        for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (child.GetComponent<Block>()._onBackground)
                _count++;
        }
        if (_count == num && !_onBlock) _back = true;
        return _back;
    }

    public void OnBlock(bool trigger)
    {
        _onBlock = trigger;
    }

    private void Update()
    {
        if (OnBackground(gameObject.transform.childCount) && obj == null)
        {
            obj = Instantiate(gameObject, new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0), Quaternion.identity);
            obj.transform.localScale = new Vector3(1f, 1f, 1);
            obj.GetComponent<DestroyGO>().enabled = false;
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                Transform child = obj.transform.GetChild(i);
                child.GetComponent<Renderer>().material.color = new Vector4(1, 1, 1, 0.3f);
                child.GetComponent<BoxCollider2D>().enabled = false;
            }
            Destroy(obj, 0.1f);
        }
    }
}
