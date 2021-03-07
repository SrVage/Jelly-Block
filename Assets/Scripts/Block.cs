using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameObject _control = null;
    [SerializeField] private GameObject _verticalChecker = null;
    [SerializeField] private GameObject _horizontalChecker = null;
    public bool _onBackground = false;
    public bool _otherBlock = false;
    private int _numOfChild = 0;
    private Vector3 _start = Vector3.zero;
    private Camera _cam = null;
    public Sprite sprite = null;
    private void Awake()
    {
       
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _control = GameObject.Find("Control");
    }

    private void OnMouseDown()
    {
        if (transform.parent == null) return;
        _start = transform.parent.position;

    }

    private void OnMouseDrag()
    {
        if (transform.parent == null) return;
        transform.parent.localScale = new Vector3 (1.1f,1.1f,1);
        _control.GetComponent<Control>().onDrag = true;
        Vector3 _pos = new Vector3(_cam.ScreenToWorldPoint(Input.mousePosition).x, _cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        var offset = transform.parent.position - transform.position;
        transform.parent.position = _pos+offset;
    }
    

    private void OnMouseUp()
    {
        if (transform.parent == null) return;
        transform.parent.localScale = new Vector3(1f, 1f, 1);
        _control.GetComponent<Control>().onDrag = false;
        _numOfChild = transform.parent.childCount;
        if (transform.parent.gameObject.GetComponent<DestroyGO>().OnBackground(_numOfChild))
        {
            transform.parent.position = new Vector3(Mathf.RoundToInt(transform.parent.position.x), Mathf.RoundToInt(transform.parent.position.y), 0);
            Destroy(transform.parent.gameObject, 0.2f);
            transform.parent.gameObject.GetComponent<DestroyGO>().DestroyParent(_numOfChild);
            //Instantiate(_verticalChecker, Vector3.zero, Quaternion.identity);
            //Instantiate(_horizontalChecker, Vector3.zero, Quaternion.identity);
           // gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
            transform.parent.position = _start;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(true);
        if (collision.gameObject.CompareTag("Background")) _onBackground = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StayBlock")) transform.parent.gameObject.GetComponent<DestroyGO>().OnBlock(false);
        if (collision.gameObject.CompareTag("Background")) _onBackground = false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.shapeCount);
        if (collision.CompareTag("Block")) collision.gameObject.GetComponentInParent<DestroyGO>().OnBlock(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block")) collision.gameObject.GetComponentInParent<DestroyGO>().OnBlock(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
