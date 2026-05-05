using System;

[System.Serializable]
public class PlayerData
{
    public int coins = 0;
    public int gems = 0;

    public int level = 1;
    public Int32 xp = 0;
    public int xpToLevelUp = 400;
    public string username;
    public string playerID;

    public int selectedCharacter = 0;

    public bool[] unlockedCharacters;

    public float soundVolume = 1f;
    public bool vibration = true;
    public bool sfx = true;
    public bool music = true;
    public int enemykilled = 0;
    public int nextenemykillingtarget = 40;

    public PlayerData()
    {
        unlockedCharacters = new bool[5];
        unlockedCharacters[0] = true; // default unlocked
    }
}