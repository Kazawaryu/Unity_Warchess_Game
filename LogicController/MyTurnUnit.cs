using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OOAD_WarChess.Pawn;
public enum Character
{
    Player,
    Enemy
}

public class MyTurnUnit : MonoBehaviour
{
    public Character characterState { get; set; }

    public TurnCell _currentCell;
    public Outline _selfOut;
    public Animator _selfAnim;
    public Pawn pawnObj;
    public bool _isAniming = false;

    public HealthBar _HPBar;
    public HealthBar _MPBar;

    public GameObject _skillboard { get; set; }


    public int WEP = 1;
    public int HEL = 1;
    public int ARM = 1;
    public int BOT = 1;

    // Start is called before the first frame update
    void Start()
    {
        _selfOut = GetComponent<Outline>();
        _selfAnim = GetComponent<Animator>();
    }


    public void SetCell(TurnCell cell)
    {

        _currentCell = cell;
        transform.position = cell.transform.position;
        WEP = 1;
        HEL = 1;
        ARM = 1;
        BOT = 1;
}

    public void smoothMove(List<TurnCell> path)
    {
        StartCoroutine(MoveCor(path));
    }


    IEnumerator MoveCor(List<TurnCell> path)
    {
        _isAniming = true;
        _selfAnim.SetBool("isMove", true);
        _currentCell.normalLight();
        while (path.Count>0)
        {
            float workTime = 0;
            Vector3 begin_pos = transform.position;
            Vector3 end_pos = path[0].transform.position;
            GameObject tmp = new GameObject();
            tmp.transform.position = transform.position;
            tmp.transform.LookAt(end_pos);
            Quaternion originRotue = transform.rotation;
            Quaternion desRotue = tmp.transform.rotation;
            Destroy(tmp);
            while (true)
            {
                workTime += Time.deltaTime * 2;
                transform.position = Vector3.Lerp(begin_pos, end_pos, workTime);
                transform.rotation = Quaternion.Lerp(originRotue,desRotue,workTime * 2);

                if (workTime >= 1)
                {
                    transStateToRoad();
                    _currentCell = path[0];
                    transReturnToCharacter();

                    path[0].normalLight();

                    path.RemoveAt(0);
                    break;
                }
                yield return null;
            }

        }
        _selfAnim.SetBool("isMove", false);
        _isAniming = false;
    }

    public void setEnemy(GameObject CanvasItself)
    {
        _selfOut = GetComponent<Outline>();
        _selfAnim = GetComponent<Animator>();

        _selfOut.enabled = true;
        _selfOut.OutlineColor = Color.red;
        characterState = Character.Enemy;

        GameObject _HpObj = Resources.Load<GameObject>("Hp_slider");
        GameObject _MpObj = Resources.Load<GameObject>("Mp_slider");

        GameObject _hpslider = Instantiate(_HpObj);
        GameObject _mpslider = Instantiate(_MpObj);

        _HPBar = _hpslider.GetComponent<HealthBar>();
        _MPBar = _mpslider.GetComponent<HealthBar>();

        _hpslider.transform.SetParent(CanvasItself.transform);
        _mpslider.transform.SetParent(CanvasItself.transform);

        _HPBar.newBar();
        _MPBar.newBar();

        _HPBar.SetTarget(transform);
        _MPBar.SetTarget(transform);

        _HPBar._offsetX = 0;
        _HPBar._offsetY = -10;
        _MPBar._offsetX = 0;
        _MPBar._offsetY = -20;

        _HPBar.changeValue(pawnObj.HP, pawnObj.HP);
        _MPBar.changeValue(pawnObj.MP, pawnObj.MP);
    }


    public void setPlayer(GameObject CanvasItself)
    {

        _selfOut = GetComponent<Outline>();
        _selfAnim = GetComponent<Animator>();

        _selfOut.enabled = true;
        _selfOut.OutlineColor = Color.white;
        characterState = Character.Player;

        GameObject _HpObj = Resources.Load<GameObject>("Hp_slider");
        GameObject _MpObj = Resources.Load<GameObject>("Mp_slider");

        GameObject _hpslider = Instantiate(_HpObj);
        GameObject _mpslider = Instantiate(_MpObj);

        _HPBar = _hpslider.GetComponent<HealthBar>();
        _MPBar = _mpslider.GetComponent<HealthBar>();

        _hpslider.transform.SetParent(CanvasItself.transform);
        _mpslider.transform.SetParent(CanvasItself.transform);

        _HPBar.newBar();
        _MPBar.newBar();

        _HPBar.SetTarget(transform);
        _MPBar.SetTarget(transform);

        _HPBar._offsetX = 0;
        _HPBar._offsetY = -10;
        _MPBar._offsetX = 0;
        _MPBar._offsetY = -20;

        _HPBar.changeValue(pawnObj.HP, pawnObj.HP);
        _MPBar.changeValue(pawnObj.MP, pawnObj.MP);

    }

    public void setNowCharacter()
    {
        _selfOut = GetComponent<Outline>();
        _selfAnim = GetComponent<Animator>();

        _selfOut.enabled = true;
        _selfOut.OutlineColor = Color.green;
    }

    public void normalizeCharacter()
    {
        _selfOut = GetComponent<Outline>();
        _selfAnim = GetComponent<Animator>();

        _selfOut.enabled = true;

        if (characterState == Character.Player)
            _selfOut.OutlineColor = Color.white;
        else
            _selfOut.OutlineColor = Color.red;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void transStateToPlayer()
    {
        _currentCell.GetComponent<TurnCell>().transStateToPlayer();
    }

    public void transStateToEnemy()
    {
        _currentCell.GetComponent<TurnCell>().transStateToEnemy();
    }

    public void transStateToObstrust()
    {
        _currentCell.GetComponent<TurnCell>().transStateToObstrust();
    }

    public void transStateToRoad()
    {
        _currentCell.GetComponent<TurnCell>().transStateToRoad();
    }

    public void transReturnToCharacter()
    {
        if (characterState == Character.Player)
            transStateToPlayer();
        else
            transStateToEnemy();
    }
}
