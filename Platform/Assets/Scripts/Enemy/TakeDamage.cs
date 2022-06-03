using UnityEngine;

namespace Enemy
{
    public class TakeDamage : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(PlayerTag)) Destroy(gameObject);
        }
    }
}
