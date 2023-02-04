using OOAD_WarChess.Pawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyEventArgs
{
    public int id;
    public GameObject obj;
}

public class UICanvas : MonoBehaviour
{
    public MyTurn TURN;
    Button[] buttons;
    public delegate void SkillButtonClickDelegate(MyEventArgs arg);
    public SkillButtonClickDelegate SkillButtonClick { get; set; }

    public GameObject CanvasItself;



    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            MyEventArgs arg = new MyEventArgs();
            arg.id = i + 1;
            arg.obj = buttons[i].gameObject;
            buttons[i].onClick.AddListener(() => OnButtonClickAction(arg));
        }
    }

    void OnButtonClickAction(MyEventArgs arg)
    {
        if (SkillButtonClick != null)
        {
            SkillButtonClick(arg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void skipOnclick()
    {
        if (!TURN.isAnimaing)
            TURN.tranStateToSkip();
    }

    [System.Obsolete]
    public void skillOnclick()
    {
        if (!TURN.isAnimaing)
            TURN.tranStateToSkill();
    }

    public void moveOnclick() {
        if (!TURN.isAnimaing)
            TURN.transStateToMove();
    }

    public void itemOnclick()
    {
        if (!TURN.isAnimaing)
            TURN.tranStateToItem();
    }

}
