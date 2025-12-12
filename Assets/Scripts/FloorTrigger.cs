using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public GameObject floorPrefab;   // assign prefab in inspector
    public bool isLeftTrigger;       // tick for left trigger
    private Transform player;

    private void Start()
    {
        // find the player in the scene by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Vector3 spawnPos = transform.position;
        spawnPos.y -= 1.19f;

        if (isLeftTrigger)
            spawnPos.x -= 7.5f;
        else
            spawnPos.x += 7.5f;

        // Spawn the new floor
        GameObject newTile = Instantiate(floorPrefab, spawnPos, Quaternion.identity);

        // Assign the player to the new tile so it can self-destruct
        newTile.GetComponent<FloorTile>().SetPlayer(player);

        // Kill the opposite trigger on the new tile
        FloorTrigger[] triggers = newTile.GetComponentsInChildren<FloorTrigger>();
        foreach (FloorTrigger t in triggers)
        {
            if (t.isLeftTrigger != isLeftTrigger)
                Destroy(t.gameObject);
        }

        // Kill THIS trigger so it can't re-spawn
        Destroy(gameObject);
    }
}
