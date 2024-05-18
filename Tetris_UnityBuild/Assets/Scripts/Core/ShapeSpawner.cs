using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class ShapeSpawner : MonoBehaviour
    {
        // variables

        [SerializeField] private Shape[] shapes;

        private Vector3Int spawnPosition = new Vector3Int(5, 23, 0);


        // Methods

        public Shape SpawnShape()
        {
            Shape shape = Instantiate(GetRandomShape(), spawnPosition, Quaternion.identity);

            return shape;
        }

        private Shape GetRandomShape()
        {
            int randomIndex = Random.Range(0, shapes.Length);

            return shapes[randomIndex];
        }
    }
}
