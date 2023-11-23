using UnityEngine;

public class GhostHouse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<GhostAI>().Recovery();
    }
}
