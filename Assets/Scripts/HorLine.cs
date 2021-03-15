using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorLine : MonoBehaviour
{
    public List<GameObject> _obj = new List<GameObject>();
    //public HashSet<GameObject> _obj = new HashSet<GameObject>();
    private GameObject _ref = null;
    private GameObject _plusScores = null;
    //public GameObject[] _objects = null;
    private bool _startAnim = false;
    private GameObject _control = null;
    public bool _isChange = false;

    private void Awake()
    {
        _plusScores = Resources.Load<GameObject>("Prefabs/plusScores");
        _control = GameObject.Find("Control");
    }

    // Start is called before the first frame update
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("ProjBlock")) _ref = collision.gameObject;
    //    if (collision.gameObject.CompareTag("ProjBlock") || collision.gameObject.CompareTag("StayBlock"))
    //    {
    //        _obj.Add(collision.gameObject);
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ProjBlock")) _ref = collision.gameObject;
        if (collision.gameObject.CompareTag("ProjBlock") || collision.gameObject.CompareTag("StayBlock"))
        {
            if (!_obj.Contains(collision.gameObject))
            _obj.Add(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_startAnim) return;
        if (collision.gameObject.CompareTag("ProjBlock") || collision.gameObject.CompareTag("StayBlock"))
        {
            _obj.Remove(collision.gameObject);
        }
    }

    private void Toggle()
    {
        //_obj.Clear();
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;
        _control.GetComponent<Control>().clear = false;


    }

    private void FixedUpdate()
    {
        if (_control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame) return;
        if (_obj.Count == 8 && !_startAnim)
        {
            if (!_control.GetComponent<Control>().onDrag)
            {
                _startAnim = true;
                Invoke("DestroyPlusScores", 0.15f);
               // DestroyPlusScores();
            }
            else if (_control.GetComponent<Control>().onDrag && !_isChange) Invoke("Check", 0.01f);
        }
        else
        {
            if (_isChange)
                Invoke("CheckBack", 0.01f);
        }
    }



    private void Check()
    {
        if (_obj.Count == 8 && _control.GetComponent<Control>().onDrag && !_isChange)
            ChangeSprite();
    }

    private void Update()
    {
        if (_control.GetComponent<Control>().clear)
        {
            _obj.Clear();
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Toggle", 0.02f);
            //_control.GetComponent<Control>().clear = false;
        }

    }

    private void DestroyPlusScores()
    {
        if (_obj.Count != 8)
        {
            _startAnim = false;
            return;
        }
        else
        {
            _control.GetComponent<Control>().DestroyLline();
            foreach (GameObject obj in _obj)
            {
                obj.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/icons2_36");
                Animator anim = obj.GetComponentInChildren<Animator>();
                anim.SetTrigger("Destroy");
                obj.GetComponent<BoxCollider2D>().enabled = false;
            }
            Invoke("Coin", 0.2f);
        }
    }

    private void Coin()
    {
        foreach (GameObject obj in _obj)
        {
            Destroy(obj);
        }
        _control.GetComponent<Control>().ScoresPlus();
        var plus = Instantiate(_plusScores, gameObject.transform.position, Quaternion.identity);
        Destroy(plus, 1.0f);
        _control.GetComponent<Control>().scores += 100;
        _control.GetComponent<Control>().clear = true;
        //_obj.Clear();
        _startAnim = false;
    }

    private void ChangeSprite()
    {
        //_control.GetComponent<Control>().isSimilarBlocks = true;
        if (_obj != null)
        {
            foreach (GameObject obj in _obj)
            {
                obj.GetComponentInChildren<SpriteRenderer>().sprite = _ref.GetComponentInChildren<SpriteRenderer>().sprite;
                Animator anim = obj.GetComponentInChildren<Animator>();
                anim.SetBool("vibr", true);
            }
            _isChange = true;
        }
    }

    private void CheckBack()
    {
        if (_isChange)
            ChangeOldSprite();
    }

    private void ChangeOldSprite()
    {
        //if (_control.GetComponent<Control>().isSimilarBlocks) return;
        if (_obj.Count < 8 && _isChange)
        {
            if (_obj != null)
            {
                foreach (GameObject obj in _obj)
                {
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = obj.GetComponent<Block>().sprite;
                    Animator anim = obj.GetComponentInChildren<Animator>();
                    anim.SetBool("vibr", false);
                }
            }
            _isChange = false;
        }
    }

}
