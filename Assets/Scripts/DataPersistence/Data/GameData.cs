using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public int deathCount;

    public Vector3 playerPosition;

    public Vector3 cameraPosition;

    public Vector2 cameraMaxPosition;

    public Vector2 cameraMinPosition;

    public SerializableDictionary<string, string> slotsType;

    public SerializableDictionary<string, int> slotsCount;

    public SerializableDictionary<string, bool> builtHouses;


    public GameData()
    {
        this.deathCount = 0;
        playerPosition = Vector3.zero;
        cameraPosition = new Vector3(0,0,-10f);
        cameraMaxPosition = new Vector2(27.06f, 22.33f);
        cameraMinPosition = new Vector2(-24f, -25.4f);
        slotsType = new SerializableDictionary<string,string>();
        slotsCount = new SerializableDictionary<string, int>();
    }

    public int GetPercentageComplete()
    {
        return 0;
    }
}
