using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab = null;
    [SerializeField] private Sprite [] _sprites = null;
    private GameObject _control = null;

    private struct Block
    {
        public int x;
        public int y;
        public GameObject block;

        public Block (int x, int y, GameObject block)
        {
            this.x = x;
            this.y = y;
            this.block = block;
        }
    }

    private Block[] _piece = new Block[]
    {
        new Block(),
        new Block(),
        new Block(),
        new Block(),
        new Block(),
        new Block(),
        new Block(),
        new Block(),
        new Block(),
};

    private Block[,] _block;

    private int[,] _shapes = new int[,]
    {
        {5, 20, 20, 20, 20, 20, 20, 20 ,20}, //одиночный
        {5, 6, 20, 20, 20, 20, 20, 20 ,20}, //двойной
        {5, 6, 7, 20, 20, 20, 20, 20 ,20}, //тройной прямой
        {5, 6, 10, 20, 20, 20, 20, 20 ,20}, //тройной изогнутый
        {4, 5, 6, 7, 20, 20, 20, 20 ,20}, //четверной прямой
        {4, 5, 6, 2, 20, 20, 20, 20 ,20}, //четверной изогнутый
        {5, 6, 9, 10, 20, 20, 20, 20 ,20}, //квадрат
        {5, 6, 8, 9, 20, 20, 20, 20 ,20}, //z
        {4, 8, 9, 12, 20, 20, 20, 20 ,20}, //t
        {0, 1, 2, 4, 5, 6, 8, 9 ,10}, //девятирной квадрат
        {0, 1, 2, 4, 20, 6, 8, 9 ,10}, //восьмерной квадрат
    };

    // Start is called before the first frame update
    void Start()
    {
        _control = GameObject.Find("Control");
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (_control.GetComponent<Control>().time <= 0 || _control.GetComponent<Control>()._endOfGame) return;
        if (GameObject.Find("New Game Object") == null)
            Generate();            
    }

    private void Generate()
    {
        for (int j = 0; j < 3; j++)
        {

            int n = Random.Range(0, 11);
            int[] shape = new int [9];
            int num = 0;
            for (int k = 0; k < 9; k++)
            {
                if (_shapes[n, k] != 20)
                {
                    shape[num] = _shapes[n, k];
                    num++;
                }
            }
                System.Array.Resize<int>(ref shape, num);
            System.Array.Resize<Block>(ref _piece, num);
            for (int i = 0; i < shape.Length; i++)
            {
                    _piece[i].x = Mathf.RoundToInt(shape[i] % 4);
                    _piece[i].y = Mathf.RoundToInt(shape[i] / 4);
                }
            Sprite sprite = _sprites[Random.Range(0, _sprites.Length)];
            GameObject obj = new GameObject();
            
            for (int i = 0; i < shape.Length; i++)
            {
                _piece[i].block = Instantiate(_blockPrefab, new Vector2(_piece[i].x, _piece[i].y), Quaternion.identity, obj.transform); ;
                SpriteRenderer sr = _piece[i].block.GetComponentInChildren<SpriteRenderer>();
                sr.sprite = sprite;
            }
            DestroyGO _script = obj.AddComponent<DestroyGO>();
            obj.transform.position = new Vector3(6.5f+3f*j, -8, 0);
            obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            
        }

    }
   

}
