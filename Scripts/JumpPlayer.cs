using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    public Player _player;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag != "Player")
        _player.SetGroundnes(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag != "Player")
        _player.SetGroundnes(false);
    }
}
