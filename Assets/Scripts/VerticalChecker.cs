using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalChecker : MonoBehaviour
{
    public List<GameObject> _obj = new List<GameObject>();
    private Vector3 _oldPos = new Vector3 (5f, 2f, 0);
    private int _count = 0;
    // Start is called before the first frame update
    public void Move()
    {

        transform.position = _oldPos + new Vector3(1, 0, 0);
        StartCoroutine("MoveAndWait");
    }
    private IEnumerator MoveAndWait()
    {
        yield return new WaitForSeconds(0.05f);
        _oldPos = transform.position;
        transform.position = new Vector3(-10, 0, 0);
        yield return new WaitForSeconds(0.05f);
        Move();
       // StopCoroutine("MoveAndWait");
    } 
    private void Start()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
            _obj.Add(collision.gameObject);
    }

    private IEnumerator DestrObj()
    {
        Debug.Log("Destroy");
       foreach (GameObject obj in _obj)
        {
            Destroy(obj);
            //_obj.RemoveAt(i-1);
        }
        yield return null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_obj.Count >= 9)
        {
            foreach (GameObject obj in _obj)
            {
                Destroy(obj);
                //_obj.RemoveAt(i-1);
            }
        } //StartCoroutine("DestrObj");
        _obj.Clear();
    }

    private void Update()
    {
        if (transform.position.x > 14.5)
            Destroy(gameObject);
    }

}
