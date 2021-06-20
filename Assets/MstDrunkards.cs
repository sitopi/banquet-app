using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MstDrunkard
{
    public string name;
    public int level;
    public int hp;
    public int attack;
    public int move_speed;

}

[System.Serializable]
public class MstDrunkards
{
    public MstDrunkard[] drunkards;
}