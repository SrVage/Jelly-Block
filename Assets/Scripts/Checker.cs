using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private GameObject _control = null;
    private float _startYPos = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        _control = GameObject.Find("Control");
        _startYPos = transform.position.y;
        Invoke("Check", 0.01f);
    }
    private void Check()
    {
        int _count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
                if (child.GetComponent<Block>()._onBackground && !child.GetComponent<Block>().otherBlock)
                    _count++;
        }
        if (_count == transform.childCount)
        {
            _control.GetComponent<Control>().move++;
        }
        Move();
    }
    private void Move()
    {
        if (transform.position.x<13)
        {
            transform.position += new Vector3(1, 0, 0);
            //Check();
            Invoke("Check", 0.04f);
        }
        else
        {
            //if (transform.position.y > ( _startYPos-1))
            //{
            //    transform.position += new Vector3(-9, -1, 0);
            //    //Check();
            //    Invoke("Check", 0.04f);
            //}
            //else
            //{
                _control.GetComponent<Control>().numOfChecker--;
                Destroy(gameObject);
            }

        }
        
    }
