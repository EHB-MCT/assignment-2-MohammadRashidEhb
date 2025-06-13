using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] sections;
    [SerializeField] private GameObject StartSection;
    [SerializeField] private float sectionLength = 85f;
    [SerializeField] private int maxSections = 5;

    [Header("Player")]
    [SerializeField] private Transform player;

    private float zSpawnPos = 85f;
    private List<GameObject> activeSections = new List<GameObject>();
    private bool isSpawning = false;
    private string difficulty;

    private void Start()
    {
        difficulty = PlayerPrefs.GetString("difficulty", "medium");

        if (StartSection != null)
        {
            activeSections.Add(StartSection);
        }
    }

    private void Update()
    {
        float distanceToEnd = zSpawnPos - player.position.z;

        if (distanceToEnd < 100f && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnSection());
        }

        CleanupSections();
    }

    private IEnumerator SpawnSection()
    {
        int maxIndex = (difficulty == "hard") ? sections.Length : Mathf.Min(2, sections.Length);

        int index = Random.Range(0, maxIndex);
        GameObject newSection = Instantiate(
            sections[index],
            new Vector3(0f, 0f, zSpawnPos),
            Quaternion.identity
        );

        activeSections.Add(newSection);
        zSpawnPos += sectionLength;

        yield return new WaitForSeconds(0.1f);
        isSpawning = false;
    }

    private void CleanupSections()
    {
        if (activeSections.Count > maxSections)
        {
            GameObject oldSection = activeSections[0];
            activeSections.RemoveAt(0);
            Destroy(oldSection);
        }
    }
}
