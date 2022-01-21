using System;
using System.Net.NetworkInformation;
using InspectorButton;
using Interact;
using UnityEngine;

namespace DefaultNamespace.Interactable

{
    /// <summary>
    /// Код стырен у Valve, тут мрак но писать самим некогда
    /// </summary>
    [InspectorButtonClass]
    public class InteractableHighLightView : MonoBehaviour
    {
        [Header("Settings")]
        public bool NeedHighlight;

        [Header("Links")]
        [SerializeField] private Interact.Interactable interactable;

        [Tooltip(
            "An array of child gameObjects to not render a highlight for. Things like transparent parts, vfx, etc.")]
        public GameObject[] hideOnHighlight;
        [SerializeField] private Material highlightMat;

        [Header("Monitoring")]
        [SerializeField] private bool isHighlightEnabled;
        [SerializeField] private int hoverCount;

        protected MeshRenderer[] highlightRenderers;
        protected MeshRenderer[] existingRenderers;
        protected GameObject highlightHolder;
        protected SkinnedMeshRenderer[] highlightSkinnedRenderers;
        protected SkinnedMeshRenderer[] existingSkinnedRenderers;


        [InspectorButton("EnableHighlight")]
        public void AddHighlight()
        {
            if (!NeedHighlight) return;

            if (hoverCount > 0)
            {
                hoverCount++;

                return;
            }

            isHighlightEnabled = true;
            CreateHighlightRenderers();
            UpdateHighlightRenderers();
            hoverCount = 1;
        }

        [InspectorButton("RemoveHighlight")]
        public void RemoveHighlight()
        {
            isHighlightEnabled = false;
            hoverCount--;
            if (hoverCount == 0)
            {
                if (highlightHolder != null)
                    Destroy(highlightHolder);
            }
        }

        private void OnEnable()
        {
            interactable.CanBeInteractedBegin.AddListener(OnCanBeInteractedBegin);
            interactable.CanBeInteractedEnd.AddListener(OnCanBeInteractedEnd);
        }

        private void OnDisable()
        {
            interactable.CanBeInteractedBegin.RemoveListener(OnCanBeInteractedBegin);
            interactable.CanBeInteractedEnd.RemoveListener(OnCanBeInteractedEnd);
        }

        private void OnCanBeInteractedBegin(InteractableInteractedEventArgs arg0)
        {
            AddHighlight();
        }

        private void OnCanBeInteractedEnd(InteractableInteractedEventArgs arg0)
        {
            RemoveHighlight();
        }

        private void Update()
        {
            if (isHighlightEnabled)
            {
                UpdateHighlightRenderers();
            }
        }

        protected virtual bool ShouldIgnoreHighlight(Component component)
        {
            return ShouldIgnore(component.gameObject);
        }

        protected virtual bool ShouldIgnore(GameObject check)
        {
            for (int ignoreIndex = 0; ignoreIndex < hideOnHighlight.Length; ignoreIndex++)
            {
                if (check == hideOnHighlight[ignoreIndex])
                    return true;
            }

            return false;
        }


        protected virtual void CreateHighlightRenderers()
        {
            existingSkinnedRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            highlightHolder = new GameObject("Highlighter");
            highlightSkinnedRenderers = new SkinnedMeshRenderer[existingSkinnedRenderers.Length];

            for (int skinnedIndex = 0; skinnedIndex < existingSkinnedRenderers.Length; skinnedIndex++)
            {
                SkinnedMeshRenderer existingSkinned = existingSkinnedRenderers[skinnedIndex];

                if (ShouldIgnoreHighlight(existingSkinned))
                    continue;

                GameObject newSkinnedHolder = new GameObject("SkinnedHolder");
                newSkinnedHolder.transform.parent = highlightHolder.transform;
                SkinnedMeshRenderer newSkinned = newSkinnedHolder.AddComponent<SkinnedMeshRenderer>();
                Material[] materials = new Material[existingSkinned.sharedMaterials.Length];
                for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
                {
                    materials[materialIndex] = highlightMat;
                }

                newSkinned.sharedMaterials = materials;
                newSkinned.sharedMesh = existingSkinned.sharedMesh;
                newSkinned.rootBone = existingSkinned.rootBone;
                newSkinned.updateWhenOffscreen = existingSkinned.updateWhenOffscreen;
                newSkinned.bones = existingSkinned.bones;

                highlightSkinnedRenderers[skinnedIndex] = newSkinned;
            }

            MeshFilter[] existingFilters = this.GetComponentsInChildren<MeshFilter>(true);
            existingRenderers = new MeshRenderer[existingFilters.Length];
            highlightRenderers = new MeshRenderer[existingFilters.Length];

            for (int filterIndex = 0; filterIndex < existingFilters.Length; filterIndex++)
            {
                MeshFilter existingFilter = existingFilters[filterIndex];
                MeshRenderer existingRenderer = existingFilter.GetComponent<MeshRenderer>();

                if (existingFilter == null || existingRenderer == null || ShouldIgnoreHighlight(existingFilter))
                    continue;

                GameObject newFilterHolder = new GameObject("FilterHolder");
                newFilterHolder.transform.parent = highlightHolder.transform;
                MeshFilter newFilter = newFilterHolder.AddComponent<MeshFilter>();
                newFilter.sharedMesh = existingFilter.sharedMesh;
                MeshRenderer newRenderer = newFilterHolder.AddComponent<MeshRenderer>();

                Material[] materials = new Material[existingRenderer.sharedMaterials.Length];
                for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
                {
                    materials[materialIndex] = highlightMat;
                }

                newRenderer.sharedMaterials = materials;

                highlightRenderers[filterIndex] = newRenderer;
                existingRenderers[filterIndex] = existingRenderer;
            }
        }

        protected virtual void UpdateHighlightRenderers()
        {
            if (highlightHolder == null)
                return;

            for (int skinnedIndex = 0; skinnedIndex < existingSkinnedRenderers.Length; skinnedIndex++)
            {
                SkinnedMeshRenderer existingSkinned = existingSkinnedRenderers[skinnedIndex];
                SkinnedMeshRenderer highlightSkinned = highlightSkinnedRenderers[skinnedIndex];

                if (existingSkinned != null && highlightSkinned != null)
                {
                    highlightSkinned.transform.position = existingSkinned.transform.position;
                    highlightSkinned.transform.rotation = existingSkinned.transform.rotation;
                    highlightSkinned.transform.localScale = existingSkinned.transform.lossyScale;
                    highlightSkinned.localBounds = existingSkinned.localBounds;
                    highlightSkinned.enabled = NeedHighlight && existingSkinned.enabled &&
                                               existingSkinned.gameObject.activeInHierarchy;

                    int blendShapeCount = existingSkinned.sharedMesh.blendShapeCount;
                    for (int blendShapeIndex = 0; blendShapeIndex < blendShapeCount; blendShapeIndex++)
                    {
                        highlightSkinned.SetBlendShapeWeight(blendShapeIndex,
                            existingSkinned.GetBlendShapeWeight(blendShapeIndex));
                    }
                }
                else if (highlightSkinned != null)
                    highlightSkinned.enabled = false;
            }

            for (int rendererIndex = 0; rendererIndex < highlightRenderers.Length; rendererIndex++)
            {
                MeshRenderer existingRenderer = existingRenderers[rendererIndex];
                MeshRenderer highlightRenderer = highlightRenderers[rendererIndex];

                if (existingRenderer != null && highlightRenderer != null)
                {
                    highlightRenderer.transform.position = existingRenderer.transform.position;
                    highlightRenderer.transform.rotation = existingRenderer.transform.rotation;
                    highlightRenderer.transform.localScale = existingRenderer.transform.lossyScale;
                    highlightRenderer.enabled = NeedHighlight && existingRenderer.enabled &&
                                                existingRenderer.gameObject.activeInHierarchy;
                }
                else if (highlightRenderer != null)
                    highlightRenderer.enabled = false;
            }
        }
    }
}