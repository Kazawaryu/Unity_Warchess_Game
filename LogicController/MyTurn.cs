using System.Collections;
using System;
using System.Collections.Generic;
using OOAD_WarChess.Pawn.Skill.Common;
using OOAD_WarChess.Battle;
using UnityEngine;
using OOAD_WarChess.Item;
using OOAD_WarChess.Pawn;
using OOAD_WarChess.Pawn.PawnClass;
using UnityEngine.UI;
using OOAD_WarChess.Item.Potion;
using OOAD_WarChess.Item.Util;

public enum MainState
{
    None_main,
    Move_main,
    Skill_main,
    Item_main,
    Skip_main
}

public enum SkillType
{
    None_skill,
    SinglePlayer,
    RangePlayer,
    SingleEnemy,
    RangeEnemy,
}

public enum ItemType
{
    None_item,
    Preusing,
    Using
}


public class MyTurn : MonoBehaviour
{
    GameObject winPrefab ;
    GameObject win ;
    GameObject losePrefab;
    GameObject lose;
    Transform _currentTarget;
    TurnCell _currentCell;
    MyTurnUnit _currentUnit;
   
    GameObject _originCanvas;
    UICanvas _currentCanvans;
    GameObject _contentObj;
    ScrollViewContentTool _content;

    enum MoveType
    {
        None_move,
        PreSelect_move,
        Moving_move,
        Select_move
    }


    MainState _turnState;
    public SkillType _skillState;
    MoveType _moveState;
    ItemType _itemState;



    GameObject _gridPoint;

    List<GameObject> _players = new List<GameObject>();
    List<GameObject> _enemies = new List<GameObject>();
    List<TurnCell> _cellList;
    MAP _map;
    int row;
    int col;

    int _round = 0;
    int _playerCount;
    int _enemyCount;

    
    public int skill_x;
    public int skill_y;
    public int skill_range;
    public int skill_Num;
    public bool isAnimaing;
    MyTurnUnit _interactiveUnit;

    GameObject _pBag;
    GameObject _eBag;
    IItem nextItem;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
    }


    [Obsolete]
    public void defaultBegin()
    {
        var tempLog = "";
        //生成独有技能面版
        GameObject _originSkillBoard = Resources.Load<GameObject>("SkillBoard");
        for (int i = 0; i < _players.Count; i++)
        {
            GameObject _player_SkillBoard = Instantiate(_originSkillBoard);
            SkillBoard _player_SkillBoard_Comp = _player_SkillBoard.transform.FindChild("Viewport").transform.FindChild("Content").GetComponent<SkillBoard>();
            MyTurnUnit _player_SkillBoard_Unit = _players[i].GetComponent<MyTurnUnit>();
            _player_SkillBoard_Unit._skillboard = _player_SkillBoard;
            _player_SkillBoard_Comp.starts();
            _player_SkillBoard.transform.SetParent(_originCanvas.transform);
            for (int j = 1; j < _player_SkillBoard_Unit.pawnObj.Skills.Count; j++)
            {
                _player_SkillBoard_Comp.addSkillButton(_player_SkillBoard_Unit.pawnObj.Skills[j], _player_SkillBoard_Unit.characterState, this);
            }
            RectTransform _player_rectTransform = _player_SkillBoard.GetComponent<RectTransform>();
            _player_rectTransform.localPosition = new Vector3(400, 30, 0);
            _player_SkillBoard.SetActive(false);

            _players[i].GetComponent<MyTurnUnit>().pawnObj.GainMoney(200, out tempLog);
        }

        for (int i = 0; i < _enemies.Count; i++)
        {
            GameObject _player_SkillBoard = Instantiate(_originSkillBoard);
            SkillBoard _player_SkillBoard_Comp = _player_SkillBoard.transform.FindChild("Viewport").transform.FindChild("Content").GetComponent<SkillBoard>();
            MyTurnUnit _player_SkillBoard_Unit = _enemies[i].GetComponent<MyTurnUnit>();
            _player_SkillBoard_Unit._skillboard = _player_SkillBoard;
            _player_SkillBoard_Comp.starts();
            _player_SkillBoard.transform.SetParent(_originCanvas.transform);
            for (int j = 1; j < _player_SkillBoard_Unit.pawnObj.Skills.Count; j++)
            {
                _player_SkillBoard_Comp.addSkillButton(_player_SkillBoard_Unit.pawnObj.Skills[j], _player_SkillBoard_Unit.characterState, this);
            }
            RectTransform _player_rectTransform = _player_SkillBoard.GetComponent<RectTransform>();
            _player_rectTransform.localPosition = new Vector3(400, 30, 0);
            _player_SkillBoard.SetActive(false);

            _enemies[i].GetComponent<MyTurnUnit>().pawnObj.GainMoney(200, out tempLog);
        }

        //加载结算画面
        winPrefab = Resources.Load<GameObject>("win");
        win = Instantiate(winPrefab);
        win.transform.SetParent(_originCanvas.transform);
        RectTransform win_rectTransform = win.GetComponent<RectTransform>();
        win_rectTransform.localPosition = new Vector3(0, 240, 0);
        win.SetActive(false);

        losePrefab = Resources.Load<GameObject>("lose");
        lose = Instantiate(losePrefab);
        lose.transform.SetParent(_originCanvas.transform);
        RectTransform lose_rectTransform = lose.GetComponent<RectTransform>();
        lose_rectTransform.localPosition = new Vector3(0, 240, 0);
        lose.SetActive(false);

        GameObject fixButtons = GameObject.Find("surrender");
        fixButtons.AddComponent<FixButton>();
        fixButtons.GetComponent<FixButton>().construstors(this);


        //加载背包
        _pBag = GameObject.Find("Canvas/PlayerBag");
        _eBag = GameObject.Find("Canvas/EnemyBag");
        int count = _originCanvas.transform.childCount - 1;//Panel移位
        _contentObj.transform.SetSiblingIndex(count);//Panel移位

        ItemButton pWEP = _pBag.transform.FindChild("person/WEP").GetComponent<ItemButton>();
        ItemButton pFOT = _pBag.transform.FindChild("person/FOT").GetComponent<ItemButton>();
        ItemButton pHED = _pBag.transform.FindChild("person/HED").GetComponent<ItemButton>();
        ItemButton pBOD = _pBag.transform.FindChild("person/BOD").GetComponent<ItemButton>();
        ItemButton eWEP = _eBag.transform.FindChild("person/WEP").GetComponent<ItemButton>();
        ItemButton eFOT = _eBag.transform.FindChild("person/FOT").GetComponent<ItemButton>();
        ItemButton eHED = _eBag.transform.FindChild("person/HED").GetComponent<ItemButton>();
        ItemButton eBOD = _eBag.transform.FindChild("person/BOD").GetComponent<ItemButton>();
        pWEP.updateConstructor(0, this);
        pFOT.updateConstructor(1, this);
        pHED.updateConstructor(2, this);
        pBOD.updateConstructor(3, this);
        eWEP.updateConstructor(0, this);
        eFOT.updateConstructor(1, this);
        eHED.updateConstructor(2, this);
        eBOD.updateConstructor(3, this);

        ItemButton pI0 = _pBag.transform.FindChild("bag/ITE0").GetComponent<ItemButton>();
        ItemButton pI1 = _pBag.transform.FindChild("bag/ITE1").GetComponent<ItemButton>();
        ItemButton pI2 = _pBag.transform.FindChild("bag/ITE2").GetComponent<ItemButton>();
        ItemButton pI3 = _pBag.transform.FindChild("bag/ITE3").GetComponent<ItemButton>();
        ItemButton pI4 = _pBag.transform.FindChild("bag/ITE4").GetComponent<ItemButton>();
        ItemButton pI5 = _pBag.transform.FindChild("bag/ITE5").GetComponent<ItemButton>();
        ItemButton eI0 = _eBag.transform.FindChild("bag/ITE0").GetComponent<ItemButton>();
        ItemButton eI1 = _eBag.transform.FindChild("bag/ITE1").GetComponent<ItemButton>();
        ItemButton eI2 = _eBag.transform.FindChild("bag/ITE2").GetComponent<ItemButton>();
        ItemButton eI3 = _eBag.transform.FindChild("bag/ITE3").GetComponent<ItemButton>();
        ItemButton eI4 = _eBag.transform.FindChild("bag/ITE4").GetComponent<ItemButton>();
        ItemButton eI5 = _eBag.transform.FindChild("bag/ITE5").GetComponent<ItemButton>();

        pI0.itemConstructor(new HealPotion(30), this,true);
        pI1.itemConstructor(new HealPotion(40), this,true);
        pI2.itemConstructor(new Knife(20), this,false);
        pI3.itemConstructor(new Beer(4), this,true);
        pI4.itemConstructor(new Knife(80), this,false);
        pI5.itemConstructor(new Beer(6), this,true);
        eI0.itemConstructor(new HealPotion(30), this,true);
        eI1.itemConstructor(new HealPotion(40), this,true);
        eI2.itemConstructor(new Knife(20), this,false);
        eI3.itemConstructor(new Beer(4), this,true);
        eI4.itemConstructor(new Knife(80), this,false);
        eI5.itemConstructor(new Beer(6), this,true);

        //加载第一个人物
        _pBag.SetActive(false);
        _eBag.SetActive(false);
        _currentUnit = _players[0].GetComponent<MyTurnUnit>();
        Debug.Log(_currentUnit.pawnObj.Name);
        isAnimaing = false;
        NextTurn();
    }

    [Obsolete]
    public void CasionBegin()
    {
        //加载地图cell布局
        row = 20;
        col = 20;
        _gridPoint = GameObject.Find("Grids");
        _gridPoint.AddComponent<MAP>();
        _map = _gridPoint.GetComponent<MAP>();
        _cellList = _map.get_cellList(row, col);

        //加载Canvans
        _originCanvas = GameObject.Find("Canvas");
        _currentCanvans = _originCanvas.GetComponent<UICanvas>();
        _currentCanvans.TURN = this;
        _currentCanvans.CanvasItself = _originCanvas;
        _contentObj = GameObject.Find("DetailInformation/Viewport/Content");
        _content = _contentObj.GetComponent<ScrollViewContentTool>();

        //加载人物模型
        GameObject Presit_m = Resources.Load<GameObject>("Preist_m");
        GameObject Knight_m = Resources.Load<GameObject>("Knight_m");
        GameObject Archer_m = Resources.Load<GameObject>("Archer_m");
        GameObject Warrior_m = Resources.Load<GameObject>("Warrior_m");

        GameObject _player0 = Instantiate(Knight_m);
        GameObject _player1 = Instantiate(Archer_m);
        GameObject _player2 = Instantiate(Presit_m);
        GameObject _player3 = Instantiate(Warrior_m);
        MyTurnUnit _player0_unit = _player0.GetComponent<MyTurnUnit>();
        MyTurnUnit _player1_unit = _player1.GetComponent<MyTurnUnit>();
        MyTurnUnit _player2_unit = _player2.GetComponent<MyTurnUnit>();
        MyTurnUnit _player3_unit = _player3.GetComponent<MyTurnUnit>();

        //设置初始位置
        _player0_unit.SetCell(_map._cellList[24]);
        _player1_unit.SetCell(_map._cellList[27]);
        _player2_unit.SetCell(_map._cellList[144]);
        _player3_unit.SetCell(_map._cellList[147]);

        //挂载后端脚本
        _player0_unit.pawnObj = PawnClass.Create("Knight", PawnClassType.Knight);
        _player1_unit.pawnObj = PawnClass.Create("Archer", PawnClassType.Archer);
        _player2_unit.pawnObj = PawnClass.Create("Priest", PawnClassType.Priest);
        _player3_unit.pawnObj = PawnClass.Create("Warrior", PawnClassType.Warrior);

        //设置前端属性
        _player0_unit.setPlayer(_originCanvas);
        _player0_unit.transStateToPlayer();
        _player1_unit.setPlayer(_originCanvas);
        _player1_unit.transStateToPlayer();
        _player2_unit.setEnemy(_originCanvas);
        _player2_unit.transStateToEnemy();
        _player3_unit.setEnemy(_originCanvas);
        _player3_unit.transStateToEnemy();

        //加入回合判断队列
        _players.Add(_player0);
        _players.Add(_player1);
        _enemies.Add(_player2);
        _enemies.Add(_player3);

        //加载地图障碍物
        GameObject _orginChair = Resources.Load<GameObject>("Chair");

        GameObject cloneChair0 = Instantiate(_orginChair);
        cloneChair0.transform.SetParent(_map.transform);
        cloneChair0.transform.position = _cellList[63].transform.position;
        _cellList[63].GetComponent<TurnCell>()._currentStage = cellStage.obstrust;

        GameObject cloneChair1 = Instantiate(_orginChair);
        cloneChair1.transform.SetParent(_map.transform);
        cloneChair1.transform.position = _cellList[88].transform.position;
        _cellList[88].GetComponent<TurnCell>()._currentStage = cellStage.obstrust;

        GameObject cloneChair2 = Instantiate(_orginChair);
        cloneChair2.transform.SetParent(_map.transform);
        cloneChair2.transform.position = _cellList[101].transform.position;
        _cellList[111].GetComponent<TurnCell>()._currentStage = cellStage.obstrust;

        GameObject cloneChair3 = Instantiate(_orginChair);
        cloneChair3.transform.SetParent(_map.transform);
        cloneChair3.transform.position = _cellList[126].transform.position;
        _cellList[146].GetComponent<TurnCell>()._currentStage = cellStage.obstrust;

        //加载剩余元素
        defaultBegin();
    }

    [Obsolete]
    public void WetlandBegin()
    {
        //加载地图cell布局
        row = 100;
        col = 100;
        _gridPoint = GameObject.Find("Grids");
        _gridPoint.AddComponent<MAP>();
        _map = _gridPoint.GetComponent<MAP>();
        _cellList = _map.get_cellList(row, col);

        //加载Canvans
        _originCanvas = GameObject.Find("Canvas");
        _currentCanvans = _originCanvas.GetComponent<UICanvas>();
        _currentCanvans.TURN = this;
        _currentCanvans.CanvasItself = _originCanvas;
        _contentObj = GameObject.Find("DetailInformation/Viewport/Content");
        _content = _contentObj.GetComponent<ScrollViewContentTool>();

        //加载人物模型
        GameObject Presit_m = Resources.Load<GameObject>("Preist_m");
        GameObject Knight_m = Resources.Load<GameObject>("Knight_m");
        GameObject Archer_m = Resources.Load<GameObject>("Archer_m");
        GameObject Warrior_m = Resources.Load<GameObject>("Warrior_m");

        GameObject _player0 = Instantiate(Knight_m);
        GameObject _player1 = Instantiate(Archer_m);
        GameObject _player2 = Instantiate(Presit_m);
        GameObject _player3 = Instantiate(Warrior_m);
        MyTurnUnit _player0_unit = _player0.GetComponent<MyTurnUnit>();
        MyTurnUnit _player1_unit = _player1.GetComponent<MyTurnUnit>();
        MyTurnUnit _player2_unit = _player2.GetComponent<MyTurnUnit>();
        MyTurnUnit _player3_unit = _player3.GetComponent<MyTurnUnit>();

        GameObject _player4 = Instantiate(Knight_m);
        GameObject _player5 = Instantiate(Archer_m);
        GameObject _player6 = Instantiate(Presit_m);
        GameObject _player7 = Instantiate(Warrior_m);
        MyTurnUnit _player4_unit = _player4.GetComponent<MyTurnUnit>();
        MyTurnUnit _player5_unit = _player5.GetComponent<MyTurnUnit>();
        MyTurnUnit _player6_unit = _player6.GetComponent<MyTurnUnit>();
        MyTurnUnit _player7_unit = _player7.GetComponent<MyTurnUnit>();


        //设置初始位置
        _player0_unit.SetCell(_map._cellList[204]);
        _player1_unit.SetCell(_map._cellList[207]);
        _player2_unit.SetCell(_map._cellList[210]);
        _player3_unit.SetCell(_map._cellList[213]);
        _player4_unit.SetCell(_map._cellList[804]);
        _player5_unit.SetCell(_map._cellList[807]);
        _player6_unit.SetCell(_map._cellList[810]);
        _player7_unit.SetCell(_map._cellList[813]);


        //挂载后端脚本
        _player0_unit.pawnObj = PawnClass.Create("Knight", PawnClassType.Knight);
        _player1_unit.pawnObj = PawnClass.Create("Archer", PawnClassType.Archer);
        _player2_unit.pawnObj = PawnClass.Create("Priest", PawnClassType.Priest);
        _player3_unit.pawnObj = PawnClass.Create("Warrior", PawnClassType.Warrior);
        _player4_unit.pawnObj = PawnClass.Create("Knight", PawnClassType.Knight);
        _player5_unit.pawnObj = PawnClass.Create("Archer", PawnClassType.Archer);
        _player6_unit.pawnObj = PawnClass.Create("Priest", PawnClassType.Priest);
        _player7_unit.pawnObj = PawnClass.Create("Warrior", PawnClassType.Warrior);

        //设置前端属性
        _player0_unit.setPlayer(_originCanvas);
        _player0_unit.transStateToPlayer();
        _player1_unit.setPlayer(_originCanvas);
        _player1_unit.transStateToPlayer();
        _player2_unit.setPlayer(_originCanvas);
        _player2_unit.transStateToPlayer();
        _player3_unit.setPlayer(_originCanvas);
        _player3_unit.transStateToPlayer();

        _player4_unit.setEnemy(_originCanvas);
        _player4_unit.transStateToEnemy();
        _player5_unit.setEnemy(_originCanvas);
        _player5_unit.transStateToEnemy();
        _player6_unit.setEnemy(_originCanvas);
        _player6_unit.transStateToEnemy();
        _player7_unit.setEnemy(_originCanvas);
        _player7_unit.transStateToEnemy();

        //加入回合判断队列
        _players.Add(_player0);
        _players.Add(_player1);
        _players.Add(_player2);
        _players.Add(_player3);
        _enemies.Add(_player4);
        _enemies.Add(_player5);
        _enemies.Add(_player6);
        _enemies.Add(_player7);

        //加载剩余元素
        defaultBegin();
    }

    [Obsolete]
    public void DungeonBegin()
    {
        //加载地图cell布局
        row = 30;
        col = 30;
        _gridPoint = GameObject.Find("Grids");
        _gridPoint.AddComponent<MAP>();
        _map = _gridPoint.GetComponent<MAP>();
        _cellList = _map.get_cellList(row, col);

        //加载Canvans
        _originCanvas = GameObject.Find("Canvas");
        _currentCanvans = _originCanvas.GetComponent<UICanvas>();
        _currentCanvans.TURN = this;
        _currentCanvans.CanvasItself = _originCanvas;
        _contentObj = GameObject.Find("DetailInformation/Viewport/Content");
        _content = _contentObj.GetComponent<ScrollViewContentTool>();

        //加载人物模型
        GameObject Presit_m = Resources.Load<GameObject>("Preist_m");
        GameObject Knight_m = Resources.Load<GameObject>("Knight_m");
        GameObject Archer_m = Resources.Load<GameObject>("Archer_m");
        GameObject Warrior_m = Resources.Load<GameObject>("Warrior_m");

        GameObject _player0 = Instantiate(Knight_m);
        GameObject _player1 = Instantiate(Archer_m);
        GameObject _player2 = Instantiate(Presit_m);
        GameObject _player3 = Instantiate(Warrior_m);
        MyTurnUnit _player0_unit = _player0.GetComponent<MyTurnUnit>();
        MyTurnUnit _player1_unit = _player1.GetComponent<MyTurnUnit>();
        MyTurnUnit _player2_unit = _player2.GetComponent<MyTurnUnit>();
        MyTurnUnit _player3_unit = _player3.GetComponent<MyTurnUnit>();


        //设置初始位置
        _player0_unit.SetCell(_map._cellList[34]);
        _player1_unit.SetCell(_map._cellList[37]);
        _player2_unit.SetCell(_map._cellList[244]);
        _player3_unit.SetCell(_map._cellList[247]);

        //挂载后端脚本
        _player0_unit.pawnObj = PawnClass.Create("Knight", PawnClassType.Knight);
        _player1_unit.pawnObj = PawnClass.Create("Archer", PawnClassType.Archer);
        _player2_unit.pawnObj = PawnClass.Create("Priest", PawnClassType.Priest);
        _player3_unit.pawnObj = PawnClass.Create("Warrior", PawnClassType.Warrior);

        //设置前端属性
        _player0_unit.setPlayer(_originCanvas);
        _player0_unit.transStateToPlayer();
        _player1_unit.setPlayer(_originCanvas);
        _player1_unit.transStateToPlayer();
        _player2_unit.setEnemy(_originCanvas);
        _player2_unit.transStateToEnemy();
        _player3_unit.setEnemy(_originCanvas);
        _player3_unit.transStateToEnemy();

        //加入回合判断队列
        _players.Add(_player0);
        _players.Add(_player1);
        _enemies.Add(_player2);
        _enemies.Add(_player3);

        //加载剩余元素
        defaultBegin();
    }


    private void NextTurn()
    {
        skill_x = 1;
        skill_y = 1;
        if (_round % 2 == 0)
        {
            //我方回合

            _currentUnit._skillboard.SetActive(false);
            _map.CellinVisableALL();
            _currentUnit._MPBar.changeValue(6, 6);

            //以回合为单位结算
            for (int i = 0; i < _players.Count; i++)
            {
                Tuple<int, string> lineRes = _players[i].GetComponent<MyTurnUnit>().pawnObj.UpdateModifiers(); 
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                Tuple<int, string> lineRes = _enemies[i].GetComponent<MyTurnUnit>().pawnObj.UpdateModifiers();
            }

            GameObject next = _players[_playerCount % _players.Count];
            _currentUnit = next.GetComponent<MyTurnUnit>();
            _currentUnit.setNowCharacter();
            _currentUnit.transStateToPlayer();
            
            
            _playerCount++;
        }
        else
        {
            //敌方回合

            _currentUnit._skillboard.SetActive(false);
            _map.CellinVisableALL();
            _currentUnit._MPBar.changeValue(6, 6);

            for (int i = 0; i < _players.Count; i++)
            {
                Tuple<int, string> lineRes = _players[i].GetComponent<MyTurnUnit>().pawnObj.UpdateModifiers();
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                Tuple<int, string> lineRes = _enemies[i].GetComponent<MyTurnUnit>().pawnObj.UpdateModifiers();
            }
            GameObject next = _enemies[_enemyCount % _enemies.Count];
            _currentUnit = next.GetComponent<MyTurnUnit>();
            _currentUnit.setNowCharacter();
            _currentUnit.transStateToEnemy();
            
            
            _enemyCount++;
        }

        _turnState = MainState.None_main;
        _skillState = SkillType.None_skill;
        _moveState = MoveType.None_move;
        _itemState = ItemType.None_item;

        _round++;

        int pd = 0;
        int ed = 0;
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].GetComponent<MyTurnUnit>().pawnObj.HP <= 0)
                pd++;
        }
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].GetComponent<MyTurnUnit>().pawnObj.HP <= 0)
                ed++;
        }
        if (pd == _players.Count)
        {
            lose.SetActive(true);
            //P2获胜
        }
        if (ed == _enemies.Count)
        {
            win.SetActive(true);
            //P1获胜
        }
        Debug.Log(pd + " " + ed);
        Debug.Log(_currentUnit.pawnObj.Name);

    }


    // Update is called once per frame
    void Update()
    {

        
        MouseDetect(); 
        MouseInput();
        if (_currentUnit == null)
        {
            Debug.Log(_players.Count + " " + _enemies.Count);
            _currentUnit = _players[0].GetComponent<MyTurnUnit>();
        }
        if (!_currentUnit._isAniming)
            isAnimaing = false;
        else
            isAnimaing = true;
        
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(1) && _turnState != MainState.None_main)
        {
            _turnState = MainState.None_main;
            _skillState = SkillType.None_skill;
            _moveState = MoveType.None_move;
            _itemState = ItemType.None_item;
            _interactiveUnit = null;
            _currentUnit._skillboard.SetActive(false);

            _map.cleanALL();
            _map.CellinVisableALL();
        }else if (Input.GetMouseButtonDown(0))
        {
            switch (_turnState)
            {
                case MainState.None_main:
                    break;
                case MainState.Move_main:
                    if (_moveState == MoveType.PreSelect_move)
                    {
                        if (_currentCell != null)
                        {
                            if (_currentUnit.pawnObj.HP > 0 && _currentUnit.pawnObj.ACTPOINT >= MAP.Instance._path.Count)
                            {
                                int beforeACTPOINT = _currentUnit.pawnObj.ACTPOINT;
                                _moveState = MoveType.Moving_move;
                                _currentUnit.smoothMove(MAP.Instance._path);
                                _currentUnit.pawnObj.Skills[0].Cast(_currentUnit.pawnObj, MAP.Instance._path.Count);
                                String nextLine = "[" + _currentUnit.pawnObj.Name + "] " + " Move, cost " + (beforeACTPOINT - _currentUnit.pawnObj.ACTPOINT) + " Action point.";

                                _content.addTextLine(nextLine);
                                _currentUnit._MPBar.changeValue(_currentUnit.pawnObj.ACTPOINT, 6);
                            }
                        }
                    }
                    
                    break;
                case MainState.Skill_main:
                    switch (_skillState)
                    {
                        case SkillType.None_skill:
                            break;
                        case SkillType.SinglePlayer:
                            Debug.Log(_currentUnit.pawnObj.Skills[skill_Num].Name + " " + skill_Num);
                            if (_currentUnit.pawnObj.HP > 0 && _interactiveUnit != null && _interactiveUnit.characterState == Character.Player && _interactiveUnit._currentCell.ifVisable)
                            {
                                if (_currentUnit.pawnObj.ACTPOINT >= _currentUnit.pawnObj.Skills[skill_Num].APCost)
                                {

                                    int interacteOriginHP = _interactiveUnit.pawnObj.HP;
                                    _interactiveUnit._currentCell.normalLight();
                                    var log = "";
                                    Tuple<int, string> res = _currentUnit.pawnObj.Skills[skill_Num].Cast(_currentUnit.pawnObj, _interactiveUnit.pawnObj, out log);
                                    addContent(log);
                                    _skillState = SkillType.None_skill;
                                    _currentUnit._skillboard.SetActive(false);
                                    _interactiveUnit._HPBar.changeValue(_interactiveUnit.pawnObj.HP, _interactiveUnit.pawnObj.GetAttribute(PawnAttribute.HP));
                                    _currentUnit._MPBar.changeValue(_currentUnit.pawnObj.ACTPOINT, _currentUnit.pawnObj.GetAttribute(PawnAttribute.ACTPOINT));

                                    playAnima();
                                    //判断接收技能者的状态，并切换动画状态
                                    if (_interactiveUnit.pawnObj.HP < interacteOriginHP)
                                    {
                                        if (_interactiveUnit.pawnObj.HP > 0)
                                        {
                                            _interactiveUnit._selfAnim.SetTrigger("impact");
                                        }
                                        else
                                        {
                                            _interactiveUnit._selfAnim.SetTrigger("die");
                                            var logs = "";
                                            _currentUnit.pawnObj.GainExp(1000, out logs);
                                            addContent(logs);
                                            _currentUnit.pawnObj.GainMoney(600, out logs);
                                            addContent(logs);
                                        }
                                    }
                                    else 
                                    {
                                        //_interactiveUnit所在格子position处播放增益动画
                                    }
                                }
                                else
                                {
                                    String nextLine = "[" + _currentUnit.pawnObj.Name + "] need (" + _currentUnit.pawnObj.Skills[skill_Num].APCost + ") Action Point, but only have " + _currentUnit.pawnObj.ACTPOINT + " Action Point.";
                                    _content.addTextLine(nextLine);
                                    _currentUnit._skillboard.SetActive(false);
                                }
                            }
                            
                            _interactiveUnit = null;
                            _turnState = MainState.None_main;
                            _skillState = SkillType.None_skill;
                            _map.cleanALL();
                            _map.CellinVisableALL();
                            break;
                        case SkillType.RangePlayer:
                            Debug.Log(_currentUnit.pawnObj.Skills[skill_Num].Name + " " + skill_Num);
                            if (_currentUnit.pawnObj.HP > 0 && _interactiveUnit != null && _interactiveUnit.characterState == Character.Player && _interactiveUnit._currentCell.ifVisable)
                            {
                                if (_currentUnit.pawnObj.ACTPOINT >= _currentUnit.pawnObj.Skills[skill_Num].APCost)
                                {
                                    _map.cleanALL();
                                    List<MyTurnUnit> _nowInteractiveUnits = new List<MyTurnUnit>();
                                    int center = _cellList.FindIndex(x => x.Equals(_interactiveUnit._currentCell));
                                    for (int i = 0; i < _players.Count; i++)
                                    {
                                        MyTurnUnit _tempUnit = _players[i].GetComponent<MyTurnUnit>();
                                        int next = _cellList.FindIndex(x => x.Equals(_tempUnit._currentCell));
                                        if (_tempUnit._currentCell.ifVisable)
                                        {
                                            for (int j = -_currentUnit.pawnObj.Skills[skill_Num].Range / 2; j < _currentUnit.pawnObj.Skills[skill_Num].Range / 2; j++)
                                            {
                                                int centerP = center + j * col;
                                                int low = Math.Max(centerP - center % col, centerP - _currentUnit.pawnObj.Skills[skill_Num].Range / 2);
                                                int high = Math.Min(centerP - center % col + col, centerP + _currentUnit.pawnObj.Skills[skill_Num].Range / 2);
                                                if (next >= low && next <= high)
                                                    _nowInteractiveUnits.Add(_tempUnit);
                                            }

                                        }
                                    }
                                    
                                    playAnima();
                                    for (int i = 0; i < _nowInteractiveUnits.Count; i++)
                                    {
                                        int interacteOriginHP = _nowInteractiveUnits[i].pawnObj.HP;
                                        var log = "";
                                        Tuple<int, string> res = _currentUnit.pawnObj.Skills[skill_Num].Cast(_currentUnit.pawnObj, _nowInteractiveUnits[i].pawnObj, out log);
                                        addContent(log);
                                        _nowInteractiveUnits[i]._HPBar.changeValue(_nowInteractiveUnits[i].pawnObj.HP, _nowInteractiveUnits[i].pawnObj.GetAttribute(PawnAttribute.HP));
                                        //判断接收技能者的状态，并切换动画状态
                                        if (_nowInteractiveUnits[i].pawnObj.HP < interacteOriginHP)
                                        {
                                            if (_nowInteractiveUnits[i].pawnObj.HP > 0)
                                            {
                                                _nowInteractiveUnits[i]._selfAnim.SetTrigger("impact");
                                            }
                                            else
                                            {
                                                _nowInteractiveUnits[i]._selfAnim.SetTrigger("die");
                                                var logs = "";
                                                _currentUnit.pawnObj.GainExp(1000, out logs);
                                                addContent(logs);
                                                _currentUnit.pawnObj.GainMoney(600, out logs);
                                                addContent(logs);
                                            }
                                        }
                                        else
                                        {
                                            //_interactiveUnit所在格子position处播放增益动画
                                        }

                                    }


                                    _skillState = SkillType.None_skill;
                                    _currentUnit._skillboard.SetActive(false);
                                    _currentUnit._MPBar.changeValue(_currentUnit.pawnObj.ACTPOINT, _currentUnit.pawnObj.GetAttribute(PawnAttribute.ACTPOINT));
                                }
                                else
                                {
                                    String nextLine = "[" + _currentUnit.pawnObj.Name + "] need (" + _currentUnit.pawnObj.Skills[skill_Num].APCost + ") Action Point, but only have " + _currentUnit.pawnObj.ACTPOINT + " Action Point.";
                                    _content.addTextLine(nextLine);
                                    _currentUnit._skillboard.SetActive(false);
                                }
                            }
                            _interactiveUnit = null;
                            _turnState = MainState.None_main;
                            _skillState = SkillType.None_skill;
                            _map.cleanALL();
                            _map.CellinVisableALL();
                            break;
                        case SkillType.SingleEnemy:
                            Debug.Log(_currentUnit.pawnObj.Skills[skill_Num].Name + " " + skill_Num);
                            if (_currentUnit.pawnObj.HP > 0 && _interactiveUnit != null && _interactiveUnit.characterState == Character.Enemy && _interactiveUnit._currentCell.ifVisable)
                            {
                                if (_currentUnit.pawnObj.ACTPOINT >= _currentUnit.pawnObj.Skills[skill_Num].APCost)
                                {
                                    int interacteOriginHP = _interactiveUnit.pawnObj.HP;
                                    _interactiveUnit._currentCell.normalLight();
                                    var log = "";
                                    Tuple<int, string> res = _currentUnit.pawnObj.Skills[skill_Num].Cast(_currentUnit.pawnObj, _interactiveUnit.pawnObj,out log);
                                    addContent(log);
                                    _skillState = SkillType.None_skill;
                                    _currentUnit._skillboard.SetActive(false);
                                    _interactiveUnit._HPBar.changeValue(_interactiveUnit.pawnObj.HP, _interactiveUnit.pawnObj.GetAttribute(PawnAttribute.HP));
                                    _currentUnit._MPBar.changeValue(_currentUnit.pawnObj.ACTPOINT, _currentUnit.pawnObj.GetAttribute(PawnAttribute.ACTPOINT));
                                    playAnima();
                                    //判断接收技能者的状态，并切换动画状态
                                    if (_interactiveUnit.pawnObj.HP < interacteOriginHP)
                                    {
                                        if (_interactiveUnit.pawnObj.HP > 0)
                                        {
                                            _interactiveUnit._selfAnim.SetTrigger("impact");
                                        }
                                        else
                                        {
                                            _interactiveUnit._selfAnim.SetTrigger("die");
                                            var logs = "";
                                            _currentUnit.pawnObj.GainExp(1000, out logs);
                                            addContent(logs);
                                            _currentUnit.pawnObj.GainMoney(600, out logs);
                                            addContent(logs);
                                        }
                                    }
                                    else
                                    {
                                        //_interactiveUnit所在格子position处播放增益动画
                                    }
                                }
                                else
                                {
                                    String nextLine = "[" + _currentUnit.pawnObj.Name + "] need (" + _currentUnit.pawnObj.Skills[skill_Num].APCost + ") Action Point, but only have " + _currentUnit.pawnObj.ACTPOINT + " Action Point.";
                                    _content.addTextLine(nextLine);
                                    _currentUnit._skillboard.SetActive(false);
                                }
                            }
                            _interactiveUnit = null;
                            _turnState = MainState.None_main;
                            _skillState = SkillType.None_skill;
                            _map.cleanALL();
                            _map.CellinVisableALL();
                            break;
                        case SkillType.RangeEnemy:
                            if (_currentUnit.pawnObj.HP > 0 && _interactiveUnit != null && _interactiveUnit.characterState == Character.Enemy && _interactiveUnit._currentCell.ifVisable)
                            {
                                if (_currentUnit.pawnObj.ACTPOINT >= _currentUnit.pawnObj.Skills[skill_Num].APCost)
                                {
                                    _map.cleanALL();
                                    List<MyTurnUnit> _nowInteractiveUnits = new List<MyTurnUnit>();
                                    int center = _cellList.FindIndex(x => x.Equals(_interactiveUnit._currentCell));
                                    for (int i = 0; i < _enemies.Count; i++)
                                    {
                                        MyTurnUnit _tempUnit = _enemies[i].GetComponent<MyTurnUnit>();
                                        int next = _cellList.FindIndex(x => x.Equals(_tempUnit._currentCell));
                                        if (_tempUnit._currentCell.ifVisable && _tempUnit != _currentUnit)
                                        {
                                            for (int j = -_currentUnit.pawnObj.Skills[skill_Num].Range / 2; j < _currentUnit.pawnObj.Skills[skill_Num].Range / 2; j++)
                                            {
                                                int centerP = center + j * col;
                                                int low = Math.Max(centerP - center % col, centerP - _currentUnit.pawnObj.Skills[skill_Num].Range / 2);
                                                int high = Math.Min(centerP - center % col + col, centerP + _currentUnit.pawnObj.Skills[skill_Num].Range / 2);
                                                if (next >= low && next <= high)
                                                    _nowInteractiveUnits.Add(_tempUnit);
                                            }
                                            
                                        }
                                    }
                                    
                                    playAnima();

                                    for (int i = 0; i < _nowInteractiveUnits.Count; i++)
                                    {
                                        int interacteOriginHP = _nowInteractiveUnits[i].pawnObj.HP;
                                        var log = "";
                                        Tuple<int, string> res = _currentUnit.pawnObj.Skills[skill_Num].Cast(_currentUnit.pawnObj, _nowInteractiveUnits[i].pawnObj, out log);
                                        String nextLine = "[" + _currentUnit.pawnObj.Name + "] use " + _currentUnit.pawnObj.Skills[skill_Num].Name + " to [" + _nowInteractiveUnits[i].pawnObj.Name + "] , skill value is (" + res.Item1 + ") , cost (" + _currentUnit.pawnObj.Skills[skill_Num].APCost + ") Action Point.";
                                        addContent(log);
                                        Debug.Log(_nowInteractiveUnits[i].pawnObj.HP + " " + _nowInteractiveUnits[i].pawnObj.GetAttribute(PawnAttribute.HP));
                                        _nowInteractiveUnits[i]._HPBar.changeValue(_nowInteractiveUnits[i].pawnObj.HP, _nowInteractiveUnits[i].pawnObj.GetAttribute(PawnAttribute.HP));
                                        //判断接收技能者的状态，并切换动画状态
                                        if (_nowInteractiveUnits[i].pawnObj.HP < interacteOriginHP)
                                        {
                                            if (_nowInteractiveUnits[i].pawnObj.HP > 0)
                                            {
                                                _nowInteractiveUnits[i]._selfAnim.SetTrigger("impact");
                                            }
                                            else
                                            {
                                                _nowInteractiveUnits[i]._selfAnim.SetTrigger("die");
                                                var logs = "";
                                                _currentUnit.pawnObj.GainExp(1000, out logs);
                                                addContent(logs);
                                                _currentUnit.pawnObj.GainMoney(600, out logs);
                                                addContent(logs);
                                            }
                                        }
                                        else
                                        {
                                            //_interactiveUnit所在格子position处播放增益动画
                                        }
                                    }

                                    
                                    _skillState = SkillType.None_skill;
                                    _currentUnit._skillboard.SetActive(false);
                                    _currentUnit._MPBar.changeValue(_currentUnit.pawnObj.ACTPOINT, _currentUnit.pawnObj.GetAttribute(PawnAttribute.ACTPOINT));
                                }
                                else
                                {
                                    String nextLine = "[" + _currentUnit.pawnObj.Name + "] need (" + _currentUnit.pawnObj.Skills[skill_Num].APCost + ") Action Point, but only have " + _currentUnit.pawnObj.ACTPOINT + " Action Point.";
                                    _content.addTextLine(nextLine);
                                    _currentUnit._skillboard.SetActive(false);
                                }
                            }
                            _interactiveUnit = null;
                            _turnState = MainState.None_main;
                            _skillState = SkillType.None_skill;
                            _map.cleanALL();
                            _map.CellinVisableALL();
                            break;
                        default:
                            break;
                    }
                    break;
                case MainState.Item_main:
                    if (_itemState == ItemType.Preusing)
                    {
                        int interacteOriginHP = _interactiveUnit.pawnObj.HP;
                        if (_interactiveUnit != null)
                        {
                            var log = "";
                            Tuple<int, string> res = nextItem.Cast(_currentUnit.pawnObj, _interactiveUnit.pawnObj, out log);
                            addContent(log);
                        }
                        if (_interactiveUnit.pawnObj.HP < interacteOriginHP)
                        {
                            if (_interactiveUnit.pawnObj.HP > 0)
                            {
                                _interactiveUnit._selfAnim.SetTrigger("impact");
                            }
                            else
                            {
                                _interactiveUnit._selfAnim.SetTrigger("die");
                                var logs = "";
                                _currentUnit.pawnObj.GainExp(1000, out logs);
                                addContent(logs);
                            }
                        }
                        _currentUnit._MPBar.changeValue(_currentUnit.pawnObj.ACTPOINT, _currentUnit.pawnObj.GetAttribute(PawnAttribute.ACTPOINT));
                        _interactiveUnit._HPBar.changeValue(_interactiveUnit.pawnObj.HP, _interactiveUnit.pawnObj.GetAttribute(PawnAttribute.HP)); 
                        _itemState = ItemType.None_item;
                        _map.cleanALL();
                        _map.CellinVisableALL();
                    }
                    break;
                case MainState.Skip_main:
                    break;
                default:
                    break;
            }
        }
    }

    private void MouseDetect()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            _currentTarget = hitInfo.transform;
            TurnCell cell = _currentTarget.GetComponent<TurnCell>();
            MyTurnUnit unit = _currentTarget.GetComponent<MyTurnUnit>();
            if (cell != null)
            {
                _currentCell = cell;
                switch (_turnState)
                {
                    case MainState.None_main:
                        break;
                    case MainState.Move_main:
                        if (_moveState == MoveType.PreSelect_move)
                        { 
                            MAP.Instance.Astar(_currentUnit._currentCell, cell);
                        }
                        if (_moveState == MoveType.Moving_move)
                        {
                            if (!_currentUnit._isAniming)
                            {
                                _map.cleanALL();
                                _map.CellinVisableALL();
                                _turnState = MainState.None_main;
                                _moveState = MoveType.Select_move;
                            }
                        }
                        break;
                    case MainState.Skill_main:
                        if (_skillState != SkillType.None_skill)
                        {
                            _map.cleanALL();
            
                        }
                        break;
                    case MainState.Item_main:
                        if (_itemState != ItemType.None_item)
                        {
                            _map.cleanALL();
                        }
                        break;
                    case MainState.Skip_main:
                        break;
                    default:
                        break;
                }
            }

            if (unit != null)
            {
                _interactiveUnit = unit;
                switch (_turnState)
                {
                    case MainState.None_main:
                        break;
                    case MainState.Move_main:

                        break;
                    case MainState.Skill_main:

                        switch (_skillState)
                        {
                            case SkillType.None_skill:
                                break;
                            case SkillType.SinglePlayer:
                                if (unit.characterState == Character.Player)
                                {
                                   unit._currentCell.Highlight();
                                }
                                break;
                            case SkillType.RangePlayer:
                                if (unit.characterState == Character.Player)
                                {
                                    _map.cleanALL();
                                    int cerid = _cellList.FindIndex(x => x.Equals(_interactiveUnit._currentCell));
                                    for (int i = 0; i < skill_y; i++)
                                       {
                                        int nextA = cerid + col * (i - skill_y / 2);
                                        for (int j = 0; j < skill_x; j++)
                                        {
                                            int nextB = nextA + (j - skill_x / 2);
                                            if (nextB >= 0 && nextB < _cellList.Count)
                                                _cellList[nextB].Highlight();
                                        }
                                    }
                                
                                }
                                break;
                            case SkillType.SingleEnemy:
                                if (unit.characterState == Character.Enemy)
                                {
                                    unit._currentCell.enemyLight();
                                }
                                break;
                            case SkillType.RangeEnemy:
                                if (unit.characterState == Character.Enemy)
                                {  
                                        _map.cleanALL();
                                        int cerid = _cellList.FindIndex(x => x.Equals(_interactiveUnit._currentCell));
                                        for (int i = 0; i < skill_y; i++)
                                        {
                                            int nextA = cerid + col * (i - skill_y / 2);
                                            for (int j = 0; j < skill_x; j++)
                                            {
                                                int nextB = nextA + (j - skill_x / 2);
                                                if (nextB >= 0 && nextB < _cellList.Count)
                                                    _cellList[nextB].enemyLight();
                                            }
                                        }                    
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case MainState.Item_main:
                        Debug.Log(unit.pawnObj.Name+" "+unit.characterState);
                            if (unit.characterState == Character.Enemy)
                            {
                                unit._currentCell.enemyLight();
                                _interactiveUnit = unit;
                            }
                            if (unit.characterState == Character.Player)
                            {
                                unit._currentCell.Highlight();
                                _interactiveUnit = unit;
                            }
                        
                        break;
                    case MainState.Skip_main:
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            if (_currentCell != null)
            {
                _interactiveUnit = null;
                _map.cleanALL();
                _currentCell = null;
                
            }
        }
    }

    
    public void skilltoShowNeiborCells(int range)
    {
        _map.cleanALL();
        _turnState = MainState.Skill_main;
        showNeiborCells(_currentUnit._currentCell, range);
    }

    private void showNeiborCells(TurnCell centerCell, int R)
    {
        int center = _cellList.FindIndex(x => x.Equals(centerCell));
        for (int i = R; i >= - R; i--)
        {
            int min = (center / col + i) * col;
            int max = (center / col + i + 1) * col;
            for (int j = Math.Abs(i) - R; j <= Math.Abs(Math.Abs(i) - R); j++)
            {
                int cnt = center + i * col + j;
                if (cnt >= 0 && cnt <= _cellList.Count)
                {
                    if (cnt < max && cnt >= min)
                        _map.CellVisable(cnt);
                }
            }
        }
    }

    //修改对象实时所在格子状态的方法
    public void transStateToMove()
    {
        if (_moveState == MoveType.None_move)
        {
            showNeiborCells(_currentUnit._currentCell, _currentUnit.pawnObj.ACTPOINT);
            _turnState = MainState.Move_main;
            _moveState = MoveType.PreSelect_move;
           
        }
    }

    [Obsolete]
    public void tranStateToSkill()
    {
        _turnState = MainState.Skill_main;
        //更新技能UI界面
        _currentUnit._skillboard.SetActive(true);
    }

    [Obsolete]
    private void ifUpate()
    {
        SkillBoard _player_SkillBoard_Comp = _currentUnit._skillboard.transform.FindChild("Viewport").transform.FindChild("Content").GetComponent<SkillBoard>();
        if (_currentUnit.pawnObj.Skills.Count != _player_SkillBoard_Comp.transform.childCount + 1)
        {
            _player_SkillBoard_Comp.addSkillButton(_currentUnit.pawnObj.Skills[_currentUnit.pawnObj.Skills.Count - 1], _currentUnit.characterState, this);
        }
    }

    public void tranStateToItem()
    {
        GameObject Apic;
        GameObject Kpic;
        GameObject Ppic;
        GameObject Spic;
        _turnState = MainState.Item_main;
        //更新道具UI界面
        if (_currentUnit.characterState == Character.Player)
        {
            _pBag.SetActive(true);
            Text nameText = _pBag.transform.Find("person/NAME").GetComponent<Text>();
            nameText.text = _currentUnit.pawnObj.Name;
            int count = _originCanvas.transform.childCount - 1;//Panel移位
            _pBag.transform.SetSiblingIndex(count);//Panel移位

            Text LVL0 = _pBag.transform.Find("person/LVL0").GetComponent<Text>();
            Text LVL1 = _pBag.transform.Find("person/LVL1").GetComponent<Text>();
            Text LVL2 = _pBag.transform.Find("person/LVL2").GetComponent<Text>();
            Text LVL3 = _pBag.transform.Find("person/LVL3").GetComponent<Text>();
            Text MONEY = _pBag.transform.Find("person/MONEY").GetComponent<Text>();

            LVL0.text = "" + _currentUnit.WEP;
            LVL1.text = "" + _currentUnit.HEL;
            LVL2.text = "" + _currentUnit.ARM;
            LVL3.text = "" + _currentUnit.BOT;
            MONEY.text = "" + _currentUnit.pawnObj.Money;

            Apic = GameObject.Find("Canvas/PlayerBag/person/Apic");
            Kpic = GameObject.Find("Canvas/PlayerBag/person/Kpic");
            Ppic = GameObject.Find("Canvas/PlayerBag/person/Ppic");
            Spic = GameObject.Find("Canvas/PlayerBag/person/Spic");
        }
        else
        {
            _eBag.SetActive(true);
            Text nameText = _eBag.transform.Find("person/NAME").GetComponent<Text>();
            nameText.text = _currentUnit.pawnObj.Name;
            int count = _originCanvas.transform.childCount - 1;//Panel移位
            _eBag.transform.SetSiblingIndex(count);//Panel移位

            Text LVL0 = _eBag.transform.Find("person/LVL0").GetComponent<Text>();
            Text LVL1 = _eBag.transform.Find("person/LVL1").GetComponent<Text>();
            Text LVL2 = _eBag.transform.Find("person/LVL2").GetComponent<Text>();
            Text LVL3 = _eBag.transform.Find("person/LVL3").GetComponent<Text>();
            Text MONEY = _eBag.transform.Find("person/MONEY").GetComponent<Text>();

            LVL0.text = "" + _currentUnit.WEP;
            LVL1.text = "" + _currentUnit.HEL;
            LVL2.text = "" + _currentUnit.ARM;
            LVL3.text = "" + _currentUnit.BOT;
            MONEY.text = "" + _currentUnit.pawnObj.Money;

            Apic = GameObject.Find("Canvas/EnemyBag/person/Apic");
            Kpic = GameObject.Find("Canvas/EnemyBag/person/Kpic");
            Ppic = GameObject.Find("Canvas/EnemyBag/person/Ppic");
            Spic = GameObject.Find("Canvas/EnemyBag/person/Spic");
        }


        Apic.SetActive(false);
        Kpic.SetActive(false);
        Ppic.SetActive(false);
        Spic.SetActive(false);

        switch (_currentUnit.pawnObj.Name)
        {
            case "Archer":
                Apic.SetActive(true);
                break;
            case "Knight":
                Kpic.SetActive(true);
                break;
            case "Priest":
                Ppic.SetActive(true);
                break;
            case "Warrior":
                Spic.SetActive(true);
                break;
            default:
                break;
        }



    }

    [Obsolete]
    public void tranStateToSkip()
    {
        _turnState = MainState.Skip_main;
        _map.cleanALL();

        _interactiveUnit = null;
        _currentUnit.normalizeCharacter();
        var log = "";
        _currentUnit.pawnObj.GainExp(200, out log);
        addContent(log);
        ifUpate();
        NextTurn();
    }

    private void addContent(String log)
    {
        string[] sArray = log.Split('\n');
        foreach (string ss in sArray)
        {
            if (!ss.Equals(""))
                _content.addTextLine(ss);
        }

    }

    private void playAnima()
    {
        switch (skill_Num - 1)
        {
            case 0:
                _currentUnit._selfAnim.SetTrigger("Skill0");
                break;
            case 1:
                _currentUnit._selfAnim.SetTrigger("Skill1");
                break;
            case 2:
                _currentUnit._selfAnim.SetTrigger("Skill2");
                break;
            case 3:
                _currentUnit._selfAnim.SetTrigger("Skill3");
                break;
            default:
                _content.addTextLine("There must be something wrong with skill number you've pressed, and it is " + skill_Num);
                break;
        }
    }

    public void useItemtoPlayer()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].GetComponent<MyTurnUnit>()._currentCell._cellObj.SetActive(true);
            _players[i].GetComponent<MyTurnUnit>()._currentCell.ifVisable = true;
        }
    }

    public void useItemtoEnemy()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].GetComponent<MyTurnUnit>()._currentCell._cellObj.SetActive(true);
            _enemies[i].GetComponent<MyTurnUnit>()._currentCell.ifVisable = true;
        }
    }


    public void useHealItem(IItem item)
    {
        _itemState = ItemType.Preusing;
        if (_currentUnit.characterState == Character.Player)
        {
            _pBag.SetActive(false);
            _eBag.SetActive(false);
            useItemtoPlayer();
        }
        else
        {
            _pBag.SetActive(false);
            _eBag.SetActive(false);
            useItemtoEnemy();
        }
        nextItem = item;
    }

    public void useDamageItem(IItem item)
    {
        _itemState = ItemType.Preusing;
        if (_currentUnit.characterState == Character.Player)
        {
            _pBag.SetActive(false);
            _eBag.SetActive(false);
            useItemtoEnemy();
        }
        else
        {
            _pBag.SetActive(false);
            _eBag.SetActive(false);
            useItemtoPlayer();
        }
        nextItem = item;
    }

    public void updateWepon(int count)
    {
        _pBag.SetActive(false);
        _eBag.SetActive(false);
        if (_currentUnit.pawnObj.Money >= 180)
        {
            var log = "";
            _currentUnit.pawnObj.LoseMoney(180, out log);
            _content.addTextLine(log);
            switch (count)
            {
                case 0:
                    _content.addTextLine("[" + _currentUnit.pawnObj.Name + "] physcial attack up.");
                    _currentUnit.pawnObj.Weapon.PHY_ATK++;
                    _currentUnit.WEP++;
                    break;
                case 1:
                    _content.addTextLine("[" + _currentUnit.pawnObj.Name + "] magic defence up.");
                    _content.addTextLine("[" + _currentUnit.pawnObj.Name + "] physcial defence up.");
                    _currentUnit.pawnObj.Weapon.MAG_DEF++;
                    _currentUnit.pawnObj.Weapon.PHY_DEF++;
                    _currentUnit.HEL++;
                    break;
                case 2:
                    _content.addTextLine("[" + _currentUnit.pawnObj.Name + "] magic defence up a lot.");
                    _currentUnit.pawnObj.Weapon.MAG_DEF+=2;
                    _currentUnit.BOT++;
                    break;
                case 3:
                    _content.addTextLine("[" + _currentUnit.pawnObj.Name + "] physcial defence up a lot.");
                    _currentUnit.pawnObj.Weapon.PHY_DEF+=2;
                    _currentUnit.ARM++;
                    break;
                default:
                    break;
            }
        }
        else
        {
            _content.addTextLine("[" + _currentUnit.pawnObj.Name + "] does not have such money.");
        }
        tranStateToItem();
    }

    public void fixCharacterState()
    {
        if (_currentUnit.characterState == Character.Player)
        {
            lose.SetActive(true);
        }
        else
        {
            win.SetActive(true);
        }
    }

}
