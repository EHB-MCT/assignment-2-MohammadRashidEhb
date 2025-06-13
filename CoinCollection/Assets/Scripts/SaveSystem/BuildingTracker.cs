using UnityEngine;

public static class BuildingTracker
{
    private const string TotalCountKey = "BuildingsVisited";
    
    public static void MarkBuildingVisited(string buildingName)
    {
        string buildingKey = $"Building_{buildingName}_Visited";
        
        if (!PlayerPrefs.HasKey(buildingKey))
        {
            PlayerPrefs.SetInt(buildingKey, 1);
            int currentTotal = PlayerPrefs.GetInt(TotalCountKey, 0);
            PlayerPrefs.SetInt(TotalCountKey, currentTotal + 1);
            PlayerPrefs.Save(); // Explicitly save after changes
        }
    }

    public static bool AllBuildingsVisited(int requiredCount)
    {
        return PlayerPrefs.GetInt(TotalCountKey, 0) >= requiredCount;
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(TotalCountKey);
        PlayerPrefs.Save(); // Ensure reset is saved
    }
}
