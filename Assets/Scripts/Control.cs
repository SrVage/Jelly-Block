using UnityEngine;
public class Control : MonoBehaviour
{
    public bool onDrag = false;
    public int scores = 0;
    [SerializeField] private GameObject _horizontal = null;
    [SerializeField] private GameObject _vertical = null;
    private Vector3 _posHor = new Vector3(10.5f, -2, 1);
    private Vector3 _posVer = new Vector3(7, 1.5f, 1);
    [SerializeField] private GUIStyle _gui = null;
    [SerializeField] private GUIStyle _gui2 = null;
    [SerializeField] private GUIStyle _gui3 = null;
    public bool isSimilarBlocks = false;
    private float _timer = 0;
    public bool clear = false;
    [SerializeField] private AudioClip[] audioClips = null;
    private AudioSource audio = null;
    public float time = 20;
    private int _minutes = 0;
    private int _seconds = 0;
    public int move = 0;
    public bool startChecker = false;
    public bool _endOfGame = false;
    private GameObject[] obj = null;

    private void Awake()
    {
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
            Invoke("Reset", 0.05f);
        }
    }


    private void Reset()
    {
            startChecker = false;
            Invoke("CheckEnd", 3f);   
    }

    private void CheckEnd()
    {
        if (move == 0 && !startChecker)
            Invoke("EndOf", 3f);
    }

    private void EndOf()
    {
        if (move == 0) _endOfGame = true;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 550, 100, 400, 200), scores.ToString(), _gui);
        GUI.Label(new Rect(200, 100, 400, 200), _minutes.ToString()+":"+_seconds.ToString(), _gui);
        //GUI.Label(new Rect(Screen.width/2, 50, 100, 50), move.ToString(), _gui);
       // if (_endOfGame) GUI.Label(new Rect(Screen.width / 2, 50, 300, 50), "Нет ходов", _gui);
        if (time <= 0)
        {
            GUI.Box(new Rect((Screen.width / 2) - 300, 300, 600, 800), "Время вышло", _gui2);
            GUI.Label(new Rect((Screen.width/2)-50, 500, 100, 50), "Очки: " + scores.ToString(), _gui3);
            if (GUI.Button(new Rect((Screen.width / 2) - 150, 700, 300, 200), "Повтор", _gui2)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        if (_endOfGame)
        {
            GUI.Box(new Rect((Screen.width / 2) - 300, 300, 600, 800), "Ходов нет", _gui2);
            GUI.Label(new Rect((Screen.width / 2) - 50, 500, 100, 50), "Очки: " + scores.ToString(), _gui3);
            if (GUI.Button(new Rect((Screen.width / 2) - 150, 700, 300, 200), "Повтор", _gui2)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
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
}
