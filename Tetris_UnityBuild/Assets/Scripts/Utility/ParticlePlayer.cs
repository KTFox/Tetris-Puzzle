using UnityEngine;

namespace TetrisPuzzle.Utilities
{
    public class ParticlePlayer : MonoBehaviour
    {
        // Variables

        [SerializeField] private ParticleSystem[] particles;


        // Methods

        private void Awake()
        {
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        public void Play()
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
        }
    }
}
