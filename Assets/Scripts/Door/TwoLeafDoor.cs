using System;
using UnityEngine;

namespace Door
{
    public class TwoLeafDoor : Door
    {
        private Vector3 _closedLeftDoorPosition;
        private Vector3 _closedRightDoorPosition;

        private Vector3 _leftDestinationPosition;
        private Vector3 _rightDestinationPosition;

        private Transform _leftDoor;
        private Transform _rightDoor;


        private void Start()
        {
            _leftDoor = transform.GetChild(0);
            _rightDoor = transform.GetChild(1);

            var leftDoorPosition = _leftDoor.position;
            _leftDestinationPosition = leftDoorPosition;
            _closedLeftDoorPosition = leftDoorPosition;

            var rightDoorPosition = _rightDoor.position;
            _rightDestinationPosition = rightDoorPosition;
            _closedRightDoorPosition = rightDoorPosition;
        }

        private void Update()
        {
            _leftDoor.position = Vector3.MoveTowards(_leftDoor.position, _leftDestinationPosition, speed);
            _rightDoor.position = Vector3.MoveTowards(_rightDoor.position, _rightDestinationPosition, speed);
        }

        public override void Open()
        {
            var openWidth = movePositiveDir ? openingWidth : -openingWidth;

            switch (direction)
            {
                case DoorDirection.Vertical:
                    _leftDestinationPosition = _closedLeftDoorPosition - new Vector3(0, openWidth, 0);
                    _rightDestinationPosition = _closedRightDoorPosition - new Vector3(0, -openWidth, 0);
                    break;
                case DoorDirection.Horizontal:
                    _leftDestinationPosition = _closedLeftDoorPosition - new Vector3(openWidth, 0);
                    _rightDestinationPosition = _closedRightDoorPosition - new Vector3(-openWidth, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Close()
        {
            _leftDestinationPosition = _closedLeftDoorPosition;
            _rightDestinationPosition = _closedRightDoorPosition;
        }
    }
}
