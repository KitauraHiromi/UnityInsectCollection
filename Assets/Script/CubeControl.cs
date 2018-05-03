using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CubeControl : MonoBehaviour {

    // Rigidbody rigidBody;
    // GameObject messagepanel;
    // public Vector3 force = new Vector3(0, 10, 0);
    // public ForceMode forceMode = ForceMode.VelocityChange;

    float x, y, z;
    float speed, radius, yPosition;

    private string message = "テストメッセージ\n"
    + "ここに種ごとの説明が入る予定\n";

    void RandomWalk(ref float x, ref float y, ref float z){

    }

    void GoToDestination(Vector3 destination, ref float x, ref float y, ref float z){
        Vector3 tmp = destination - this.transform.position;
        tmp /= tmp.magnitude;
        x = tmp[0] * speed;
        y = tmp[1] * speed;
        z = tmp[2] * speed;
    }

    void WalkAround(ref float x, ref float y, ref float z){
        x = radius * Mathf.Sin(Time.time * speed / 2 / radius / Mathf.PI);
        y = 0.0f;
        z = radius * Mathf.Cos (Time.time * speed / 2/ radius / Mathf.PI);
    }

    // Use this for initialization
    void Start () {
        speed = 1.0f;
        radius = 0.3f;
    }

    void Update(){
        // グローバルな移動を実装
        // WalkAround(ref x, ref y, ref z);
        GoToDestination(new Vector3(0, 0, 0), ref x, ref y, ref z);
        this.transform.rotation = Quaternion.LookRotation(new Vector3(-x, -y, -z));
        this.transform.position += new Vector3(x, y, z);
    }


    public void OnUserAction(Message messageScript)
    {
        messageScript.SetMessagePanel(message);
    }
}