using UnityEngine;

public class Fallzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                RespawnUIController.Instance.ShowRespawnSequence(() => playerRespawn.Respawn());
                Debug.Log("[Fallzone] Player fell and was respawned.");
            }
        }
    }
}
