using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {
	public float radius = 50.0f;
	public float power = 100.0f;

	void Update () {
		if( Input.GetKeyUp(KeyCode.Space) ) {
			Debug.Log("Explosion!!");
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
			foreach (Collider hit in colliders) {
				if (hit && hit.rigidbody) {
					//Debug.Log("log");
					hit.rigidbody.AddExplosionForce(power, explosionPos, radius, 3.0f);
				}
			}
		}
	}
}
