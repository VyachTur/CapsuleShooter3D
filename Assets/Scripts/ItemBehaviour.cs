using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    private GameBehaviour gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Hero")
        {
            Destroy (this.gameObject);

            print("Item collected!");

            gameManager.Items += 1;
        }
    }
}
