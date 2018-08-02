using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InsectParam{
	public string name;
	public string tag;
	public int id;

	public InsectParam(string _name, string _tag, int _id){
		name = _name;
		tag = _tag;
		id = _id;
	}
}
