using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Bonus objectToSpawn;
    public GameObject planetCollider;
    //public float maxSpawnRadius;
    private GravityController gravityController;
    private float _timer;
    public int count;
    [SerializeField] private int maxSpawn;

    public Transform center;
    public float maxRadius;
    public float spawnInterval = 1f;

    private Collider2D objectCollider;

    [SerializeField] public Sprite PositiveSprite;
    [SerializeField] public Sprite NegativeSprite;
    private void Start()
    {
        gravityController = GameObject.FindFirstObjectByType<GravityController>();
        objectCollider = planetCollider.GetComponent<CircleCollider2D>();
        if (objectCollider == null)
        {
            Debug.LogError("Object to spawn must have a collider component attached.");
            return;
        }
    }
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5f && count < maxSpawn)
        {
            SpawnObject();
            _timer = 0;
        }
        
    }

    void SpawnObject()
    {
        float minRadius = Mathf.Max(objectCollider.bounds.extents.x, objectCollider.bounds.extents.z);
        float radius = Random.Range(minRadius, maxRadius);
        Vector2 randomPos = Random.insideUnitCircle.normalized * radius;
        Vector3 spawnPos = new Vector3(randomPos.x, randomPos.y, 0);

        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        objectToSpawn.Effect = (int)(Random.Range(-10, 10));
        if (objectToSpawn.Effect > 0)
        {
            objectToSpawn.Positive = true;
        }
        if (objectToSpawn.Effect < 0)
        {
            objectToSpawn.Positive = false;
        }
        count += 1;
       
    }
}
