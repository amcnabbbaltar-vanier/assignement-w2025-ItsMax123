using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public AnimationCurve curve;
    public float y;

    private void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            y + curve.Evaluate((Time.time % curve.length)),
            transform.position.z
        );
        transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));
    }
}