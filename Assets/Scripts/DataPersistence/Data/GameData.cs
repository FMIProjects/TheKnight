using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;

    public Vector3 playerPosition;

    public Vector3 cameraPosition;

    public Vector2 cameraMaxPosition;

    public Vector2 cameraMinPosition;

    public GameData()
    {
        this.deathCount = 0;
        playerPosition = Vector3.zero;
        cameraPosition = Vector3.zero;
        cameraMaxPosition = new Vector2(27.06f, 22.33f);
        cameraMinPosition = new Vector2(-24f, -25.4f);
    }
}
