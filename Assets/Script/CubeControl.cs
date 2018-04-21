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
    }
    
    public void OnUserAction(Message messageScript)
    {
        messageScript.SetMessagePanel(message);
    }
}