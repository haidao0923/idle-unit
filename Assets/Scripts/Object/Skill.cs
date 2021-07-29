
public class Skill
{
    public string name;
    public string description;
    public SkillType skillType;
    public StatType statType;
    public int skillPower;
    public int extraEffect;
    public string status;
    public TargetType targetType;

    public Skill(string name, SkillType skillType, StatType statType = StatType.STR, int skillPower = 100, int extraEffect = 1, string status = "") {
        this.name = name;
        this.skillType = skillType;
        this.statType = statType;
        this.skillPower = skillPower;
        this.extraEffect = extraEffect;
        this.status = status;
        switch(skillType) {
            case SkillType.ATTACK:
                targetType = TargetType.ENEMY;
                if (extraEffect == 1) {
                    description = string.Format("Deal damage to 1 enemy\n({0} pow: {1}", statType.ToString(), skillPower);
                } else {
                    description = string.Format("Deal damage to {2} enemies\n({0} pow: {1}", statType.ToString(), skillPower, extraEffect);
                }
                if (status != "") {
                    description += ", " + status + ")";
                } else {
                    description += ")";
                }
                break;
            case SkillType.ATTACK_ADJACENT:
                targetType = TargetType.ENEMY;
                if (extraEffect == 1) {
                    description = string.Format("Deal damage to 1 enemy and any adjacent\n({0} pow: {1}", statType.ToString(), skillPower);
                } else {
                    description = string.Format("Deal damage to {2} enemies and any adjacent\n({0} pow: {1}", statType.ToString(), skillPower, extraEffect);
                }
                if (status != "") {
                    description += ", " + status + ")";
                } else {
                    description += ")";
                }
                break;
            case SkillType.AOE_ATTACK:
                targetType = TargetType.ENEMY;
                if (extraEffect == 1) {
                    description = string.Format("Deal damage to all enemies\n({0} pow: {1}", statType.ToString(), skillPower);
                } else {
                    description = string.Format("Deal damage to all enemies x{2}\n({0} pow: {1}", statType.ToString(), skillPower, extraEffect);
                }
                if (status != "") {
                    description += ", " + status + ")";
                } else {
                    description += ")";
                }
                break;
            case SkillType.BOOST:
                if (extraEffect == 1) {
                    targetType = TargetType.SELF;
                    description = string.Format("Boost {2} Self\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    targetType = TargetType.ALLY;
                    description = string.Format("Boost {3} {2} allies\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.AOE_BOOST:
                targetType = TargetType.ALLY;
                if (extraEffect == 1) {
                    description = string.Format("Boost {2} all allies\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    description = string.Format("Boost {3} all allies x{2}\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.DEBUFF:
                targetType = TargetType.ENEMY;
                if (extraEffect == 1) {
                    description = string.Format("Lower {2} 1 enemy\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    description = string.Format("Lower {3} {2} enemies\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.AOE_DEBUFF:
                targetType = TargetType.ENEMY;
                if (extraEffect == 1) {
                    description = string.Format("Lower {2} all enemies\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    description = string.Format("Lower {3} all enemies x{2}\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.DODGE:
                targetType = TargetType.SELF;
                description = string.Format("Enable dodge ({0}%)\n", skillPower);
                break;
            case SkillType.AOE_DODGE:
                targetType = TargetType.ALLY;
                description = string.Format("Enable dodge all allies ({0}%)\n", skillPower);
                break;
            case SkillType.HEAL:
                if (extraEffect == 1) {
                    targetType = TargetType.SELF;
                    description = string.Format("Heal Self\n({0} pow: {1}", statType.ToString(), skillPower);
                } else {
                    targetType = TargetType.ALLY;
                    description = string.Format("Heal {2} allies\n({0} pow: {1}", statType.ToString(), skillPower, extraEffect);
                }
                if (status != "") {
                    description += ", " + status + ")";
                } else {
                    description += ")";
                }
                break;
            case SkillType.AOE_HEAL:
                targetType = TargetType.ALLY;
                if (extraEffect == 1) {
                    description = string.Format("Heal all allies\n({0} pow: {1}", statType.ToString(), skillPower);
                } else {
                    description = string.Format("Heal all allies x{2}\n({0} pow: {1}", statType.ToString(), skillPower, extraEffect);
                }
                if (status != "") {
                    description += ", " + status + ")";
                } else {
                    description += ")";
                }
                break;
            case SkillType.PROTECTION:
                targetType = TargetType.ALLY;
                if (extraEffect == 1) {
                    description = string.Format("Grant protection to 1 ally\n");
                } else {
                    description = string.Format("Grant protection to {0} allies\n", extraEffect);
                }
                break;
            case SkillType.AOE_PROTECTION:
                targetType = TargetType.ALLY;
                if (extraEffect == 1) {
                    description = string.Format("Grant protection to all allies\n");
                } else {
                    description = string.Format("Grant protection to all allies x{0}\n", extraEffect);
                }
                break;
        }
    }
}

public enum SkillType {
    ATTACK,
    ATTACK_ADJACENT,
    AOE_ATTACK,
    BOOST,
    AOE_BOOST,
    DEBUFF,
    AOE_DEBUFF,
    DODGE,
    AOE_DODGE,
    HEAL,
    AOE_HEAL,
    PROTECTION,
    AOE_PROTECTION,
}

public enum StatType {
    HLT,
    STR,
    MAG,
    DEF,
    AGI
}

public enum TargetType {
    ALLY, ENEMY, SELF
}