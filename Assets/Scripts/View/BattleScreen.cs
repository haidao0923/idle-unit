using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleScreen : MonoBehaviour
{
    Player player; SkillEffect skillEffect;
    Adventure currentAdventure;
    int currentWave; WaveType waveType; Text waveDisplay;
    int minionWaveCount, restWaveCount, eliteWaveCount, bossWaveCount;
    public SideSpecificStatus playerSide, enemySide;
    public bool isActing, isSummoning;
    public ActingUnit actingUnit;
    Transform skillDisplay, advanceBanner, adventureStats;

    public static class BattleStatus {
        public static int highestAgility;
        public static bool isPoisoned;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<Player>();
        skillEffect = GetComponent<SkillEffect>();
        skillDisplay = transform.Find("Skill Display");
        advanceBanner = transform.Find("Advance Banner");
        adventureStats = transform.Find("Adventure Stats");
        waveDisplay = transform.Find("Wave Display").GetComponent<Text>();
        playerSide = new SideSpecificStatus(UnitType.PLAYER);
        enemySide = new SideSpecificStatus(UnitType.ENEMY);
        actingUnit = new ActingUnit();
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
            if (!currentAdventure.cleared) {
                currentAdventure.cleared = true;
                Adventure.clearedAdventures += 1;
            }
            EndBattleScreen();
            return;
        }
        InitializeWave();
    }
    void OpenReviveMenu() {
        Time.timeScale = 0;
        Transform reviveMenu = transform.Find("Revive Menu");
        reviveMenu.gameObject.SetActive(true);
        // Enable/Disable WatchAd/UsePhoenixFeather Buttons
        Transform buttons = reviveMenu.Find("Border/Background/Menu");
        ConsumableDatabase.currentReviveTime = System.DateTime.Now;
        if ((ConsumableDatabase.currentReviveTime - ConsumableDatabase.lastRevivedTime).TotalHours >= 4) {
            buttons.GetChild(0).GetComponent<Button>().interactable = true;
            buttons.GetChild(0).GetChild(0).GetComponent<Text>().text = "Watch Ad";
        } else {
            buttons.GetChild(0).GetComponent<Button>().interactable = false;
            buttons.GetChild(0).GetChild(0).GetComponent<Text>().text = "Currently On Cooldown";
        }
        buttons.GetChild(1).GetChild(0).GetComponent<Text>().text = "Use Feather\nOwned: " + ConsumableDatabase.consumables["Misc"][2].quantity;
        if (ConsumableDatabase.consumables["Misc"][2].quantity > 0) {
            buttons.GetChild(1).GetComponent<Button>().interactable = true;
        } else {
            buttons.GetChild(1).GetComponent<Button>().interactable = false;
        }
    }
    public void WatchAd() {
        // Show Ad
        ConsumableDatabase.lastRevivedTime = System.DateTime.Now;
        ConsumableDatabase.currentReviveTime = ConsumableDatabase.lastRevivedTime;
        ReviveUnit();
        SaveAndLoad.data.SaveCooldownTimer();
    }
    public void UsePhoenixFeather() {
        ConsumableDatabase.consumables["Misc"][2].quantity -= 1;
        ReviveUnit();
    }
    public void AcceptDefeat() {
        Time.timeScale = 1;
        EndBattleScreen();
    }
    void ReviveUnit() {
        Time.timeScale = 1;
        playerSide.activeFormation = new Unit[5];
        InitializePlayerUnit();
        for (int j = 0; j < enemySide.activeFormation.Length; j++) {
            if (enemySide.activeFormation[j] != null) {
                enemySide.activeFormation[j].ResetAgility();
            }
        }
    }
    void EndBattleScreen() {
        isActing = true;
        StopAllCoroutines();
        adventureStats.gameObject.SetActive(true);
        adventureStats.GetComponent<AdventureStats>().OpenMenu(currentAdventure, currentWave, minionWaveCount, restWaveCount, eliteWaveCount, bossWaveCount);
        CopyExperienceGained();
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
        actingUnit.unit = null;
        UpdateAgility(playerSide);
        UpdateAgility(enemySide);
        if (BattleStatus.isPoisoned) {
            yield return new WaitForSeconds(1);
        }
        if (!playerSide.alive) {
            OpenReviveMenu();
            yield return null;
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
            actingUnit.currentSkill = actingUnit.unit.getSkill();
            //Before Every Action
            DisplayBanner(skillDisplay, actingUnit.currentSkill.name);
            actingUnit.transform.GetComponent<Animator>().Play("Grow");
            //Call Skill Action
            skillEffect.StartSkillAction(actingUnit.currentSkill.skillType, actingUnit, playerSide, enemySide);
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
                    expGained = (int) WaveTypeExp.MINION;
                    minionWaveCount++;
                    currentAdventure.currentPoint += (int) WaveTypePoint.MINION;
                    break;
                case WaveType.Rest:
                    expGained = (int) WaveTypeExp.REST;
                    restWaveCount++;
                    currentAdventure.currentPoint += (int) WaveTypePoint.REST;
                    break;
                case WaveType.Elite:
                    expGained = (int) WaveTypeExp.ELITE;
                    eliteWaveCount++;
                    currentAdventure.currentPoint += (int) WaveTypePoint.ELITE;
                    break;
                case WaveType.Boss:
                    expGained = (int) WaveTypeExp.BOSS;
                    bossWaveCount++;
                    currentAdventure.currentPoint += (int) WaveTypePoint.BOSS;
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
            CopyExperienceGained();
        }
    }
    void CopyExperienceGained() {
        for (int i = 0; i < 10; i++) {
            if (playerSide.formation[i] != null) {
                player.inventory[player.formation[i]].SetLevel(playerSide.formation[i].level);
                player.inventory[player.formation[i]].exp = playerSide.formation[i].exp;
            }
        }
        SaveAndLoad.data.SaveInventory();
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
            int random = Random.Range(0, 100);
            if (random >= 90) {
                waveType = WaveType.Elite;
            } else if (random >= 75) {
                waveType = WaveType.Rest;
            } else {
                waveType = WaveType.Minion;
            }
        }
        waveDisplay.text = "Wave\n" + currentWave + "/" + currentAdventure.waveCount + "\n" + waveType.ToString();
        string transitionText = string.Format("Wave {0} - {1}", currentWave, waveType.ToString());
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
                RestEffect(ref transitionText);
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
        DisplayBanner(advanceBanner, transitionText);
    }
    void RestEffect(ref string transitionText) {
        int random = Random.Range(0, 6);
        switch(random) {
            case 0:
                int coinGained = Random.Range(1, 11) * 50;
                ConsumableDatabase.consumables["Misc"][0].quantity += coinGained;
                transitionText += "\nYour formation found " + coinGained + " coins";
                break;
            case 1:
                transitionText += "\nYour formation was afflicted with random status effects";
                for (int i = 0; i < playerSide.activeFormation.Length; i++) {
                    if (playerSide.activeFormation[i] != null && playerSide.activeFormation[i].stat.currentHealth > 0) {
                        Unit unit = playerSide.activeFormation[i];
                        unit.SetStatus("burn, poison, freeze, stun", skillEffect.GetUnitTransform(UnitType.PLAYER, i), unit.stat.health / 5);
                    }
                }
                break;
            case 2:
                transitionText += "\nYour formation regained their health";
                for (int i = 0; i < playerSide.activeFormation.Length; i++) {
                    if (playerSide.activeFormation[i] != null && playerSide.activeFormation[i].stat.currentHealth > 0) {
                        Unit unit = playerSide.activeFormation[i];
                        unit.Heal(skillEffect.GetUnitTransform(UnitType.PLAYER, i), null, new Skill("None", SkillType.HEAL), unit.stat.health);
                    }
                }
                break;
            case 3:
                transitionText += "\nYour formation became stronger";
                for (int i = 0; i < playerSide.activeFormation.Length; i++) {
                    if (playerSide.activeFormation[i] != null && playerSide.activeFormation[i].stat.currentHealth > 0) {
                        Unit unit = playerSide.activeFormation[i];
                        unit.Boost(skillEffect.GetUnitTransform(UnitType.PLAYER, i), null, new Skill("None", SkillType.BOOST, status: "HLT, STR, MAG, DEF, AGI"), unit.stat.currentMagic / 4);
                    }
                }
                break;
            case 4:
                transitionText += "\nYour formation became weaker";
                for (int i = 0; i < playerSide.activeFormation.Length; i++) {
                    if (playerSide.activeFormation[i] != null && playerSide.activeFormation[i].stat.currentHealth > 0) {
                        Unit unit = playerSide.activeFormation[i];
                        unit.Debuff(skillEffect.GetUnitTransform(UnitType.PLAYER, i), null, new Skill("None", SkillType.BOOST, status: "HLT, STR, MAG, DEF, AGI"), unit.stat.currentMagic / 4);
                    }
                }
                break;
            case 5:
                transitionText += "\nYour formation was blessed with protection";
                for (int i = 0; i < playerSide.activeFormation.Length; i++) {
                    if (playerSide.activeFormation[i] != null && playerSide.activeFormation[i].stat.currentHealth > 0) {
                        Unit unit = playerSide.activeFormation[i];
                        unit.EnableProtection(skillEffect.GetUnitTransform(UnitType.PLAYER, i));
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
                        actingUnit.unit = side.activeFormation[i];
                        actingUnit.index = i;
                        actingUnit.transform = transform.GetChild(side.transformIndex).GetChild(i);
                        actingUnit.unitType = side.currentSide;
                    }
                    side.activeFormation[i].stat.currentAgility += side.activeFormation[i].stat.agility + side.activeFormation[i].stat.agilityModifier;
                    side.alive = true;
                }
            }
        }
    }
    void RemoveStatusEffectAfterActing() {
        // Remove status effect and trigger burn effect
        Debug.Log(actingUnit.unit.stat.ToString());

        int healthModifierDifference = actingUnit.unit.stat.healthModifier - actingUnit.unit.stat.healthModifier / 3;

        actingUnit.unit.stat.health -= healthModifierDifference;
        if (actingUnit.unit.stat.currentHealth > actingUnit.unit.stat.health) {
            actingUnit.unit.stat.currentHealth = actingUnit.unit.stat.health;
        } else if (healthModifierDifference < 0) {
            actingUnit.unit.stat.currentHealth -= healthModifierDifference;
        }
        actingUnit.unit.stat.currentStrength = actingUnit.unit.stat.strength + actingUnit.unit.stat.strengthModifier;
        actingUnit.unit.stat.currentMagic = actingUnit.unit.stat.magic + actingUnit.unit.stat.magicModifier;
        actingUnit.unit.stat.currentDefense = actingUnit.unit.stat.defense + actingUnit.unit.stat.defenseModifier;
        actingUnit.unit.stat.currentAgility = actingUnit.unit.stat.agility + actingUnit.unit.stat.agilityModifier;

        actingUnit.unit.stat.healthModifier /= 3;
        actingUnit.unit.stat.strengthModifier /= 3;
        actingUnit.unit.stat.magicModifier /= 3;
        actingUnit.unit.stat.defenseModifier /= 3;
        actingUnit.unit.stat.agilityModifier /= 3;

        if (actingUnit.unit.stat.StatusContains("burn")) {
            actingUnit.unit.TakeBurnDamage(actingUnit.transform);
        }
        actingUnit.unit.ClearStatus(actingUnit.transform);
    }

}

public enum WaveType {
    Minion, Rest, Elite, Boss
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
public class ActingUnit {
    public Unit unit;
    public UnitType unitType;
    public int index;
    public Transform transform;
    public Skill currentSkill;
}