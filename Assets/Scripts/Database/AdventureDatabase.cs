using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureDatabase : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[15];
    public static Adventure[] adventures = new Adventure[40];
    public static Adventure[] challengeAdventures = new Adventure[40];
    public static Adventure[] ascendedAdventures = new Adventure[40];

    void Awake()
    {
        InitializeAdventures();
        InitializeChallengeAdventures();
        InitializeAscendedAdventures();
    }
    // {1000, 2000, 5000, 8000, 12000, 16000, 20000, 25000, 35000, 50000, 75000, 100000}
    // Elite should be 4-6 higher and boss should be 6-8 higher
    void InitializeAdventures() {
        adventures[0] = new Adventure(0, "Haunted Forest 1", 10, new int[] {0,8,13}, -7, -5,
        new int[] {8,8,8,8,8}, LevelAdvantage.BOSS, new int[] {13,0,2,0,13}, LevelAdvantage.BOSS, sprites[0],
        new Reward[] {new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.POTION, 3, 0),
        new Reward(RewardType.CARD, 5, 2), new Reward(RewardType.COIN, 20000), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.CARD, 10, 2), new Reward(RewardType.COIN, 40000), new Reward(RewardType.POTION, 2, 1),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 15, 8), new Reward(RewardType.COIN, 100000)});

        adventures[1] = new Adventure(1, "Haunted Forest 2", 15, new int[] {0,2,3}, -5, -3,
        new int[] {0,3,13,3,0,-1,-1,3,-1,-1}, LevelAdvantage.BOSS, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, LevelAdvantage.BOSS, sprites[1],
        new Reward[] {new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.POTION, 3, 0), new Reward(RewardType.MISC, 5, 2), new Reward(RewardType.STONE, 8, 0),
        new Reward(RewardType.CARD, 1, 15), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 2, 15),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 3, 15), new Reward(RewardType.COIN, 100000)});

        adventures[2] = new Adventure(2, "Haunted Forest 3", 20, new int[] {0,3,13}, -3, -1,
        new int[] {8,0,8,0,8,-1,8,-1,8,-1}, LevelAdvantage.BOSS, new int[] {3,13,25,13,3}, LevelAdvantage.BOSS, sprites[2],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.CARD, 5, 8), new Reward(RewardType.STONE, 5, 0),
        new Reward(RewardType.CAP, 5, 0), new Reward(RewardType.CARD, 1, 25), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.COIN, 40000), new Reward(RewardType.POTION, 8, 0),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 3, 25), new Reward(RewardType.COIN, 100000)});

        adventures[3] = new Adventure(3, "Haunted Forest 4", 25, new int[] {0,3,8}, -4, -2,
        new int[] {0,0,25,0,0}, LevelAdvantage.BOSS, new int[] {3,25,18,25,3}, LevelAdvantage.BOSS, sprites[3],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.CARD, 1, 25),
        new Reward(RewardType.CAP, 2, 1), new Reward(RewardType.COIN, 20000), new Reward(RewardType.POTION, 1, 1),
        new Reward(RewardType.COIN, 35000), new Reward(RewardType.STONE, 10, 0), new Reward(RewardType.CARD, 2, 18),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 3, 18), new Reward(RewardType.COIN, 100000)});

        adventures[4] = new Adventure(4, "Haunted Forest 5", 30, new int[] {3,13,18,25}, -1, 2,
        new int[] {8,8,19,8,8}, LevelAdvantage.BOSS, new int[] {19,19,14,19,19,8,8,-1,8,8}, LevelAdvantage.BOSS, sprites[4],
        new Reward[] {new Reward(RewardType.CARD, 3, 14), new Reward(RewardType.COIN, 5000), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.CAP, 8, 0), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.CARD, 2, 19), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 12, 14),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.POTION, 3, 1), new Reward(RewardType.COIN, 100000)});

        adventures[5] = new Adventure(5, "Thunder City 1", 15, new int[] {9,13}, 9, 11,
        new int[] {12,12,12,12,12}, LevelAdvantage.ALL, new int[] {12,12,34,12,12}, LevelAdvantage.BOSS, sprites[5],
        new Reward[] {new Reward(RewardType.POTION, 1, 0), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.MISC, 5, 2),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.CARD, 5, 12), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.CARD, 10, 12), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 2, 34),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.COIN, 75000), new Reward(RewardType.COIN, 100000)});

        adventures[6] = new Adventure(6, "Thunder City 2", 20, new int[] {9,10,13}, 11, 13,
        new int[] {10,10,17,10,10}, LevelAdvantage.BOSS, new int[] {13,10,41,10,13,13,-1,-1,-1,13}, LevelAdvantage.BOSS, sprites[6],
        new Reward[] {new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CARD, 1, 41),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.POTION, 1, 1), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.CARD, 2, 17), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 2, 41),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.STONE, 15, 0), new Reward(RewardType.COIN, 100000)});

        adventures[7] = new Adventure(7, "Thunder City 3", 25, new int[] {10,13}, 13, 15,
        new int[] {10,10,34,10,10}, LevelAdvantage.BOSS, new int[] {16,16,42,16,16}, LevelAdvantage.BOSS, sprites[7],
        new Reward[] {new Reward(RewardType.STONE, 3, 0), new Reward(RewardType.CARD, 1, 34), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.CAP, 1, 1), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 1, 16),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.CARD, 2, 42), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.POTION, 1, 2), new Reward(RewardType.COIN, 100000)});

        adventures[8] = new Adventure(8, "Thunder City 4", 30, new int[] {16}, 15, 17,
        new int[] {41,41,41,41,41}, LevelAdvantage.ALL, new int[] {42,42,40,42,42}, LevelAdvantage.BOSS, sprites[8],
        new Reward[] {new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.COIN, 5000), new Reward(RewardType.STONE, 5, 0),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.CARD, 1, 41), new Reward(RewardType.CAP, 3, 1),
        new Reward(RewardType.MISC, 8, 2), new Reward(RewardType.CARD, 2, 41), new Reward(RewardType.CARD, 2, 40),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.COIN, 75000), new Reward(RewardType.COIN, 100000)});

        adventures[9] = new Adventure(9, "Thunder City 5", 35, new int[] {10,16,41}, 17, 19,
        new int[] {16,16,42,16,16}, LevelAdvantage.BOSS, new int[] {37,37,37,37,37}, LevelAdvantage.BOSS, sprites[9],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CARD, 1, 42),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CAP, 8, 0), new Reward(RewardType.POTION, 1, 1),
        new Reward(RewardType.CARD, 2, 37), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 2, 37),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.STONE, 10, 1), new Reward(RewardType.COIN, 100000)});

        adventures[10] = new Adventure(10, "Color Metropolis 1", 20, new int[] {17,23}, 30, 32,
        new int[] {17,20,20,20,17}, LevelAdvantage.BOSS, new int[] {23,23,24,23,23}, LevelAdvantage.BOSS, sprites[10],
        new Reward[] {new Reward(RewardType.POTION, 1, 0), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.CAP, 2, 1), new Reward(RewardType.CARD, 1, 23), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 2, 24),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 3, 20), new Reward(RewardType.COIN, 100000)});

        adventures[11] = new Adventure(11, "Color Metropolis 2", 25, new int[] {9,10,36}, 32, 34,
        new int[] {36,10,33,10,36}, LevelAdvantage.BOSS, new int[] {9,9,38,9,9,36,-1,-1,-1,36}, LevelAdvantage.BOSS, sprites[11],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.CARD, 1, 33), new Reward(RewardType.POTION, 4, 0), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.MISC, 6, 2), new Reward(RewardType.STONE, 8, 0), new Reward(RewardType.CARD, 2, 33),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 3, 38), new Reward(RewardType.COIN, 100000)});

        adventures[12] = new Adventure(12, "Color Metropolis 3", 30, new int[] {10,26,33}, 34, 36,
        new int[] {26,21,21,21,26}, LevelAdvantage.ALL, new int[] {39,39,39,39,39,39,39,39,39,39}, LevelAdvantage.ALL, sprites[12],
        new Reward[] {new Reward(RewardType.MISC, 2, 2), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CARD, 1, 39),
        new Reward(RewardType.CAP, 1, 1), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 2, 26),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.CARD, 2, 21), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.COIN, 75000), new Reward(RewardType.COIN, 100000)});

        adventures[13] = new Adventure(13, "Color Metropolis 4", 35, new int[] {8,14,21}, 36, 38,
        new int[] {8,14,30,14,8}, LevelAdvantage.BOSS, new int[] {14,21,79,21,14}, LevelAdvantage.BOSS, sprites[13],
        new Reward[] {new Reward(RewardType.CAP, 1, 2), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CARD, 1, 30),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.CARD, 1, 30), new Reward(RewardType.POTION, 1, 1),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.COIN, 40000), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 79), new Reward(RewardType.COIN, 100000)});

        adventures[14] = new Adventure(14, "Color Metropolis 5", 40, new int[] {27,29,32}, 38, 40,
        new int[] {49,49,49,49,49}, LevelAdvantage.ALL, new int[] {32,49,52,29,27}, LevelAdvantage.BOSS, sprites[14],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.CAP, 3, 1), new Reward(RewardType.CARD, 1, 32),
        new Reward(RewardType.CARD, 2, 27), new Reward(RewardType.POTION, 4, 0), new Reward(RewardType.CARD, 2, 29),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 1, 49),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 52), new Reward(RewardType.COIN, 100000)});

        adventures[15] = new Adventure(15, "Lost Sand 1", 25, new int[] {1,6}, 50, 52,
        new int[] {6,1,31,1,6,-1,1,-1,1,-1}, LevelAdvantage.BOSS, new int[] {6,6,53,6,6}, LevelAdvantage.BOSS, sprites[15],
        new Reward[] {new Reward(RewardType.CAP, 2, 1), new Reward(RewardType.CARD, 1, 31), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.CAP, 1, 2), new Reward(RewardType.COIN, 20000), new Reward(RewardType.CARD, 2, 31),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.CARD, 1, 53),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.STONE, 10, 1), new Reward(RewardType.COIN, 100000)});

        adventures[16] = new Adventure(16, "Lost Sand 2", 30, new int[] {21,30}, 52, 54,
        new int[] {30,30,79,30,30}, LevelAdvantage.BOSS, new int[] {21,21,22,21,21}, LevelAdvantage.BOSS, sprites[16],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.POTION, 2, 0),
        new Reward(RewardType.CARD, 1, 30), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.CARD, 2, 22), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 1, 79),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CAP, 2, 2), new Reward(RewardType.COIN, 100000)});

        adventures[17] = new Adventure(17, "Lost Sand 3", 35, new int[] {1,5,6}, 54, 56,
        new int[] {6,6,35,6,6}, LevelAdvantage.BOSS, new int[] {1,35,63,35,1,5,5,-1,5,5}, LevelAdvantage.BOSS, sprites[17],
        new Reward[] {new Reward(RewardType.CAP, 2, 1), new Reward(RewardType.CARD, 3, 6), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.MISC, 4, 2), new Reward(RewardType.COIN, 20000), new Reward(RewardType.STONE, 5, 0),
        new Reward(RewardType.CARD, 1, 35), new Reward(RewardType.COIN, 40000), new Reward(RewardType.STONE, 6, 1),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 63), new Reward(RewardType.COIN, 100000)});

        adventures[18] = new Adventure(18, "Lost Sand 4", 40, new int[] {1,5,6}, 56, 58,
        new int[] {6,6,68,6,6}, LevelAdvantage.BOSS, new int[] {1,68,55,68,1}, LevelAdvantage.BOSS, sprites[18],
        new Reward[] {new Reward(RewardType.STONE, 3, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CARD, 4, 1),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.CAP, 5, 1), new Reward(RewardType.POTION, 1, 1),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 1, 55), new Reward(RewardType.CARD, 1, 68),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.COIN, 75000), new Reward(RewardType.COIN, 100000)});

        adventures[19] = new Adventure(19, "Lost Sand 5", 45, new int[] {1,5,6}, 58, 60,
        new int[] {55,5,55,5,55,5,-1,-1,-1,5}, LevelAdvantage.ALL, new int[] {55,55,59,55,55}, LevelAdvantage.BOSS, sprites[19],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.CAP, 2, 1),
        new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.POTION, 4, 0), new Reward(RewardType.CARD, 5, 5),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.CARD, 1, 55), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 59), new Reward(RewardType.COIN, 100000)});

        adventures[20] = new Adventure(20, "Hidden Field 1", 30, new int[] {0,1}, 70, 72,
        new int[] {0,0,22,0,0}, LevelAdvantage.BOSS, new int[] {22,0,51,0,22}, LevelAdvantage.BOSS, sprites[20],
        new Reward[] {new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.CAP, 5, 0), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.CARD, 1, 22), new Reward(RewardType.COIN, 20000), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.CARD, 2, 22), new Reward(RewardType.POTION, 2, 2), new Reward(RewardType.CARD, 1, 51),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.COIN, 75000), new Reward(RewardType.COIN, 100000)});

        adventures[21] = new Adventure(21, "Hidden Field 2", 35, new int[] {0,6}, 72, 74,
        new int[] {21,21,21,21,21}, LevelAdvantage.ALL, new int[] {50,50,50,50,50}, LevelAdvantage.ALL, sprites[21],
        new Reward[] {new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.CARD, 5, 0),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.CARD, 2, 21), new Reward(RewardType.CARD, 1, 50), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CAP, 8, 1), new Reward(RewardType.COIN, 100000)});

        adventures[22] = new Adventure(22, "Hidden Field 3", 40, new int[] {13,30}, 74, 76,
        new int[] {13,13,23,13,13}, LevelAdvantage.ALL, new int[] {61,13,61,13,61}, LevelAdvantage.ALL, sprites[22],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.CARD, 1, 30), new Reward(RewardType.STONE, 5, 0),
        new Reward(RewardType.CAP, 3, 1), new Reward(RewardType.COIN, 20000), new Reward(RewardType.CARD, 2, 23),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CAP, 1, 2),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 61), new Reward(RewardType.COIN, 100000)});

        adventures[23] = new Adventure(23, "Hidden Field 4", 45, new int[] {7}, 76, 78,
        new int[] {7,7,63,7,7}, LevelAdvantage.BOSS, new int[] {7,7,58,7,7,7,7,-1,7,7}, LevelAdvantage.BOSS, sprites[23],
        new Reward[] {new Reward(RewardType.STONE, 3, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CAP, 5, 1), new Reward(RewardType.CARD, 1, 63),
        new Reward(RewardType.CARD, 1, 58), new Reward(RewardType.CAP, 10, 0), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 15, 7), new Reward(RewardType.COIN, 100000)});

        adventures[24] = new Adventure(24, "Hidden Field 5", 50, new int[] {7,13,21,22}, 78, 80,
        new int[] {7,7,7,7,7,13,21,-1,22,13}, LevelAdvantage.ALL, new int[] {13,13,91,13,13}, LevelAdvantage.BOSS, sprites[24],
        new Reward[] {new Reward(RewardType.MISC, 2, 2), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.CARD, 4, 13),
        new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.POTION, 4, 0), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.CARD, 1, 22), new Reward(RewardType.COIN, 40000), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 91), new Reward(RewardType.COIN, 100000)});

        adventures[25] = new Adventure(25, "Rainbow Island 1", 35, new int[] {48,56}, 90, 92,
        new int[] {45,45,45,45,45}, LevelAdvantage.ALL, new int[] {48,48,57,48,48,56,56,57,56,56}, LevelAdvantage.BOSS, sprites[25],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.CARD, 1, 56), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.COIN, 20000), new Reward(RewardType.CARD, 1, 48),
        new Reward(RewardType.MISC, 8, 2), new Reward(RewardType.CAP, 8, 0), new Reward(RewardType.CARD, 1, 45),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 57), new Reward(RewardType.COIN, 100000)});

        adventures[26] = new Adventure(26, "Rainbow Island 2", 40, new int[] {27,32}, 92, 94,
        new int[] {23,27,23,27,23}, LevelAdvantage.ALL, new int[] {27,32,60,32,27}, LevelAdvantage.BOSS, sprites[26],
        new Reward[] {new Reward(RewardType.STONE, 3, 0), new Reward(RewardType.CAP, 3, 1), new Reward(RewardType.CARD, 1, 23),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 2, 23),
        new Reward(RewardType.POTION, 6, 0), new Reward(RewardType.CARD, 1, 60), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.COIN, 75000), new Reward(RewardType.COIN, 100000)});

        adventures[27] = new Adventure(27, "Rainbow Island 3", 45, new int[] {78,84}, 94, 96,
        new int[] {78,78,82,78,78}, LevelAdvantage.ALL, new int[] {84,84,81,84,84}, LevelAdvantage.BOSS, sprites[27],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.STONE, 3, 0),
        new Reward(RewardType.CAP, 3, 1), new Reward(RewardType.COIN, 20000), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.CARD, 1, 78), new Reward(RewardType.CARD, 1, 82), new Reward(RewardType.CARD, 1, 81),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 84), new Reward(RewardType.COIN, 100000)});

        adventures[28] = new Adventure(28, "Rainbow Island 4", 50, new int[] {71,76}, 96, 98,
        new int[] {72,72,72,72,72}, LevelAdvantage.ALL, new int[] {76,71,73,71,76}, LevelAdvantage.BOSS, sprites[28],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.CARD, 1, 76), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 1, 71), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.POTION, 6, 0), new Reward(RewardType.MISC, 10, 2), new Reward(RewardType.CARD, 1, 72),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 73), new Reward(RewardType.COIN, 100000)});

        adventures[29] = new Adventure(29, "Rainbow Island 5", 55, new int[] {67,69}, 98, 100,
        new int[] {69,69,80,69,69,-1,-1,80,-1,-1}, LevelAdvantage.BOSS, new int[] {69,69,96,69,69}, LevelAdvantage.BOSS, sprites[29],
        new Reward[] {new Reward(RewardType.STONE, 3, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CAP, 1, 2),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.COIN, 20000), new Reward(RewardType.POTION, 1, 1),
        new Reward(RewardType.STONE, 10, 0), new Reward(RewardType.CARD, 1, 80), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 96), new Reward(RewardType.COIN, 100000)});

        adventures[30] = new Adventure(30, "Burning Town 1", 40, new int[] {52,75}, 110, 112,
        new int[] {75,75,74,75,75}, LevelAdvantage.BOSS, new int[] {83,83,83,83,83}, LevelAdvantage.ALL, sprites[30],
        new Reward[] {new Reward(RewardType.CAP, 5, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.COIN, 20000), new Reward(RewardType.POTION, 1, 1),
        new Reward(RewardType.CARD, 1, 75), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 1, 74),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 83), new Reward(RewardType.COIN, 100000)});

        adventures[31] = new Adventure(31, "Burning Town 2", 45, new int[] {66}, 112, 114,
        new int[] {66,66,67,66,66}, LevelAdvantage.BOSS, new int[] {66,66,69,66,66,66,-1,-1,-1,66}, LevelAdvantage.BOSS, sprites[31],
        new Reward[] {new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.POTION, 3, 0), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.CARD, 1, 66), new Reward(RewardType.STONE, 10, 0),
        new Reward(RewardType.POTION, 6, 0), new Reward(RewardType.CARD, 1, 67), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 69), new Reward(RewardType.COIN, 100000)});

        adventures[32] = new Adventure(32, "Burning Town 3", 50, new int[] {45,66}, 114, 116,
        new int[] {64,64,64,64,64}, LevelAdvantage.ALL, new int[] {66,45,65,45,66}, LevelAdvantage.BOSS, sprites[32],
        new Reward[] {new Reward(RewardType.STONE, 3, 1), new Reward(RewardType.MISC, 3, 2), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.CAP, 5, 0), new Reward(RewardType.CARD, 1, 45), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.CARD, 1, 64), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 65), new Reward(RewardType.COIN, 100000)});

        adventures[33] = new Adventure(33, "Burning Town 4", 55, new int[] {45, 47}, 116, 118,
        new int[] {47,47,46,47,47,47,47,-1,47,47}, LevelAdvantage.BOSS, new int[] {46,46,46,46,46,47,47,92,47,47}, LevelAdvantage.BOSS, sprites[33],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.STONE, 5, 0),
        new Reward(RewardType.COIN, 10000), new Reward(RewardType.CARD, 1, 47), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.CAP, 1, 2), new Reward(RewardType.CARD, 1, 46), new Reward(RewardType.POTION, 10, 0),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 92), new Reward(RewardType.COIN, 100000)});

        adventures[34] = new Adventure(34, "Burning Town 5", 60, new int[] {46,47}, 118, 120,
        new int[] {47,46,92,46,47}, LevelAdvantage.BOSS, new int[] {47,92,107,92,46}, LevelAdvantage.BOSS, sprites[34],
        new Reward[] {new Reward(RewardType.POTION, 1, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CARD, 1, 47),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.COIN, 20000), new Reward(RewardType.CAP, 1, 2),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 1, 46),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 107), new Reward(RewardType.COIN, 100000)});

        adventures[35] = new Adventure(35, "Void Zone 1", 45, new int[] {43,85}, 130, 132,
        new int[] {43,43,43,43,43}, LevelAdvantage.ALL, new int[] {85,85,85,85,85}, LevelAdvantage.ALL, sprites[35],
        new Reward[] {new Reward(RewardType.POTION, 2, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.STONE, 5, 0),
        new Reward(RewardType.CARD, 2, 43), new Reward(RewardType.COIN, 20000), new Reward(RewardType.CAP, 1, 2),
        new Reward(RewardType.CARD, 1, 85), new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 1, 85),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.COIN, 75000), new Reward(RewardType.COIN, 100000)});

        adventures[36] = new Adventure(36, "Void Zone 2", 50, new int[] {43,85,86}, 132, 134,
        new int[] {43,85,86,85,43}, LevelAdvantage.BOSS, new int[] {86,86,86,86,86,86,86,86,86,86}, LevelAdvantage.ALL, sprites[36],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.POTION, 3, 0), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 1, 85), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.CARD, 1, 86), new Reward(RewardType.CARD, 1, 86), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.POTION, 3, 1), new Reward(RewardType.COIN, 100000)});

        adventures[37] = new Adventure(37, "Void Zone 3", 55, new int[] {43,85,86}, 134, 136,
        new int[] {85,86,86,86,85,85,-1,-1,-1,85}, LevelAdvantage.ALL, new int[] {85,85,87,86,86,-1,87,87,87,-1}, LevelAdvantage.BOSS, sprites[37],
        new Reward[] {new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.COIN, 5000), new Reward(RewardType.STONE, 5, 1),
        new Reward(RewardType.CAP, 5, 1), new Reward(RewardType.CARD, 1, 86), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.CAP, 1, 2), new Reward(RewardType.CARD, 1, 87),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 87), new Reward(RewardType.COIN, 100000)});

        adventures[38] = new Adventure(38, "Void Zone 4", 60, new int[] {85,86,87}, 136, 138,
        new int[] {88,88,88,88,88}, LevelAdvantage.ALL, new int[] {87,87,89,87,87}, LevelAdvantage.BOSS, sprites[38],
        new Reward[] {new Reward(RewardType.MISC, 2, 2), new Reward(RewardType.CAP, 5, 0), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.STONE, 5, 0), new Reward(RewardType.CARD, 1, 88), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.CARD, 1, 88), new Reward(RewardType.CARD, 1, 89),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.STONE, 15, 1), new Reward(RewardType.COIN, 100000)});

        adventures[39] = new Adventure(39, "Void Zone 5", 65, new int[] {14,37,79,91,89}, 138, 140,
        new int[] {89,89,96,89,89}, LevelAdvantage.ALL, new int[] {89,89,111,89,89,14,79,-1,37,91}, LevelAdvantage.BOSS, sprites[39],
        new Reward[] {new Reward(RewardType.COIN, 3000), new Reward(RewardType.COIN, 5000), new Reward(RewardType.CARD, 1, 37),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CAP, 1, 2), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.STONE, 5, 1), new Reward(RewardType.CARD, 1, 79), new Reward(RewardType.CAP, 1, 2),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 1, 111), new Reward(RewardType.COIN, 100000)});
    }

    void InitializeChallengeAdventures() {
        challengeAdventures[0] = new Adventure(0, "Haunted Forest 1", 10, new int[] {0,3,8,13}, -5, -3,
        new int[] {8,0,8,0,8,-1,8,-1,8,-1}, LevelAdvantage.BOSS, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, LevelAdvantage.BOSS, sprites[0],
        new Reward[] {new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.POTION, 3, 0), new Reward(RewardType.MISC, 5, 2), new Reward(RewardType.STONE, 8, 0),
        new Reward(RewardType.CARD, 1, 15), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 1, 15),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 2, 15), new Reward(RewardType.COIN, 100000)});

        challengeAdventures[1] = new Adventure(1, "Haunted Forest 2", 15, new int[] {0,2,3,13}, -3, -1,
        new int[] {8,0,8,0,8,-1,8,-1,8,-1}, LevelAdvantage.BOSS, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, LevelAdvantage.BOSS, sprites[1],
        new Reward[] {new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.POTION, 3, 0), new Reward(RewardType.MISC, 5, 2), new Reward(RewardType.STONE, 8, 0),
        new Reward(RewardType.CARD, 1, 15), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 1, 15),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 2, 15), new Reward(RewardType.COIN, 100000)});
    }
    void InitializeAscendedAdventures() {
        ascendedAdventures[0] = new Adventure(0, "Haunted Forest 1", 10, new int[] {0,3,8,13}, -5, -3,
        new int[] {8,0,8,0,8,-1,8,-1,8,-1}, LevelAdvantage.BOSS, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, LevelAdvantage.BOSS, sprites[0],
        new Reward[] {new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.POTION, 3, 0), new Reward(RewardType.MISC, 5, 2), new Reward(RewardType.STONE, 8, 0),
        new Reward(RewardType.CARD, 1, 15), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 1, 15),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 2, 15), new Reward(RewardType.COIN, 100000)});

        ascendedAdventures[1] = new Adventure(1, "Haunted Forest 2", 15, new int[] {0,2,3,13}, -3, -1,
        new int[] {8,0,8,0,8,-1,8,-1,8,-1}, LevelAdvantage.BOSS, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, LevelAdvantage.BOSS, sprites[1],
        new Reward[] {new Reward(RewardType.CAP, 3, 0), new Reward(RewardType.STONE, 2, 1), new Reward(RewardType.COIN, 8000),
        new Reward(RewardType.POTION, 3, 0), new Reward(RewardType.MISC, 5, 2), new Reward(RewardType.STONE, 8, 0),
        new Reward(RewardType.CARD, 1, 15), new Reward(RewardType.COIN, 40000), new Reward(RewardType.CARD, 1, 15),
        new Reward(RewardType.STONE, 1, 2), new Reward(RewardType.CARD, 2, 15), new Reward(RewardType.COIN, 100000)});
    }
}
