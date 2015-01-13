using UnityEngine;
using System.Collections;

public class JenkinsRotator : MonoBehaviour {

	public float speed = 2.0f;

	void Update () {
		this.transform.Rotate( Vector3.up, -Time.deltaTime * speed );
	}
}
