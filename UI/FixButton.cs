using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixButton : MonoBehaviour
{
    MyTurn Fixturn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void construstors(MyTurn turn)
    {
        Fixturn = turn;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate () {
            this.onClick(this.gameObject);
        });
    }

    public void onClick(GameObject obj)
    {
        Fixturn.fixCharacterState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
