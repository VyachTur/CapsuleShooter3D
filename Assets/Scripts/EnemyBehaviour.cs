using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform patrolRoute;
    public List<Transform> locations;

    private Transform player;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    private bool isPlayerDetected = false;

    private System.Random rand;


    [SerializeField] private int _lives = 10;

    public int EnemyLives
    {
        get { return _lives; }

        private set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }

    void Start()
    {
        rand = new System.Random();

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Hero").transform;

        InitializePatrolRoute();

        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if(isPlayerDetected)
        {
            agent.destination = player.position;
        }

        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0) return;

        agent.destination = locations[locationIndex].position;

        //locationIndex = (locationIndex + 1) % locations.Count;

        locationIndex = (rand.Next(0, locations.Count) + 1) % locations.Count;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hero")
        {
            isPlayerDetected = true;

            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Hero")
        {
            isPlayerDetected = false;

            Debug.Log("Player out of range, resume patrol");
        }
    }
}
