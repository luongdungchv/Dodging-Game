using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DL.Utils
{
    public static class WorldUtils
    {
        public static Vector2 GetOrthoCameraWorldSpaceSize(Camera cam, RectTransform refCanvas){
            if(cam.orthographic){
                var zoom = cam.orthographicSize;
                var ratio = refCanvas.sizeDelta.y / refCanvas.sizeDelta.x;
                var worldHeight = zoom * 2;
                var worldWidth = worldHeight / ratio;
                return new Vector2(worldWidth, worldHeight);
            }
            return -Vector2.one;
        }
    }
}
