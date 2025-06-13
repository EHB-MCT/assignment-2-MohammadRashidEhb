using UnityEngine;
using UnityEngine.Video;
using StarterAssets;

public class VideoPlayTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject targetCube;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    private Camera mainCam;
    private bool isLookingAtRemote = false;

    [System.Obsolete]
    private void Start()
    {
        mainCam = Camera.main;
        if (starterAssetsInputs == null)
            starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
    }

    private void Update()
    {
        CheckRemoteInteraction();

        if (starterAssetsInputs != null && starterAssetsInputs.interact && isLookingAtRemote)
        {
            // Toggle play/pause
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                Debug.Log("⏸ Video paused.");
            }
            else
            {
                videoPlayer.Play();
                Debug.Log("▶️ Video playing.");
            }
            starterAssetsInputs.interact = false; // Reset input
        }
    }

    private void CheckRemoteInteraction()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == targetCube)
            {
                if (!isLookingAtRemote)
                {
                    isLookingAtRemote = true;
                    Item_Name.instance?.EnableInteractionText("Play/Pause Video");
                }
            }
            else
            {
                if (isLookingAtRemote)
                {
                    isLookingAtRemote = false;
                    Item_Name.instance?.DisableInteractionText();
                }
            }
        }
        else
        {
            if (isLookingAtRemote)
            {
                isLookingAtRemote = false;
                Item_Name.instance?.DisableInteractionText();
            }
        }
    }
}
