using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    [Header ("Game")]
    [SerializeField] private GameObject _ingame = null;
    [SerializeField] private Text _time = null;
    [SerializeField] private Text _scores = null;
    [SerializeField] private Image _pause = null;

    [Header ("Menu")]
    [SerializeField] private Image _menu = null;
    [SerializeField] private Image _win = null;
    [SerializeField] private Text _end = null;
    [SerializeField] private Text _endScores = null;
    [SerializeField] private Image _endCoin = null;
    [SerializeField] private Image _righArrow = null;
    [SerializeField] private Image _close = null;
    [SerializeField] private Image _preferences = null;
    [SerializeField] private Image _play = null;

    [Header("Preferences")]
    [SerializeField] private Image _prefMenu = null;
    [SerializeField] private Image _accept = null;
    [SerializeField] private Image _prefIcon = null;
    [SerializeField] private Slider _music = null;
    [SerializeField] private Slider _sound = null;



    [Header ("Game objects")]
    [SerializeField] private GameObject _control = null;
    [SerializeField] private Sprite[] _sprites = null;
    [SerializeField] private Sprite[] _spritesMenu = null;

    public float mus = 1;
    public float sound = 1;




    private float r, g, b = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _ingame.SetActive(true);
        _win.gameObject.SetActive(false);
        _win.enabled = false;
        _end.enabled = false;
        _prefMenu.gameObject.SetActive(false);
        _menu.gameObject.SetActive(false);
        Invoke("TextColour", 0.1f);
    }

    private void TextColour()
    {
        if (_control.GetComponent<Control>().colourScheme == 0)
        {
            
            r = 237f/255f;
            g = 253f/255f;
            b = 104f/255f;
        }
        if (_control.GetComponent<Control>().colourScheme == 1)
        {
            r = 255f / 255f;
            g = 159f / 255f;
            b = 62f / 255f;
        }
        if (_control.GetComponent<Control>().colourScheme == 2)
        {
            r = 255f / 255f;
            g = 143f / 255f;
            b = 114f / 255f;
        }
        if (_control.GetComponent<Control>().colourScheme == 3)
        {
            r = 90f / 255f;
            g = 119f / 255f;
            b = 166f / 255f;
        }
        _time.color = new Color(r, g, b);
        _scores.color = new Color(r, g, b);
        _end.color = new Color(r, g, b);
        _endScores.color = new Color(r, g, b);
        _righArrow.color = new Color(r, g, b); ;
        _close.color = new Color(r, g, b);
        _preferences.color = new Color(r, g, b);
        _accept.color = new Color(r, g, b);
        _prefIcon.color = new Color(r, g, b);
        _pause.color = new Color(r, g, b);
        _play.color = new Color(r, g, b);
    }

    // Update is called once per frame
    void Update()
    {
             mus = _music.value;
            sound = _sound.value;
            _time.text = _control.GetComponent<Control>()._minutes.ToString() + ":" + _control.GetComponent<Control>()._seconds.ToString();
            _scores.text = _control.GetComponent<Control>().scores.ToString();
        if (_control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame)
        {
            Invoke("ShowMenu", 0.2f);
        }
    }

    private void ShowMenu()
    {
        _endScores.enabled = true;
        _endCoin.enabled = true;
        _play.enabled = false;
        _win.gameObject.SetActive(true);
        _ingame.SetActive(false);
        _end.enabled = true;
        _menu.gameObject.SetActive(true);
        _menu.sprite = _spritesMenu[_control.GetComponent<Control>().colourScheme];
        _prefMenu.sprite = _spritesMenu[_control.GetComponent<Control>().colourScheme];
        _win.enabled = true;
        _win.sprite = _sprites[_control.GetComponent<Control>().colourScheme];
        _endScores.text = _control.GetComponent<Control>().scores.ToString();

        if (_control.GetComponent<Control>().time <= 0)
        {
            _end.text = "End of time";
        }
        if (_control.GetComponent<Control>()._endOfGame)
        {
            _end.text = "No moves";
        }
    }

    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Preferences()
    {
        _prefMenu.gameObject.SetActive(true);
    }

    public void QuitPreferences()
    {
        _prefMenu.gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _play.enabled = true;
        _win.gameObject.SetActive(true);
        _ingame.SetActive(false);
        _end.enabled = true;
        _menu.gameObject.SetActive(true);
        _menu.sprite = _spritesMenu[_control.GetComponent<Control>().colourScheme];
        _prefMenu.sprite = _spritesMenu[_control.GetComponent<Control>().colourScheme];
        _win.enabled = true;
        _win.sprite = _sprites[_control.GetComponent<Control>().colourScheme];
        _endScores.enabled = false;
        _endCoin.enabled = false;
        _end.text = "Pause";
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _win.gameObject.SetActive(false);
        _ingame.SetActive(true);
        _menu.gameObject.SetActive(false);

    }
}
