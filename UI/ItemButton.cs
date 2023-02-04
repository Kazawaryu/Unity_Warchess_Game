using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OOAD_WarChess.Item;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    IItem _item = null;
    MyTurn _turn = null;
    bool _isItem = false;
    bool _ifHeal = false;
    int _updateCount = -1;

    // Start is called before the first frame update


    public void itemConstructor(IItem item,MyTurn turn,bool ifHeal)
    {
        _isItem = true;
        _item = item;
        _turn = turn;
        _ifHeal = ifHeal;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate () {
            this.onClick(this.gameObject);
        });
    }

    public void updateConstructor(int count,MyTurn turn)
    {
        _isItem = false;
        _turn = turn;
        _updateCount = count;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate () {
            this.onClick(this.gameObject);
        });
    }

    void onClick(GameObject obj)
    {
        if (_isItem)
        {
            if (_ifHeal)
            {
                _turn.useHealItem(_item);
            }
            else
            {
                _turn.useDamageItem(_item);
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            _turn.updateWepon(_updateCount);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
