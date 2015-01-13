using UnityEngine;
using System.Collections;

public class JenkinsCreator : MonoBehaviour {

	public GameObject jenkinsPrefab;

	public float instantiateInterval = 1.0f;

	private Vector3 jenkinsAppearPoint;
	private Quaternion jenkinsRotation;

	void Start() {
		jenkinsAppearPoint = this.transform.position;
		jenkinsRotation = this.transform.rotation;

		StartCoroutine("InstantiateLoop");
	}

	//void Update() {
	IEnumerator InstantiateLoop() {
		while( true ) {
			yield return new WaitForSeconds(instantiateInterval);
			GameObject.Instantiate( jenkinsPrefab, jenkinsAppearPoint, jenkinsRotation);
		}
	}
}
