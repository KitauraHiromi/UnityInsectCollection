using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]

public class CollectionDatabase : MonoBehaviour {

	public List<InsectParam> collection = new List<InsectParam>();

	// Use this for initialization
	void Start () {
		collection.Add(new InsectParam("test", "test", 0));	
	}
	
	public void test(){
		Debug.Log("test");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
