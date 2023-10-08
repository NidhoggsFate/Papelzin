using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ScenaryController : MonoBehaviour
{

    public GameObject backgroundPrefab;
    public GameObject backgroundParent;
    
    public Transform backgroundDespawnPoint;

    public int amountBackground;
    public float velocity;

    public List<GameObject> activeBacgkround;

    private GameObject lastBackgroundSpawned;

    private float spriteLenght;


    private void Start()
    {
        lastBackgroundSpawned = activeBacgkround.First();
        var spriteRenderer = lastBackgroundSpawned.GetComponent<SpriteRenderer>();
        spriteLenght = spriteRenderer.sprite.bounds.extents.x;
    }

    
    void Update()
    {
        for (int i = 0; i < activeBacgkround.Count; i++)
        {
            var currentSky = activeBacgkround[i];
            currentSky.transform.position += (Vector3.left * velocity);
           
            if (currentSky.transform.position.x < backgroundDespawnPoint.position.x)
            {
                activeBacgkround.Remove(currentSky);
                Destroy(currentSky);
            }
        }
        
        if (activeBacgkround.Count < amountBackground)
        {
            SpawnBackground();
        }
    }

    private void SpawnBackground()
    {
        var lastInstantiatedPosition = lastBackgroundSpawned.transform.position;
        lastBackgroundSpawned = Instantiate(backgroundPrefab, new Vector3(lastInstantiatedPosition.x + (spriteLenght *2), 0), Quaternion.identity);
        lastBackgroundSpawned.transform.parent = backgroundParent.transform;
        activeBacgkround.Add(lastBackgroundSpawned);
    }
}
