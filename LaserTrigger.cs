using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    public GameObject ripplePrefab;
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            GameObject ripple = Instantiate(ripplePrefab, spawnPoint.position, Quaternion.identity);
            ripple.GetComponent<RippleController>().StartRipple();
        }
    }
}
