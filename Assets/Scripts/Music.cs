using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private GameObject _canvas = null;
    private AudioSource _as = null;
    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _as.volume = _canvas.GetComponent<Canvas>().mus;
    }
}
