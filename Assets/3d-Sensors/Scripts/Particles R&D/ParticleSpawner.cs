using UnityEngine;
using System.Collections;

public class ParticleSpawner : MonoBehaviour
{
    public bool atStart = true, useVFX = true, isRandomPos=true;
    public GameObject particlePrefab, particleVFXPrefab;
    public Transform centerTransform,target;
    public int particleCount = 100;
    public float spawnRadius = 5f;
    public float initialSpeed = 2f;
    public float spawnDelay = 0.1f;
    private int generatedSpheres;
    public int limitedVFXNum;
    private int limitedVFXnum =0;
    [Header(" ( Particles values ) ")]
    [SerializeField] private bool isMeshRenderer = true;
    [SerializeField] private Vector2 particleForce = new Vector2(20f, 30f);
    [SerializeField] private Vector2 particleForceMagnitude = new Vector2(2f, 4f);
    [SerializeField] private Vector2 particleNoiseScale = new Vector2(0.1f, 1f);
    [SerializeField] private Vector2 particleSeprationDistance = new Vector2(0.05f, 0.1f);

    private Coroutine spawnCoroutine;

    void Start()
    {
        if (atStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnParticlesWithDelay());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnParticlesWithDelay()
    {
        for (int i = generatedSpheres; i < particleCount; i++)
        {
            generatedSpheres++;
            limitedVFXnum++;
            SpawnParticle();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnParticle()
    {
        Vector3 randomPos = Vector3.zero ;
        if (isRandomPos)
        {
            randomPos = centerTransform.position + Random.insideUnitSphere * spawnRadius;
        }
        else 
        {
            randomPos = centerTransform.position;
        }
        GameObject particle = Instantiate(particlePrefab, randomPos, Quaternion.identity);
        particle.GetComponent<MeshRenderer>().enabled = isMeshRenderer;

        if (useVFX && particleVFXPrefab != null && limitedVFXnum == limitedVFXNum)
        {
            GameObject particleVFX = Instantiate(particleVFXPrefab, Vector3.zero, Quaternion.identity);
            ParticlesVFX pbVFX = particleVFX.GetComponent<ParticlesVFX>();
            if (pbVFX != null)
            {
                pbVFX.target = particle.transform;
            }
            limitedVFXnum = 0;
        }

        Rigidbody rb = particle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDirection = Random.onUnitSphere;
            rb.velocity = randomDirection * initialSpeed;
        }

        ParticleBehavior pb = particle.AddComponent<ParticleBehavior>();
        if (pb != null)
        {
            pb.centerTransform = target;
            pb.attractionForce = Random.Range(particleForce.x, particleForce.y);
            pb.randomForceMagnitude = Random.Range(particleForceMagnitude.x, particleForceMagnitude.y);
            pb.noiseScale = Random.Range(particleNoiseScale.x, particleNoiseScale.y);
            pb.separationDistance = Random.Range(particleSeprationDistance.x, particleSeprationDistance.y);
        }
    }
}
