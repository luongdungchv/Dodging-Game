using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMapMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer obj1, obj2;
    [SerializeField] private float refHeight;

    private float speed;
    private Vector2 direction;
    private bool isMove;
    private float upperBound;

    private void Start() {
        this.Init();
    }

    private void Update() {
        if(this.isMove){
            obj1.transform.Translate(direction * speed * Time.deltaTime, Space.World);
            obj2.transform.Translate(direction * speed * Time.deltaTime, Space.World);
            if(obj1.transform.position.y >= upperBound){
                obj1.transform.position -= (Vector3)(Vector2.up * upperBound * 2);
            }
            if(obj2.transform.position.y >= upperBound){
                obj2.transform.position -= (Vector3)(Vector2.up * upperBound * 2);
            }
        }
    }
    private void Init(){
        var worldCameraSize = DL.Utils.WorldUtils.GetOrthoCameraWorldSpaceSize(Camera.main, UIController.Instance.RefCanvas);
        this.upperBound = worldCameraSize.y;

        var ratio = UIController.Instance.RefCanvas.sizeDelta.y / refHeight;
        var scaleX = 1 / ratio;
        obj1.transform.localScale = obj1.transform.localScale.Set(x: scaleX);
        obj2.transform.localScale = obj2.transform.localScale.Set(x: scaleX);

    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }
    public void SetDirection(Vector2 dir){
        this.direction = dir;
    }
    public void StartMoving(){  
        this.isMove = true;
    }
    public void StopMoving(){
        this.isMove = false;
    }
}
