using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private AudioSource crashSound;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HandleCrash());
    }

    private IEnumerator HandleCrash()
    {
        if (crashSound != null) crashSound.Play();

        if (playerObject != null)
            playerObject.GetComponent<PlayerMove>().enabled = false;

        if (playerAnimator != null)
            playerAnimator.Play("Slipping");

        if (cameraAnimator != null)
            cameraAnimator.Play("CollisionCam");

        yield return new WaitForSeconds(3f);

        if (fadePanel != null)
            fadePanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("OfficeMenu");
    }
}
