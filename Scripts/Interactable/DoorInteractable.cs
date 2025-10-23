using CasinoCut.Interactable;
using UnityEngine;

namespace CasinoCut.Interactable
{
    public class DoorInteractable : BaseInteractable
    {
        private bool isOpened = false;

        public override void Interact(GameObject interactor)
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

        public override string GetInteractionPrompt()
        {
            // The prompt changes based on the door's state
            return isOpened ? "Close [E]" : "Open [E]";
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
