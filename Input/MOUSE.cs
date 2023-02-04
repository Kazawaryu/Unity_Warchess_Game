/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class MOUSE : MonoBehaviour
{

    Transform _currentTarget;
    TurnCell _currentCell;
    GameObject _originUnit;
    public MyTurnUnit _currentunit;
    TurnStateEnum _currentState;

    GameObject _originCanvas;
    UICanvas _currentCanvans;

    // Start is called before the first frame update
    void Start()
    {
        _originUnit = Resources.Load<GameObject>("Player_0");

        _originCanvas = GameObject.Find("Canvas");
        _currentCanvans = _originCanvas.GetComponent<UICanvas>();
*//*        _currentCanvans._mouse = this;*//*
        _currentunit = new MyTurnUnit();
    }

    // Update is called once per frame
    void Update()
    {
        MouseDetect();
        MouseInput();
        UIDetect();
    }
    



 

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (_currentState)
            {
                *//*case TurnStateEnum.Place_Player:
                    if (_currentCell != null)
                    {
                        //此处需要加入判断放置的条件
                        GameObject clone = Instantiate(_originUnit);
                        clone.GetComponent<MyTurnUnit>().SetCell(_currentCell);
                        _currentState = TurnStateEnum.Select_Player;
                    }
                    break;*/
                /*case TurnStateEnum.Select_Player:
                    if (_currentunit != null)
                    {
                        MyTurnUnit unit = _currentTarget.GetComponent<MyTurnUnit>();
                        if (_currentunit == unit)
                        {
                            _currentunit.Selected();
                            _currentState = TurnStateEnum.UI_Player;
                            Debug.Log("UI操作待选状态");
                        }
                    }
                    break;*//*
                case TurnStateEnum.GetPath_Player:
                    if (_currentCell != null)
                    {
                        _currentunit.smoothMove(MAP.Instance._path);
                        _currentunit.DirDisSelect();
*//*                        _currentState = TurnStateEnum.Select_Player;*//*
                    }
                    break;
                case TurnStateEnum.Move_Player:
                    if (_currentCell != null)
                    {
                        _currentunit.DirDisSelect();
*//*                        _currentState = TurnStateEnum.Select_Player;*//*
                    }
                    break;
                case TurnStateEnum.UI_Player:
                    //更新UI
                    Debug.Log("更新UI界面");


                    break;
                default:
                    break;
            }
        }else if (Input.GetMouseButtonDown(1))
        {
            if (_currentState == TurnStateEnum.GetPath_Player)
            {
                if (_currentunit != null)
                {
                    _currentunit.DirDisSelect();
*//*                    _currentState = TurnStateEnum.Select_Player;*//*
                }
            }
        }
        

    }

    private void MouseDetect()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay,out hitInfo))
        {
            _currentTarget = hitInfo.transform;
            TurnCell cell = _currentTarget.GetComponent<TurnCell>();
            if (cell != null)
            {
                
                if (_currentState == TurnStateEnum.GetPath_Player)
                {
                    MAP.Instance.Astar(_currentunit._currentCell,cell);
                }
  *//*              else if (_currentState == TurnStateEnum.Place_Player)
                {


                    if (_currentCell != null && _currentCell != cell)
                    {
                        _currentCell.normalLight();
                    }
                    _currentCell = cell;
                    cell.Highlight();
                }*//*
            }


            MyTurnUnit unit = _currentTarget.GetComponent<MyTurnUnit>();
            if (unit != null)
            {
                if (_currentunit != null && _currentunit != unit)
                {
                    _currentunit.DisSelect();
                }
                _currentunit = unit;
                _currentunit.PreSelect();
                
            }
            else
            {
                if (_currentunit != null )
                {
                    _currentunit.DisSelect();
                    
                }
            }
        }
        else
        {
            if (_currentCell != null)
            {
                _currentCell.normalLight();
                _currentCell = null;
            }
        }
    }

    private void UIDetect()
    {
        if (_currentState == TurnStateEnum.UI_Player)
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        }
    }


    public void transStateToPathScanning()
    {
        _currentState = TurnStateEnum.GetPath_Player;
    }

}


*/