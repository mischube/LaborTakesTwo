using System;
using UnityEngine;

namespace Door
{
    public class SingleLeafDoor : Door
    {
        private Vector3 _closedPosition;
        private Vector3 _destinationPosition;

        private Transform _door;

        private void Start()
        {
            _door = transform.GetChild(0);
            var position = _door.position;
            _closedPosition = position;
            _destinationPosition = position;
        }

        private void Update()
        {
            if (_door.position == _destinationPosition)
                return;

            _door.position = Vector3.MoveTowards(_door.position, _destinationPosition, speed);
        }

        public override void Open()
        {
            var openWidth = movePositiveDir ? openingWidth : -openingWidth;
            var doorPosition = _door.position;

            _destinationPosition = direction switch
            {
                DoorDirection.Vertical => doorPosition + new Vector3(0, openWidth, 0),
                DoorDirection.Horizontal => doorPosition + new Vector3(openWidth, 0, 0),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override void Close()
        {
            _destinationPosition = _closedPosition;
        }
    }
}
