using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[15];
    public static Unit[] units = new Unit[15];

    void Awake()
    {
        units[0] = new Unit("Rock Lizard", Rarity.COMMON, Element.EARTH, new Unit.Stat(1000,1000,1000,1000,1000), sprites[0]);
        units[0].setSkill(2, new Skill("Rock Smash", SkillType.ATTACK, StatType.STR, 210));
        units[1] = new Unit("Defender", Rarity.COMMON, Element.EARTH, new Unit.Stat(1000,1000,1000,1000,1000), sprites[1]);
        units[2] = new Unit("Thief", Rarity.COMMON, Element.EARTH, new Unit.Stat(1000,1000,1000,1000,1000), sprites[2]);
        units[3] = new Unit("Cotton", Rarity.COMMON, Element.WIND, new Unit.Stat(1000,1000,1000,1000,1000), sprites[3]);
        units[4] = new Unit("Wing", Rarity.COMMON, Element.WIND, new Unit.Stat(1000,1000,1000,1000,1000), sprites[4]);
        units[5] = new Unit("Archer", Rarity.COMMON, Element.WIND, new Unit.Stat(1000,1000,1000,1000,1000), sprites[5]);
        units[6] = new Unit("Axeman", Rarity.COMMON, Element.FIRE, new Unit.Stat(1000,1000,1000,1000,1000), sprites[6]);
        units[7] = new Unit("Anubis", Rarity.COMMON, Element.FIRE, new Unit.Stat(1000,1000,1000,1000,1000), sprites[7]);
        units[8] = new Unit("Undead", Rarity.COMMON, Element.FIRE, new Unit.Stat(1000,1000,1000,1000,1000), sprites[8]);
        units[9] = new Unit("Water Knight", Rarity.COMMON, Element.WATER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[9]);
        units[10] = new Unit("Blue Imp", Rarity.COMMON, Element.WATER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[10]);
        units[11] = new Unit("Goblin Raider", Rarity.COMMON, Element.WATER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[11]);
        units[12] = new Unit("Lancer", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[12]);
        units[13] = new Unit("Thunder Moth", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[13]);
        units[14] = new Unit("Witch", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1000,1000,1000,1000,1000), sprites[14]);

        units[4].setSkill(1, new Skill("Rock Smash", SkillType.AOE_ATTACK, StatType.STR, 10, 5));
    }

}
