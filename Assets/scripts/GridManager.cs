using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Transform _cam;
    [SerializeField] Transform board;

    [SerializeField] GameObject _buttonPrefab;
    [SerializeField] private GameObject _ladderPrefab;
    [SerializeField] private GameObject _snackPrefab;

    private Dictionary<int, Vector3> _tilesPos;
    private Dictionary<int, int> _assetPos;
    private GameObject player;
    private GameObject dice;
    private int _playerPos = 0;


    void Awake(){
        player = GameObject.Find("player");
        dice = GameObject.Find("dice");
        // iconText = GetComponent<TMP_Text>();
    }


    void Start() {
        // GenerateGrid();
        _tilesPos = new Dictionary<int, Vector3>();
        Debug.Log(board.transform.position);

        Debug.Log(board.GetComponent<GridLayoutGroup>().cellSize);
        board.GetComponent<GridLayoutGroup>().cellSize = new Vector2(900/_width, 900/_width);
        var tileNo = 0;
        for (int i= 0; i < _height; i++){
            for (int j = 0; j < _width; j++){

                GameObject button = (GameObject)Instantiate(_buttonPrefab);
                if(i%2 == 0){
                    tileNo = i*_width + j + 1;
                }
                else{
                    tileNo = i*_width + _width -j;
                }

                _tilesPos[tileNo] = new Vector3(-2.5f + j*(0.5f + 0.06f), -1.75f + i * (0.5f + 0.06f), 0f);


                button.GetComponentInChildren<TextMeshProUGUI>().text = $"{tileNo}";
                
                button.name = $"tile {i} {j}";
                // button.GetComponent<Image>().color = Color.red;
                button.transform.SetParent(board.transform, false);
                // Debug.Log(button.GetComponent<RectTransform>().position);
            }
        }
        // GameObject line = (GameObject)Instantiate(_linePrefab);
        // var lineRend = line.GetComponent<LineRenderer>();
        // lineRend.SetPosition(0, _tilesPos[98]); //x,y and z position of the starting point of the line
        // lineRend.SetPosition(1, _tilesPos[48]); 
        // line.transform.SetParent(board.transform, false);
        player.transform.position = _tilesPos[1];
        generatePos();
        foreach (KeyValuePair<int, int> pos in _assetPos){
            // Debug.Log(pos.Key);
            // Debug.Log(pos.Value);
            if(pos.Key > pos.Value){
                createSL(_tilesPos[pos.Value], _tilesPos[pos.Key], _snackPrefab);
            }
            else{
                createSL(_tilesPos[pos.Key], _tilesPos[pos.Value], _ladderPrefab);
            }
        }
            
        
    }


    void generatePos(){
        _assetPos = new Dictionary<int, int>();
        var _default = 0;
        for(int i=0; i< 5; i++){
            var a = Random.Range(11, 100);
            var b = Random.Range(2, 100);
            
            while (a- b < _width + 5 || _assetPos.TryGetValue(a, out _default) || _assetPos.ContainsValue(a)|| _assetPos.ContainsValue(b))
            {
                a = Random.Range(1, 100);
                b = Random.Range(1, 100);
            }
            _assetPos[a] = b;
        }

        for(int i=0; i< 3; i++){
            var a = Random.Range(2, 100);
            var b = Random.Range(11, 100);
            while (b-a < _width + 5 || _assetPos.TryGetValue(a, out _default) || _assetPos.ContainsValue(a)|| _assetPos.ContainsValue(b))
            {
                a = Random.Range(1, 100);
                b = Random.Range(1, 100);
            }
            _assetPos[a] = b;
        }
    }

    void createSL(Vector3 start, Vector3 end, GameObject prefab){

        GameObject line = (GameObject)Instantiate(prefab);
        var lineRender = line.GetComponent<LineRenderer>();
        lineRender.SetPosition(0, start + new Vector3(0, 0.08f, 0)); //x,y and z position of the starting point of the line
        lineRender.SetPosition(1,end - new Vector3(0, 0.08f, 0)); 
        lineRender.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.5f));
        line.transform.SetParent(board.transform, false);
        
    }

    public void clickDice(){
        var diceNo = Random.Range(1, 6);
        dice.GetComponentInChildren<TextMeshProUGUI>().text = $"{diceNo}";

        if((_playerPos + diceNo)  <= _width*_height){

            _playerPos += diceNo;
            
            Debug.Log(_playerPos);
            // player.transform.position = _tilesPos[_playerPos];

            if(_assetPos.ContainsKey(_playerPos)){
                player.transform.position = _tilesPos[_playerPos];
                var newtile = _assetPos[_playerPos];
                _playerPos = newtile;
                player.transform.position = _tilesPos[_playerPos];
            }
            else{
                player.transform.position = _tilesPos[_playerPos];
            }
        }
        
    }

}
