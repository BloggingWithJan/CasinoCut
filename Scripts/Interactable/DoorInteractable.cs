using CasinoCut.Interactable;
using UnityEngine;

namespace CasinoCut.Interactable
{
    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        private bool isOpened = false;

        public void Interact(GameObject interactor)
        {
            if (isOpened)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }

        public string GetInteractionPrompt()
        {
            // The prompt changes based on the door's state
            return isOpened ? "Press E to Close" : "Press E to Open";
        }

        // --- Specific Door Logic ---
        private void OpenDoor()
        {
            isOpened = true;
            transform.Rotate(0f, 90f, 0f);
        }

        private void CloseDoor()
        {
            isOpened = false;
            transform.Rotate(0f, -90f, 0f);
        }
    }
}
