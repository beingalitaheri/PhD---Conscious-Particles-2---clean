using UnityEngine;

public class ParticleSpawnerController : MonoBehaviour
{
    [Header("Particle Spawners")]
    public ParticleSpawner[] particleSpawners; // 

    [Header("Target Settings")]
    public Transform RightHand;
    public Transform LeftHand;        // 
    public float activationDistance = 10f;  // 

    [Header("Required Object Settings")]
    public GameObject requiredObject_Left;
    public GameObject requiredObject_Right;        // 

    private bool isRequiredObjectActive = false;

    private void Start()
    {
        foreach (var spawner in particleSpawners)
        {
            if (spawner != null)
            {
                spawner.StopSpawning();
            }
        }
    }
    void Update()
    {
        if (particleSpawners == null || RightHand == null || LeftHand == null)
        {
            return;
        }

        bool currentRequiredObjectStatusRight = requiredObject_Right.activeInHierarchy;
        bool currentRequiredObjectStatusLeft = requiredObject_Left.activeInHierarchy;

        if (!currentRequiredObjectStatusRight && !currentRequiredObjectStatusLeft)
        {
            foreach (var spawner in particleSpawners)
            {
                if (spawner != null)
                {
                    spawner.StopSpawning();
                }
            }
        }
        else
        {
            foreach (var spawner in particleSpawners)
            {
                if (spawner == null)
                {
                    continue;
                }

                float distanceLeft = Vector3.Distance(spawner.transform.position, LeftHand.position);
                float distanceRight = Vector3.Distance(spawner.transform.position, RightHand.position);

                if (distanceLeft <= activationDistance)
                {
                    Debug.Log("Left hand is near!");
                    spawner.StartSpawning();
                }
                else if (distanceRight <= activationDistance)
                {
                    Debug.Log("Right hand is near!");
                    spawner.StartSpawning();
                }
                else 
                {
                    Debug.Log("both hands is out of area!");
                    spawner.StopSpawning();
                }
            }
        }

    }
}
