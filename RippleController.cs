using UnityEngine;

public class RippleController : MonoBehaviour
{
    public float expansionSpeed = 2f;
    public float maxSize = 3f;
    public float decayAir = 0.05f;
    public float decayLiquid = 0.3f;

    private float currentDecay;
    private float life = 0f;

    public static string backingType = "air";

    public void StartRipple()
    {
        currentDecay = (backingType == "air") ? decayAir : decayLiquid;
    }

    void Update()
    {
        transform.localScale += Vector3.one * expansionSpeed * Time.deltaTime;
        life += Time.deltaTime;

        float fade = Mathf.Exp(-currentDecay * life * 10f);
        Color color = GetComponent<Renderer>().material.color;
        color.a = fade;
        GetComponent<Renderer>().material.color = color;

        if (transform.localScale.x > maxSize || fade < 0.01f)
            Destroy(gameObject);
    }
}
