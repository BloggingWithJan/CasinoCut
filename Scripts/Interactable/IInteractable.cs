using UnityEngine;

namespace CasinoCut.Interactable
{
    public interface IInteractable
    {
        // The core method that executes the specific interaction logic.
        void Interact(GameObject interactor);

        // Returns the text prompt (e.g., "Press E to Open," "Press E to Drink").
        string GetInteractionPrompt();
    }
}
