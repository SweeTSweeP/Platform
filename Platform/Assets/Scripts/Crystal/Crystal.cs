using UnityEngine;

namespace Crystal
{
    public class Crystal : MonoBehaviour
    {
        private const string PlayerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(PlayerTag)) Destroy(gameObject);
        }
    }
}
