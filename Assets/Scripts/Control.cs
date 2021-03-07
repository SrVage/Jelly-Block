using UnityEngine;
public class Control : MonoBehaviour
{
    public bool onDrag = false;
    [SerializeField] private GameObject _horizontal = null;
    [SerializeField] private GameObject _vertical = null;
    private Vector3 _posHor = new Vector3(10, -2, 1);
    private Vector3 _posVer = new Vector3(6, 2, 1);

    private void Awake()
    {
        for (int i =0; i<9; i++)
        {
            Instantiate(_horizontal, _posHor, Quaternion.identity);
            _posHor += Vector3.up;
            Instantiate(_vertical, _posVer, Quaternion.identity);
            _posVer += new Vector3(1, 0, 0);
        }
    }
}
