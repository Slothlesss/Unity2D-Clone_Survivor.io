using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent
{
    public int PresentTime { get; private set; }
    public string MonsterType { get; private set; }
    public int NumberMonster { get; private set; }
    public float CreateTime { get; private set; }

    public int MaxMonster { get; private set; }
    public WaveEvent(int presentTime, string monsterType, int numberMonster, float createTime, int maxMonster)
    {
        this.PresentTime = presentTime;
        this.MonsterType = monsterType;
        this.NumberMonster = numberMonster;
        this.CreateTime = createTime;
        this.MaxMonster = maxMonster;
    }   
}
