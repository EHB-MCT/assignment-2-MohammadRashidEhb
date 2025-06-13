using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 lastCheckpointPosition;
    private GameObject playerArmature;
    public int fallCount = 0; 


    void Awake()
    {
        playerArmature = GameObject.Find("PlayerArmature");
        // Start with no checkpoint (will use scene's default spawn)
        lastCheckpointPosition = playerArmature.transform.position;
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;
        Debug.Log($"[PlayerRespawn] Checkpoint set to: {checkpointPosition}");
    }

    public void Respawn()
    {
        fallCount++;
        if (playerArmature == null) return;

        CharacterController cc = playerArmature.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        playerArmature.transform.position = lastCheckpointPosition;

        if (cc != null) cc.enabled = true;
    }
}
