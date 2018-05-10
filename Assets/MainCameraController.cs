using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    [SerializeField]
    private GameObject MainCamera;   // インスペクターで主観カメラを紐づける
    [SerializeField]
    private GameObject ThirdPersonCamera;   // インスペクターで第三者視点カメラを紐づける
     
    GameObject targetObj;
    Vector3 targetPos;
    Quaternion MainCameraRotation, ThirdPersonCameraRotation, moveForward;
     
    void Start () {
        targetObj = GameObject.Find("unitychan");
        targetPos = targetObj.transform.position;
        moveForward = targetObj.transform.rotation;
        ThirdPersonCameraRotation = ThirdPersonCamera.transform.rotation;
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
                            // ↓現在のactive状態から反転 
            MainCamera.SetActive(!MainCamera.activeInHierarchy);
            ThirdPersonCamera.SetActive(!ThirdPersonCamera.activeInHierarchy);
            // MainCamera.transform.rotation = moveForward;
            // ThirdPersonCamera.transform.rotation = moveForward;
            ThirdPersonCamera.transform.localRotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
            ThirdPersonCamera.transform.localPosition = new Vector3(0, 0.5f, 3f);
        }

        // Cameraはunitychanの子要素なので，targetの移動量分、自分（カメラ）も移動するスクリプトを書く必要はない
        // transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;
        moveForward = targetObj.transform.rotation;
     
        // マウスの右クリックを押している間
        if (Input.GetMouseButton(1)) {
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する

            if (ThirdPersonCamera.activeInHierarchy) {
                ThirdPersonCamera.transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            }
            else if (MainCamera.activeInHierarchy) {
                MainCamera.transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
            }
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            // transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }
            // MainCameraRotation = MainCamera.transform.rotation;
            // ThirdPersonCameraRotation = ThirdPersonCamera.transform.rotation;
    }
}
