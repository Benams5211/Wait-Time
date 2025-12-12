using UnityEngine;

public class FloorTile : MonoBehaviour
{
    private Transform player;
    public float destroyDistance = 30f; // distance before tile self-destructs

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(player.position, transform.position);

        if (dist > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
