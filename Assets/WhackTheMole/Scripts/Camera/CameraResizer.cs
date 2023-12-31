﻿using WhackTheMole.Scripts.GameMachines;
using System.Collections.Generic;
using WhackTheMole.Scripts.Grid;
using Cinemachine;
using UnityEngine;

namespace WhackTheMole.Scripts.Camera
{
    public class CameraResizer : IGameStateListener
    {
        private readonly float _padding;
        private readonly IEnumerable<ICell> _cells;
        private readonly CinemachineVirtualCamera _virtualCamera;

        private CameraResizer(CinemachineVirtualCamera virtualCamera, GridGeneratorBase gridGenerator,
            float padding)
        {
            _cells = gridGenerator.Cells;
            _virtualCamera = virtualCamera;
            _padding = padding;
        }

        public void OnStartGame()
        {
            ResizeCamera();
        }

        private void ResizeCamera()
        {
            var bounds = CalculateBounds();
            var cameraTransform = _virtualCamera.transform;
            var cameraPosition = bounds.center - cameraTransform.forward * bounds.size.magnitude +
                                 new Vector3(0, _padding);
            cameraTransform.position = cameraPosition;
        }

        private Bounds CalculateBounds()
        {
            var bounds = new Bounds();
            foreach (var cell in _cells)
            {
                bounds.Encapsulate(cell.Transform.position);
            }

            return bounds;
        }
    }
}