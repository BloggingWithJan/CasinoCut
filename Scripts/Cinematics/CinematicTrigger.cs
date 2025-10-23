using UnityEngine;
using UnityEngine.Playables;

namespace CasinoCut.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !alreadyTriggered)
            {
                Debug.Log("Cinematic Triggered");
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
                // CinematicManager.Instance.PlayCinematic(cinematicName);
            }
        }
    }
}
