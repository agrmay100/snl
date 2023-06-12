using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetInput : MonoBehaviour
{

    public TMP_InputField gridL;
    public TMP_InputField gridW;
    public TMP_InputField snakes;
    public TMP_InputField ladder;

    [SerializeField] GameObject screen1;
    [SerializeField] GameObject screen2;

    // Start is called before the first frame update
    void Start()
    {
        // transform.SetActive(false);
        screen1.SetActive(false);
        screen2.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate(){
        Debug.Log(gridL.text);
    }
}
