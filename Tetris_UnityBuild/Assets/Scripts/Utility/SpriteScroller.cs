using UnityEngine;

namespace TetrisPuzzle.Utilities
{
    public class SpriteScroller : MonoBehaviour
    {
        // Variables

        [SerializeField] private Vector2 moveSpeed;

        private Vector2 offset;
        private Material material;


        // Methods

        private void Awake()
        {
            material = GetComponent<SpriteRenderer>().material;
        }

        private void Update()
        {
            material.mainTextureOffset += moveSpeed * Time.deltaTime;
        }
    }
}
