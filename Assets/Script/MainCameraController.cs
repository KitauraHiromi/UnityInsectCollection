using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainCameraController : MonoBehaviour {

    [SerializeField]
    private GameObject MainCamera;   // インスペクターで主観カメラを紐づける
    [SerializeField]
    private GameObject ThirdPersonCamera;   // インスペクターで第三者視点カメラを紐づける
     
    GameObject targetObj;
    Vector3 targetPos, centerPos;
    Quaternion MainCameraRotation, ThirdPersonCameraRotation, moveForward;
    float minAngleY, maxAngleY, minAngleX, maxAngleX, speed;
    int directionX, directionY;
    Texture2D texture;
    bool grab;
    string filepath;

    void Start () {
        // 操作キャラクタの指定，位置，正面方向の取得
        targetObj = GameObject.Find("unitychan");
        targetPos = targetObj.transform.position;
        moveForward = targetObj.transform.rotation;
        ThirdPersonCameraRotation = ThirdPersonCamera.transform.rotation;
        minAngleY = -180;
        maxAngleY = 180;
        minAngleX = -90f;
        maxAngleX = 90f;
        speed = 5f;
        directionX = -1;
        directionY = 1;
        texture = new Texture2D(Screen.width, Screen.height);
        grab = false;
        filepath = "tmp.png";
    }

    private void OnPostRender(){
        if(grab){
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();
            byte [] img = texture.EncodeToPNG();
            File.WriteAllBytes(filepath, img);
            grab = false;
        }
    }

    // void FixedUpdate() {
    //     // キャラクターの向きは、サードパーソンカメラが有効な時だけ変更する
    //     if (moveForward != Vector3.zero && ThirdPersonCamera.activeInHierarchy) {
    //         transform.rotation = Quaternion.LookRotation(moveForward);
    //     }
    // }
    
    void Update() {
        // スペースキーでカメラを切り替える
        if (Input.GetKeyDown(KeyCode.Space)) {
            MainCamera.SetActive(!MainCamera.activeInHierarchy);
            ThirdPersonCamera.SetActive(!ThirdPersonCamera.activeInHierarchy);
            // MainCamera.transform.rotation = moveForward;
            // ThirdPersonCamera.transform.rotation = moveForward;
            ThirdPersonCamera.transform.localRotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
            ThirdPersonCamera.transform.localPosition = new Vector3(0, 0.5f, 3f);
        }

        // マウスでカメラ位置を移動する
        if (Input.GetMouseButton(1)) {
            // マウスの移動量
            float yaw = Input.GetAxis("Mouse X");
            float roll = Input.GetAxis("Mouse Y");

            float rotateY = (transform.localRotation.eulerAngles.y > 180) ? transform.localRotation.eulerAngles.y - 360 : transform.localRotation.eulerAngles.y;
            float rotateX = (transform.localRotation.eulerAngles.x > 180) ? transform.localRotation.eulerAngles.x - 360 : transform.localRotation.eulerAngles.x;

            float angleY = Mathf.Clamp(rotateY + yaw * speed, minAngleY, maxAngleY);
            float angleX = Mathf.Clamp(rotateX + roll * speed, minAngleX, maxAngleX);
            // 回転角度を-180～180から0～360に変換
            angleY = (angleY < 0) ? angleY + 360 : angleY;
            angleX = (angleX < 0) ? angleX + 360 : angleX;
            
            if (MainCamera.activeInHierarchy){
                centerPos = targetObj.transform.position;
                centerPos.y += 1;
            }else{
                centerPos = ThirdPersonCamera.transform.position;
            }
            transform.RotateAround(centerPos, directionY * Vector3.up, angleY - transform.localRotation.eulerAngles.y);
            transform.RotateAround(centerPos, directionX * transform.right, angleX - transform.localRotation.eulerAngles.x);
        }
        // キー入力でスクリーンショットを撮る
        if(Input.GetKeyDown(KeyCode.Z)){
            grab = true;
        }
    }
}
