using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    UICanvas _curCanvas;
    // Start is called before the first frame update
    void Start()
    {
        _curCanvas = GameObject.Find("Canvas").GetComponent<UICanvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        _curCanvas.skillOnclick();
    }

}
