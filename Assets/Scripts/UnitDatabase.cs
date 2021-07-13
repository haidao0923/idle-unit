using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[15];
    public static Unit[] units = new Unit[15];

    void Awake()
    {
        units[0] = new Unit(0, "Rock Lizard", Rarity.COMMON, Element.EARTH, new Unit.Stat(1000,1000,1000,1000,1000), sprites[0]);
        units[0].setSkill(2, new Skill("Rock Smash", SkillType.ATTACK, StatType.STR, 210));
        units[1] = new Unit(1, "Defender", Rarity.COMMON, Element.EARTH, new Unit.Stat(1000,1000,1000,1000,1000), sprites[1]);
        units[2] = new Unit(2, "Thief", Rarity.COMMON, Element.EARTH, new Unit.Stat(1000,1000,1000,1000,1000), sprites[2]);
        units[3] = new Unit(3, "Cotton", Rarity.COMMON, Element.WIND, new Unit.Stat(1000,1000,1000,1000,1000), sprites[3]);
        units[4] = new Unit(4, "Wing", Rarity.COMMON, Element.WIND, new Unit.Stat(1000,1000,1000,1000,1000), sprites[4]);
        units[5] = new Unit(5, "Archer", Rarity.COMMON, Element.WIND, new Unit.Stat(1000,1000,1000,1000,1000), sprites[5]);
        units[6] = new Unit(6, "Axeman", Rarity.COMMON, Element.FIRE, new Unit.Stat(1000,1000,1000,1000,1000), sprites[6]);
        units[7] = new Unit(7, "Anubis", Rarity.COMMON, Element.FIRE, new Unit.Stat(1000,1000,1000,1000,1000), sprites[7]);
        units[8] = new Unit(8, "Undead", Rarity.COMMON, Element.FIRE, new Unit.Stat(1000,1000,1000,1000,1000), sprites[8]);
        units[9] = new Unit(9, "Water Knight", Rarity.COMMON, Element.WATER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[9]);
        units[10] = new Unit(10, "Blue Imp", Rarity.COMMON, Element.WATER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[10]);
        units[11] = new Unit(11, "Goblin Raider", Rarity.COMMON, Element.WATER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[11]);
        units[12] = new Unit(12, "Lancer", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[12]);
        units[13] = new Unit(13, "Thunder Moth", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[13]);
        units[14] = new Unit(14, "Witch", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[14]);

        units[0].setSkill(2, new Skill("Attack Alot", SkillType.ATTACK, StatType.STR, skillPower: 40, 6, status: "poison, freeze"));
        units[7].setSkill(2, new Skill("Attack Adjacent", SkillType.ATTACK_ADJACENT, StatType.STR, 50, 4, "burn, poison"));
        units[9].setSkill(2, new Skill("Protect", SkillType.AOE_PROTECTION, extraEffect: 1));
        units[10].setSkill(2, new Skill("AOE Dodge", SkillType.AOE_DODGE, skillPower: 10));
    }

    public static Unit GetUnitById(int id) {
        return new Unit(units[id]);
    }

}
