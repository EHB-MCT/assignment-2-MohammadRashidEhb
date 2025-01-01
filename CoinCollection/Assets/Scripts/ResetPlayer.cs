using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Transform resetPoint; // Drag the reset position here in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                // Disable and re-enable CharacterController to prevent glitches
                controller.enabled = false;
                other.transform.position = resetPoint.position;
                controller.enabled = true;
            }
        }
    }
}
