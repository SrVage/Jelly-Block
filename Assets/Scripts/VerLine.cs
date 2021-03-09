using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerLine : MonoBehaviour
{
    public List<GameObject> _obj = new List<GameObject>();
    public List<GameObject> _obg = new List<GameObject>();
    private GameObject _ref = null;
    public GameObject[] _objects = null;
    private bool _startAnim = false;
    private GameObject _control = null;
    private bool _isChange = false;

    private void Awake()
    {
        _control = GameObject.Find("Control");
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ProjBlock")) _ref = collision.gameObject;
        if (collision.gameObject.CompareTag("ProjBlock") || collision.gameObject.CompareTag("StayBlock"))
        {
            _obj.Add(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ProjBlock"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Toggle", 0.02f);
        }
    }

    private void Toggle()
    {
        _obj.Clear();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

    }

    private void FixedUpdate()
    {
        if (_control.GetComponent<Control>().time <= 0) return;
        if (_obj.Count == 9 && !_startAnim)
        {
            if (!_control.GetComponent<Control>().onDrag)
            {
                Debug.Log("Bam");
                _startAnim = true;
                Invoke("DestroyPlusScores", 0.25f);
            }
            else if (_control.GetComponent<Control>().onDrag && !_isChange) ChangeSprite();
        }
        else
        {
            ChangeOldSprite();
        }
    }

    private void Update()
    {
        if (_control.GetComponent<Control>().clear)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Toggle", 0.02f);
            _control.GetComponent<Control>().clear = false;
        }

    }

    private void DestroyPlusScores()
    {
        _control.GetComponent<Control>().DestroyLline();
        foreach (GameObject obj in _obj)
        {
            obj.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/icons2_36");
            Animator anim = obj.GetComponentInChildren<Animator>();
            anim.SetTrigger("Destroy");
            obj.GetComponent<BoxCollider2D>().enabled = false;
        }
        Invoke("Coin", 0.5f);
    }

    private void Coin()
    {
        foreach (GameObject obj in _obj)
        {
            Destroy(obj);
        }
        _control.GetComponent<Control>().ScoresPlus();
        _control.GetComponent<Control>().scores += 100;
        _control.GetComponent<Control>().clear = true;
        _obj.Clear();
        _startAnim = false;
    }

    private void ChangeSprite()
    {
        _control.GetComponent<Control>().isSimilarBlocks = true;
        _isChange = true;
        foreach (GameObject obj in _obj)
        {
            obj.GetComponentInChildren<SpriteRenderer>().sprite = _ref.GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }

    private void ChangeOldSprite()
    {
        if (_control.GetComponent<Control>().isSimilarBlocks) return;
        if (_obj.Count < 9 && _control.GetComponent<Control>().onDrag && _isChange)
        {
            _isChange = false;
            foreach (GameObject obj in _obj)
            {
                obj.GetComponentInChildren<SpriteRenderer>().sprite = obj.GetComponent<Block>().sprite;
            }

        }
    }

}
