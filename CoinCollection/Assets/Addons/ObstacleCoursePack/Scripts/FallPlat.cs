using System.Collections;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    public float fallTime = 0.5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (rb == null)
            Debug.LogError("No Rigidbody found on parent!");
        else
            rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player stepped on fall platform.");
            StartCoroutine(Fall(fallTime));
        }
    }

    IEnumerator Fall(float time)
    {
        yield return new WaitForSeconds(time);
        rb.isKinematic = false;
        yield return new WaitForSeconds(2f);
        Destroy(rb.gameObject);
    }
}
