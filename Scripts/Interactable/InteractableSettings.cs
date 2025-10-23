using TMPro;
using UnityEngine;

namespace CasinoCut.Interactable
{
    [CreateAssetMenu(
        fileName = "InteractableSettings",
        menuName = "ScriptableObjects/InteractableSettings"
    )]
    public class InteractableSettings : ScriptableObject
    {
        [SerializeField]
        public Color defaultGlowColor = Color.yellow;

        [SerializeField]
        public Material highlightMaterial;

        [SerializeField]
        public GameObject overlayUIPrefab;
    }
}
