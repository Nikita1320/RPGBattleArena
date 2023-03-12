using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterStatPanel : MonoBehaviour
{
    private Character character;
    [SerializeField] private Text damageStat;
    [SerializeField] private Text attackSpeedStat;
    [SerializeField] private Text moveSpeedStat;
    [SerializeField] private Text rotateSpeedStat;
    [SerializeField] private Text healthPointStat;
    [SerializeField] private Text armorStat;
    [SerializeField] private Text resistMagicStat;

    [SerializeField] private Text rangeAreaAttacked;
    [SerializeField] private Text rangeAttack;
    [SerializeField] private Text lifeTimeBullet;
    [SerializeField] private Text speedBullet;
    public void Init(Character _character)
    {
        if (character != null)
        {
            UnSubscribeOnChangedValueStat();
        }

        character = _character;

        RederDamageStat();
        RederAttackSpeedStat();
        RederMoveSpeedStat();
        RederRotateSpeedStat();
        RederHealthPointStat();
        RederArmorStat();
        RederResistMagicStat();

        if (character.CharacterData.CharacterStat.GetType() == typeof(RangeCharacterStatData))
        {
            rangeAreaAttacked.transform.parent.gameObject.SetActive(false);
            rangeAttack.transform.parent.gameObject.SetActive(false);

            lifeTimeBullet.transform.parent.gameObject.SetActive(true);
            speedBullet.transform.parent.gameObject.SetActive(true);

            RederLifeTimeBulletStat();
            RederSpeedBulletStat();
        }
        else
        {
            rangeAreaAttacked.transform.parent.gameObject.SetActive(true);
            rangeAttack.transform.parent.gameObject.SetActive(true);

            lifeTimeBullet.transform.parent.gameObject.SetActive(false);
            speedBullet.transform.parent.gameObject.SetActive(false);

            RederRangeAreaStat();
            RederRangeAttackStat();
        }

        SubscribeOnChangedValueStat();
    }
    private void RederDamageStat()
    {
        damageStat.text = $"Damage(+{character.Stats[TypeStat.Damage].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.Damage].Value}";
    }
    private void RederAttackSpeedStat()
    {
        attackSpeedStat.text = $"AttackSpeed(+{character.Stats[TypeStat.AttackSpeed].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.AttackSpeed].Value}";
    }
    private void RederMoveSpeedStat()
    {
        moveSpeedStat.text = $"MoveSpeed(+{character.Stats[TypeStat.MoveSpeed].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.MoveSpeed].Value}";
    }
    private void RederRotateSpeedStat()
    {
        rotateSpeedStat.text = $"RotateSpeed(+{character.Stats[TypeStat.RotateSpeed].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.RotateSpeed].Value}";
    }
    private void RederHealthPointStat()
    {
        healthPointStat.text = $"Health(+{character.Stats[TypeStat.Health].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.Health].Value}";
    }
    private void RederArmorStat()
    {
        armorStat.text = $"Armor(+{character.Stats[TypeStat.Armor].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.Armor].Value}";
    }
    private void RederResistMagicStat()
    {
        resistMagicStat.text = $"ResistMagic(+{character.Stats[TypeStat.ResistMagic].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.ResistMagic].Value}";
    }
    private void RederRangeAreaStat()
    {
        rangeAreaAttacked.text = $"RangeAreaAttack(+{character.Stats[TypeStat.RangeArea].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.RangeArea].Value}";
    }
    private void RederRangeAttackStat()
    {
        rangeAttack.text = $"RangeAttack(+{character.Stats[TypeStat.RangeAttack].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.RangeAttack].Value}";
    }
    private void RederLifeTimeBulletStat()
    {
        lifeTimeBullet.text = $"LifeTimeBullet(+{character.Stats[TypeStat.LifeTimeBullet].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.LifeTimeBullet].Value}";
    }
    private void RederSpeedBulletStat()
    {
        speedBullet.text = $"SpeedBullet(+{character.Stats[TypeStat.SpeedBullet].IncreseValueByLevel}/LvL) = {character.Stats[TypeStat.SpeedBullet].Value}";
    }
    private void SubscribeOnChangedValueStat()
    {
        character.Stats[TypeStat.Damage].changedValue += RederDamageStat;
        character.Stats[TypeStat.AttackSpeed].changedValue += RederAttackSpeedStat;
        character.Stats[TypeStat.MoveSpeed].changedValue += RederMoveSpeedStat;
        character.Stats[TypeStat.RotateSpeed].changedValue += RederRotateSpeedStat;
        character.Stats[TypeStat.Health].changedValue += RederHealthPointStat;
        character.Stats[TypeStat.Armor].changedValue += RederArmorStat;
        character.Stats[TypeStat.ResistMagic].changedValue += RederResistMagicStat;

        if (character.CharacterData.CharacterStat.GetType() == typeof(RangeCharacterStatData))
        {
            character.Stats[TypeStat.LifeTimeBullet].changedValue += RederLifeTimeBulletStat;
            character.Stats[TypeStat.SpeedBullet].changedValue += RederSpeedBulletStat;
        }
        else
        {
            character.Stats[TypeStat.RangeArea].changedValue += RederRangeAreaStat;
            character.Stats[TypeStat.RangeAttack].changedValue += RederRangeAttackStat;
        }
    }
    private void UnSubscribeOnChangedValueStat()
    {
        character.Stats[TypeStat.Damage].changedValue -= RederDamageStat;
        character.Stats[TypeStat.AttackSpeed].changedValue -= RederAttackSpeedStat;
        character.Stats[TypeStat.MoveSpeed].changedValue -= RederMoveSpeedStat;
        character.Stats[TypeStat.RotateSpeed].changedValue -= RederRotateSpeedStat;
        character.Stats[TypeStat.Health].changedValue -= RederHealthPointStat;
        character.Stats[TypeStat.Armor].changedValue -= RederArmorStat;
        character.Stats[TypeStat.ResistMagic].changedValue -= RederResistMagicStat;

        if (character.CharacterData.CharacterStat.GetType() == typeof(RangeCharacterStatData))
        {
            character.Stats[TypeStat.LifeTimeBullet].changedValue -= RederLifeTimeBulletStat;
            character.Stats[TypeStat.SpeedBullet].changedValue -= RederSpeedBulletStat;
        }
        else
        {
            character.Stats[TypeStat.RangeArea].changedValue -= RederRangeAreaStat;
            character.Stats[TypeStat.RangeAttack].changedValue -= RederRangeAttackStat;
        }
    }
}
