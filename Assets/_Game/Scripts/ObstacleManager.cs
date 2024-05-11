using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private Queue<Obstacle> obstaclePool;
    
    [SerializeField] private int cellCount;
    [SerializeField] private float initialMoveSpeed;
    [SerializeField] private bool moveUp;
    private Queue<Obstacle> activeObstacle;

    public void Init(){
        var worldCameraSize = DL.Utils.WorldUtils.GetOrthoCameraWorldSpaceSize(Camera.main, UIController.Instance.RefCanvas);
        obstaclePool.ForEach(x => x.InitializeProperties(initialMoveSpeed, moveUp ? Vector2.up : Vector2.down, cellCount, worldCameraSize.x));
    }
}
