using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float onscreenDelay = 5f;

    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }
}
