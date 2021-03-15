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

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        colourScheme = Random.Range(0, 4);
        audio = GetComponent<AudioSource>();
        for (int i =0; i<8; i++)
        {
            Instantiate(_horizontal, _posHor, Quaternion.identity);
            _posHor += Vector3.up;
            Instantiate(_vertical, _posVer, Quaternion.identity);
            _posVer += new Vector3(1, 0, 0);
        }
    }

    private void Update()
    {
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
