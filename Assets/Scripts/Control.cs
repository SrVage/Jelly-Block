using System.Collections;
using UnityEngine;
public class Control : MonoBehaviour
{
    public bool onDrag = false;
    public int scores = 0;
    [SerializeField] private GameObject _horizontal = null;
    [SerializeField] private GameObject _vertical = null;
    private Vector3 _posHor = new Vector3(10.5f, -2, 1);
    private Vector3 _posVer = new Vector3(7, 1.5f, 1);
    private Vector3 _posBack = new Vector3(7f, -2f, 1);
    public bool isSimilarBlocks = false;
    private float _timer = 0;
    public bool clear = false;
    [SerializeField] private AudioClip[] audioClips = null;
    private AudioSource audio = null;
    public float time = 20;
    public int _minutes = 0;
    public int _seconds = 0;
    public int move = 0;
    public bool startChecker = false;
    public bool _endOfGame = false;
    private GameObject[] obj = null;
    public int numOfChecker = 0;
    public int _recheck = 0;
    public int colourScheme = 0;
    [SerializeField] private GameObject _canvas = null;
    [SerializeField] private GameObject _backgroundBlock = null;
    private GameObject[] _blocks = null;
    private int _prevCount = 0;
    private float _backtime = 0;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        colourScheme = 0; //= Random.Range(0, 4);
        audio = GetComponent<AudioSource>();
        for (int i =0; i<8; i++)
        {
            Instantiate(_horizontal, _posHor, Quaternion.identity);
            _posHor += Vector3.up;
            Instantiate(_vertical, _posVer, Quaternion.identity);
            _posVer += new Vector3(1, 0, 0);
        }
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                Instantiate(_backgroundBlock, _posBack, Quaternion.identity);
                _posBack += new Vector3(1, 0, 0);
            }
            _posBack += new Vector3(-8, 1, 0);

        }

    }

    private void Update()
    {
        _blocks = null;
        _blocks = GameObject.FindGameObjectsWithTag("Bs");
        if (_blocks.Length!=_prevCount)
        {
            move = 0;
            startChecker = true;
        }
        _prevCount = _blocks.Length;
        audio.volume = _canvas.GetComponent<Canvas>().sound;
        if (time <= 0 || _endOfGame) return;
        time -= Time.deltaTime;
        _seconds = System.Convert.ToInt32(time)%60;
        _minutes =System.Convert.ToInt32((time)-_seconds)/60;
        _timer += Time.deltaTime;
        if (_timer > 0.5f)
        {
            isSimilarBlocks = false;
            _timer = 0;
        }
        if (startChecker)
        {
           // StopCoroutine("Check");
            Invoke("Reset", 0.05f);
        }
        if (time<=11)
        {
            _backtime += Time.deltaTime;
            if (_backtime >=1)
            {
                // Handheld.Vibrate();
                if (_canvas.GetComponent<Canvas>().haptic!=0) AndroidManager.HapticFeedback();
                _backtime = 0;
            }
        }
    }

    //IEnumerator Check ()
    //{
    //    int ch = 0;
    //    Debug.Log("cor");
    //    yield return new WaitForSeconds(0.1f);
    //    for (int i = 0; i < 4; i++)
    //    {
    //        if (numOfChecker == 0 && move == 0)
    //        {
    //            ch++;
    //            Debug.Log(ch);
    //        }
    //            yield return new WaitForSeconds(0.2f);
    //    }
    //    if (ch > 2)
    //    {
    //        EndOf();
    //    }
    //    else StopCoroutine("Check");
    //}


    private void Reset()
    {
        Debug.Log("reset");
        //StopCoroutine("Check");
        startChecker = false;
        Invoke("Check", 1f);
        //StartCoroutine("Check");
    }

    private void Check()
    {
        Debug.Log("check");
        if (numOfChecker == 0 && move == 0) EndOf();
    }

    private void EndOf()
    {
        Debug.Log("end");
        _endOfGame = true;
        GameOver();
    }

    public void Lift()
    {
        audio.PlayOneShot(audioClips[0]);
    }
    public void Drop()
    {
        audio.PlayOneShot(audioClips[1]);
    }

    public void DestroyLline()
    {
        audio.PlayOneShot(audioClips[2]);
    }

    public void ScoresPlus()
    {
        audio.PlayOneShot(audioClips[3]);
    }

    public void GameOver()
    {
        audio.PlayOneShot(audioClips[4]);
    }
}
