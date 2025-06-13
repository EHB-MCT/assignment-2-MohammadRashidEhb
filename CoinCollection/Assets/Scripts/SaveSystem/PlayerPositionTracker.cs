using UnityEngine;
public static class PlayerPositionTracker
{
    private const string PositionKey = "LastCityPosition";

    public static Vector3 LastCityPosition
    {
        get => JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(PositionKey, "{}"));
        set => PlayerPrefs.SetString(PositionKey, JsonUtility.ToJson(value));
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteKey(PositionKey);
    }
}
