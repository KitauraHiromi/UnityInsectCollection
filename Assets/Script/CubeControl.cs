using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CubeControl : MonoBehaviour {

    // Rigidbody rigidBody;
    // GameObject messagepanel;
    // public Vector3 force = new Vector3(0, 10, 0);
    // public ForceMode forceMode = ForceMode.VelocityChange;

    private float speed, radius, yPosition;
    private CharacterController characterController;
    private Animator animator;

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
        speed = 1.0f;
        radius = 0.3f;
        characterController = GetComponent <CharacterController> ();
        animator = GetComponent <Animator> ();
        animator.SetBool("IsRoost", false);
    }

    void Update(){
        // グローバルな移動を実装
        // WalkAround(ref x, ref y, ref z);
        float dist = Vector3.Distance(this.transform.position, new Vector3(0, 0, 0));
        Vector3 plane = new Vector3(0, -1, 0);

        if (dist > 0.5){
            SpeedControl(dist, 1.0f);
            GoToDestination(new Vector3(0, 0, 0));
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
}