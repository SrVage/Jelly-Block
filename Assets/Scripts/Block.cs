using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject _verticalChecker = null;
    private bool _onBackground = false;
    private int _numOfChild = 0;
    private Vector3 _start = Vector3.zero;
    private Camera _cam = null;
    private void Awake()
    {
        
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnMouseDown()
    {
        if (transform.parent == null) return;
        _start = transform.parent.position;

    }

    private void OnMouseDrag()
    {
        if (transform.parent == null) return;
        Vector3 _pos = new Vector3(_cam.ScreenToWorldPoint(Input.mousePosition).x, _cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        var offset = transform.parent.position - transform.position;
        transform.parent.position = _pos+offset;
    }
    

    private void OnMouseUp()
    {
        if (transform.parent == null) return;
        if (_onBackground)
        {
            transform.parent.position = new Vector3(Mathf.RoundToInt(transform.parent.position.x), Mathf.RoundToInt(transform.parent.position.y), 0);
            Destroy(transform.parent.gameObject, 0.2f);
            _numOfChild = transform.parent.childCount;
            transform.parent.gameObject.GetComponent<DestroyGO>().DestroyParent(_numOfChild);
            Instantiate(_verticalChecker, Vector3.zero, Quaternion.identity);
        }
        else
            transform.parent.position = _start;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background"))  _onBackground = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background")) _onBackground = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
