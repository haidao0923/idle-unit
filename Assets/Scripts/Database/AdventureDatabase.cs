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

    void InitializeAdventures() {
        adventures[0] = new Adventure(0, "Haunted Forest 1", 10, new int[] {0,3,8,13}, -7, -5,
        new int[] {8,8,8,8,8}, new int[] {-2,-2,-2,-2,-2}, new int[] {13,0,2,0,13}, new int[] {0,0,1,0,0}, sprites[0],
        new Reward[] {new Reward(RewardType.CARD, 1, 50), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 10000),
        new Reward(RewardType.COIN, 15000), new Reward(RewardType.COIN, 20000), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.COIN, 40000), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.COIN, 70000), new Reward(RewardType.COIN, 100000), new Reward(RewardType.COIN, 200000)});

        adventures[1] = new Adventure(1, "Haunted Forest 2", 15, new int[] {0,2,3,13}, -5, -3,
        new int[] {0,3,13,3,0,3,3,-1,3,3}, new int[] {-1,-1,1,-1,-1,-1,-1,-1,-1,-1}, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, new int[] {0,0,1,0,0,0,0,0,0,0}, sprites[1],
        new Reward[] {new Reward(RewardType.COIN, 1000), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 10000),
        new Reward(RewardType.COIN, 15000), new Reward(RewardType.COIN, 20000), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.COIN, 40000), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.COIN, 70000), new Reward(RewardType.COIN, 100000), new Reward(RewardType.COIN, 200000)});
    }

    void InitializeChallengeAdventures() {
        challengeAdventures[0] = new Adventure(0, "Haunted Forest 1", 10, new int[] {0,3,8,13}, -5, -3,
        new int[] {8,8,8,8,8}, new int[] {-1,-1,-1,-1,-1}, new int[] {13,0,2,0,13}, new int[] {0,0,1,0,0}, sprites[0],
        new Reward[] {new Reward(RewardType.COIN, 1000), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 10000),
        new Reward(RewardType.COIN, 15000), new Reward(RewardType.COIN, 20000), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.COIN, 40000), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.COIN, 70000), new Reward(RewardType.COIN, 100000), new Reward(RewardType.COIN, 200000)});

        challengeAdventures[1] = new Adventure(1, "Haunted Forest 2", 15, new int[] {0,2,3,13}, -3, -1,
        new int[] {0,3,13,3,0,3,3,-1,3,3}, new int[] {1,1,3,1,1,1,1,1,1,1}, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, new int[] {2,2,5,2,2,2,2,2,2,2}, sprites[1],
        new Reward[] {new Reward(RewardType.COIN, 1000), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 10000),
        new Reward(RewardType.COIN, 15000), new Reward(RewardType.COIN, 20000), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.COIN, 40000), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.COIN, 70000), new Reward(RewardType.COIN, 100000), new Reward(RewardType.COIN, 200000)});
    }
    void InitializeAscendedAdventures() {
        ascendedAdventures[0] = new Adventure(0, "Haunted Forest 1", 10, new int[] {0,3,8,13}, -5, -3,
        new int[] {8,8,8,8,8}, new int[] {-1,-1,-1,-1,-1}, new int[] {13,0,2,0,13}, new int[] {0,0,1,0,0}, sprites[0],
        new Reward[] {new Reward(RewardType.COIN, 1000), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 10000),
        new Reward(RewardType.COIN, 15000), new Reward(RewardType.COIN, 20000), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.COIN, 40000), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.COIN, 70000), new Reward(RewardType.COIN, 100000), new Reward(RewardType.COIN, 200000)});

        ascendedAdventures[1] = new Adventure(1, "Haunted Forest 2", 15, new int[] {0,2,3,13}, -3, -1,
        new int[] {0,3,13,3,0,3,3,-1,3,3}, new int[] {1,1,3,1,1,1,1,1,1,1}, new int[] {2,2,15,2,2,-1,2,-1,2,-1}, new int[] {2,2,5,2,2,2,2,2,2,2}, sprites[1],
        new Reward[] {new Reward(RewardType.COIN, 1000), new Reward(RewardType.COIN, 5000), new Reward(RewardType.COIN, 10000),
        new Reward(RewardType.COIN, 15000), new Reward(RewardType.COIN, 20000), new Reward(RewardType.COIN, 25000),
        new Reward(RewardType.COIN, 30000), new Reward(RewardType.COIN, 40000), new Reward(RewardType.COIN, 50000),
        new Reward(RewardType.COIN, 70000), new Reward(RewardType.COIN, 100000), new Reward(RewardType.COIN, 200000)});
    }
}
