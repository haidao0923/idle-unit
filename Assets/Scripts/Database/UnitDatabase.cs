using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[112];
    public static Unit[] units = new Unit[112];

    void Awake()
    {
        units[0] = new Unit(0, "Rock Lizard", Rarity.Common, Element.Earth, new Unit.Stat(1159,1083,864,1110,1046), sprites[0]);
        units[0].setSkill(2, new Skill("Rock Smash", SkillType.ATTACK, StatType.STR, 220));

        units[1] = new Unit(1, "Defender", Rarity.Common, Element.Earth, new Unit.Stat(1162,1020,836,1185,967), sprites[1]);
        units[1].setSkill(2, new Skill("Shield", SkillType.BOOST, StatType.STR, 140, status: "DEF"));

        units[2] = new Unit(2, "Thief", Rarity.Common, Element.Earth, new Unit.Stat(985,1120,890,865,1183), sprites[2]);
        units[2].setSkill(2, new Skill("Double Strike", SkillType.ATTACK, StatType.AGI, 105, 2));

        units[3] = new Unit(3, "Cotton", Rarity.Common, Element.Wind, new Unit.Stat(872,1105,1120,967,1180), sprites[3]);
        units[3].setSkill(2, new Skill("Cuteness", SkillType.DODGE, skillPower: 15));

        units[4] = new Unit(4, "Wing", Rarity.Common, Element.Wind, new Unit.Stat(1100,1139,850,1103,1082), sprites[4]);
        units[4].setSkill(2, new Skill("Breathless Wind", SkillType.DEBUFF, StatType.AGI, 35, 2, "DEF"));

        units[5] = new Unit(5, "Archer", Rarity.Common, Element.Wind, new Unit.Stat(982,1084,856,879,1110), sprites[5]);
        units[5].setSkill(2, new Skill("Poison Arrow", SkillType.ATTACK, StatType.AGI, 210, status: "poison"));

        units[6] = new Unit(6, "Axe Warrior", Rarity.Common, Element.Fire, new Unit.Stat(1113,1164,989,1054,1067), sprites[6]);
        units[6].setSkill(2, new Skill("Giant Axe", SkillType.ATTACK, StatType.STR, 205, status: "vulnerable"));

        units[7] = new Unit(7, "Anubis", Rarity.Common, Element.Fire, new Unit.Stat(879,962,1185,865,1079), sprites[7]);
        units[7].setSkill(2, new Skill("Fireball", SkillType.ATTACK, StatType.MAG, 215, status: "burn"));

        units[8] = new Unit(8, "Undead", Rarity.Common, Element.Fire, new Unit.Stat(1066,970,1040,1082,982), sprites[8]);
        units[8].setSkill(2, new Skill("Undying", SkillType.HEAL, StatType.HLT, 230));

        units[9] = new Unit(9, "Water Knight", Rarity.Common, Element.Water, new Unit.Stat(1117,1038,1060,1055,1093), sprites[9]);
        units[9].setSkill(2, new Skill("Water Slash", SkillType.ATTACK, StatType.MAG, 195, status: "freeze"));

        units[10] = new Unit(10, "Blue Imp", Rarity.Common, Element.Water, new Unit.Stat(987,1063,1097,1028,1162), sprites[10]);
        units[10].setSkill(2, new Skill("Slip", SkillType.DEBUFF, StatType.MAG, 35, 3, status: "AGI"));

        units[11] = new Unit(11, "Goblin Raider", Rarity.Common, Element.Water, new Unit.Stat(1145,1093,982,999,1163), sprites[11]);
        units[11].setSkill(2, new Skill("Siphoning", SkillType.ATTACK, StatType.AGI, 100, 2, "weaken"));

        units[12] = new Unit(12, "Lancer", Rarity.Common, Element.Thunder, new Unit.Stat(1040,1130,1099,1103,1017), sprites[12]);
        units[12].setSkill(2, new Skill("Thunder Lance", SkillType.ATTACK, StatType.STR, 190, status: "stun"));

        units[13] = new Unit(13, "Thunder Moth", Rarity.Common, Element.Thunder, new Unit.Stat(1129,1001,1136,988,1140), sprites[13]);
        units[13].setSkill(2, new Skill("Magic Dust", SkillType.BOOST, StatType.MAG, 30, 3, "MAG"));

        units[14] = new Unit(14, "Witch", Rarity.Common, Element.Thunder, new Unit.Stat(982,892,1188,1002,1067), sprites[14]);
        units[14].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 60, 3, "stun"));

        units[15] = new Unit(15, "Ana the Assassin", Rarity.Rare, Element.Earth, new Unit.Stat(1082,1283,1105,1155,1237), sprites[15]);
        units[15].setSkill(1, new Skill("Double Attack", SkillType.ATTACK, StatType.STR, 45, 2));
        units[15].setSkill(2, new Skill("Dagger Toss", SkillType.ATTACK, StatType.STR, 60, 4, "poison"));
        units[15].setSkill(3, new Skill("Rush", SkillType.BOOST, StatType.STR, 190, 1, "AGI"));

        units[16] = new Unit(16, "Devil Knight", Rarity.Rare, Element.Earth, new Unit.Stat(1199,1290,1020,1100,1043), sprites[16]);
        units[16].setSkill(2, new Skill("Heavy Slash", SkillType.ATTACK, StatType.STR, 245, status: "vulnerable"));

        units[17] = new Unit(17, "Earth Crocodile", Rarity.Rare, Element.Earth, new Unit.Stat(1115,1183,985,1076,1266), sprites[17]);
        units[17].setSkill(2, new Skill("Sweep", SkillType.AOE_ATTACK, StatType.AGI, 65));

        units[18] = new Unit(18, "Haunted Tree", Rarity.Rare, Element.Earth, new Unit.Stat(1236,1166,1188,1117,1149), sprites[18]);
        units[18].setSkill(2, new Skill("Fear", SkillType.DEBUFF, StatType.MAG, 20, 3, "STR,MAG"));
        units[18].setSkill(3, new Skill("Haunt", SkillType.AOE_DEBUFF, StatType.MAG, 25, status: "AGI"));

        units[19] = new Unit(19, "Mummy", Rarity.Rare, Element.Earth, new Unit.Stat(1074,1295,1167,1089,1084), sprites[19]);
        units[19].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 110));
        units[19].setSkill(2, new Skill("Rotten Venom", SkillType.ATTACK, StatType.STR, 85, 3, "poison"));
        units[19].setSkill(3, new Skill("Decay", SkillType.DEBUFF, StatType.MAG, 65, 1, "STR"));

        units[20] = new Unit(20, "Orc Captain", Rarity.Rare, Element.Earth, new Unit.Stat(1194,1233,1052,1104,1143), sprites[20]);
        units[20].setSkill(2, new Skill("Axe Slam", SkillType.ATTACK_ADJACENT, StatType.STR, 110));

        units[21] = new Unit(21, "Rock Golem", Rarity.Rare, Element.Earth, new Unit.Stat(1233,1160,999,1245,1110), sprites[21]);
        units[21].setSkill(2, new Skill("Shield Bash", SkillType.ATTACK, StatType.DEF, 255));
        units[21].setSkill(3, new Skill("Rock Barrier", SkillType.AOE_BOOST, StatType.MAG, 20, status: "DEF"));

        units[22] = new Unit(22, "Sand Worm", Rarity.Rare, Element.Earth, new Unit.Stat(1120,1098,1135,1199,1067), sprites[22]);
        units[22].setSkill(2, new Skill("Earthquake", SkillType.AOE_ATTACK, StatType.DEF, 45));
        units[22].setSkill(3, new Skill("Quicksand", SkillType.DEBUFF, StatType.AGI, 65, status: "DEF,AGI"));

        units[23] = new Unit(23, "Green Wyvern", Rarity.Rare, Element.Wind, new Unit.Stat(1211,1088,925,1268,1198), sprites[23]);
        units[23].setSkill(1, new Skill("Guard Strike", SkillType.ATTACK, StatType.DEF, 45, 2));
        units[23].setSkill(2, new Skill("Defend", SkillType.BOOST, StatType.DEF, 160, status: "DEF"));

        units[24] = new Unit(24, "Leaf Warrior", Rarity.Rare, Element.Wind, new Unit.Stat(1200,1298,975,1153,1211), sprites[24]);
        units[24].setSkill(2, new Skill("Heroic Code", SkillType.PROTECTION, extraEffect: 3));

        units[25] = new Unit(25, "Proud Bird", Rarity.Rare, Element.Wind, new Unit.Stat(1244,1186,1111,1211,1087), sprites[25]);
        units[25].setSkill(2, new Skill("Protect", SkillType.PROTECTION, extraEffect: 2));
        units[25].setSkill(3, new Skill("Alliance", SkillType.AOE_BOOST, StatType.AGI, 25, status: "DEF"));

        units[26] = new Unit(26, "Saw Girl", Rarity.Rare, Element.Wind, new Unit.Stat(1100,1222,1257,1111,1099), sprites[26]);
        units[26].setSkill(1, new Skill("Double Slash", SkillType.ATTACK, StatType.STR, 70, 2));
        units[26].setSkill(2, new Skill("Nightfall", SkillType.AOE_DEBUFF, StatType.MAG, 10, status: "HLT,DEF"));

        units[27] = new Unit(27, "Fairy", Rarity.Rare, Element.Wind, new Unit.Stat(1147,1098,1233,987,1214), sprites[27]);
        units[27].setSkill(2, new Skill("Restoration", SkillType.AOE_HEAL, StatType.MAG, 50));

        units[28] = new Unit(28, "Fire Toad", Rarity.Rare, Element.Fire, new Unit.Stat(1083,1246,1289,1100,996), sprites[28]);
        units[28].setSkill(1, new Skill("Fire Attack", SkillType.ATTACK, StatType.STR, 90, status: "burn"));
        units[28].setSkill(2, new Skill("Fire Spit", SkillType.AOE_ATTACK, StatType.STR, 60, status: "burn"));

        units[29] = new Unit(29, "Flame Deer", Rarity.Rare, Element.Fire, new Unit.Stat(1098,1217,1188,1113,1211), sprites[29]);
        units[29].setSkill(2, new Skill("Charge", SkillType.ATTACK, StatType.STR, 260));
        units[29].setSkill(3, new Skill("Run", SkillType.BOOST, StatType.STR, 130, status: "DEF,AGI"));

        units[30] = new Unit(30, "Scroll Mage", Rarity.Rare, Element.Fire, new Unit.Stat(976,1199,1266,943,1211), sprites[30]);
        units[30].setSkill(1, new Skill("Fire Scroll", SkillType.ATTACK, StatType.MAG, 90, status: "burn"));
        units[30].setSkill(2, new Skill("Ice Scroll", SkillType.ATTACK, StatType.MAG, 210, status: "freeze"));
        units[30].setSkill(3, new Skill("Binding Scroll", SkillType.ATTACK, StatType.STR, 280, status: "silence"));

        units[31] = new Unit(31, "Sphinx", Rarity.Rare, Element.Fire, new Unit.Stat(1282,1237,1082,1288,1078), sprites[31]);
        units[31].setSkill(1, new Skill("Swipe", SkillType.ATTACK, StatType.STR, 50, 2));
        units[31].setSkill(2, new Skill("Protection", SkillType.PROTECTION, extraEffect: 2));
        units[31].setSkill(3, new Skill("Empower", SkillType.BOOST, StatType.STR, 25, 2, "STR,DEF"));

        units[32] = new Unit(32, "Vampire", Rarity.Rare, Element.Fire, new Unit.Stat(1233,1266,1156,1098,1211), sprites[32]);
        units[32].setSkill(2, new Skill("Weakening", SkillType.ATTACK, StatType.STR, 215, status: "weaken"));
        units[32].setSkill(3, new Skill("Evasion", SkillType.DODGE, skillPower: 25));

        units[33] = new Unit(33, "Ice Turtle", Rarity.Rare, Element.Water, new Unit.Stat(1278,1122,1088,1245,1168), sprites[33]);
        units[33].setSkill(2, new Skill("Hard Shell", SkillType.BOOST, StatType.STR, 165, status: "DEF"));

        units[34] = new Unit(34, "Lizardmen", Rarity.Rare, Element.Water, new Unit.Stat(1211,1277,988,1223,1041), sprites[34]);
        units[34].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 110));
        units[34].setSkill(2, new Skill("Double Slash", SkillType.ATTACK, StatType.STR, 120, 2, "vulnerable"));
        units[34].setSkill(3, new Skill("Guardian", SkillType.PROTECTION, extraEffect: 2));

        units[35] = new Unit(35, "Magic Crocodile", Rarity.Rare, Element.Water, new Unit.Stat(1156,1275,1299,1002,1100), sprites[35]);
        units[35].setSkill(1, new Skill("Bite", SkillType.ATTACK, StatType.STR, 130));
        units[35].setSkill(2, new Skill("Poisonous Bite", SkillType.ATTACK, StatType.STR, 245, status: "poison"));

        units[36] = new Unit(36, "Pirate Captain", Rarity.Rare, Element.Water, new Unit.Stat(1056,999,1188,982,1214), sprites[36]);
        units[36].setSkill(2, new Skill("Water Gun", SkillType.ATTACK, StatType.MAG, 80, 3, "freeze"));

        units[37] = new Unit(37, "Slimey", Rarity.Rare, Element.Water, new Unit.Stat(1026,911,1243,1088,1277), sprites[37]);
        units[37].setSkill(1, new Skill("Freeze Attack", SkillType.ATTACK, StatType.AGI, 90, status: "freeze"));
        units[37].setSkill(2, new Skill("Elemental Attack", SkillType.ATTACK, StatType.MAG, 205, status: "burn, poison, freeze, stun"));

        units[38] = new Unit(38, "Electroctopus", Rarity.Rare, Element.Thunder, new Unit.Stat(975,1211,1233,1168,1079), sprites[38]);
        units[38].setSkill(1, new Skill("Shock Attack", SkillType.ATTACK, StatType.STR, 85, status: "stun"));
        units[38].setSkill(2, new Skill("Paralysis", SkillType.ATTACK, StatType.AGI, 75, 3, "stun"));
        units[38].setSkill(3, new Skill("Regenerate", SkillType.HEAL, StatType.DEF, 230));

        units[39] = new Unit(39, "Iron Maiden", Rarity.Rare, Element.Thunder, new Unit.Stat(1288,1237,1000,1266,982), sprites[39]);
        units[39].setSkill(1, new Skill("Entrapment", SkillType.ATTACK, StatType.STR, 90, status: "silence"));
        units[39].setSkill(2, new Skill("Protection", SkillType.PROTECTION, extraEffect: 2));

        units[40] = new Unit(40, "Mayor", Rarity.Rare, Element.Thunder, new Unit.Stat(1173,1233,1088,1211,1144), sprites[40]);
        units[40].setSkill(1, new Skill("Poison Attack", SkillType.ATTACK, StatType.STR, 100, status: "poison"));
        units[40].setSkill(2, new Skill("Affliction", SkillType.ATTACK, StatType.STR, 125, 2, "burn, poison"));

        units[41] = new Unit(41, "Puppet", Rarity.Rare, Element.Thunder, new Unit.Stat(1087,925,1299,985,1211), sprites[41]);
        units[41].setSkill(1, new Skill("Shadow Strike", SkillType.ATTACK, StatType.MAG, 90, status: "silence"));
        units[41].setSkill(2, new Skill("Penetrating Strike", SkillType.ATTACK, StatType.MAG, 230, status: "purge"));
        units[41].setSkill(3, new Skill("Fluid Motion", SkillType.DODGE, skillPower: 20));

        units[42] = new Unit(42, "Orbital Dragon", Rarity.Rare, Element.Thunder, new Unit.Stat(1287,1100,1239,1158,1249), sprites[42]);
        units[42].setSkill(1, new Skill("Lightning", SkillType.ATTACK, StatType.MAG, 40, 2, "stun"));
        units[42].setSkill(2, new Skill("Empower", SkillType.BOOST, StatType.STR, 40, 3, "STR"));

        units[43] = new Unit(43, "Void Crystal", Rarity.Rare, Element.Void, new Unit.Stat(1244,1099,1267,1300,1100), sprites[43]);
        units[43].setSkill(2, new Skill("Defend", SkillType.BOOST, StatType.MAG, 165, status: "DEF"));
        units[43].setSkill(3, new Skill("Shield", SkillType.AOE_BOOST, StatType.MAG, 25, status: "DEF"));

        units[44] = new Unit(44, "Bamboo Master Bob", Rarity.Epic, Element.Earth, new Unit.Stat(1188,1200,1143,1162,1094), sprites[44]);
        units[44].setSkill(1, new Skill("Bamboo Blitz", SkillType.ATTACK, StatType.STR, 100, 3, "vulnerable"));
        units[44].setSkill(2, new Skill("Lunch Break", SkillType.HEAL, StatType.MAG, 220));
        units[44].setSkill(3, new Skill("Meditation Class", SkillType.AOE_BOOST, StatType.DEF, 40, status: "DEF"));

        units[45] = new Unit(45, "Robot", Rarity.Epic, Element.Earth, new Unit.Stat(1104,1100,1162,1123,976), sprites[45]);
        units[45].setSkill(1, new Skill("Penetrating Strike", SkillType.ATTACK, StatType.STR, 70, status: "purge"));
        units[45].setSkill(2, new Skill("Steel Armor", SkillType.BOOST, StatType.MAG, 230, status: "DEF"));
        units[45].setSkill(3, new Skill("Energy Shield", SkillType.AOE_PROTECTION));

        units[46] = new Unit(46, "Ant Queen", Rarity.Epic, Element.Earth, new Unit.Stat(964,1169,921,1103,1129), sprites[46]);
        units[46].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 130));
        units[46].setSkill(2, new Skill("Sting", SkillType.ATTACK, StatType.STR, 165, 2, "burn"));
        units[46].setSkill(3, new Skill("Alliance", SkillType.AOE_BOOST, StatType.STR, 20, status: "STR,DEF"));

        units[47] = new Unit(47, "Ant Sentinel", Rarity.Epic, Element.Earth, new Unit.Stat(1199,1076,863,1183,1067), sprites[47]);
        units[47].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 120));
        units[47].setSkill(2, new Skill("Hard Shell", SkillType.BOOST, StatType.STR, 240, status: "DEF"));
        units[47].setSkill(3, new Skill("Protect", SkillType.PROTECTION, extraEffect: 4));

        units[48] = new Unit(48, "Dryad Warrior", Rarity.Epic, Element.Earth, new Unit.Stat(982,1192,877,1064,1172), sprites[48]);
        units[48].setSkill(1, new Skill("Double Attack", SkillType.ATTACK, StatType.STR, 60, 2));
        units[48].setSkill(2, new Skill("Quadruple Attack", SkillType.ATTACK, StatType.STR, 85, 4));
        units[48].setSkill(3, new Skill("Wind-up", SkillType.BOOST, StatType.MAG, 275, status: "AGI"));

        units[49] = new Unit(49, "Earth Lion", Rarity.Epic, Element.Earth, new Unit.Stat(1113,1126,999,1087,1121), sprites[49]);
        units[49].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 120));
        units[49].setSkill(2, new Skill("Hunt", SkillType.ATTACK_ADJACENT, StatType.STR, 65, 2));
        units[49].setSkill(3, new Skill("Pride", SkillType.BOOST, StatType.HLT, 55, 3, "STR"));

        units[50] = new Unit(50, "Hydra", Rarity.Epic, Element.Earth, new Unit.Stat(1200,1088,1044,1055,1144), sprites[50]);
        units[50].setSkill(1, new Skill("Triple Attack", SkillType.ATTACK, StatType.STR, 35, 3));
        units[50].setSkill(2, new Skill("Tail Whip", SkillType.AOE_ATTACK, StatType.STR, 100));
        units[50].setSkill(3, new Skill("Regenerate", SkillType.HEAL, StatType.MAG, 340, status: "purify"));

        units[51] = new Unit(51, "Man-eater", Rarity.Epic, Element.Earth, new Unit.Stat(1075,1162,1199,1079,1121), sprites[51]);
        units[51].setSkill(1, new Skill("Innate Protection", SkillType.PROTECTION));
        units[51].setSkill(2, new Skill("Poison Attack", SkillType.ATTACK_ADJACENT, StatType.STR, 125, status: "poison"));
        units[51].setSkill(3, new Skill("Numbing Bite", SkillType.ATTACK, StatType.STR, 340, status: "weaken"));

        units[52] = new Unit(52, "Mineral Bear", Rarity.Epic, Element.Earth, new Unit.Stat(1199,1136,1179,1173,1087), sprites[52]);
        units[52].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 120));
        units[52].setSkill(2, new Skill("Grapple", SkillType.ATTACK, StatType.STR, 345, status: "vulnerable"));
        units[52].setSkill(3, new Skill("Mineral Throw", SkillType.ATTACK, StatType.STR, 120, 3, "burn, poison, weaken"));

        units[53] = new Unit(53, "Serpent", Rarity.Epic, Element.Earth, new Unit.Stat(1000,1166,1200,1113,1089), sprites[53]);
        units[53].setSkill(1, new Skill("Poison Attack", SkillType.ATTACK, StatType.STR, 120, status: "poison"));
        units[53].setSkill(2, new Skill("Spurt Venom", SkillType.ATTACK_ADJACENT, StatType.STR, 115, status: "poison"));
        units[53].setSkill(3, new Skill("Poison Slash", SkillType.AOE_ATTACK, StatType.STR, 95, status: "poison"));

        units[54] = new Unit(54, "Flying Hero, Wingeno", Rarity.Epic, Element.Wind, new Unit.Stat(1082,1198,1144,976,1200), sprites[54]);
        units[54].setSkill(1, new Skill("Purging Attack", SkillType.ATTACK, StatType.STR, 110, status: "purge"));
        units[54].setSkill(2, new Skill("Blade Drop", SkillType.ATTACK_ADJACENT, StatType.STR, 70, 2));
        units[54].setSkill(3, new Skill("Flight", SkillType.DODGE, skillPower: 30));

        units[55] = new Unit(55, "Book Magician", Rarity.Epic, Element.Wind, new Unit.Stat(1087,999,1107,877,1143), sprites[55]);
        units[55].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.AGI, 130));
        units[55].setSkill(2, new Skill("Book of Wisdom", SkillType.AOE_BOOST, StatType.MAG, 50, status: "MAG"));
        units[55].setSkill(3, new Skill("Levitation", SkillType.AOE_DODGE, skillPower: 5));

        units[56] = new Unit(56, "Dryad Archer", Rarity.Epic, Element.Wind, new Unit.Stat(982,1145,1088,1086,1179), sprites[56]);
        units[56].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.AGI, 135));
        units[56].setSkill(2, new Skill("Focused Shot", SkillType.ATTACK, StatType.MAG, 330, status: "burn, poison"));
        units[56].setSkill(3, new Skill("Trick Shot", SkillType.ATTACK, StatType.STR, 175, 2, "vulnerable, weaken"));

        units[57] = new Unit(57, "Fat Dragon", Rarity.Epic, Element.Wind, new Unit.Stat(1156,1167,899,1086,1044), sprites[57]);
        units[57].setSkill(1, new Skill("Strike", SkillType.ATTACK, StatType.STR, 130));
        units[57].setSkill(2, new Skill("Descent", SkillType.AOE_ATTACK, StatType.STR, 100));
        units[57].setSkill(3, new Skill("Buff", SkillType.BOOST, StatType.STR, 55, 3, "DEF"));

        units[58] = new Unit(58, "Fylar", Rarity.Epic, Element.Wind, new Unit.Stat(1114,1088,1163,1067,1103), sprites[58]);
        units[58].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.MAG, 110));
        units[58].setSkill(2, new Skill("Hypnotism", SkillType.ATTACK, StatType.MAG, 155, 2, "charm"));
        units[58].setSkill(3, new Skill("Heal", SkillType.HEAL, StatType.MAG, 165, 3));

        units[59] = new Unit(59, "Isis", Rarity.Epic, Element.Wind, new Unit.Stat(1029,1108,1133,966,1074), sprites[59]);
        units[59].setSkill(1, new Skill("Wing Flap", SkillType.ATTACK, StatType.STR, 25, 4));
        units[59].setSkill(2, new Skill("Charm", SkillType.ATTACK, StatType.MAG, 320, status: "charm"));
        units[59].setSkill(3, new Skill("Blessing", SkillType.AOE_HEAL, StatType.MAG, 95));

        units[60] = new Unit(60, "Reaper", Rarity.Epic, Element.Wind, new Unit.Stat(1013,1188,1097,962,1117), sprites[60]);
        units[60].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 120));
        units[60].setSkill(2, new Skill("Weaken", SkillType.DEBUFF, StatType.MAG, 40, 3, "STR"));
        units[60].setSkill(3, new Skill("Great Slash", SkillType.AOE_ATTACK, StatType.STR, 45, 2));

        units[61] = new Unit(61, "Youcan", Rarity.Epic, Element.Wind, new Unit.Stat(1164,1200,1093,1182,1179), sprites[61]);
        units[61].setSkill(1, new Skill("Swoop", SkillType.ATTACK, StatType.STR, 140));
        units[61].setSkill(2, new Skill("Aggression", SkillType.BOOST, StatType.DEF, 270, status: "STR"));
        units[61].setSkill(3, new Skill("Rush", SkillType.BOOST, StatType.MAG, 320, status: "AGI"));

        units[62] = new Unit(62, "Dragon Queen Tiamat", Rarity.Epic, Element.Fire, new Unit.Stat(1008,1182,854,1086,1113), sprites[62]);
        units[62].setSkill(1, new Skill("Double Slash", SkillType.ATTACK, StatType.STR, 60, 2));
        units[62].setSkill(2, new Skill("Corrupting Strike", SkillType.ATTACK, StatType.STR, 165, 2, "weaken, silence"));
        units[62].setSkill(3, new Skill("Great Flame", SkillType.ATTACK, StatType.STR, 355, status: "burn"));

        units[63] = new Unit(63, "Blood Sage", Rarity.Epic, Element.Fire, new Unit.Stat(1199,1145,1076,1119,1087), sprites[63]);
        units[63].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 130));
        units[63].setSkill(2, new Skill("Blood Strike", SkillType.ATTACK, StatType.HLT, 195, 2));
        units[63].setSkill(3, new Skill("Blood Rain", SkillType.AOE_ATTACK, StatType.HLT, 105));

        units[64] = new Unit(64, "Candle Knight", Rarity.Epic, Element.Fire, new Unit.Stat(1119,1124,1133,1086,989), sprites[64]);
        units[64].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 120, status: "burn"));
        units[64].setSkill(2, new Skill("Candlelight", SkillType.HEAL, StatType.MAG, 105, 4, "burn, purify"));
        units[64].setSkill(3, new Skill("Wax Attack", SkillType.AOE_ATTACK, StatType.MAG, 90, status: "burn"));

        units[65] = new Unit(65, "Charred Demon", Rarity.Epic, Element.Fire, new Unit.Stat(1102,866,1193,1111,999), sprites[65]);
        units[65].setSkill(1, new Skill("Burn All", SkillType.AOE_ATTACK, StatType.MAG, 30, status: "burn"));
        units[65].setSkill(2, new Skill("Burn More", SkillType.AOE_ATTACK, StatType.MAG, 35, 3, "burn"));
        units[65].setSkill(3, new Skill("Burnt", SkillType.DODGE, skillPower: 1));

        units[66] = new Unit(66, "Fire Wolf", Rarity.Epic, Element.Fire, new Unit.Stat(985,1137,1162,1034,1156), sprites[66]);
        units[66].setSkill(1, new Skill("Assault", SkillType.ATTACK, StatType.STR, 130, status: "burn"));
        units[66].setSkill(2, new Skill("Charge", SkillType.BOOST, StatType.MAG, 55, 3, "AGI"));
        units[66].setSkill(3, new Skill("Wolf Pack", SkillType.AOE_BOOST, StatType.MAG, 30, status: "STR"));

        units[67] = new Unit(67, "Flame Gargoyle", Rarity.Epic, Element.Fire, new Unit.Stat(1104,1191,1176,1013,1022), sprites[67]);
        units[67].setSkill(1, new Skill("Weaken Strike", SkillType.ATTACK, StatType.STR, 100, status: "weaken"));
        units[67].setSkill(2, new Skill("Piercing Strike", SkillType.ATTACK, StatType.STR, 325, status: "purge"));
        units[67].setSkill(3, new Skill("Triple Attack", SkillType.ATTACK, StatType.MAG, 115, 3, "burn"));

        units[68] = new Unit(68, "Flaming Armor", Rarity.Epic, Element.Fire, new Unit.Stat(1162,855,1178,1192,923), sprites[68]);
        units[68].setSkill(1, new Skill("Burn", SkillType.ATTACK, StatType.MAG, 100, status: "burn"));
        units[68].setSkill(2, new Skill("Flame Strike", SkillType.ATTACK, StatType.MAG, 115, 3, "burn"));
        units[68].setSkill(3, new Skill("Harden Armor", SkillType.BOOST, StatType.MAG, 235, status: "DEF"));

        units[69] = new Unit(69, "Red Angel", Rarity.Epic, Element.Fire, new Unit.Stat(1077,1055,1129,1086,1133), sprites[69]);
        units[69].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.MAG, 140));
        units[69].setSkill(2, new Skill("Affliction", SkillType.ATTACK, StatType.STR, 110, 3, "vulnerable, weaken"));
        units[69].setSkill(3, new Skill("Burning Judgement", SkillType.ATTACK_ADJACENT, StatType.MAG, 65, 2, "burn"));

        units[70] = new Unit(70, "Frostia of Unbreakable Ice", Rarity.Epic, Element.Water, new Unit.Stat(1100,1008,1200,927,1127), sprites[70]);
        units[70].setSkill(1, new Skill("Frozen Touch", SkillType.ATTACK, StatType.MAG, 130, status: "freeze"));
        units[70].setSkill(2, new Skill("Ice Barrier", SkillType.PROTECTION, extraEffect: 3));
        units[70].setSkill(3, new Skill("Blizzard", SkillType.AOE_ATTACK, StatType.MAG, 85, status: "freeze"));

        units[71] = new Unit(71, "Blax", Rarity.Epic, Element.Water, new Unit.Stat(1119,1145,921,1116,1087), sprites[71]);
        units[71].setSkill(1, new Skill("Freezing Slash", SkillType.ATTACK, StatType.STR, 50, 2, "freeze"));
        units[71].setSkill(2, new Skill("Great Strike", SkillType.ATTACK, StatType.STR, 365, status: "freeze"));
        units[71].setSkill(3, new Skill("Defend", SkillType.BOOST, StatType.MAG, 50, 3, "DEF"));

        units[72] = new Unit(72, "Frozen Horn", Rarity.Epic, Element.Water, new Unit.Stat(1175,1146,1089,1129,1113), sprites[72]);
        units[72].setSkill(1, new Skill("Charge", SkillType.ATTACK, StatType.STR, 130, status: "freeze"));
        units[72].setSkill(2, new Skill("Horn Attack", SkillType.ATTACK, StatType.MAG, 170, 2, "freeze"));
        units[72].setSkill(3, new Skill("Magic Bull", SkillType.AOE_BOOST, StatType.AGI, 40, status: "MAG"));

        units[73] = new Unit(73, "Nateneci", Rarity.Epic, Element.Water, new Unit.Stat(995,1109,1142,1198,1083), sprites[73]);
        units[73].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 140));
        units[73].setSkill(2, new Skill("Ice Blast", SkillType.ATTACK, StatType.MAG, 85, 4, "freeze"));
        units[73].setSkill(3, new Skill("Evasion", SkillType.DODGE, skillPower: 25));

        units[74] = new Unit(74, "Lamia", Rarity.Epic, Element.Water, new Unit.Stat(1067,1089,1145,1111,996), sprites[74]);
        units[74].setSkill(1, new Skill("Poison Attack", SkillType.ATTACK, StatType.MAG, 115, status: "poison"));
        units[74].setSkill(2, new Skill("Wrap", SkillType.ATTACK, StatType.STR, 110, 3, "poison, freeze, weaken"));
        units[74].setSkill(3, new Skill("Slithering", SkillType.DEBUFF, StatType.AGI, 35, 4, "DEF"));

        units[75] = new Unit(75, "Poison Mage", Rarity.Epic, Element.Water, new Unit.Stat(872,1113,1199,925,979), sprites[75]);
        units[75].setSkill(1, new Skill("Magic Attack", SkillType.ATTACK, StatType.MAG, 60, status: "burn, poison, vulnerable, weaken"));
        units[75].setSkill(2, new Skill("Poison Wave", SkillType.AOE_ATTACK, StatType.MAG, 90, status: "poison"));
        units[75].setSkill(3, new Skill("Spirit Boost", SkillType.AOE_BOOST, StatType.MAG, 20, status: "HLT,MAG"));

        units[76] = new Unit(76, "Water Elemental", Rarity.Epic, Element.Water, new Unit.Stat(1173,1132,1164,1098,1017), sprites[76]);
        units[76].setSkill(1, new Skill("Frozen Grab", SkillType.ATTACK, StatType.MAG, 110, status: "freeze"));
        units[76].setSkill(2, new Skill("Cold Touch", SkillType.ATTACK, StatType.MAG, 135, 2, "freeze"));
        units[76].setSkill(3, new Skill("Frostbite", SkillType.ATTACK, StatType.STR, 65, 5, "poison, freeze"));

        units[77] = new Unit(77, "Lightning Tigras", Rarity.Epic, Element.Thunder, new Unit.Stat(963,1142,1025,987,1186), sprites[77]);
        units[77].setSkill(1, new Skill("Quick Attack", SkillType.ATTACK, StatType.AGI, 45, 3, "stun"));
        units[77].setSkill(2, new Skill("Stunning Speed", SkillType.ATTACK, StatType.AGI, 75, 4, "stun"));
        units[77].setSkill(3, new Skill("Adrenaline", SkillType.BOOST, StatType.STR, 285, status: "AGI"));

        units[78] = new Unit(78, "Abomination", Rarity.Epic, Element.Thunder, new Unit.Stat(1015,1133,1165,1109,1123), sprites[78]);
        units[78].setSkill(1, new Skill("Fear Attack", SkillType.ATTACK, StatType.STR, 110, status: "silence"));
        units[78].setSkill(2, new Skill("Paralysis", SkillType.ATTACK, StatType.MAG, 165, 2, "stun"));
        units[78].setSkill(3, new Skill("Weird Healing", SkillType.AOE_HEAL, StatType.MAG, 60, status: "stun, purify"));

        units[79] = new Unit(79, "Ancient Priestess", Rarity.Epic, Element.Thunder, new Unit.Stat(1139,1175,1000,1086,1162), sprites[79]);
        units[79].setSkill(1, new Skill("Attack", SkillType.AOE_ATTACK, StatType.STR, 35));
        units[79].setSkill(2, new Skill("Wrap", SkillType.ATTACK, StatType.STR, 165, 2, "silence"));
        units[79].setSkill(3, new Skill("Halt", SkillType.ATTACK, StatType.STR, 115, 3, "stun"));

        units[80] = new Unit(80, "Holy Maiden", Rarity.Epic, Element.Thunder, new Unit.Stat(1112,888,1133,954,1085), sprites[80]);
        units[80].setSkill(1, new Skill("Protection", SkillType.PROTECTION, extraEffect: 2));
        units[80].setSkill(2, new Skill("Light Beam", SkillType.ATTACK_ADJACENT, StatType.MAG, 55, 2));
        units[80].setSkill(3, new Skill("Judgement", SkillType.ATTACK, StatType.MAG, 350, status: "purge, purify"));

        units[81] = new Unit(81, "Light Dragon", Rarity.Epic, Element.Thunder, new Unit.Stat(1100,1169,1089,1117,1125), sprites[81]);
        units[81].setSkill(1, new Skill("Roar", SkillType.AOE_ATTACK, StatType.STR, 20, status: "stun"));
        units[81].setSkill(2, new Skill("Dragon Claw", SkillType.ATTACK, StatType.STR, 180, 2));
        units[81].setSkill(3, new Skill("Healing", SkillType.HEAL, StatType.MAG, 245, 2));

        units[82] = new Unit(82, "Minotaur", Rarity.Epic, Element.Thunder, new Unit.Stat(1088,1145,1002,1033,1109), sprites[82]);
        units[82].setSkill(1, new Skill("Wing Attack", SkillType.ATTACK, StatType.STR, 65, 2));
        units[82].setSkill(2, new Skill("Distraction", SkillType.DEBUFF, StatType.MAG, 30, 3, "STR, MAG, AGI"));
        units[82].setSkill(3, new Skill("Great Attack", SkillType.ATTACK, StatType.STR, 310, status: "vulnerable, weaken, stun"));

        units[83] = new Unit(83, "Rider", Rarity.Epic, Element.Thunder, new Unit.Stat(1177,899,1115,1102,1183), sprites[83]);
        units[83].setSkill(1, new Skill("Gallop", SkillType.BOOST, StatType.STR, 90, status: "AGI"));
        units[83].setSkill(2, new Skill("Thunder Strike", SkillType.ATTACK, StatType.MAG, 115, 3, "stun"));
        units[83].setSkill(3, new Skill("Cleave", SkillType.AOE_ATTACK, StatType.STR, 105));

        units[84] = new Unit(84, "Triad", Rarity.Epic, Element.Thunder, new Unit.Stat(1100,1088,1137,924,1133), sprites[84]);
        units[84].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 140));
        units[84].setSkill(2, new Skill("Flash", SkillType.AOE_ATTACK, StatType.MAG, 35, 2, "stun"));
        units[84].setSkill(3, new Skill("Dodge", SkillType.DODGE, skillPower: 20));

        units[85] = new Unit(85, "Swarm", Rarity.Epic, Element.Void, new Unit.Stat(987,1077,1133,1086,1056), sprites[85]);
        units[85].setSkill(1, new Skill("Grab", SkillType.ATTACK, StatType.STR, 130));
        units[85].setSkill(2, new Skill("Surround", SkillType.ATTACK, StatType.MAG, 115, 3, "weaken"));
        units[85].setSkill(3, new Skill("Heal", SkillType.HEAL, StatType.MAG, 350));

        units[86] = new Unit(86, "Beholder", Rarity.Epic, Element.Void, new Unit.Stat(1020,1117,1199,945,1111), sprites[86]);
        units[86].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.MAG, 110));
        units[86].setSkill(2, new Skill("Cursed Eye", SkillType.ATTACK, StatType.MAG, 335, status: "vulnerable, weaken, silence"));
        units[86].setSkill(3, new Skill("Cursed Eye 2", SkillType.ATTACK, StatType.MAG, 170, 2, "burn, poison, freeze, stun"));

        units[87] = new Unit(87, "Mecha Scorpion", Rarity.Epic, Element.Void, new Unit.Stat(1178,1135,1095,1127,988), sprites[87]);
        units[87].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.STR, 130));
        units[87].setSkill(2, new Skill("Sting", SkillType.ATTACK, StatType.STR, 175, 2, "poison"));
        units[87].setSkill(3, new Skill("Laser Beam", SkillType.AOE_ATTACK, StatType.STR, 80, status: "silence"));

        units[88] = new Unit(88, "Exterminator", Rarity.Epic, Element.Void, new Unit.Stat(1082,1197,1146,1101,998), sprites[88]);
        units[88].setSkill(1, new Skill("Great Slash", SkillType.ATTACK, StatType.STR, 35, 3));
        units[88].setSkill(2, new Skill("Burning Strike", SkillType.ATTACK, StatType.MAG, 340, status: "burn"));
        units[88].setSkill(3, new Skill("Enhance", SkillType.BOOST, StatType.MAG, 45, 3, "STR"));

        units[89] = new Unit(89, "End Spawn", Rarity.Epic, Element.Void, new Unit.Stat(982,1021,1182,1174,1063), sprites[89]);
        units[89].setSkill(1, new Skill("Attack", SkillType.ATTACK, StatType.MAG, 130));
        units[89].setSkill(2, new Skill("Mind Control", SkillType.ATTACK, StatType.MAG, 95, 3, "charm"));
        units[89].setSkill(3, new Skill("Slippery", SkillType.AOE_DODGE, skillPower: 10));

        units[90] = new Unit(90, "Grappl", Rarity.Legendary, Element.Earth, new Unit.Stat(1276,1243,1258,1109,1176), sprites[90]);
        units[90].setSkill(1, new Skill("Grab", SkillType.ATTACK, StatType.STR, 125, 2));
        units[90].setSkill(2, new Skill("Nature's Charm", SkillType.ATTACK, StatType.MAG, 155, 3, "charm"));
        units[90].setSkill(3, new Skill("Nature's Protection", SkillType.AOE_PROTECTION));

        units[91] = new Unit(91, "Itca", Rarity.Legendary, Element.Earth, new Unit.Stat(1110,1267,1123,1211,1088), sprites[91]);
        units[91].setSkill(1, new Skill("Dry Attack", SkillType.ATTACK, StatType.MAG, 80, 3, "burn"));
        units[91].setSkill(2, new Skill("Heat Wave", SkillType.AOE_ATTACK, StatType.MAG, 120, status: "burn"));
        units[91].setSkill(3, new Skill("Protective Spike", SkillType.PROTECTION, extraEffect: 5));

        units[92] = new Unit(92, "Rach", Rarity.Legendary, Element.Earth, new Unit.Stat(1214,1233,1088,1112,1166), sprites[92]);
        units[92].setSkill(1, new Skill("Multi-Attack", SkillType.ATTACK, StatType.STR, 40, 6));
        units[92].setSkill(2, new Skill("Bind", SkillType.ATTACK, StatType.STR, 170, 3, "silence"));
        units[92].setSkill(3, new Skill("Sixth Sense", SkillType.AOE_BOOST, StatType.MAG, 20, status: "HLT, STR, MAG, DEF, AGI"));

        units[93] = new Unit(93, "Taurus", Rarity.Legendary, Element.Earth, new Unit.Stat(1277,1255,1102,1234,1178), sprites[93]);
        units[93].setSkill(1, new Skill("Charge", SkillType.ATTACK, StatType.STR, 260));
        units[93].setSkill(2, new Skill("Buff", SkillType.BOOST, StatType.STR, 40, 3, "STR,DEF"));
        units[93].setSkill(3, new Skill("Nature's Defender", SkillType.PROTECTION, extraEffect: 6));

        units[94] = new Unit(94, "Verde", Rarity.Legendary, Element.Earth, new Unit.Stat(1020,1285,985,1079,1066), sprites[94]);
        units[94].setSkill(1, new Skill("Poison Slash", SkillType.ATTACK, StatType.STR, 55, 4, "poison"));
        units[94].setSkill(2, new Skill("Large Strike", SkillType.ATTACK_ADJACENT, StatType.STR, 75, 3));
        units[94].setSkill(3, new Skill("Grow", SkillType.BOOST, StatType.MAG, 380, status: "STR"));

        units[95] = new Unit(95, "Alicorn", Rarity.Legendary, Element.Wind, new Unit.Stat(1233,1135,1211,1199,1267), sprites[95]);
        units[95].setSkill(1, new Skill("Blessing", SkillType.PROTECTION, extraEffect: 3));
        units[95].setSkill(2, new Skill("Holy Gallop", SkillType.ATTACK, StatType.MAG, 260, 2, "purify"));
        units[95].setSkill(3, new Skill("Striking Horn", SkillType.ATTACK, StatType.STR, 550, status: "stun"));

        units[96] = new Unit(96, "Grea", Rarity.Legendary, Element.Wind, new Unit.Stat(1217,1195,1243,1077,1288), sprites[96]);
        units[96].setSkill(1, new Skill("Enchanting Aura", SkillType.AOE_BOOST, StatType.MAG, 20, status: "STR,MAG"));
        units[96].setSkill(2, new Skill("Dive", SkillType.ATTACK_ADJACENT, StatType.STR, 160));
        units[96].setSkill(3, new Skill("Heavenly Scream", SkillType.AOE_ATTACK, StatType.MAG, 45, 2, "burn, poison, weaken"));

        units[97] = new Unit(97, "Ishar", Rarity.Legendary, Element.Wind, new Unit.Stat(1183,1279,934,1127,1000), sprites[97]);
        units[97].setSkill(1, new Skill("Minor Quake", SkillType.ATTACK_ADJACENT, StatType.STR, 85));
        units[97].setSkill(2, new Skill("Valiant Assault", SkillType.ATTACK, StatType.STR, 90, 6));
        units[97].setSkill(3, new Skill("Sky Speed", SkillType.BOOST, StatType.STR, 65, 4, "AGI"));

        units[98] = new Unit(98, "Ventoss", Rarity.Legendary, Element.Wind, new Unit.Stat(922,1088,1233,1100,1286), sprites[98]);
        units[98].setSkill(1, new Skill("Triple Shot", SkillType.ATTACK, StatType.AGI, 75, 3, "purge"));
        units[98].setSkill(2, new Skill("Surging Shot", SkillType.AOE_ATTACK, StatType.MAG, 115, status: "stun"));
        units[98].setSkill(3, new Skill("Agile", SkillType.AOE_DODGE, skillPower: 15));

        units[99] = new Unit(99, "Cerberus", Rarity.Legendary, Element.Fire, new Unit.Stat(1245,1233,1216,1246,1089), sprites[99]);
        units[99].setSkill(1, new Skill("Swipe", SkillType.AOE_ATTACK, StatType.STR, 50));
        units[99].setSkill(2, new Skill("Fireball", SkillType.ATTACK, StatType.MAG, 135, 3, "burn"));
        units[99].setSkill(3, new Skill("Guard Dog", SkillType.BOOST, StatType.STR, 80, 3, "DEF"));

        units[100] = new Unit(100, "Hell Frog", Rarity.Legendary, Element.Fire, new Unit.Stat(1116,1089,1300,1214,1093), sprites[100]);
        units[100].setSkill(1, new Skill("Leap", SkillType.ATTACK_ADJACENT, StatType.MAG, 66, 2, "weaken"));
        units[100].setSkill(2, new Skill("Hell Healing", SkillType.HEAL, StatType.MAG, 66, 6, "burn"));
        units[100].setSkill(3, new Skill("Hell Inferno", SkillType.ATTACK, StatType.MAG, 666, status: "burn, poison, charm"));

        units[101] = new Unit(101, "Ignis", Rarity.Legendary, Element.Fire, new Unit.Stat(1100,1214,1235,1112,1089), sprites[101]);
        units[101].setSkill(1, new Skill("Fire Attack", SkillType.ATTACK, StatType.STR, 220, status: "burn"));
        units[101].setSkill(2, new Skill("Fire Slash", SkillType.ATTACK, StatType.MAG, 135, 4, "burn"));
        units[101].setSkill(3, new Skill("Flame Barrier", SkillType.PROTECTION, extraEffect: 6));

        units[102] = new Unit(102, "Illnoct", Rarity.Legendary, Element.Fire, new Unit.Stat(1267,1055,1266,1234,1278), sprites[102]);
        units[102].setSkill(1, new Skill("Guard", SkillType.BOOST, StatType.STR, 155, status: "DEF"));
        units[102].setSkill(2, new Skill("Silence", SkillType.ATTACK, StatType.MAG, 285, status: "silence"));
        units[102].setSkill(3, new Skill("Ruin", SkillType.DEBUFF, StatType.MAG, 40, 6, "DEF"));

        units[103] = new Unit(103, "Aquarius", Rarity.Legendary, Element.Water, new Unit.Stat(960,1088,1235,980,1213), sprites[103]);
        units[103].setSkill(1, new Skill("Water Surge", SkillType.AOE_ATTACK, StatType.MAG, 35, status: "freeze"));
        units[103].setSkill(2, new Skill("Great Protection", SkillType.PROTECTION, extraEffect: 7));
        units[103].setSkill(3, new Skill("Magic Water", SkillType.AOE_BOOST, StatType.AGI, 30, 2, "MAG"));

        units[104] = new Unit(104, "Cancer", Rarity.Legendary, Element.Water, new Unit.Stat(1231,1266,922,1243,1149), sprites[104]);
        units[104].setSkill(1, new Skill("Pinch", SkillType.ATTACK, StatType.STR, 115, 2));
        units[104].setSkill(2, new Skill("Greater Pinch", SkillType.ATTACK, StatType.STR, 230, 2, "weaken"));
        units[104].setSkill(3, new Skill("Huge Pinch", SkillType.ATTACK, StatType.STR, 260, 2, "vulnerable, weaken"));

        units[105] = new Unit(105, "Death", Rarity.Legendary, Element.Water, new Unit.Stat(1174,1023,1288,1127,1163), sprites[105]);
        units[105].setSkill(1, new Skill("Frozen Attack", SkillType.ATTACK, StatType.MAG, 150, status: "freeze"));
        units[105].setSkill(2, new Skill("Silent Death", SkillType.AOE_ATTACK, StatType.MAG, 45, 2, "silence"));
        units[105].setSkill(3, new Skill("Fading Death", SkillType.DODGE, skillPower: 30));

        units[106] = new Unit(106, "King Fish", Rarity.Legendary, Element.Water, new Unit.Stat(1100,1238,988,1089,1123), sprites[106]);
        units[106].setSkill(1, new Skill("Dive", SkillType.ATTACK, StatType.STR, 130, 2));
        units[106].setSkill(2, new Skill("Splash", SkillType.ATTACK_ADJACENT, StatType.STR, 75, 2));
        units[106].setSkill(3, new Skill("Weakening Wave", SkillType.AOE_DEBUFF, StatType.STR, 25, status: "STR, MAG, AGI"));

        units[107] = new Unit(107, "Dynamo", Rarity.Legendary, Element.Thunder, new Unit.Stat(1247,1108,1195,1217,1144), sprites[107]);
        units[107].setSkill(1, new Skill("Lightning Bolt", SkillType.ATTACK, StatType.MAG, 220, status: "stun"));
        units[107].setSkill(2, new Skill("Snap", SkillType.AOE_ATTACK, StatType.MAG, 120, status: "burn, stun"));
        units[107].setSkill(3, new Skill("Magic Up", SkillType.AOE_BOOST, StatType.STR, 60, status: "MAG"));

        units[108] = new Unit(108, "Kronos", Rarity.Legendary, Element.Thunder, new Unit.Stat(982,1109,1207,1214,1300), sprites[108]);
        units[108].setSkill(1, new Skill("Regression", SkillType.AOE_BOOST, StatType.MAG, 25, status: "AGI"));
        units[108].setSkill(2, new Skill("Focused Shot", SkillType.AOE_DEBUFF, StatType.MAG, 40, status: "AGI"));
        units[108].setSkill(3, new Skill("Regression", SkillType.DEBUFF, StatType.AGI, 115, status: "HLT, STR, MAG"));

        units[109] = new Unit(109, "Nine-tailed Fox", Rarity.Legendary, Element.Thunder, new Unit.Stat(1045,1289,1088,1174,1003), sprites[109]);
        units[109].setSkill(1, new Skill("Petal Strike", SkillType.AOE_ATTACK, StatType.STR, 45));
        units[109].setSkill(2, new Skill("Tail Attack", SkillType.ATTACK, StatType.STR, 55, 9, "burn, vulnerable"));
        units[109].setSkill(3, new Skill("Haste", SkillType.AOE_BOOST, StatType.MAG, 60, status: "AGI"));

        units[110] = new Unit(110, "Zeus", Rarity.Legendary, Element.Thunder, new Unit.Stat(1285,1089,1217,1193,1211), sprites[110]);
        units[110].setSkill(1, new Skill("Thunder Bolt", SkillType.ATTACK, StatType.STR, 110, 2, "stun"));
        units[110].setSkill(2, new Skill("Thunderstorm", SkillType.AOE_ATTACK, StatType.MAG, 130, status: "stun"));
        units[110].setSkill(3, new Skill("Greater Thunderstorm", SkillType.AOE_ATTACK, StatType.MAG, 70, 2, "stun"));

        units[111] = new Unit(111, "The End", Rarity.Legendary, Element.Void, new Unit.Stat(1299,1248,1226,1237,988), sprites[111]);
        units[111].setSkill(1, new Skill("Silence All", SkillType.AOE_ATTACK, StatType.STR, 40, status: "silence"));
        units[111].setSkill(2, new Skill("Mass Drain", SkillType.AOE_ATTACK, StatType.MAG, 105, status: "vulnerable, weaken"));
        units[111].setSkill(3, new Skill("Devour", SkillType.ATTACK, StatType.STR, 600, status: "purge, freeze"));


    }

    public static Unit GetUnitById(int id) {
        return new Unit(units[id]);
    }

}
