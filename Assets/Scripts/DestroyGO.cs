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
    private Vector3 _start = Vector3.zero;
    private Camera _cam = null;
    private Vector3 _offset = Vector3.zero;
    private float _timer = 0;
    [SerializeField] private GameObject _around = null;
    private Vector3 _arPoss = Vector3.zero;
    private GameObject _canvas = null;

    private void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _control = GameObject.Find("Control");
        _canvas = GameObject.Find("Canvas");
        _start = transform.position;
        Size();
        Invoke("Around", 0.2f);
        
    }

    private void Around()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (child.gameObject.tag == "Untagged")
            {
                _around = child.gameObject;
            }
        }
    }

    private void OnMouseDown()
    {
        if (_control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame || _control.GetComponent<Control>().numOfChecker>0) return;
        _timer = 0;
        //Handheld.Vibrate();
        if (_canvas.GetComponent<Canvas>().haptic != 0) AndroidManager.HapticFeedback();
        _start = transform.position;
        _control.GetComponent<Control>().Lift();
        Vector3 _pos = new Vector3(_cam.ScreenToWorldPoint(Input.mousePosition).x, _cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        _offset = transform.position - new Vector3(1, -1, 0) - _pos;
    }


    private void OnMouseDrag()
    {
        if (_control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame || _timer < 0.1) return;
        transform.localScale = new Vector3(1f, 1f, 1f);
        _control.GetComponent<Control>().onDrag = true;
        Vector3 _pos = new Vector3(_cam.ScreenToWorldPoint(Input.mousePosition).x, _cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        transform.position = _pos + _offset;
    }


    private void OnMouseUp()
    {
        if (_control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame) return;
        if (_timer < 0.1f)
        {
            gameObject.transform.RotateAround(_around.transform.position, Vector3.forward, 90);
            return;
        }
        _control.GetComponent<Control>().onDrag = false;
        if (OnBackground(transform.childCount))
        {
            //Handheld.Vibrate();
            if (_canvas.GetComponent<Canvas>().haptic != 0) AndroidManager.HapticFeedback();
            _control.GetComponent<Control>().Drop();
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
            Destroy(gameObject, 0.2f);
            DestroyParent(transform.childCount);
            //_control.GetComponent<Control>().move = 0;
            //_control.GetComponent<Control>().startChecker = true;

           // Invoke("Checker", 0.2f);
        }
        else
        {
            transform.position = _start;
            transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        }
    }

    private void Checker()
    {
        _control.GetComponent<Control>().move = 0;
        _control.GetComponent<Control>().startChecker = true;
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
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    Transform child = gameObject.transform.GetChild(i);
            //    child.transform.position = new Vector3(Mathf.RoundToInt(child.transform.position.x), Mathf.RoundToInt(child.transform.position.y), 0);
            //}
           // _control.GetComponent<Control>().startChecker = true;
        }
    }

    public void DestroyParent(int num)
    {
         for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(0);
            if (child.gameObject.tag == "Untagged") Destroy(child.gameObject);
                
            else 
            {
                child.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
                child.transform.position = new Vector3(Mathf.RoundToInt(child.transform.position.x), Mathf.RoundToInt(child.transform.position.y), 0);
                child.transform.tag = "StayBlock";
                child.GetComponent<BoxCollider2D>().isTrigger = true;
                child.GetComponent<BoxCollider2D>().edgeRadius = 0.1f;
                //child.GetComponent<Block>().enabled = false;
                child.GetComponent<Rigidbody2D>().isKinematic = false;
                child.parent = null; 
            }
        }
    }

    public bool OnBackground(int num)
    {      
        int _count = 0;
        for (int i = 0; i < num; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (!child.CompareTag("Untagged"))
            {
                if (child.GetComponent<Block>()._onBackground && !child.GetComponent<Block>().otherBlock)
                    _count++;
            }
        }
        if (_count == num-1) return true;
        else return false;
    }

    public void OnBlock(bool trigger)
    {
        _onBlock = trigger;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
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
        obj = Instantiate(gameObject, new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0), gameObject.transform.rotation);
        obj.tag = "Untagged";
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.GetComponent<DestroyGO>().enabled = false;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            if (child.gameObject.tag == "Untagged")
            {
                Debug.Log("Destroy");
                Destroy(child.gameObject);
            }
            else
            {
                child.GetComponentInChildren<Renderer>().material.color = new Vector4(1, 1, 1, 0.3f);
                child.GetComponentInChildren<Animator>().enabled = false;
                child.tag = "ProjBlock";
                child.GetComponent<Block>().enabled = false;
                child.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }

    public void CheckFreeSpace()
    {
        if (_checker == null)
        {
            for (int k = 0; k < 8; k++)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                _checker = Instantiate(gameObject, new Vector3(5, (4-1*k), 0), Quaternion.identity);
                _checker.GetComponent<DestroyGO>().enabled = false;
                _checker.GetComponent<BoxCollider2D>().enabled = false;
                _checker.transform.localScale = new Vector3(1, 1, 1);
                gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                _checker.tag = "Checker";

                for (int i = 0; i < _checker.transform.childCount; i++)
                {
                    Transform child = _checker.transform.GetChild(i);
                    if (child.gameObject.tag == "Untagged")
                    {
                        Destroy(child.gameObject);
                    }
                    else
                    {
                        child.GetComponentInChildren<Animator>().enabled = false;
                        child.GetComponentInChildren<SpriteRenderer>().enabled = false;
                        child.GetComponent<BoxCollider2D>().isTrigger = true;
                        child.GetComponent<Block>().enabled = true;
                    }
                }
                Checker script = _checker.AddComponent<Checker>();
                if (_checker.transform.childCount == 0)
                {
                    Destroy(_checker);
                    return;
                }
                _control.GetComponent<Control>().numOfChecker++;
            }
        }
    }
}
