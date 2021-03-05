using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab = null;
    [SerializeField] private Sprite [] _sprites = null;
    private int _w = 9, _h = 9;

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

    private Block[] _piece = new Block[4]
    {
        new Block(),
        new Block(),
        new Block(),
        new Block()
    };

    private Block[,] _block;

    private int[,] _shapes = new int[,]
    {
        {6,7,11,12}, //cube
        {10,11,12,13}, //horizontal line
        {1,6,11,16}, //vertical line
        {6,7,12,13} //z
    };

    // Start is called before the first frame update
    void Start()
    {
        _block = new Block[_w, _h];
        Generate();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("New Game Object") == null)
            Generate();            
    }



    private void Generate()
    {
        for (int j = 0; j < 3; j++)
        {
            GameObject obj = new GameObject();
            DestroyGO _script = obj.AddComponent<DestroyGO>();
            int n = Random.Range(0, 4);
            for (int i = 0; i < 4; i++)
            {
                _piece[i].x = _shapes[n, i] % 5;
                _piece[i].y = -_shapes[n, i] / 5;
            }
            Sprite sprite = _sprites[Random.Range(0, _sprites.Length)];
            for (int i = 0; i < 4; i++)
            {
                _piece[i].block = Instantiate(_blockPrefab, new Vector2(_piece[i].x, _piece[i].y), Quaternion.identity, obj.transform);
                _piece[i].block.name.Replace("(Clone)", " ");
                SpriteRenderer sr = _piece[i].block.GetComponent<SpriteRenderer>();
                sr.sprite = sprite;
            }
            obj.transform.position = new Vector3(6*j, -4, 0);
        }
    }
   

}
