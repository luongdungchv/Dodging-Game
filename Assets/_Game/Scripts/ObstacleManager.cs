using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using Sirenix.Utilities;
using UnityEngine;

public class ObstacleManager : Sirenix.OdinInspector.SerializedMonoBehaviour
{
    [SerializeField] private Queue<Obstacle> obstaclePool;
    [SerializeField] private ParallaxMapMover parallaxMover;
    
    [Header("Level Properties")]
    [SerializeField] private int cellCount;
    [SerializeField] private float thresholdOffset;
    [SerializeField] private bool moveUp;
    [SerializeField] private int maxHolePerObstacle;
    [Header("Time Properties")]
    [SerializeField] private float obstacleInterval;
    [SerializeField] private float minObsInterval, obsIntervalDecrement;
    [SerializeField] private float incrementInterval;
    [SerializeField] private float initialMoveSpeed, speedIncrement;
    [SerializeField] private float maxObstacleSpeed;
    [SerializeField] private int maxHoleCount;
    [SerializeField] private Queue<Obstacle> activeObstacles;

    private float upperBound, lowerBound, rightBound, leftBound;
    private float currentSpeed, currentObsInterval;
    private Coroutine moveCoroutine, speedCoroutine;

    private void Start(){
        //GameManager.Instance.OnGameOver.AddListener(HandleGameOver);
        this.Init();
        this.StartMoving();
    }

    private void Update(){
        if(activeObstacles.Count == 0) return;
        var topObstacle = activeObstacles.Peek();
        if(topObstacle.transform.position.y > upperBound){
            topObstacle = activeObstacles.Dequeue();
            topObstacle.StopMoving();
            this.obstaclePool.Enqueue(topObstacle);
        }
    }

    public void Init(){
        var worldCameraSize = DL.Utils.WorldUtils.GetOrthoCameraWorldSpaceSize(Camera.main, UIController.Instance.RefCanvas);

        this.upperBound = worldCameraSize.y / 2 + thresholdOffset;
        this.lowerBound = -upperBound;
        this.rightBound = worldCameraSize.x / 2;
        this.leftBound = -worldCameraSize.x / 2;

        obstaclePool.ForEach(x => {
            x.InitializeProperties(initialMoveSpeed, moveUp ? Vector2.up : Vector2.down, cellCount, worldCameraSize.x);
            x.transform.position = new Vector2(-worldCameraSize.x / 2, 0);
            x.gameObject.SetActive(false);
        });

        this.currentSpeed = initialMoveSpeed;
        this.currentObsInterval = this.obstacleInterval;

        activeObstacles = new Queue<Obstacle>();
    }
    public void StartMoving(){
        this.moveCoroutine = StartCoroutine(IEMove());
        this.speedCoroutine = StartCoroutine(IEIncreaseDifficulty());
        parallaxMover.SetDirection(moveUp ? Vector2.up : Vector2.down);
        parallaxMover.SetSpeed(this.currentSpeed);
        parallaxMover.StartMoving();
    }
    
    private IEnumerator IEMove(){
        while(true){
            var obstacle = this.obstaclePool.Dequeue();

            var disableCellCount = Random.Range(1, this.maxHoleCount + 1);
            var list = new List<int>();
            for(int i = 0; i < disableCellCount; i++){
                list.Add(Random.Range(0, this.cellCount));
            }

            obstacle.DisableRandomCells(list);
            obstacle.StartMoving();
            obstacle.transform.position = new Vector2(leftBound, lowerBound);

            this.activeObstacles.Enqueue(obstacle);

            yield return new WaitForSeconds(this.currentObsInterval);
        }
    }
    private IEnumerator IEIncreaseDifficulty(){
        while(true){
            yield return new WaitForSeconds(this.incrementInterval);

            this.currentSpeed += speedIncrement;
            this.currentObsInterval -= obsIntervalDecrement;

            if(currentSpeed > maxObstacleSpeed) currentSpeed = maxObstacleSpeed;
            if(currentObsInterval < minObsInterval) currentObsInterval = minObsInterval;

            this.obstaclePool.ForEach(x => x.SetSpeed(currentSpeed));
            this.activeObstacles.ForEach(x => x.SetSpeed(currentSpeed));
            parallaxMover.SetSpeed(currentSpeed);

            Player.Instance.IncreaseAnimSpeed();
        }
    }
    private void HandleGameOver(){
        Time.timeScale = 0;
        this.obstaclePool.ForEach(x => x.StopMoving(false));
        this.activeObstacles.ForEach(x => x.StopMoving(false));
    }
}
