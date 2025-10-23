using TMPro;
using UnityEngine;

namespace CasinoCut.Interactable
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        [Header("Highlighting Configuration")]
        [Tooltip("The material that holds the glow shader (e.g., M_GlowHighlight).")]
        // NOTE: This material must be dragged onto the component in the Inspector once.
        public Material highlightMaterial;

        [Tooltip("The color the object will glow when targeted.")]
        // This is the variable you wanted to define once per component type
        public Color glowColor = Color.yellow;

        public GameObject overlayUIElement;
        public TextMeshProUGUI overlayUIPromptText;

        // Internal variables for material swapping
        protected Renderer objectRenderer;
        protected Material originalMaterial;

        protected virtual void Awake()
        {
            // 1. Get the renderer component
            objectRenderer = GetComponent<Renderer>();

            if (objectRenderer != null)
            {
                // 2. Store the original material for restoration later
                originalMaterial = objectRenderer.material;
            }
            else
            {
                Debug.LogError(
                    $"BaseInteractable on {gameObject.name} requires a Renderer component!"
                );
            }

            if (overlayUIElement != null)
            {
                overlayUIElement.SetActive(false);
            }
        }

        // --- IInteractable Implementation (Generic Visuals) ---

        public void Highlight()
        {
            if (objectRenderer != null && highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
                objectRenderer.material.SetColor("_GlowColor", glowColor);
                overlayUIPromptText.text = GetInteractionPrompt();
                overlayUIElement.SetActive(true);
            }
        }

        public void UnHighlight()
        {
            if (objectRenderer != null && originalMaterial != null)
            {
                // Restore the stored original material
                objectRenderer.material = originalMaterial;
                overlayUIElement.SetActive(false);
            }
        }

        // --- IInteractable Implementation (Unique Logic - MUST BE IMPLEMENTED) ---

        // The derived classes (Door, TV, etc.) MUST provide their own interaction logic.
        public abstract void Interact(GameObject interactor);

        // The derived classes (Door, TV, etc.) MUST provide their own prompt text.
        public abstract string GetInteractionPrompt();
    }
}
