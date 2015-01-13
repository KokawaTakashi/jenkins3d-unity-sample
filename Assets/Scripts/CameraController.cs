using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Vector3 endPosition;
	public float cameraSpeed;

	private Vector3 cameraPosition;

	void Start () {
		cameraPosition = this.transform.position;
	}
	
	void Update () {
		if( 0 < Vector3.Distance( cameraPosition, endPosition ) ) {
			cameraPosition = this.transform.position;
			this.transform.position = Vector3.Slerp( cameraPosition, endPosition, cameraSpeed );
		}
	}
}
