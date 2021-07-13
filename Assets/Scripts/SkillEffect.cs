using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    ActingUnit actingUnit;
    SideSpecificStatus playerSide, enemySide;
    public class TargetUnit {
        public Unit unit;
        public Transform transform;
        public TargetUnit(Unit unit, Transform transform) {
            this.unit = unit;
            this.transform = transform;
        }
    }

    Dictionary<int, TargetUnit> GetPotentialTargets(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = new Dictionary<int, TargetUnit>();
        Unit[] tempFormation = new Unit[5];
        UnitType targetUnitType = UnitType.PLAYER;
        switch (targetType) {
            case TargetType.BUFF:
                if (actingUnit.unitType == UnitType.PLAYER) {
                    tempFormation = playerSide.activeFormation;
                    targetUnitType = UnitType.PLAYER;
                } else {
                    tempFormation = enemySide.activeFormation;
                    targetUnitType = UnitType.ENEMY;
                }
                break;
            case TargetType.DEBUFF:
                if (actingUnit.unitType == UnitType.PLAYER) {
                    tempFormation = enemySide.activeFormation;
                    targetUnitType = UnitType.ENEMY;
                } else {
                    tempFormation = playerSide.activeFormation;
                    targetUnitType = UnitType.PLAYER;
                }
                break;
            case TargetType.SELF:
                targetUnit.Add(actingUnit.index, new TargetUnit(actingUnit.unit, actingUnit.transform));
                break;
        }
        for (int i = 0; i < tempFormation.Length; i++) {
            if (tempFormation[i] != null && tempFormation[i].stat.currentHealth > 0) {
                targetUnit.Add(i, new TargetUnit(tempFormation[i], GetUnitTransform(targetUnitType, i)));
            }
        }
        return targetUnit;
    }
    public Transform GetUnitTransform(UnitType unitType, int index) {
        if (unitType == UnitType.PLAYER) {
            return transform.GetChild(playerSide.transformIndex).GetChild(index);
        } else {
            return transform.GetChild(enemySide.transformIndex).GetChild(index);
        }
    }
    bool RemoveDeadUnit(int index, Dictionary<int, TargetUnit> targetUnit, List<int> targetUnitIndex) {
        if (!targetUnit.ContainsKey(index)) {
            return false;
        }
        if (targetUnit[index].unit.stat.currentHealth <= 0) {
            targetUnit.Remove(index);
            targetUnitIndex.Remove(index);
            return true;
        }
        return false;
    }
    public void StartSkillAction(SkillType skillType, ActingUnit parameterUnit, SideSpecificStatus playerSide, SideSpecificStatus enemySide) {
        actingUnit = parameterUnit;
        this.playerSide = playerSide;
        this.enemySide = enemySide;
        Debug.Log(actingUnit.currentSkill.skillType.ToString());
        switch (actingUnit.currentSkill.skillType) {
            case SkillType.ATTACK:
                StartCoroutine(Attack(TargetType.DEBUFF));
                break;
            case SkillType.ATTACK_ADJACENT:
                StartCoroutine(Attack_Adjacent(TargetType.DEBUFF));
                break;
            case SkillType.AOE_ATTACK:
                StartCoroutine(AOE_Attack(TargetType.DEBUFF));
                break;
            case SkillType.BOOST:
                if (actingUnit.currentSkill.extraEffect == 1) {
                    StartCoroutine(Boost(TargetType.SELF));
                } else {
                    StartCoroutine(Boost(TargetType.BUFF));
                }
                break;
            case SkillType.AOE_BOOST:
                StartCoroutine(AOE_Boost(TargetType.BUFF));
                break;
            case SkillType.DEBUFF:
                StartCoroutine(Debuff(TargetType.DEBUFF));
                break;
            case SkillType.AOE_DEBUFF:
                StartCoroutine(AOE_Debuff(TargetType.DEBUFF));
                break;
            case SkillType.DODGE:
                StartCoroutine(Dodge(TargetType.SELF));
                break;
            case SkillType.AOE_DODGE:
                StartCoroutine(AOE_Dodge(TargetType.BUFF));
                break;
            case SkillType.HEAL:
                StartCoroutine(Heal(TargetType.BUFF));
                break;
            case SkillType.AOE_HEAL:
                StartCoroutine(AOE_Heal(TargetType.BUFF));
                break;
            case SkillType.PROTECTION:
                StartCoroutine(Protection(TargetType.BUFF));
                break;
            case SkillType.AOE_PROTECTION:
                StartCoroutine(AOE_Protection(TargetType.BUFF));
                break;
        }
    }
    public IEnumerator Attack(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        // Pick a random unit from possible units and attack it/play animation
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            Attack_Effect(unit, transform);

            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
            RemoveDeadUnit(index, targetUnit, targetUnitIndex);
        }
        yield return null;
    }
    public IEnumerator Attack_Adjacent(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        // Pick a random unit from possible units and attack it/play animation
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            Attack_Effect(unit, transform, 2);
            yield return new WaitForSeconds(1.25f / actingUnit.currentSkill.extraEffect);

            if (targetUnit.ContainsKey(index - 1)) {
                unit = targetUnit[index - 1].unit;
                transform = targetUnit[index - 1].transform;
                Attack_Effect(unit, transform, 2);
            }
            if (targetUnit.ContainsKey(index + 1)) {
                unit = targetUnit[index + 1].unit;
                transform = targetUnit[index + 1].transform;
                Attack_Effect(unit, transform, 2);
            }
            yield return new WaitForSeconds(1.25f / actingUnit.currentSkill.extraEffect);

            RemoveDeadUnit(index, targetUnit, targetUnitIndex);
            RemoveDeadUnit(index - 1, targetUnit, targetUnitIndex);
            RemoveDeadUnit(index + 1, targetUnit, targetUnitIndex);
        }
        yield return null;
    }
    public IEnumerator AOE_Attack(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;
                Attack_Effect(unit, transform);
            }
            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
            for (int j = 0; j < targetUnitIndex.Count; j++) {
                RemoveDeadUnit(targetUnitIndex[j], targetUnit, targetUnitIndex);
            }
        }
        yield return null;
    }
    public void Attack_Effect(Unit unit, Transform transform, int speedMultiplier = 1) {
        if (actingUnit.currentSkill.status.ToLower().Contains("purge") && (Random.Range(0, 10) == 0)) {
            unit.stat.dodgePercent = 0;
            unit.stat.protectionCount = 1;
            unit.TriggerProtection(transform);
        }
        if (Random.Range(0, 100) < unit.stat.dodgePercent) {
            unit.DodgeDamage(transform, actingUnit.currentSkill.extraEffect);
        } else if (unit.stat.protectionCount > 0) {
            unit.TriggerProtection(transform);
        } else {
            unit.TakeDamage(transform, actingUnit.unit, actingUnit.currentSkill);
        }
        transform.GetComponent<Animator>().SetFloat("attackSpeed", actingUnit.currentSkill.extraEffect * speedMultiplier);
        transform.GetComponent<Animator>().Play("Attack");
    }
    public IEnumerator Boost(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.Boost(transform, actingUnit.unit, actingUnit.currentSkill);

            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Boost(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.Boost(transform, actingUnit.unit, actingUnit.currentSkill);
            }
            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Debuff(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.Debuff(transform, actingUnit.unit, actingUnit.currentSkill);

            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Debuff(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.Debuff(transform, actingUnit.unit, actingUnit.currentSkill);
            }
            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Dodge(TargetType targetType) {
        actingUnit.unit.EnableDodge(actingUnit.transform, actingUnit.currentSkill);
        yield return new WaitForSeconds(2.5f);
    }
    public IEnumerator AOE_Dodge(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.EnableDodge(transform, actingUnit.currentSkill);
            }
            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Heal(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.Heal(transform, actingUnit.unit, actingUnit.currentSkill);

            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Heal(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.Heal(transform, actingUnit.unit, actingUnit.currentSkill);
            }
            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Protection(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.EnableProtection(transform);

            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Protection(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < actingUnit.currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.EnableProtection(transform);
            }
            yield return new WaitForSeconds(2.5f / actingUnit.currentSkill.extraEffect);
        }
        yield return null;
    }

}
