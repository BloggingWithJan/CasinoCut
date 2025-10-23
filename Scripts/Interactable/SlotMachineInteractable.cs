using UnityEngine;

namespace CasinoCut.Interactable
{
    public class SlotMachineInteractable : BaseInteractable
    {
        private bool isPlaying = false;

        public override void Interact(GameObject interactor)
        {
            if (isPlaying)
            {
                StopPlaying();
            }
            else
            {
                StartPlaying();
            }
        }

        public override string GetInteractionPrompt()
        {
            return isPlaying ? "Stop [E]" : "Play [E]";
        }

        // --- Specific Slot Machine Logic ---
        private void StartPlaying()
        {
            isPlaying = true;
            // Add logic to start the slot machine
        }

        private void StopPlaying()
        {
            isPlaying = false;
            // Add logic to stop the slot machine
        }
    }
}
