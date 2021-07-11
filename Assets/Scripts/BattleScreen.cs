using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleScreen : MonoBehaviour
{
    Player player;
    Adventure currentAdventure;
    int currentWave; WaveType waveType; Text waveDisplay;
    int minionWaveCount, restWaveCount, eliteWaveCount, bossWaveCount;
    SideSpecificStatus playerSide, enemySide;
    public bool isActing, isSummoning;
    Unit actingUnit; int actingUnitIndex; Transform actingUnitTransform; UnitType actingUnitType;
    Skill currentSkill; Transform skillDisplay, advanceBanner;

    public static class BattleStatus {
        public static int highestAgility;
        public static bool isPoisoned;
    }
    public class SideSpecificStatus {
        public UnitType currentSide;
        public Unit[] formation = new Unit[10];
        public Unit[] activeFormation = new Unit[5];
        public int transformIndex;
        public bool alive;
        public SideSpecificStatus(UnitType unitType) {
            currentSide = unitType;
            if (currentSide == UnitType.PLAYER) {
                transformIndex = 2;
            } else if (currentSide == UnitType.ENEMY) {
                transformIndex = 1;
            }
        }

    }
    public class TargetUnit {
        public Unit unit;
        public Transform transform;
        public TargetUnit(Unit unit, Transform transform) {
            this.unit = unit;
            this.transform = transform;
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        skillDisplay = transform.Find("Skill Display");
        advanceBanner = transform.Find("Advance Banner");
        waveDisplay = transform.Find("Wave Display").GetComponent<Text>();
        playerSide = new SideSpecificStatus(UnitType.PLAYER);
        enemySide = new SideSpecificStatus(UnitType.ENEMY);
    }

    public void StartAdventure(Adventure adventure) {
        isActing = false; isSummoning = false;
        currentAdventure = adventure;
        currentWave = 0;
        minionWaveCount = 0; restWaveCount = 0; eliteWaveCount = 0; bossWaveCount = 0;
        transform.Find("Title").GetComponent<Text>().text = currentAdventure.name;
        waveDisplay.text = "Wave\n" + currentWave + "/" + currentAdventure.waveCount + "\n" + waveType.ToString();
        playerSide.activeFormation = new Unit[5];
        DisableBothSideTransform();
        InitializePlayerUnit();
        Transition();
    }
    void Transition() {
        enemySide.activeFormation = new Unit[5]; // Clear enemy formation array
        BattleStatus.highestAgility = -99999;
        currentWave += 1; // Increment Wave Count
        if (currentWave > currentAdventure.waveCount) {
            // Win
            currentAdventure.cleared = true;
            EndBattleScreen();
            return;
        }
        InitializeWave();
    }

    void Update() {
        if (!isActing) {
            StartCoroutine(Act());
        }
    }

    public IEnumerator Act() {
        isActing = true;
        SpawnUnit();
        if (isSummoning) {
            yield return new WaitForSeconds(1.5f);
            isSummoning = false;
        }
        playerSide.alive = false;
        enemySide.alive = false;
        BattleStatus.isPoisoned = false;
        BattleStatus.highestAgility = -99999;
        actingUnit = null;
        UpdateAgility(playerSide);
        UpdateAgility(enemySide);
        if (BattleStatus.isPoisoned) {
            yield return new WaitForSeconds(1);
        }
        if (!playerSide.alive) {
            /*Lose Prompt
            CopyExperienceGained();
            activePlayerFormation = new Unit[5];
            InitializePlayerUnit();
            for each enemy unit: reset agility;*/
            EndBattleScreen();
            yield break;
        } else if (!enemySide.alive) {
            //Win
            //Advance Wave
            //Refresh EnemyFormation
            DisplayExperienceGain();
            yield return new WaitForSeconds(1);
            Transition();
            yield return new WaitForSeconds(1.5f);
        }
        if (actingUnit != null && playerSide.alive && enemySide.alive) {
            currentSkill = actingUnit.getSkill();
            //Before Every Action
            DisplayBanner(skillDisplay, currentSkill.name);
            actingUnitTransform.GetComponent<Animator>().Play("Grow");
            //Call Skill Action
            StartSkillAction(currentSkill.skillType);
            yield return new WaitForSeconds(2.5f);
            RemoveStatusEffectAfterActing();
            yield return null;
        }
        isActing = false;
    }

    void SpawnUnit() {
        SpawnUnitHelper(playerSide);
        SpawnUnitHelper(enemySide);
    }
    private void SpawnUnitHelper(SideSpecificStatus side) {
        for (int i = 0; i < 5; i++) {
            Unit tempUnit = null;
            if (side.activeFormation[i] == null) {
                if (side.formation[i] != null) {
                    // Spawn first
                    tempUnit = side.formation[i];
                } else if (side.formation[i + 5] != null) {
                    // Spawn second
                    tempUnit = side.formation[i + 5];
                }
            }
            else if (side.activeFormation[i].stat.currentHealth <= 0 && side.activeFormation[i] == side.formation[i]
                && side.formation[i + 5] != null) {
                //Spawn second
                tempUnit = side.formation[i + 5];
            }
            if (tempUnit != null) {
                side.activeFormation[i] = tempUnit;
                Transform tempUnitTransform = transform.GetChild(side.transformIndex).GetChild(i);
                tempUnit.ClearStatus(tempUnitTransform);
                if (BattleStatus.highestAgility > 0) {
                    tempUnit.stat.currentAgility += BattleStatus.highestAgility;
                }
                tempUnitTransform.gameObject.SetActive(true);
                tempUnitTransform.Find("Protection").gameObject.SetActive(false);
                tempUnitTransform.GetComponent<Image>().sprite = tempUnit.GetElementSprite();
                tempUnitTransform.GetChild(0).GetComponent<Image>().sprite = tempUnit.sprite;
                tempUnitTransform.GetComponent<Animator>().Play("Spawn");
                tempUnitTransform.GetComponent<Animator>().SetBool("isDead", false);
                tempUnitTransform.Find("Health Bar").GetComponent<Slider>().value = 1;
                isSummoning = true;
            }
        }
    }
    void InitializePlayerUnit() {
        for (int i = 0; i < 10; i++) {
            if (player.formation[i] == -1) {
                playerSide.formation[i] = null;
            } else {
                playerSide.formation[i] = new Unit(player.inventory[player.formation[i]]);
            }
        }
    }
    void DisplayExperienceGain() {
        if (currentWave > 0) {
            // Display exp gain
            Transform playerTransformGroup = transform.GetChild(playerSide.transformIndex);
            int expGained = 0;
            switch (waveType) {
                case WaveType.Minion:
                    expGained = 10;
                    minionWaveCount++;
                    break;
                case WaveType.Rest:
                    expGained = 5;
                    restWaveCount++;
                    break;
                case WaveType.Elite:
                    expGained = 30;
                    eliteWaveCount++;
                    break;
                case WaveType.Boss:
                    expGained = 50;
                    bossWaveCount++;
                    break;
            }
            for (int j = 0; j < playerSide.formation.Length; j++) {
                if (playerSide.formation[j] != null) {
                    bool gainedLevel = playerSide.formation[j].GainExp(expGained, playerTransformGroup.GetChild(j % 5));
                    if (playerSide.formation[j] == playerSide.activeFormation[j % 5] && playerSide.formation[j].stat.currentHealth > 0) {
                        playerTransformGroup.GetChild(j % 5).Find("Exp Text").GetComponent<Text>().text = "+" + expGained + " exp";
                        playerTransformGroup.GetChild(j % 5).Find("Exp Text").gameObject.SetActive(true);
                        playerTransformGroup.GetChild(j % 5).Find("Exp Text").GetComponent<Animation>().Play();
                        if (gainedLevel) {
                            playerTransformGroup.GetChild(j % 5).Find("Level Up Text").gameObject.SetActive(true);
                            playerTransformGroup.GetChild(j % 5).Find("Level Up Text").GetComponent<Animation>().Play();
                        }
                    }
                    playerSide.formation[j].ResetAgility();
                }
            }
        }
    }
    void EndBattleScreen() {
        isActing = true;
        StopAllCoroutines();
        Transform adventureStats = transform.Find("Adventure Stats");
        adventureStats.gameObject.SetActive(true);
        adventureStats.GetComponent<AdventureStats>().OpenMenu(currentAdventure, currentWave, minionWaveCount, restWaveCount, eliteWaveCount, bossWaveCount);
        CopyExperienceGained();
    }
    void CopyExperienceGained() {
        for (int i = 0; i < 10; i++) {
            if (playerSide.formation[i] != null) {
                player.inventory[player.formation[i]].SetLevel(playerSide.formation[i].level);
                player.inventory[player.formation[i]].exp = playerSide.formation[i].exp;
            }
        }
    }
    void DisableBothSideTransform() {
        foreach (Transform child in transform.GetChild(1)) {
            child.gameObject.SetActive(false);
            child.localScale = new Vector3(1,1,1);
        }
        foreach (Transform child in transform.GetChild(2)) {
            child.gameObject.SetActive(false);
            child.Find("Exp Text").gameObject.SetActive(false);
            child.Find("Level Up Text").gameObject.SetActive(false);
            child.localScale = new Vector3(1,1,1);
        }
    }
    void InitializeWave() {
        if (currentWave == 1) {
            waveType = WaveType.Minion;
        } else if (currentWave == currentAdventure.waveCount) {
            waveType = WaveType.Boss;
        } else {
            int random = Random.Range(0, 4);
            if (random == 3) {
                waveType = WaveType.Elite;
            } else {
                waveType = WaveType.Minion;
            }
        }
        waveDisplay.text = "Wave\n" + currentWave + "/" + currentAdventure.waveCount + "\n" + waveType.ToString();
        DisplayBanner(advanceBanner, string.Format("Wave {0} - {1}", currentWave, waveType.ToString()));
        for (int i = 0; i < 10; i++) {
            enemySide.formation[i] = null;
        }
        switch (waveType) {
            case WaveType.Minion:
                for (int i = 0; i < 10; i++) {
                    if (i >= 5 && Random.Range(0, 2) > 0) {
                        enemySide.formation[i] = null;
                        continue;
                    }
                    int random = Random.Range(0, currentAdventure.minionsId.Length);
                    enemySide.formation[i] = new Unit(UnitDatabase.units[currentAdventure.minionsId[random]], Random.Range(currentAdventure.minMinionLevel, currentAdventure.maxMinionLevel + 1));
                }
                break;
            case WaveType.Rest:
                // Display Rest Reward (waveDisplay.text += "\nWhile resting, your party found")
                break;
            case WaveType.Elite:
                for (int i = 0; i < currentAdventure.eliteFormation.Length; i++) {
                    if (currentAdventure.eliteFormation[i] == -1) {
                        enemySide.formation[i] = null;
                    } else {
                        enemySide.formation[i] = new Unit(UnitDatabase.units[currentAdventure.eliteFormation[i]], currentAdventure.eliteFormationLevel[i]);
                    }
                }
                break;
            case WaveType.Boss:
                for (int i = 0; i < currentAdventure.bossFormation.Length; i++) {
                    if (currentAdventure.bossFormation[i] == -1) {
                        enemySide.formation[i] = null;
                    } else {
                        enemySide.formation[i] = new Unit(UnitDatabase.units[currentAdventure.bossFormation[i]], currentAdventure.bossFormationLevel[i]);
                    }
                }
                break;
        }
    }
    void DisplayBanner(Transform transform, string text) {
        transform.gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Text>().text = text;
        transform.gameObject.GetComponent<Animation>().Play();
    }
    void UpdateAgility(SideSpecificStatus side) {
        for (int i = 0; i < 5; i++) {
            if (side.activeFormation[i] != null && side.activeFormation[i].stat.currentHealth > 0) {
                // Trigger poison and check death
                bool isDead = false;
                if (side.activeFormation[i].stat.StatusContains("poison")) {
                    side.activeFormation[i].TakePoisonDamage(transform.GetChild(side.transformIndex).GetChild(i));
                    isDead = side.activeFormation[i].stat.currentHealth <= 0;
                    BattleStatus.isPoisoned = true;
                }
                if (!isDead) {
                    if (side.activeFormation[i].stat.currentAgility > BattleStatus.highestAgility) {
                        BattleStatus.highestAgility = side.activeFormation[i].stat.currentAgility;
                        actingUnit = side.activeFormation[i];
                        actingUnitIndex = i;
                        actingUnitTransform = transform.GetChild(side.transformIndex).GetChild(i);
                        actingUnitType = side.currentSide;
                    }
                    side.activeFormation[i].stat.currentAgility += side.activeFormation[i].stat.agility + side.activeFormation[i].stat.agilityModifier;
                    side.alive = true;
                }
            }
        }
    }
    void RemoveStatusEffectAfterActing() {
        // Remove status effect and trigger burn effect
        Debug.Log(actingUnit.stat.ToString());

        int healthModifierDifference = actingUnit.stat.healthModifier - actingUnit.stat.healthModifier / 3;

        actingUnit.stat.health -= healthModifierDifference;
        actingUnit.stat.currentStrength = actingUnit.stat.strength + actingUnit.stat.strengthModifier;
        actingUnit.stat.currentMagic = actingUnit.stat.magic + actingUnit.stat.magicModifier;
        actingUnit.stat.currentDefense = actingUnit.stat.defense + actingUnit.stat.defenseModifier;
        actingUnit.stat.currentAgility = actingUnit.stat.agility + actingUnit.stat.agilityModifier;

        actingUnit.stat.healthModifier /= 3;
        actingUnit.stat.strengthModifier /= 3;
        actingUnit.stat.magicModifier /= 3;
        actingUnit.stat.defenseModifier /= 3;
        actingUnit.stat.agilityModifier /= 3;

        if (actingUnit.stat.StatusContains("burn")) {
            actingUnit.TakeBurnDamage(actingUnitTransform);
        }
        actingUnit.ClearStatus(actingUnitTransform);
    }
    Dictionary<int, TargetUnit> GetPotentialTargets(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = new Dictionary<int, TargetUnit>();
        Unit[] tempFormation = new Unit[5];
        UnitType targetUnitType = UnitType.PLAYER;
        switch (targetType) {
            case TargetType.BUFF:
                if (actingUnitType == UnitType.PLAYER) {
                    tempFormation = playerSide.activeFormation;
                    targetUnitType = UnitType.PLAYER;
                } else {
                    tempFormation = enemySide.activeFormation;
                    targetUnitType = UnitType.ENEMY;
                }
                break;
            case TargetType.DEBUFF:
                if (actingUnitType == UnitType.PLAYER) {
                    tempFormation = enemySide.activeFormation;
                    targetUnitType = UnitType.ENEMY;
                } else {
                    tempFormation = playerSide.activeFormation;
                    targetUnitType = UnitType.PLAYER;
                }
                break;
            case TargetType.SELF:
                targetUnit.Add(actingUnitIndex, new TargetUnit(actingUnit, actingUnitTransform));
                break;
        }
        for (int i = 0; i < tempFormation.Length; i++) {
            if (tempFormation[i] != null && tempFormation[i].stat.currentHealth > 0) {
                targetUnit.Add(i, new TargetUnit(tempFormation[i], GetUnitTransform(targetUnitType, i)));
            }
        }
        return targetUnit;
    }
    Transform GetUnitTransform(UnitType unitType, int index) {
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
    void StartSkillAction(SkillType skillType) {
        switch (currentSkill.skillType) {
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
                if (currentSkill.extraEffect == 1) {
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
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            Attack_Effect(unit, transform);

            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
            RemoveDeadUnit(index, targetUnit, targetUnitIndex);
        }
        yield return null;
    }
    public IEnumerator Attack_Adjacent(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        // Pick a random unit from possible units and attack it/play animation
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            Attack_Effect(unit, transform, 2);
            yield return new WaitForSeconds(1.25f / currentSkill.extraEffect);

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
            yield return new WaitForSeconds(1.25f / currentSkill.extraEffect);

            RemoveDeadUnit(index, targetUnit, targetUnitIndex);
            RemoveDeadUnit(index - 1, targetUnit, targetUnitIndex);
            RemoveDeadUnit(index + 1, targetUnit, targetUnitIndex);
        }
        yield return null;
    }
    public IEnumerator AOE_Attack(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;
                Attack_Effect(unit, transform);
            }
            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
            for (int j = 0; j < targetUnitIndex.Count; j++) {
                RemoveDeadUnit(targetUnitIndex[j], targetUnit, targetUnitIndex);
            }
        }
        yield return null;
    }
    public void Attack_Effect(Unit unit, Transform transform, int speedMultiplier = 1) {
        if (currentSkill.status.ToLower().Contains("purge") && (Random.Range(0, 10) == 0)) {
            unit.stat.dodgePercent = 0;
            unit.stat.protectionCount = 1;
            unit.TriggerProtection(transform);
        }
        if (Random.Range(0, 100) < unit.stat.dodgePercent) {
            unit.DodgeDamage(transform, currentSkill.extraEffect);
        } else if (unit.stat.protectionCount > 0) {
            unit.TriggerProtection(transform);
        } else {
            unit.TakeDamage(transform, actingUnit, currentSkill);
        }
        transform.GetComponent<Animator>().SetFloat("attackSpeed", currentSkill.extraEffect * speedMultiplier);
        transform.GetComponent<Animator>().Play("Attack");
    }
    public IEnumerator Boost(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.Boost(transform, actingUnit, currentSkill);

            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Boost(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.Boost(transform, actingUnit, currentSkill);
            }
            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Debuff(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.Debuff(transform, actingUnit, currentSkill);

            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Debuff(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.Debuff(transform, actingUnit, currentSkill);
            }
            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Dodge(TargetType targetType) {
        actingUnit.EnableDodge(actingUnitTransform, currentSkill);
        yield return new WaitForSeconds(2.5f);
    }
    public IEnumerator AOE_Dodge(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.EnableDodge(transform, currentSkill);
            }
            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Heal(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.Heal(transform, actingUnit, currentSkill);

            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Heal(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.Heal(transform, actingUnit, currentSkill);
            }
            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator Protection(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        List<int> targetUnitIndex = new List<int>(targetUnit.Keys);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int index = targetUnitIndex[Random.Range(0, targetUnitIndex.Count)];
            Unit unit = targetUnit[index].unit;
            Transform transform = targetUnit[index].transform;

            unit.EnableProtection(transform);

            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
    public IEnumerator AOE_Protection(TargetType targetType) {
        Dictionary<int, TargetUnit> targetUnit = GetPotentialTargets(targetType);
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            foreach (TargetUnit element in targetUnit.Values) {
                Unit unit = element.unit;
                Transform transform = element.transform;

                unit.EnableProtection(transform);
            }
            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
        }
        yield return null;
    }
}

public enum WaveType {
    Minion, Rest, Elite, Boss
}