using UnityEngine;

namespace TetrisPuzzle.Core
{
    public class ShapeHolder : MonoBehaviour
    {
        // Variables

        [SerializeField] private Transform holdingSpace;

        private readonly Vector3 DEFAULT_SHAPE_SCALE = Vector3.one;
        private readonly Vector3 HEALD_SHAPE_SCALE = new Vector3(0.5f, 0.5f, 0.5f);

        private Shape heldShape;
        private bool canSwitch = true;

        // Properties

        public bool IsEmptyHolder => heldShape == null;
        public bool CanSwitch => canSwitch;


        // Methods

        public void HoldShape(Shape shape)
        {
            if (heldShape != null) return;

            shape.transform.position = holdingSpace.position + shape.BoxOffset;
            shape.transform.localScale = HEALD_SHAPE_SCALE;
            shape.transform.eulerAngles = Vector3.zero;

            heldShape = shape;
        }

        public Shape SwitchShape(Shape shapeToHold, Vector3 releasePosition)
        {
            Shape shapeToRelease = null;

            if (canSwitch)
            {
                shapeToRelease = heldShape;
                ReleaseShape(releasePosition);
                HoldShape(shapeToHold);
                canSwitch = false;
            }

            return shapeToRelease;
        }

        private void ReleaseShape(Vector3 releasePosition)
        {
            heldShape.transform.position = releasePosition;
            heldShape.transform.localScale = DEFAULT_SHAPE_SCALE;
            heldShape = null;
        }

        public void SetCanSwitch(bool value)
        {
            canSwitch = value;
        }
    }
}
