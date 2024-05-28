using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class ShapeSpawner : MonoBehaviour
    {
        // variables

        [SerializeField] private Shape[] shapes;
        [SerializeField] private Transform queueSpace;

        private readonly Vector3Int SPAWN_POSITION = new Vector3Int(5, 23, 0);
        private readonly Vector3 DEFAULT_SHAPE_SCALE = Vector3.one;
        private readonly Vector3 QUEUED_SHAPE_SCALE = new Vector3(0.5f, 0.5f, 0.5f);

        private Shape nextShape;

        // Properties

        public Vector3Int SpawnPosition => SPAWN_POSITION;


        // Methods

        public Shape SpawnShape()
        {
            if (nextShape == null)
            {
                SetNextShape();
            }

            Shape newShape = nextShape;
            newShape.transform.position = SPAWN_POSITION;
            newShape.transform.localScale = DEFAULT_SHAPE_SCALE;

            SetNextShape();

            return newShape;
        }

        private void SetNextShape()
        {
            Shape randomShape = GetRandomShape();
            nextShape = Instantiate(randomShape, queueSpace.position + randomShape.BoxOffset, Quaternion.identity);
            nextShape.transform.localScale = QUEUED_SHAPE_SCALE;
        }

        private Shape GetRandomShape()
        {
            int randomIndex = Random.Range(0, shapes.Length);

            return shapes[randomIndex];
        }
    }
}
