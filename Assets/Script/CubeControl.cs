using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
public class CubeControl : MonoBehaviour {

    // Rigidbody rigidBody;
    // GameObject messagepanel;
    // public Vector3 force = new Vector3(0, 10, 0);
    // public ForceMode forceMode = ForceMode.VelocityChange;
    private GameObject refObj, effObj;
    private float speed, radius, yPosition;
    // private CharacterController characterController;
    CollectionDatabase database;
    List<InsectParam> collection;
    private Animator animator;
    static string name = "test";
    static string tag = "test";
    bool flag = false;
    public InsectParam param = new InsectParam(name, tag, 1);

    private string message = "テストメッセージ\n"
    + "ここに種ごとの説明が入る予定\n";

    void RandomWalk(){

    }

    void GoToDestination(Vector3 destination){
        Vector3 tmp = destination - this.transform.position;
        tmp /= tmp.magnitude;
        float _x = tmp[0] * speed;
        float _y = tmp[1] * speed;
        float _z = tmp[2] * speed;
        this.transform.rotation = Quaternion.LookRotation(new Vector3(-_x, -_y, -_z));
        this.transform.position += new Vector3(_x, _y, _z);
    }

    void Roost(Vector3 plane){
        speed = 0f;
        this.transform.rotation = Quaternion.LookRotation(plane);
    }

    void WalkAround(){
        float _x = radius * Mathf.Sin(Time.time * speed / 2 / radius / Mathf.PI);
        float _y = 0.0f;
        float _z = radius * Mathf.Cos (Time.time * speed / 2/ radius / Mathf.PI);
        this.transform.rotation = Quaternion.LookRotation(new Vector3(-_x, -_y, -_z));
        this.transform.position += new Vector3(_x, _y, _z);
    }

    void SpeedControl(float dist, float thresold){
        if (dist < thresold){
            speed = speed * dist / thresold;
        }
    }

    // Use this for initialization
    void Start () {
        speed = 0.1f;
        radius = 0.3f;
        // characterController = GetComponent <CharacterController> ();
        animator = GetComponent <Animator> ();
        animator.SetBool("IsRoost", false);
        refObj = GameObject.Find( "Database" );
        effObj = GameObject.Find( "Eff_Hit_1" );
        effObj.gameObject.SetActive(false);
        database = refObj.GetComponent <CollectionDatabase> ();
        collection = database.collection;
        if(collection == null) Debug.Log("null");
    }

    void Update(){
        // グローバルな移動を実装
        // WalkAround(ref x, ref y, ref z);
        float dist = Vector3.Distance(this.transform.position, new Vector3(0, 0, 0));
        Vector3 plane = new Vector3(0, -1, 0);

        if (dist > 0.5){
            // SpeedControl(Math.Min(dist, 0.3f) , 1.0f);
            GoToDestination(new Vector3(48f, 5.5f, 54f));
        }
        else{
            Roost(plane);
            animator.SetBool("IsRoost", true);
        }
    }


    public void OnUserAction(Message messageScript)
    {
        messageScript.SetMessagePanel(message);
    }

    IEnumerator sleep(){ 
        Debug.Log("start");
        yield return new WaitForSeconds(1f);
        Debug.Log("10seconds");
    }

    void OnCollisionEnter(Collision col)
    {
        // 捕まえた個体のパラメータをデータベースオブジェクトに格納
        collection.Add(param);
        // 捕まえた個体を削除
        Destroy(this.gameObject);
        effObj.gameObject.transform.position = this.gameObject.transform.position;
        effObj.gameObject.SetActive(true);
    }
}