using UnityEngine;

namespace CasinoCut.Interactable
{
    public interface IInteractable
    {
        // The core method that executes the specific interaction logic.
        void Interact(GameObject interactor);

        // Returns the text prompt (e.g., "Press E to Open," "Press E to Drink").
        string GetInteractionPrompt();

        // New: Method to visually signal the object is targetable (e.g., change color, add glow).
        void Highlight();

        // New: Method to revert the visual change.
        void UnHighlight();
    }
}
