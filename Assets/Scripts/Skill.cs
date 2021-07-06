
public class Skill
{
    public string name;
    public string description;
    public SkillType skillType;
    public StatType statType;
    public int skillPower;
    public int extraEffect;
    public string status;

    public Skill(string name, SkillType skillType, StatType statType, int skillPower, int extraEffect = 1, string status = "") {
        this.name = name;
        this.skillType = skillType;
        this.statType = statType;
        this.skillPower = skillPower;
        this.extraEffect = extraEffect;
        this.status = status;
        switch(skillType) {
            case SkillType.ATTACK:
                if (extraEffect == 1) {
                    description = string.Format("Deal damage to 1 enemy\n({0} pow: {1} ", statType.ToString(), skillPower);
                } else {
                    description = string.Format("Deal damage to {2} enemies\n({0} pow: {1} ", statType.ToString(), skillPower, extraEffect);
                }
                description += status + ")";
                break;
            case SkillType.AOE_ATTACK:
                if (extraEffect == 1) {
                    description = string.Format("Deal damage to all enemies\n({0} pow: {1} ", statType.ToString(), skillPower);
                } else {
                    description = string.Format("Deal damage to all enemies x{2}\n({0} pow: {1} ", statType.ToString(), skillPower, extraEffect);
                }
                description += status + ")";
                break;
            case SkillType.BOOST:
                if (extraEffect == 1) {
                    description = string.Format("Boost {2} self\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    description = string.Format("Boost {3} {2} allies\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.AOE_BOOST:
                if (extraEffect == 1) {
                    description = string.Format("Boost {2} all allies\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    description = string.Format("Boost {3} all allies x{2}\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.DEBUFF:
                if (extraEffect == 1) {
                    description = string.Format("Lower {2} 1 enemy\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    description = string.Format("Lower {3} {2} enemies\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.AOE_DEBUFF:
                if (extraEffect == 1) {
                    description = string.Format("Lower {2} all enemies\n({0} pow: {1})", statType.ToString(), skillPower, status);
                } else {
                    description = string.Format("Lower {3} all enemies x{2}\n({0} pow: {1})", statType.ToString(), skillPower, extraEffect, status);
                }
                break;
            case SkillType.DODGE:
                description = string.Format("Enable dodge ({0}%)", skillPower);
                break;
            case SkillType.AOE_DODGE:
                description = string.Format("Enable dodge all allies ({0}%)", skillPower);
                break;
        }
    }
}

public enum SkillType {
    ATTACK,
    AOE_ATTACK,
    BOOST,
    AOE_BOOST,
    DEBUFF,
    AOE_DEBUFF,
    DODGE,
    AOE_DODGE,
}

public enum StatType {
    HLT,
    STR,
    MAG,
    DEF,
    AGI
}

public enum TargetType {
    BUFF, DEBUFF, SELF
}