using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f);

    private Transform target;

    void Start()
    {
        target = GameObject.Find("Hero").transform;

    }

    void LateUpdate()
    {
        this.transform.position = target.TransformPoint(camOffset);

        this.transform.LookAt(target);
    }
}
