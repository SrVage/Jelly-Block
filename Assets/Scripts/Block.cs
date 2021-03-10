using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameObject _control = null;
    public bool _onBackground = false;
    public bool otherBlock = false;
    private int _numOfChild = 0;
    private Vector3 _start = Vector3.zero;
    private Camera _cam = null;
    public Sprite sprite = null;
    public Animator anim = null;
    public float timeOfWait = 0f;
    private void Awake()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);  
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _control = GameObject.Find("Control");
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_control.GetComponent<Control>()._endOfGame)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/icons2_30");
            anim.SetTrigger("Stone");
        }
    }

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void OnMouseDown()
    {
        if (transform.parent == null || _control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame) return;
        _start = transform.parent.position;
        _control.GetComponent<Control>().Lift();

    }

    private void OnMouseDrag()
    {
        if (transform.parent == null || _control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame) return;
        transform.parent.localScale = new Vector3 (1f,1f,1f);
        _control.GetComponent<Control>().onDrag = true;
        Vector3 _pos = new Vector3(_cam.ScreenToWorldPoint(Input.mousePosition).x, _cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        var offset = transform.parent.position - transform.position;
        transform.parent.position = _pos+offset;
    }
    

    private void OnMouseUp()
    {
        if (transform.parent == null || _control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame) return;
        _control.GetComponent<Control>().onDrag = false;
        _numOfChild = transform.parent.childCount;
        if (transform.parent.gameObject.GetComponent<DestroyGO>().OnBackground(_numOfChild))
        {
            _control.GetComponent<Control>().Drop();
            transform.parent.localScale = new Vector3(1, 1, 1);
            transform.parent.position = new Vector3(Mathf.RoundToInt(transform.parent.position.x), Mathf.RoundToInt(transform.parent.position.y), 0);
            Destroy(transform.parent.gameObject, 0.2f);
            transform.parent.gameObject.GetComponent<DestroyGO>().DestroyParent(_numOfChild);
           // gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Invoke("FalseTrig", 0.2f);
        }
        else
        {
            transform.parent.position = _start;
            transform.parent.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
    }

    private void FalseTrig()
    {
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        _control.GetComponent<Control>().move = 0;
        _control.GetComponent<Control>().startChecker = true;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("StayBlock")) transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(true);
        
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) otherBlock = true;//transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(true);
        if (collision.gameObject.CompareTag("Background")) _onBackground = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) otherBlock = false; //transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(false);
        if (collision.gameObject.CompareTag("Background")) _onBackground = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) otherBlock = true;//transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(true);
        if (collision.gameObject.CompareTag("Background")) _onBackground = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) otherBlock = false; //transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(false);
        if (collision.gameObject.CompareTag("Background")) _onBackground = false;
    }
}
