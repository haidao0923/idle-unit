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
    Unit[] playerFormation = new Unit[10]; Unit[] activePlayerFormation = new Unit[5];
    Unit[] enemyFormation = new Unit[10]; Unit[] activeEnemyFormation = new Unit[5];
    public bool isActing, isSummoning;
    Unit actingUnit; Transform actingUnitTransform; UnitType actingUnitType;
    Skill currentSkill; Transform skillDisplay, advanceBanner;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        skillDisplay = transform.Find("Skill Display");
        advanceBanner = transform.Find("Advance Banner");
        waveDisplay = transform.Find("Wave Display").GetComponent<Text>();
    }

    public void StartAdventure(Adventure adventure) {
        isActing = false; isSummoning = false;
        currentAdventure = adventure;
        currentWave = 0;
        minionWaveCount = 0; restWaveCount = 0; eliteWaveCount = 0; bossWaveCount = 0;
        transform.Find("Title").GetComponent<Text>().text = currentAdventure.name;
        waveDisplay.text = "Wave\n" + currentWave + "/" + currentAdventure.waveCount + "\n" + waveType.ToString();
        activePlayerFormation = new Unit[5];
        DisableBothSideTransform();
        InitializePlayerUnit();
        Transition();
    }
    void Transition() {
        activeEnemyFormation = new Unit[5]; // Clear enemy formation array
        currentWave += 1; // Increment Wave Count
        if (currentWave > currentAdventure.waveCount) {
            // Win
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
        bool playerStillAlive = false;
        bool enemyStillAlive = false;
        bool isPoisoned = false;
        int highestAgility = -99999;
        actingUnit = null;
        for (int i = 0; i < 5; i++) {
            if (activePlayerFormation[i] != null && activePlayerFormation[i].stat.currentHealth > 0) {
                // Trigger poison and check death
                bool isDead = false;
                if (activePlayerFormation[i].stat.StatusContains("poison")) {
                    activePlayerFormation[i].TakePoisonDamage(transform.GetChild(2).GetChild(i));
                    isDead = activePlayerFormation[i].stat.currentHealth <= 0;
                    isPoisoned = true;
                }
                if (!isDead) {
                    Debug.Log(string.Format("Player - {0}: {1} - {2}", activePlayerFormation[i].name, activePlayerFormation[i].stat.currentAgility, activePlayerFormation[i]));
                    if (activePlayerFormation[i].stat.currentAgility > highestAgility) {
                        highestAgility = activePlayerFormation[i].stat.currentAgility;
                        actingUnit = activePlayerFormation[i];
                        actingUnitTransform = transform.GetChild(2).GetChild(i);
                        actingUnitType = UnitType.PLAYER;
                    }
                    activePlayerFormation[i].stat.currentAgility += activePlayerFormation[i].stat.agility + activePlayerFormation[i].stat.agilityModifier;
                    playerStillAlive = true;
                }
            }
            if (activeEnemyFormation[i] != null && activeEnemyFormation[i].stat.currentHealth > 0) {
                // Trigger poison and check death
                bool isDead = false;
                if (activeEnemyFormation[i].stat.StatusContains("poison")) {
                    activeEnemyFormation[i].TakePoisonDamage(transform.GetChild(1).GetChild(i));
                    isDead = activeEnemyFormation[i].stat.currentHealth <= 0;
                    isPoisoned = true;
                }
                if (!isDead) {
                    Debug.Log(string.Format("Enemy - {0}: {1} - {2}", activeEnemyFormation[i].name, activeEnemyFormation[i].stat.currentAgility, activeEnemyFormation[i]));
                    if (activeEnemyFormation[i].stat.currentAgility > highestAgility) {
                        highestAgility = activeEnemyFormation[i].stat.currentAgility;
                        actingUnit = activeEnemyFormation[i];
                        actingUnitTransform = transform.GetChild(1).GetChild(i);
                        actingUnitType = UnitType.ENEMY;
                    }
                    activeEnemyFormation[i].stat.currentAgility += activeEnemyFormation[i].stat.agility + activeEnemyFormation[i].stat.agilityModifier;
                    enemyStillAlive = true;
                }
            }
        }
        if (isPoisoned) {
            yield return new WaitForSeconds(1);
        }
        if (!playerStillAlive) {
            /*Lose Prompt
            activePlayerFormation = new Unit[5];
            InitializePlayerUnit();
            for each enemy unit: reset agility;*/
            EndBattleScreen();
            yield break;
        } else if (!enemyStillAlive) {
            //Win
            //Advance Wave
            //Refresh EnemyFormation
            DisplayExperienceGain();
            yield return new WaitForSeconds(1);
            Transition();
            yield return new WaitForSeconds(1.5f);
        }
        if (actingUnit != null && playerStillAlive && enemyStillAlive) {
            currentSkill = actingUnit.getSkill();
            //Before Every Action
            DisplayBanner(skillDisplay, currentSkill.name);
            actingUnitTransform.GetComponent<Animator>().speed = 1;
            actingUnitTransform.GetComponent<Animator>().Play("Grow");

            switch (currentSkill.skillType) {
                case SkillType.ATTACK:
                    StartCoroutine(Attack());
                    break;
            }
            yield return new WaitForSeconds(2.5f);
            // Remove status effect and trigger burn effect
            actingUnit.stat.healthModifier /= 3;
            actingUnit.stat.strengthModifier /= 3;
            actingUnit.stat.magicModifier /= 3;
            actingUnit.stat.defenseModifier /= 3;
            actingUnit.stat.agilityModifier /= 3;

            actingUnit.stat.currentStrength = actingUnit.stat.strength + actingUnit.stat.healthModifier;
            actingUnit.stat.currentMagic = actingUnit.stat.magic + actingUnit.stat.magicModifier;
            actingUnit.stat.currentDefense = actingUnit.stat.defense + actingUnit.stat.defenseModifier;
            actingUnit.stat.currentAgility = actingUnit.stat.agility + actingUnit.stat.agilityModifier;

            if (actingUnit.stat.StatusContains("burn")) {
                actingUnit.TakeBurnDamage(actingUnitTransform);
            }
            actingUnit.ClearStatus(actingUnitTransform);
            yield return null;
        }
        isActing = false;
    }

    public IEnumerator Attack() {
        List<Unit> targetUnit = new List<Unit>();
        List<Transform> targetUnitTransform = new List<Transform>();
        GetPotentialTargets(targetUnit, targetUnitTransform, TargetType.DEBUFF);

        // Pick a random unit from possible units and attack it/play animation
        for (int i = 0; i < currentSkill.extraEffect && targetUnit.Count > 0; i++) {
            int random = Random.Range(0, targetUnitTransform.Count);
            Unit tempUnit = targetUnit[random];
            Transform tempUnitTransform = targetUnitTransform[random];
            tempUnit.TakeDamage(tempUnitTransform, actingUnit, currentSkill);

            tempUnitTransform.GetComponent<Animator>().speed = currentSkill.extraEffect;
            tempUnitTransform.GetComponent<Animator>().Play("Attack");
            yield return new WaitForSeconds(2.5f / currentSkill.extraEffect);
            RemoveDeadUnit(tempUnit, tempUnitTransform, targetUnit, targetUnitTransform);
        }
        yield return null;
    }

    void SpawnUnit() {
        for (int i = 0; i < 5; i++) {
            //Spawn Player Unit
            SpawnUnitHelper(playerFormation, activePlayerFormation, i, 2);

            //Spawn Enemy Unit
            SpawnUnitHelper(enemyFormation, activeEnemyFormation, i, 1);
        }
    }
    void SpawnUnitHelper(Unit[] formation, Unit[] activeFormation, int i, int order) {
        Unit tempUnit = null;
        if (activeFormation[i] == null) {
            if (formation[i] != null) {
                // Spawn first
                tempUnit = formation[i];
            } else if (formation[i + 5] != null) {
                // Spawn second
                tempUnit = formation[i + 5];
            }
        }
        else if (activeFormation[i].stat.currentHealth <= 0 && activeFormation[i] == formation[i]
            && formation[i + 5] != null) {
            //Spawn second
            tempUnit = formation[i + 5];
        }
        if (tempUnit != null) {
            activeFormation[i] = tempUnit;
            Transform tempUnitTransform = transform.GetChild(order).GetChild(i);
            tempUnit.ClearStatus(tempUnitTransform);
            tempUnitTransform.gameObject.SetActive(true);
            tempUnitTransform.GetComponent<Image>().sprite = tempUnit.GetElementSprite();
            tempUnitTransform.GetChild(0).GetComponent<Image>().sprite = tempUnit.sprite;
            tempUnitTransform.GetComponent<Animator>().speed = 1;
            tempUnitTransform.GetComponent<Animator>().Play("Spawn");
            tempUnitTransform.GetComponent<Animator>().SetBool("isDead", false);
            tempUnitTransform.Find("Health Bar").GetComponent<Slider>().value = 1;
            isSummoning = true;
        }
    }

    bool RemoveDeadUnit(Unit deadUnit, Transform deadUnitTransform, List<Unit> targetUnit = null, List<Transform> targetUnitTransform = null) {
        if (deadUnit.stat.currentHealth <= 0) {
            if (targetUnit != null && targetUnitTransform != null) {
                targetUnit.Remove(deadUnit);
                targetUnitTransform.Remove(deadUnitTransform);
            }
            return true;
        }
        return false;
    }
    void GetPotentialTargets(List<Unit> targetUnit, List<Transform> targetUnitTransform, TargetType targetType) {
        Unit[] tempFormation = new Unit[5];
        Transform tempTransformGroup = null;
        switch (targetType) {
            case TargetType.BUFF:
                if (actingUnitType == UnitType.PLAYER) {
                    tempFormation = activePlayerFormation;
                    tempTransformGroup = transform.GetChild(2);
                } else {
                    tempFormation = activeEnemyFormation;
                    tempTransformGroup = transform.GetChild(1);
                }
                break;
            case TargetType.DEBUFF:
                if (actingUnitType == UnitType.PLAYER) {
                    tempFormation = activeEnemyFormation;
                    tempTransformGroup = transform.GetChild(1);
                } else {
                    tempFormation = activePlayerFormation;
                    tempTransformGroup = transform.GetChild(2);
                }
                break;
            case TargetType.SELF:
                targetUnit.Add(actingUnit);
                targetUnitTransform.Add(actingUnitTransform);
                break;
        }
        for (int i = 0; i < tempFormation.Length; i++) {
            if (tempFormation[i] != null && tempFormation[i].stat.currentHealth > 0) {
                targetUnit.Add(tempFormation[i]);
                targetUnitTransform.Add(tempTransformGroup.GetChild(i));
            }
        }
    }
    void InitializePlayerUnit() {
        for (int i = 0; i < 10; i++) {
            if (player.formation[i] == -1) {
                playerFormation[i] = null;
            } else {
                playerFormation[i] = new Unit(player.inventory[player.formation[i]]);
            }
        }
    }
    void DisplayExperienceGain() {
        if (currentWave > 0) {
            // Display exp gain
            Transform playerTransformGroup = transform.GetChild(2);
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
                    expGained = 20;
                    bossWaveCount++;
                    break;
            }
            for (int j = 0; j < playerFormation.Length; j++) {
                if (playerFormation[j] != null) {
                    bool gainedLevel = playerFormation[j].GainExp(expGained);
                    if (playerFormation[j] == activePlayerFormation[j % 5] && playerFormation[j].stat.currentHealth > 0) {
                        playerTransformGroup.GetChild(j % 5).Find("Exp Text").GetComponent<Text>().text = "+" + expGained + " exp";
                        playerTransformGroup.GetChild(j % 5).Find("Exp Text").gameObject.SetActive(true);
                        playerTransformGroup.GetChild(j % 5).Find("Exp Text").GetComponent<Animation>().Play();
                        if (gainedLevel) {
                            playerTransformGroup.GetChild(j % 5).Find("Level Up Text").gameObject.SetActive(true);
                            playerTransformGroup.GetChild(j % 5).Find("Level Up Text").GetComponent<Animation>().Play();
                        }
                    }
                    playerFormation[j].ResetAgility();
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
        for (int i = 0; i < 10; i++) {
            if (playerFormation[i] != null) {
                player.inventory[player.formation[i]].SetLevel(playerFormation[i].level);
                player.inventory[player.formation[i]].exp = playerFormation[i].exp;
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
            enemyFormation[i] = null;
        }
        switch (waveType) {
            case WaveType.Minion:
                for (int i = 0; i < 10; i++) {
                    if (i >= 5 && Random.Range(0, 2) > 0) {
                        enemyFormation[i] = null;
                        continue;
                    }
                    int random = Random.Range(0, currentAdventure.minionsId.Length);
                    enemyFormation[i] = new Unit(UnitDatabase.units[currentAdventure.minionsId[random]], Random.Range(currentAdventure.minMinionLevel, currentAdventure.maxMinionLevel + 1));
                }
                break;
            case WaveType.Rest:
                // Display Rest Reward (waveDisplay.text += "\nWhile resting, your party found")
                break;
            case WaveType.Elite:
                for (int i = 0; i < currentAdventure.eliteFormation.Length; i++) {
                    if (currentAdventure.eliteFormation[i] == -1) {
                        enemyFormation[i] = null;
                    } else {
                        enemyFormation[i] = new Unit(UnitDatabase.units[currentAdventure.eliteFormation[i]], currentAdventure.eliteFormationLevel[i]);
                    }
                }
                break;
            case WaveType.Boss:
                for (int i = 0; i < currentAdventure.bossFormation.Length; i++) {
                    if (currentAdventure.bossFormation[i] == -1) {
                        enemyFormation[i] = null;
                    } else {
                        enemyFormation[i] = new Unit(UnitDatabase.units[currentAdventure.bossFormation[i]], currentAdventure.bossFormationLevel[i]);
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
}

public enum WaveType {
    Minion, Rest, Elite, Boss
}