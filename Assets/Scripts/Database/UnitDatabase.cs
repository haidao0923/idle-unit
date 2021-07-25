using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[15];
    public static Unit[] units = new Unit[15];

    void Awake()
    {
        units[0] = new Unit(0, "Rock Lizard", Rarity.COMMON, Element.EARTH, new Unit.Stat(1159,1083,864,1110,1046), sprites[0]);
        units[0].setSkill(2, new Skill("Rock Smash", SkillType.ATTACK, StatType.STR, 210));

        units[1] = new Unit(1, "Defender", Rarity.COMMON, Element.EARTH, new Unit.Stat(1162,1020,836,1185,967), sprites[1]);
        units[1].setSkill(2, new Skill("Shield", SkillType.BOOST, StatType.STR, 50, status: "DEF"));

        units[2] = new Unit(2, "Thief", Rarity.COMMON, Element.EARTH, new Unit.Stat(985,1120,890,865,1183), sprites[2]);
        units[2].setSkill(2, new Skill("Double Strike", SkillType.ATTACK, StatType.AGI, 100, 2));

        units[3] = new Unit(3, "Cotton", Rarity.COMMON, Element.WIND, new Unit.Stat(872,1105,1120,967,1180), sprites[3]);
        units[3].setSkill(2, new Skill("Cuteness", SkillType.DODGE, skillPower: 15));

        units[4] = new Unit(4, "Wing", Rarity.COMMON, Element.WIND, new Unit.Stat(1100,1139,850,1103,1082), sprites[4]);
        units[4].setSkill(2, new Skill("Breathless Wind", SkillType.DEBUFF, StatType.AGI, 15, 2, "DEF"));

        units[5] = new Unit(5, "Archer", Rarity.COMMON, Element.WIND, new Unit.Stat(982,1084,856,879,1110), sprites[5]);
        units[5].setSkill(2, new Skill("Poison Arrow", SkillType.ATTACK, StatType.AGI, 90, 2, "poison"));

        units[6] = new Unit(6, "Axe Warrior", Rarity.COMMON, Element.FIRE, new Unit.Stat(1113,1164,989,1054,1067), sprites[6]);
        units[6].setSkill(2, new Skill("Double Slash", SkillType.ATTACK, StatType.STR, 95, 2));

        units[7] = new Unit(7, "Anubis", Rarity.COMMON, Element.FIRE, new Unit.Stat(879,962,1185,865,1079), sprites[7]);
        units[7].setSkill(2, new Skill("Fireball", SkillType.ATTACK, StatType.MAG, 170, status: "burn"));

        units[8] = new Unit(8, "Undead", Rarity.COMMON, Element.FIRE, new Unit.Stat(1066,970,1040,1082,982), sprites[8]);
        units[8].setSkill(2, new Skill("Undying", SkillType.HEAL, StatType.HLT, 40));

        units[9] = new Unit(9, "Water Knight", Rarity.COMMON, Element.WATER, new Unit.Stat(1117,1038,1060,1055,1093), sprites[9]);
        units[9].setSkill(2, new Skill("Water Slash", SkillType.ATTACK, StatType.MAG, 160, status: "freeze"));

        units[10] = new Unit(10, "Blue Imp", Rarity.COMMON, Element.WATER, new Unit.Stat(987,1063,1097,1028,1162), sprites[10]);
        units[10].setSkill(2, new Skill("Slip", SkillType.DEBUFF, StatType.MAG, 15, 3, status: "AGI"));

        units[11] = new Unit(11, "Goblin Raider", Rarity.COMMON, Element.WATER, new Unit.Stat(1145,1093,982,999,1163), sprites[11]);
        units[11].setSkill(2, new Skill("Rush", SkillType.BOOST, StatType.AGI, 60, status: "AGI"));

        units[12] = new Unit(12, "Lancer", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1040,1130,1099,1103,1017), sprites[12]);
        units[12].setSkill(2, new Skill("Thunder Lance", SkillType.ATTACK, StatType.STR, 150, status: "stun"));

        units[13] = new Unit(13, "Thunder Moth", Rarity.COMMON, Element.THUNDER, new Unit.Stat(1129,1001,1136,988,1140), sprites[13]);
        units[13].setSkill(2, new Skill("Magic Dust", SkillType.BOOST, StatType.MAG, 25, 3, "MAG"));

        units[14] = new Unit(14, "Witch", Rarity.COMMON, Element.THUNDER, new Unit.Stat(982,892,1188,1002,1067), sprites[14]);
        units[14].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[15] = new Unit(15, "Ana the Assassin", Rarity.RARE, Element.EARTH, new Unit.Stat(1082,1283,1105,1155,1237), sprites[15]);
        units[15].setSkill(1, new Skill("Double Attack", SkillType.ATTACK, StatType.STR, 45, 2));
        units[15].setSkill(2, new Skill("Dagger Toss", SkillType.ATTACK, StatType.STR, 55, 4, "poison"));
        units[15].setSkill(3, new Skill("Rush", SkillType.BOOST, StatType.STR, 85, 1, "AGI"));

        units[16] = new Unit(16, "Black Knight", Rarity.RARE, Element.EARTH, new Unit.Stat(1199,1290,1020,1100,1043), sprites[16]);
        units[16].setSkill(2, new Skill("Heavy Slash", SkillType.ATTACK, StatType.STR, 210, status: "vulnerable"));

        units[17] = new Unit(17, "Earth Crocodile", Rarity.RARE, Element.EARTH, new Unit.Stat(1115,1183,985,1076,1266), sprites[17]);
        units[17].setSkill(2, new Skill("Sweep", SkillType.AOE_ATTACK, StatType.AGI, 55));

        units[18] = new Unit(18, "Haunted Tree", Rarity.RARE, Element.EARTH, new Unit.Stat(1236,1166,1188,1117,1149), sprites[18]);
        units[18].setSkill(2, new Skill("Fear", SkillType.DEBUFF, StatType.MAG, 15, 3, "STR,MAG"));
        units[18].setSkill(3, new Skill("Haunt", SkillType.AOE_DEBUFF, StatType.MAG, 30, status: "AGI"));

        units[19] = new Unit(19, "Mummy", Rarity.RARE, Element.EARTH, new Unit.Stat(1074,1295,1167,1089,1084), sprites[19]);
        units[19].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 110));
        units[19].setSkill(2, new Skill("Rotten Venom", SkillType.ATTACK, StatType.STR, 70, 3, "poison"));
        units[19].setSkill(3, new Skill("Decay", SkillType.DEBUFF, StatType.MAG, 30, 1, "STR"));

        units[20] = new Unit(20, "Orc Captain", Rarity.RARE, Element.EARTH, new Unit.Stat(1194,1233,1052,1104,1143), sprites[20]);
        units[20].setSkill(2, new Skill("Axe Slam", SkillType.ATTACK_ADJACENT, StatType.STR, 110));

        units[21] = new Unit(21, "Rock Golem", Rarity.RARE, Element.EARTH, new Unit.Stat(1233,1160,999,1245,1110), sprites[21]);
        units[21].setSkill(2, new Skill("Shield Bash", SkillType.ATTACK, StatType.DEF, 230));
        units[21].setSkill(3, new Skill("Rock Barrier", SkillType.AOE_BOOST, StatType.MAG, 25, status: "DEF"));

        units[22] = new Unit(22, "Sand Worm", Rarity.RARE, Element.EARTH, new Unit.Stat(1120,1098,1135,1199,1067), sprites[22]);
        units[22].setSkill(2, new Skill("Earthquake", SkillType.AOE_ATTACK, StatType.DEF, 45));
        units[22].setSkill(3, new Skill("Quicksand", SkillType.DEBUFF, StatType.AGI, 20, status: "DEF,AGI"));

        units[23] = new Unit(23, "Green Wyvern", Rarity.RARE, Element.WIND, new Unit.Stat(1211,1088,925,1268,1198), sprites[23]);
        units[23].setSkill(1, new Skill("Guard Strike", SkillType.ATTACK, StatType.DEF, 45, 2));
        units[23].setSkill(2, new Skill("Defend", SkillType.BOOST, StatType.DEF, 55, status: "DEF"));

        units[24] = new Unit(24, "Leaf Warrior", Rarity.RARE, Element.WIND, new Unit.Stat(1200,1298,975,1153,1211), sprites[24]);
        units[24].setSkill(2, new Skill("Heroic Code", SkillType.PROTECTION, extraEffect: 3));

        units[25] = new Unit(25, "Proud Bird", Rarity.RARE, Element.WIND, new Unit.Stat(1244,1186,1111,1211,1087), sprites[25]);
        units[25].setSkill(2, new Skill("Enhance Defense", SkillType.BOOST, StatType.DEF, 60, status: "DEF"));
        units[25].setSkill(3, new Skill("Alliance", SkillType.AOE_BOOST, StatType.AGI, 20, status: "DEF"));

        units[26] = new Unit(26, "Saw Girl", Rarity.RARE, Element.WIND, new Unit.Stat(1100,1222,1257,1111,1099), sprites[26]);
        units[26].setSkill(1, new Skill("Double Slash", SkillType.ATTACK, StatType.STR, 70, 2));
        units[26].setSkill(2, new Skill("Nightfall", SkillType.AOE_DEBUFF, StatType.MAG, 5, status: "HLT,DEF"));

        units[27] = new Unit(27, "Fairy", Rarity.RARE, Element.WIND, new Unit.Stat(1147,1098,1233,987,1214), sprites[27]);
        units[27].setSkill(2, new Skill("Restoration", SkillType.AOE_HEAL, StatType.MAG, 30));

        units[28] = new Unit(28, "Fire Toad", Rarity.RARE, Element.FIRE, new Unit.Stat(1083,1246,1289,1100,996), sprites[28]);
        units[28].setSkill(1, new Skill("Fire Attack", SkillType.ATTACK, StatType.STR, 90, status: "burn"));
        units[28].setSkill(2, new Skill("Fire Spit", SkillType.AOE_ATTACK, StatType.STR, 50, status: "burn"));

        units[29] = new Unit(29, "Flame Deer", Rarity.RARE, Element.FIRE, new Unit.Stat(1098,1217,1188,1113,1211), sprites[29]);
        units[29].setSkill(2, new Skill("Charge", SkillType.ATTACK, StatType.STR, 230));
        units[29].setSkill(3, new Skill("Run", SkillType.BOOST, StatType.STR, 50, status: "DEF,AGI"));

        units[30] = new Unit(30, "Scroll Mage", Rarity.RARE, Element.FIRE, new Unit.Stat(976,1199,1266,943,1211), sprites[30]);
        units[30].setSkill(1, new Skill("Fire Scroll", SkillType.ATTACK, StatType.MAG, 90, status: "burn"));
        units[30].setSkill(2, new Skill("Ice Scroll", SkillType.ATTACK, StatType.MAG, 180, status: "freeze"));
        units[30].setSkill(3, new Skill("Binding Scroll", SkillType.ATTACK, StatType.STR, 230, status: "silence"));

        units[31] = new Unit(31, "Sphinx", Rarity.RARE, Element.FIRE, new Unit.Stat(1282,1237,1082,1288,1078), sprites[31]);
        units[31].setSkill(1, new Skill("Swipe", SkillType.ATTACK, StatType.STR, 50, 2));
        units[31].setSkill(2, new Skill("Protection", SkillType.PROTECTION, extraEffect: 2));
        units[31].setSkill(3, new Skill("Empower", SkillType.BOOST, StatType.STR, 20, 2, "STR,DEF"));

        units[32] = new Unit(32, "Vampire", Rarity.RARE, Element.FIRE, new Unit.Stat(1233,1266,1156,1098,1211), sprites[32]);
        units[32].setSkill(2, new Skill("Weakening", SkillType.ATTACK, StatType.STR, 170, status: "weaken"));
        units[32].setSkill(3, new Skill("Evasion", SkillType.DODGE, skillPower: 25));

        units[33] = new Unit(33, "Ice Turtle", Rarity.RARE, Element.WATER, new Unit.Stat(1278,1122,1088,1245,1168), sprites[33]);
        units[33].setSkill(2, new Skill("Hard Shell", SkillType.BOOST, StatType.STR, 65, status: "DEF"));

        units[34] = new Unit(34, "Lizardmen", Rarity.RARE, Element.WATER, new Unit.Stat(1211,1277,988,1223,1041), sprites[34]);
        units[34].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 110));
        units[34].setSkill(2, new Skill("Double Slash", SkillType.ATTACK, StatType.STR, 80, 2, "vulnerable"));
        units[34].setSkill(3, new Skill("Guardian", SkillType.PROTECTION, extraEffect: 2));

        units[35] = new Unit(35, "Magic Crocodile", Rarity.RARE, Element.WATER, new Unit.Stat(1156,1275,1299,1002,1100), sprites[35]);
        units[35].setSkill(1, new Skill("Bite", SkillType.ATTACK, StatType.STR, 130));
        units[35].setSkill(2, new Skill("Poisonous Bite", SkillType.ATTACK, StatType.STR, 210, status: "poison"));

        units[36] = new Unit(36, "Pirate Captain", Rarity.RARE, Element.WATER, new Unit.Stat(1056,999,1188,982,1214), sprites[36]);
        units[36].setSkill(2, new Skill("Water Gun", SkillType.ATTACK, StatType.MAG, 60, 3, "freeze"));

        units[37] = new Unit(37, "Slimey", Rarity.RARE, Element.WATER, new Unit.Stat(1026,911,1243,1088,1277), sprites[37]);
        units[37].setSkill(1, new Skill("Freeze Attack", SkillType.ATTACK, StatType.AGI, 90, status: "freeze"));
        units[37].setSkill(2, new Skill("Elemental Attack", SkillType.ATTACK, StatType.MAG, 150, status: "burn, poison, freeze, stun"));

        units[38] = new Unit(38, "Electroctopus", Rarity.RARE, Element.THUNDER, new Unit.Stat(975,1211,1233,1168,1079), sprites[38]);
        units[38].setSkill(1, new Skill("Shock Attack", SkillType.ATTACK, StatType.STR, 70, status: "stun"));
        units[38].setSkill(2, new Skill("Paralysis", SkillType.ATTACK, StatType.AGI, 60, 3, "stun"));
        units[38].setSkill(3, new Skill("Regenerate", SkillType.HEAL, StatType.DEF, 40));

        units[39] = new Unit(39, "Iron Maiden", Rarity.RARE, Element.THUNDER, new Unit.Stat(1288,1237,1000,1266,982), sprites[39]);
        units[39].setSkill(1, new Skill("Entrapment", SkillType.ATTACK, StatType.STR, 80, status: "silence"));
        units[39].setSkill(2, new Skill("Protection", SkillType.PROTECTION, extraEffect: 2));

        units[40] = new Unit(40, "Mayor", Rarity.RARE, Element.THUNDER, new Unit.Stat(1173,1233,1088,1211,1144), sprites[40]);
        units[40].setSkill(1, new Skill("Poison Attack", SkillType.ATTACK, StatType.STR, 100, status: "poison"));
        units[40].setSkill(2, new Skill("Affliction", SkillType.ATTACK, StatType.STR, 90, 2, "burn, poison"));

        units[41] = new Unit(41, "Puppet", Rarity.RARE, Element.THUNDER, new Unit.Stat(1087,925,1299,985,1211), sprites[41]);
        units[41].setSkill(1, new Skill("Shadow Strike", SkillType.ATTACK, StatType.MAG, 90, status: "silence"));
        units[41].setSkill(2, new Skill("Penetrating Strike", SkillType.ATTACK, StatType.MAG, 50, status: "purge"));
        units[41].setSkill(3, new Skill("Fluid Motion", SkillType.DODGE, skillPower: 20));

        units[42] = new Unit(42, "Orbital Dragon", Rarity.RARE, Element.THUNDER, new Unit.Stat(1287,1100,1239,1158,1249), sprites[42]);
        units[42].setSkill(1, new Skill("Lightning", SkillType.ATTACK, StatType.MAG, 40, 2, "stun"));
        units[42].setSkill(2, new Skill("Empower", SkillType.BOOST, StatType.STR, 25, 3, "STR"));

        units[43] = new Unit(43, "Void Crystal", Rarity.RARE, Element.VOID, new Unit.Stat(1244,1099,1267,1300,1100), sprites[43]);
        units[43].setSkill(2, new Skill("Defend", SkillType.BOOST, StatType.MAG, 60, status: "DEF"));
        units[43].setSkill(3, new Skill("Shield", SkillType.AOE_BOOST, StatType.MAG, 20, status: "DEF"));

        units[44] = new Unit(44, "Bamboo Master Bob", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[44]);
        units[44].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[45] = new Unit(45, "Robot", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[45]);
        units[45].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[46] = new Unit(46, "Ant Queen", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[46]);
        units[46].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[47] = new Unit(47, "Ant Sentinel", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[47]);
        units[47].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[48] = new Unit(48, "Dryad Warrior", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[48]);
        units[48].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[49] = new Unit(49, "Earth Lion", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[49]);
        units[49].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[50] = new Unit(50, "Hydra", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[50]);
        units[50].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[51] = new Unit(51, "Man-eater", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[51]);
        units[51].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[52] = new Unit(52, "Mineral Bear", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[52]);
        units[52].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[53] = new Unit(53, "Serpent", Rarity.EPIC, Element.EARTH, new Unit.Stat(982,892,1188,1002,1067), sprites[53]);
        units[53].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[54] = new Unit(54, "Flying Hero, Wingeno", Rarity.EPIC, Element.WIND, new Unit.Stat(982,892,1188,1002,1067), sprites[54]);
        units[54].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[55] = new Unit(55, "Book Magician", Rarity.EPIC, Element.WIND, new Unit.Stat(982,892,1188,1002,1067), sprites[55]);
        units[55].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));

        units[56] = new Unit(56, "Dryad Archer", Rarity.EPIC, Element.WIND, new Unit.Stat(982,892,1188,1002,1067), sprites[56]);
        units[56].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 40, 3, "stun"));


    }

    public static Unit GetUnitById(int id) {
        return new Unit(units[id]);
    }

}
