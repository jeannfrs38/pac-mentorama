using UnityEngine;

[RequireComponent(typeof(GhostMove))]
public class GhostAI : MonoBehaviour
{
    private GhostMove _ghostMove;
    private Transform _pacman;
    void Start()
    {
        _ghostMove = GetComponent<GhostMove>();
        _ghostMove.OnUpdateMoveTarget += GhostMove_OnUpdateMoveTarget;
        _pacman = GameObject.FindWithTag("Player").transform;
    }

    private void GhostMove_OnUpdateMoveTarget()
    {
        _ghostMove.SetTargetMoveLocation(_pacman.position);
    }
}
