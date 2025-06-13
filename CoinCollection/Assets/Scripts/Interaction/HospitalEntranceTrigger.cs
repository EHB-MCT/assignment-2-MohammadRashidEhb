using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalEntranceTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "ScavengerHunt";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🏥 Entering hospital, loading: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
