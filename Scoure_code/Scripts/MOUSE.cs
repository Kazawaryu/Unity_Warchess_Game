using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TurnStateEnum
{
    Place_Player,
    Select_Player,
    GetPath_Player,
    Move_Player
}


public class MOUSE : MonoBehaviour
{

    Transform _currentTarget;
    TurnCell _currentCell;
    GameObject _originUnit;
    MyTurnUnit _currentunit;
    TurnStateEnum _currentState;


    // Start is called before the first frame update
    void Start()
    {
        _originUnit = Resources.Load<GameObject>("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MouseDetect();
        MouseInput();
    }
    

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (_currentState)
            {
                case TurnStateEnum.Place_Player:
                    if (_currentCell != null)
                    {
                        GameObject clone = Instantiate(_originUnit);
                        clone.GetComponent<MyTurnUnit>().SetCell(_currentCell);
                        _currentState = TurnStateEnum.Select_Player;
                    }
                    break;
                case TurnStateEnum.Select_Player:
                    if (_currentunit != null)
                    {
                        MyTurnUnit unit = _currentTarget.GetComponent<MyTurnUnit>();
                        if (_currentunit == unit)
                        {


                            _currentunit.Selected();
                            _currentState = TurnStateEnum.GetPath_Player;
                        }
                    }
                    break;
                case TurnStateEnum.GetPath_Player:
                    if (_currentCell != null)
                    {
                        /*_currentunit.SetCell(_currentCell);*/
                        _currentunit.smoothMove(MAP.Instance._path);
                        /*_currentState = TurnStateEnum.Move_Player;*/

                        _currentunit.DirDisSelect();
                        _currentState = TurnStateEnum.Select_Player;
                    }
                    break;
                case TurnStateEnum.Move_Player:
                    if (_currentCell != null)
                    {
                        _currentunit.DirDisSelect();
                        _currentState = TurnStateEnum.Select_Player;
                    }
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
                    _currentState = TurnStateEnum.Select_Player;
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
                else if (_currentState == TurnStateEnum.Place_Player)
                {


                    if (_currentCell != null && _currentCell != cell)
                    {
                        _currentCell.normalLight();
                    }
                    _currentCell = cell;
                    cell.Highlight();
                }
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
}


