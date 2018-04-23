using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CubeControl : MonoBehaviour {

    // Rigidbody rigidBody;
    // GameObject messagepanel;
    // public Vector3 force = new Vector3(0, 10, 0);
    // public ForceMode forceMode = ForceMode.VelocityChange;

    private string message = "テストメッセージ\n"
    + "ここに種ごとの説明が入る予定\n";

    // Use this for initialization
    void Start () {
        // rigidBody = gameObject.GetComponent<Rigidbody>();
        // 0.0 以上 1.0 以下の乱数を取得する
    }

    void Update(){
        float x = Random.Range(-0.1f, 0.1f);
        // float y = Random.Range(-0.1f, 0.1f);
        // float z = Random.Range(-0.1f, 0.1f);
        float y = 0;
        float z = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(new Vector3(x, y, z)), 0.1f);
        this.transform.localPosition += new Vector3(0, -0.3f, 0);
    }

    public void OnUserAction(Message messageScript)
    {
        messageScript.SetMessagePanel(message);
    }
}