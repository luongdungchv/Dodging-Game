using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private List<ObstacleCell> cellObjectList;
    [SerializeField] private ObstacleCell cellObjectPrefab;
    private float speed;
    private Vector2 moveDirection;
    private int cellCount;
    private float screenWidth, xPos;

    private Rigidbody2D rb => this.GetComponent<Rigidbody2D>();

    public void InitializeProperties(float speed, Vector2 moveDirection, int cellCount, float screenWidth)
    {
        this.speed = speed;
        this.moveDirection = moveDirection;
        this.cellCount = cellCount;
        this.screenWidth = screenWidth;
        this.SpawnWalls();
    }
    public void SetXPosition(float x){
        this.xPos = x;
    }

    public void StartMoving()
    {
        this.gameObject.SetActive(true);
        this.rb.velocity = moveDirection.normalized * speed;
    }
    public void StopMoving(bool disable = true)
    {
        if(disable) this.gameObject.SetActive(false);
        this.rb.velocity = Vector2.zero;
        cellObjectList.ForEach(x => x.gameObject.SetActive(true));
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
        this.rb.velocity = moveDirection.normalized * speed;
    }
    public void DisableRandomCells(List<int> cellIndices){
        this.cellObjectList.ForEach(x => x.SetAsWall());
        cellIndices.ForEach(x =>{
            var cell = this.cellObjectList[x];
            cell.SetAsEmpty();
        });
        var randomIndex = Random.Range(0, cellIndices.Count);
        this.cellObjectList[cellIndices[randomIndex]].DetermineScorer();
    }

    private void SpawnWalls(){
        var wallSize = this.screenWidth / (float)cellCount;
        cellObjectList.ForEach(x => Destroy(x.gameObject));
        cellObjectList.Clear();
        var pos = 0f;
        for(int i = 0; i < cellCount; i++){
            var cell = Instantiate(cellObjectPrefab);
            cell.transform.SetParent(this.transform);
            cell.transform.localScale = Vector2.one * wallSize;
            cell.transform.localPosition = new Vector2(pos + wallSize / 2, 0);
            cell.SetAsWall();
            cellObjectList.Add(cell);
            pos += wallSize;
        }
    }
}
