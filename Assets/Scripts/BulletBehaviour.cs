using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float onscreenDelay = 4f;

    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }
}
