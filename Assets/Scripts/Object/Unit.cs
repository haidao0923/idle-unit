using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unit
{
    public int id;
    public string name;
    public Rarity rarity;
    public Element element;
    public Stat stat;
    public Sprite sprite;
    public int level, levelCap, capsAbsorbed;
    public int exp, maxExp;
    const int EXP_SCALE = 20; // exp to next level = level * EXP_SCALE
    public Skill firstSkill;
    public Skill secondSkill;
    public Skill thirdSkill;

    public Unit(int id, string name, Rarity rarity, Element element, Stat stat, Sprite sprite, int level = 1) {
        this.id = id;
        this.name = name;
        this.rarity = rarity;
        this.element = element;
        this.stat = new Stat(stat);
        this.sprite = sprite;
        SetLevel(level);
        this.ResetCurrentStat();
        this.setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 100, 1));
    }
    public Unit(Unit other) : this(other.id, other.name, other.rarity, other.element, other.stat, other.sprite, other.level) {
        firstSkill = other.firstSkill;
        secondSkill = other.secondSkill;
        thirdSkill = other.thirdSkill;
    }
    public Unit(Unit other, int level) : this(other) {
        SetLevel(level);
        ResetCurrentStat();
    }

    public void setSkill(int order, Skill skill) {
        switch (order) {
            case 1:
                firstSkill = skill;
                break;
            case 2:
                secondSkill = skill;
                break;
            case 3:
                thirdSkill = skill;
                break;
        }
    }

    public Skill getSkill() {
        if (stat.StatusContains("silence")) {
            return firstSkill;
        }
        int random = Random.Range(0,100);
        if (random >= 85 && thirdSkill != null) {
            return thirdSkill;
        } else if (random >= 60 && random <= 90 && secondSkill != null) {
            return secondSkill;
        } else {
            return firstSkill;
        }
    }

    public static string GetRarityAcronym(Rarity rarity) {
        switch (rarity) {
            case Rarity.COMMON:
                return "C";
            case Rarity.RARE:
                return "R";
            case Rarity.EPIC:
                return "E";
            case Rarity.LEGENDARY:
                return "L";
            default:
                return "";
        }
    }
    public static Color GetRarityColor(Rarity rarity) {
        switch (rarity) {
            case Rarity.COMMON:
                return new Color32(128,128,128,255);
            case Rarity.RARE:
                return new Color32(13,115,13,255);
            case Rarity.EPIC:
                return new Color32(90,14,115,255);
            case Rarity.LEGENDARY:
                return new Color32(243,143,0,255);
            default:
                return new Color32(0,0,0,255);
        }
    }
    public Sprite GetElementSprite() {
        AuraSprite auraSprite = GameObject.FindGameObjectWithTag("GameController").GetComponent<AuraSprite>();
        switch (element) {
            case Element.FIRE:
                return auraSprite.fireAura;
            case Element.WIND:
                return auraSprite.windAura;
            case Element.EARTH:
                return auraSprite.earthAura;
            case Element.THUNDER:
                return auraSprite.thunderAura;
            case Element.WATER:
                return auraSprite.waterAura;
            case Element.VOID:
                return auraSprite.voidAura;
            default:
                return null;
        }
    }
    public bool HasElementalAdvantage(Unit other) {
        switch (element) {
            case Element.FIRE:
                if (other.element == Element.WIND) {
                    return true;
                }
                break;
            case Element.WIND:
                if (other.element == Element.EARTH) {
                    return true;
                }
                break;
            case Element.EARTH:
                if (other.element == Element.THUNDER) {
                    return true;
                }
                break;
            case Element.THUNDER:
                if (other.element == Element.WATER) {
                    return true;
                }
                break;
            case Element.WATER:
                if (other.element == Element.FIRE) {
                    return true;
                }
                break;
        }
        return false;
    }
    public void SetLevel(int amount, Transform transform = null) {
        level = amount;
        levelCap = (int) rarity * (capsAbsorbed + 1);
        AdjustCurrentStat();
        if (transform != null) {
            ResetCurrentStat();
            ClearStatus(transform);
            transform.Find("Health Bar").GetComponent<Slider>().value = (float) this.stat.currentHealth / this.stat.health;
        }
        if (level >= levelCap) {
            maxExp = 0;
        } else {
            maxExp = level * EXP_SCALE;
        }
    }
    public bool GainExp(int amount, Transform transform = null) {
        exp += amount;
        levelCap = (int) rarity * (capsAbsorbed + 1);
        maxExp = level * EXP_SCALE;
        bool gainedLevel = false;
        if (exp >= maxExp && level < levelCap) {
            int currentTotalExp = (EXP_SCALE + (level - 1) * EXP_SCALE) / 2 * (level - 1) + exp;
            int tempLevel = Mathf.FloorToInt(-(-Mathf.Sqrt(8 * currentTotalExp * EXP_SCALE + EXP_SCALE * EXP_SCALE) + EXP_SCALE) / (2 * EXP_SCALE));
            if (tempLevel > levelCap - 1) {
                tempLevel = levelCap - 1;
            }
            int tempTotalExp = (EXP_SCALE + tempLevel * EXP_SCALE) / 2 * tempLevel;
            if (tempLevel + 1 > levelCap) {
                SetLevel(levelCap, transform);
            } else {
                SetLevel(tempLevel + 1, transform);

            }
            exp = currentTotalExp - tempTotalExp;
            gainedLevel = true;
        }
        return gainedLevel;
    }
    public void AdjustCurrentStat() {
        stat.health = (int) (stat.baseHealth * (1 + level / 10f));
        stat.strength = (int) (stat.baseStrength * (1 + level / 10f));
        stat.magic = (int) (stat.baseMagic * (1 + level / 10f));
        stat.defense = (int) (stat.baseDefense * (1 + level / 10f));
        stat.agility = (int) (stat.baseAgility * (1 + level / 10f));
    }
    public void ResetCurrentStat() {
        stat.currentHealth = stat.health;
        stat.currentStrength = stat.strength;
        stat.currentMagic = stat.magic;
        stat.currentDefense = stat.defense;
        stat.currentAgility = stat.agility;

        stat.healthModifier = 0;
        stat.strengthModifier = 0;
        stat.magicModifier = 0;
        stat.defenseModifier = 0;
        stat.agilityModifier = 0;
    }
    public void ResetAgility() {
        if (!stat.StatusContains("freeze")) {
            stat.currentAgility = stat.agility;
        } else {
            stat.currentAgility = 0;
        }
    }
    public void Heal(Transform defendingUnitTransform, Unit attackingUnit, Skill skill, int amount = 0) {
        int healAmount = amount;
        if (healAmount == 0) {
            healAmount = GetEffectiveDamage(defendingUnitTransform, attackingUnit, skill);
        }
        TakeDamage(defendingUnitTransform, damage: -healAmount);
        SetStatus(skill.status, defendingUnitTransform, healAmount);
        defendingUnitTransform.Find("Aura").GetComponent<Image>().color = new Color32(0, 255, 12, 255);
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().text = "Heal\n" + healAmount.ToString();
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().color = new Color32(0, 255, 12, 255);
        defendingUnitTransform.GetComponent<Animator>().SetFloat("boostSpeed", skill.extraEffect);
        defendingUnitTransform.GetComponent<Animator>().Play("Boost");
    }
    public void TakeDamage(Transform defendingUnitTransform, Unit attackingUnit = null, Skill skill = null, int damage = 0) {
        int damageTaken = damage;
        if (attackingUnit != null) {
            damageTaken = GetEffectiveDamage(defendingUnitTransform, attackingUnit, skill);
            SetStatus(skill.status, defendingUnitTransform, damageTaken);
            if (attackingUnit.HasElementalAdvantage(this)) {
                SetDamageText(defendingUnitTransform, damageTaken.ToString(), new Color32(236, 24, 0, 255));
            } else if (this.HasElementalAdvantage(attackingUnit)) {
                SetDamageText(defendingUnitTransform, damageTaken.ToString(), new Color32(180, 180, 180, 255));
            } else {
                SetDamageText(defendingUnitTransform, damageTaken.ToString(), new Color32(236, 211, 0, 255));
            }
        }
        if (damageTaken != 0) {
            this.stat.currentHealth -= damageTaken;
            if (stat.currentHealth > stat.health) {
                stat.currentHealth = stat.health;
            }
            defendingUnitTransform.Find("Health Bar").GetComponent<Slider>().value = (float) this.stat.currentHealth / this.stat.health;
            if (this.stat.currentHealth <= 0) {
                defendingUnitTransform.GetComponent<Animator>().SetBool("isDead", true);
            }
        }
    }
    int GetEffectiveDamage(Transform defendingUnitTransform, Unit attackingUnit, Skill skill) {
        float damage = 0;
        switch (skill.statType) {
            case StatType.HLT:
                damage = attackingUnit.stat.currentHealth * skill.skillPower / 100f;
                break;
            case StatType.STR:
                damage = attackingUnit.stat.currentStrength * skill.skillPower / 100f;
                break;
            case StatType.MAG:
                damage = attackingUnit.stat.currentMagic * skill.skillPower / 100f;
                break;
            case StatType.DEF:
                damage = attackingUnit.stat.currentDefense * skill.skillPower / 100f;
                break;
            case StatType.AGI:
                damage = attackingUnit.stat.currentAgility * skill.skillPower / 100f;
                break;
        }
        if (attackingUnit.HasElementalAdvantage(this)) {
            damage *= 1.5f;
        } else if (this.HasElementalAdvantage(attackingUnit)) {
            damage *= 0.5f;
        }
        if (skill.statType == StatType.MAG) {
            damage *= 1 - ((this.stat.currentDefense - attackingUnit.stat.currentMagic / 10f) / (1000f + (this.stat.currentDefense - attackingUnit.stat.currentMagic / 10f)));
        } else {
            damage *= 1 - (this.stat.currentDefense / (1000f + this.stat.currentDefense));
        }
        damage += Random.Range(-100, 101);
        if (damage <= 0) {
            damage = Random.Range(1, 51);
        }
        if (stat.isVulnerable) {
            damage *= 2;
            stat.isVulnerable = false;
            defendingUnitTransform.Find("Image").GetComponent<Image>().color = new Color32(255,255,255,255);
        }
        if (attackingUnit.stat.StatusContains("weaken")) {
            damage *= .75f;
        }
        return (int) damage;
    }
    void SetDamageText(Transform unitTransform, string text, Color32 color) {
        Text tempText = unitTransform.Find("Damage Text").GetComponent<Text>();
        tempText.text = text;
        tempText.color = color;
    }
    public void SetStatus(string status, Transform unitTransform, int damage = 0) {
        if (status == "") {
            return;
        }
        if (status.ToLower().Contains("burn") && !this.stat.StatusContains("burn") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(255,122,0,255);
            this.stat.status += "burn, ";
            this.stat.statusDamage += damage;
        }
        if (status.ToLower().Contains("poison") && !this.stat.StatusContains("poison") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(29,186,58,255);
            this.stat.status += "poison, ";
            this.stat.statusDamage += damage;
        }
        if (status.ToLower().Contains("freeze") && !this.stat.StatusContains("freeze") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(0,190,255,255);
            this.stat.status += "freeze, ";
            this.stat.currentAgility = 0;
        }
        if (status.ToLower().Contains("stun") && !this.stat.StatusContains("stun") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(255,255,0,255);
            this.stat.status += "stun, ";
            this.stat.currentAgility /= 3;
            this.stat.agilityModifier -= (int)(this.stat.agility * 2/3f);
        }
        if (status.ToLower().Contains("vulnerable") && !this.stat.StatusContains("vulnerable") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(255,0,0,255);
            this.stat.status += "vulnerable, ";
            this.stat.isVulnerable = true;
        }
        if (status.ToLower().Contains("weaken") && !this.stat.StatusContains("weaken") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(155,0,255,255);
            this.stat.status += "weaken, ";
            this.stat.isWeaken = true;
        }
        if (status.ToLower().Contains("charm") && !this.stat.StatusContains("charm") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(255,117,234,255);
            this.stat.status += "charm, ";
        }
        if (status.ToLower().Contains("silence") && !this.stat.StatusContains("silence") && (Random.Range(0, 5) == 0)) {
            unitTransform.Find("Image").GetComponent<Image>().color = new Color32(104,104,104,255);
            this.stat.status += "silence, ";
        }
        if (status.ToLower().Contains("purify") && !this.stat.StatusContains("purify") && (Random.Range(0, 4) > 0)) {
            if (this.stat.StatusContains("freeze")) {
                this.stat.currentAgility += this.stat.agility;
            }
            if (this.stat.StatusContains("stun")) {
                this.stat.currentAgility *= 3;
            }
            this.stat.isVulnerable = false;
            this.stat.isWeaken = false;
            ResetCurrentStat();
            ClearStatus(unitTransform);
        }
    }
    public void BoostStats(Transform defendingUnitTransform, int amount, string status) {
        if (status == "") {
            return;
        }
        if (status.ToLower().Contains("hlt")) {
            int tempAmount = amount;
            this.stat.health += amount;
            this.stat.healthModifier += amount;
            if (this.stat.currentHealth > this.stat.health) {
                tempAmount = this.stat.health - this.stat.currentHealth;
            }
            TakeDamage(defendingUnitTransform, damage: -tempAmount);
        }
        if (status.ToLower().Contains("str")) {
            this.stat.currentStrength += amount;
            this.stat.strengthModifier += amount;
        }
        if (status.ToLower().Contains("mag")) {
            this.stat.currentMagic += amount;
            this.stat.magicModifier += amount;
        }
        if (status.ToLower().Contains("def")) {
            this.stat.currentDefense += amount;
            this.stat.defenseModifier += amount;
        }
        if (status.ToLower().Contains("agi")) {
            this.stat.currentAgility += amount;
            this.stat.agilityModifier += amount;
        }
    }
    public void ClearStatus(Transform unitTransform) {
        this.stat.status = "";
        this.stat.statusDamage = 0;
        unitTransform.Find("Image").GetComponent<Image>().color = new Color32(255,255,255,255);
    }
    public void TakeBurnDamage(Transform defendingUnitTransform) {
        int tempDamage = this.stat.statusDamage / 2;
        TakeDamage(defendingUnitTransform, damage: tempDamage);
        SetDamageText(defendingUnitTransform, tempDamage.ToString(), new Color32(236, 211, 0, 255));
        defendingUnitTransform.GetComponent<Animator>().Play("Burn");
    }
    public void TakePoisonDamage(Transform defendingUnitTransform) {
        int tempDamage = this.stat.statusDamage / 10;
        TakeDamage(defendingUnitTransform, damage: tempDamage);
        SetDamageText(defendingUnitTransform, tempDamage.ToString(), new Color32(236, 211, 0, 255));
        defendingUnitTransform.GetComponent<Animator>().Play("Poison");
    }
    public void EnableDodge(Transform defendingUnitTransform, Skill skill) {
        if (stat.dodgePercent < skill.skillPower) {
            stat.dodgePercent = skill.skillPower;
        }
        defendingUnitTransform.Find("Aura").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().text = "Enable Dodge";
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        defendingUnitTransform.GetComponent<Animator>().Play("Boost");
    }
    public void EnableProtection(Transform defendingUnitTransform) {
        stat.protectionCount += 1;
        defendingUnitTransform.Find("Protection/Text").GetComponent<Text>().text = stat.protectionCount.ToString();
        defendingUnitTransform.Find("Protection").gameObject.SetActive(true);
        defendingUnitTransform.Find("Protection").GetComponent<Animation>().Play();
    }
    public void DodgeDamage(Transform defendingUnitTransform, int animationSpeed) {
        int tempDamage = 0;
        TakeDamage(defendingUnitTransform, damage: tempDamage);
        SetDamageText(defendingUnitTransform, tempDamage.ToString(), new Color32(236, 211, 0, 255));
        defendingUnitTransform.GetComponent<Animator>().SetFloat("dodgeSpeed", animationSpeed);
        defendingUnitTransform.GetComponent<Animator>().Play("Dodge");
    }
    public void TriggerProtection(Transform defendingUnitTransform) {
        int tempDamage = 0;
        TakeDamage(defendingUnitTransform, damage: tempDamage);
        SetDamageText(defendingUnitTransform, tempDamage.ToString(), new Color32(236, 211, 0, 255));
        stat.protectionCount -= 1;
        defendingUnitTransform.Find("Protection/Text").GetComponent<Text>().text = stat.protectionCount.ToString();
        if (stat.protectionCount <= 0) {
            defendingUnitTransform.Find("Protection").gameObject.SetActive(false);
        }
    }
    public void Boost(Transform defendingUnitTransform, Unit attackingUnit, Skill skill, int amount = 0) {
        int boostAmount = amount;
        if (boostAmount == 0) {
            boostAmount = GetEffectiveDamage(defendingUnitTransform, attackingUnit, skill);
        }
        BoostStats(defendingUnitTransform, boostAmount, skill.status);
        defendingUnitTransform.Find("Aura").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().text = skill.status + "\nUp";
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        defendingUnitTransform.GetComponent<Animator>().SetFloat("boostSpeed", skill.extraEffect);
        defendingUnitTransform.GetComponent<Animator>().Play("Boost");
    }
    public void Debuff(Transform defendingUnitTransform, Unit attackingUnit, Skill skill, int amount = 0) {
        int boostAmount = amount;
        if (boostAmount == 0) {
            boostAmount = GetEffectiveDamage(defendingUnitTransform, attackingUnit, skill);
        }
        BoostStats(defendingUnitTransform, -boostAmount, skill.status);
        defendingUnitTransform.Find("Aura").GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().text = skill.status + "\nDown";
        defendingUnitTransform.Find("Aura/Text").GetComponent<Text>().color = new Color32(255, 0, 0, 255);
        defendingUnitTransform.GetComponent<Animator>().SetFloat("boostSpeed", skill.extraEffect);
        defendingUnitTransform.GetComponent<Animator>().Play("Boost");
    }

    public class Stat {
        public int baseHealth;
        public int baseStrength;
        public int baseMagic;
        public int baseDefense;
        public int baseAgility;


        public int health;
        public int strength;
        public int magic;
        public int defense;
        public int agility;

        public int currentHealth;
        public int currentStrength;
        public int currentMagic;
        public int currentDefense;
        public int currentAgility;

        public int healthModifier;
        public int strengthModifier;
        public int magicModifier;
        public int defenseModifier;
        public int agilityModifier;

        public string status;
        public int statusDamage;
        public int protectionCount, dodgePercent;
        public bool isWeaken, isVulnerable;

        public Stat(int baseHealth, int baseStrength, int baseMagic, int baseDefense, int baseAgility) {
            this.baseHealth = baseHealth;
            this.baseStrength = baseStrength;
            this.baseMagic = baseMagic;
            this.baseDefense = baseDefense;
            this.baseAgility = baseAgility;
        }

        public Stat(Stat stat) : this(stat.baseHealth, stat.baseStrength, stat.baseMagic, stat.baseDefense, stat.baseAgility) {}

        public bool StatusContains(string text) {
            if (status == null) {
                return false;
            } else if (status.Contains(text)) {
                return true;
            }
            return false;
        }
        override public string ToString() {
            return string.Format("Health: {0}; Strength: {1}; Magic: {2}; Defense: {3}; Agility: {4}\nHealthC: {5}; StrengthC: {6}; MagicC: {7}; DefenseC: {8}; AgilityC: {9}\nHealthM: {10}; StrengthM: {11}; MagicM: {12}; DefenseM: {13}; AgilityM: {14}",
                health, strength, magic, defense, agility, currentHealth, currentStrength, currentMagic, currentDefense, currentAgility, healthModifier, strengthModifier, magicModifier, defenseModifier, agilityModifier);
        }
    }
}

public enum Rarity {
    COMMON = 5, RARE = 10, EPIC = 25, LEGENDARY = 50
}
public enum Element {
    FIRE, WIND, EARTH, THUNDER, WATER, VOID
}

public enum UnitType {
    PLAYER, ENEMY
}