using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorLine : MonoBehaviour
{
    private float _timer = 0;
    public List<GameObject> _obj = new List<GameObject>();
    private GameObject _ref = null;

    private GameObject _control = null;

    private void Awake()
    {
        _control = GameObject.Find("Control");
        Toggle();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block")) _ref = collision.gameObject;
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("StayBlock"))
            _obj.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_obj.Count == 9 && !_control.GetComponent<Control>().onDrag)
        {
            foreach (GameObject obj in _obj)
            {
                Destroy(obj);
            }
        }
        if (_obj.Count == 9 && _control.GetComponent<Control>().onDrag)
        {
            for (int i = 0; i < 9; i++)
            {
                _obj[i].GetComponent<SpriteRenderer>().sprite = _ref.GetComponent<SpriteRenderer>().sprite;
            }
        }

        if (_obj.Count < 9 && _timer>0.2 && _timer <0.4)
        {
            foreach (GameObject obj in _obj)
            {
               obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<Block>().sprite;
            }
        }

        if (_obj.Count < 9 && _timer > 0.4) _timer = 0;


            _obj.Clear();
        //_obj.Remove(collision.gameObject);
    }

    private void Toggle()
    {
        if (GetComponent<BoxCollider2D>().enabled) Invoke("Toggle", 0.02f);
        else Invoke("Toggle", 0.02f);
            GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;

    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }
}
