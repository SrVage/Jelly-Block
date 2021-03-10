using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyGO : MonoBehaviour
{
    private GameObject obj = null;
    private GameObject _checker = null;
    private bool _onBlock = false;
    private bool _back = false;
    private GameObject _control = null;
    private Vector3 _lastPos = Vector3.zero;

    private void Start()
    {
        _control = GameObject.Find("Control");
        Size();
    }
    private void Size()
    {
        if (transform.localScale.x < 0.6f)
        {
            transform.localScale = transform.localScale + new Vector3(0.01f, 0.01f, 0.01f);
            Invoke("Size", 0.01f);
        }
        else
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            _control.GetComponent<Control>().startChecker = true;
        }
    }

    public void DestroyParent(int num)
    {
         for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(0);
            child.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            child.transform.position = new Vector3(Mathf.RoundToInt(child.transform.position.x), Mathf.RoundToInt(child.transform.position.y), 0);
            child.transform.tag = "StayBlock";
            child.GetComponent<BoxCollider2D>().isTrigger = false;
            child.parent = null;
        }
    }

    public bool OnBackground(int num)
    {      
        int _count = 0;
        for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (child.GetComponent<Block>()._onBackground && !child.GetComponent<Block>().otherBlock)
                _count++;
        }
        if (_count == num) return true;
        else return false;
    }

    public void OnBlock(bool trigger)
    {
        _onBlock = trigger;
    }

    private void Update()
    {
        if (OnBackground(gameObject.transform.childCount) && obj == null && _control.GetComponent<Control>().onDrag)
        {
            CreateProjection();
        }
        else if (OnBackground(gameObject.transform.childCount) && obj != null && _control.GetComponent<Control>().onDrag)
        {
            obj.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        }
       else Destroy(obj);
        if (_control.GetComponent<Control>().startChecker)
        { 
            CheckFreeSpace();
                }
    }


    private void CreateProjection()
    {
        obj = Instantiate(gameObject, new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        obj.transform.localScale = new Vector3(1f, 1f, 1);
        obj.GetComponent<DestroyGO>().enabled = false;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            child.GetComponentInChildren<Renderer>().material.color = new Vector4(1, 1, 1, 0.3f);
            child.GetComponentInChildren<Animator>().enabled = false;
            child.tag = "ProjBlock";
            child.GetComponent<Block>().enabled = false;
            child.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void CheckFreeSpace()
    {
        if (_checker == null)
        {
            _checker = Instantiate(gameObject, new Vector3(4, 5, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            _checker.transform.localScale = new Vector3(1f, 1f, 1);
            _checker.GetComponent<DestroyGO>().enabled = false;
            _checker.tag = "Checker";
            for (int i = 0; i < _checker.transform.childCount; i++)
            {
                Transform child = _checker.transform.GetChild(i);
                child.GetComponentInChildren<Animator>().enabled = false;
                child.GetComponentInChildren<SpriteRenderer>().enabled = false;
                child.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            Checker script = _checker.AddComponent<Checker>();
            _control.GetComponent<Control>().numOfChecker++;
        }
    }
}
