using System.Collections;
using UnityEngine;

public class WallMovable : MonoBehaviour
{
    public float speed = 2f;
    public bool isRandom = true;
    public float moveDistance = 8f; // Total distance to move on X axis

    private float xStart;
    private float xEnd;
    private bool movingRight = true;
    private bool isWaiting = false;
    private bool canChange = true;

    void Start()
    {
        xStart = transform.position.x;
        xEnd = xStart + moveDistance;
    }

    void Update()
    {
        if (isWaiting || !canChange) return;

        float targetX = movingRight ? xEnd : xStart;
        float step = speed * Time.deltaTime;

        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            StartCoroutine(WaitToChange(0.25f));
        }
    }

    IEnumerator WaitToChange(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
        movingRight = !movingRight;

        if (isRandom && movingRight)
        {
            int num = Random.Range(0, 2);
            if (num == 1)
                StartCoroutine(Retry(1.5f));
        }
    }

    IEnumerator Retry(float time)
    {
        canChange = false;
        yield return new WaitForSeconds(time);
        int num = Random.Range(0, 2);
        if (num == 1)
            StartCoroutine(Retry(1.25f));
        else
            canChange = true;
    }
}
