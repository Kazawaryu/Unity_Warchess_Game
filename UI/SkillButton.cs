using OOAD_WarChess.Pawn.Skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public int _skillIdx { get; set; }
    MyTurn _turn;
    SkillType _type;
    ISkill _skill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Constructors(ISkill skill, int idx, Character character, MyTurn turn)
    {
        _skillIdx = idx;
        _turn = turn;
        _skill = skill;
        switch (skill.Type)
        {
            case OOAD_WarChess.Pawn.Skill.SkillType.SinglePlayer:
                if (character == Character.Player)
                    _type = SkillType.SinglePlayer;               
                else                
                    _type = SkillType.SingleEnemy;                
                break;
            case OOAD_WarChess.Pawn.Skill.SkillType.SingleEnemy:
                if (character == Character.Player)
                    _type = SkillType.SingleEnemy;
                else
                    _type = SkillType.SinglePlayer;
                break;
            case OOAD_WarChess.Pawn.Skill.SkillType.PlayerArea:
                if (character == Character.Player)
                    _type = SkillType.RangePlayer;
                else
                    _type = SkillType.RangeEnemy;
                break;
            case OOAD_WarChess.Pawn.Skill.SkillType.EnemyArea:
                if (character == Character.Player)
                    _type = SkillType.RangeEnemy;
                else
                    _type = SkillType.RangePlayer;
                break;
            default:
                break;
        }
    }

    public void OnClick()
    {
        _turn._skillState = _type;
        _turn.skill_x = _skill.EffectArea.Item1;
        _turn.skill_y = _skill.EffectArea.Item2;
        _turn.skill_range = _skill.Range;
        _turn.skill_Num = _skillIdx + 1;

        _turn.skilltoShowNeiborCells(_skill.Range);
    }
}
