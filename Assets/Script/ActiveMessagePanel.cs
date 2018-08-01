    
using UnityEngine;
using System.Collections;
    
public class ActiveMessagePanel : MonoBehaviour {

    //　MessageUIに設定されているMessageスクリプトを設定
    [SerializeField]
    private Message messageScript;

    private GameObject refObj;

    public string cubeTag="Cube";
    Ray ray;
    RaycastHit hit = new RaycastHit();

    //　表示させるメッセージ
    private string message = "";

    void Start() {
        refObj = GameObject.Find( "MessageUI" );
        messageScript = refObj.GetComponent<Message>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if(hit.collider.gameObject.CompareTag(cubeTag))
                {
                    // cubeControlクラスにmessageScriptオブジェクトを渡して，定められたメッセージを表示する.
                    hit.collider.gameObject.GetComponent<CubeControl>().OnUserAction(messageScript);
                }
            }
        }
    }
}