using System;
using UnityEngine;

namespace Game.Utils
{
    public static class CameraExtensions
    {
        public static Bounds OrthographicBounds(this Camera camera)
        {
            if (!camera.orthographic)
            {
                throw new InvalidOperationException($"Camera '{camera} isn't orthographic'");
            }
            var screenAspect = (float)Screen.width / (float)Screen.height;
            var cameraHeight = camera.orthographicSize * 2;
            var bounds = new Bounds(
                camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0)
            );
            return bounds;
        }
    }
}