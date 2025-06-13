using UnityEngine;

public class CollectItem : MonoBehaviour
{
    [SerializeField] private AudioSource collectFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[CollectItem] {other.name} collected {gameObject.name}");

            if (collectFX != null)
            {
                collectFX.Play();
            }

            ScoreCount.itemCount++;
            gameObject.SetActive(false);
        }
    }
}
