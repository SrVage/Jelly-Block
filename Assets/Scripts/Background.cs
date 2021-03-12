using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Sprite[] _background = null;
    private SpriteRenderer _sr = null;
    [SerializeField] private GameObject _control = null;

    // Start is called before the first frame update
    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        Invoke("ChangeBackground", 0.1f);
    }

    private void ChangeBackground()
    {
        _sr.sprite = _background[_control.GetComponent<Control>().colourScheme];
    }

    // Update is called once per frame
}
