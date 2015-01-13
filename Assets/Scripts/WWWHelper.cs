using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.IO;

public class WWWHelper : MonoBehaviour {

	public string JobName;
	public Text TextObject;
	public Material BlueMaterial;
	public Material RedMaterial;
	public GameObject eyes;

	private string URL_BASE = "http://localhost:8080";
	private string jobApiURL;
	private bool jobResult = true;
	private bool currentResult = true;

	private float pastCheckTime = 0.0f;
	private float checkInterval = 1.0f;

	void Start() {
		jobApiURL = URL_BASE + "/job/" + JobName + "/api/xml";
		XmlTextReader reader = new XmlTextReader (jobApiURL);
		TextObject.text = JobName;
		eyes.SetActive(false);
		pastCheckTime = Time.fixedTime;

		while (reader.Read()) 
		{
			switch (reader.NodeType) 
			{
			case XmlNodeType.Element:
				//Debug.Log("<" + reader.Name);

				if( "color".Equals(reader.Name) ) {
					reader.Read();
					//Debug.Log("color: " + reader.Value);
				}
				/*
				while (reader.MoveToNextAttribute()) {
					Debug.Log(" " + reader.Name + "='" + reader.Value + "'");
				}
				//Debug.Log(">");
				//Debug.Log(">");
				*/
				break;
			case XmlNodeType.Text:
				//Debug.Log("text: " + reader.Value);
				break;
			case XmlNodeType.EndElement:
				//Debug.Log("</" + reader.Name);
				//Debug.Log(">");
				break;
			}
		}
		//jobResult = CheckBuildResult();
		Debug.Log("Start Corutine");
		//UpdateBuildResult();
		StartCoroutine("UpdateBuildResult");
	}

	/*
	IEnumerator CheckResult() {
		float interval = 5.0f;
		while( true ) {
			yield return new WaitForSeconds(interval);
			Debug.Log("test");
			jobResult = CheckBuildResult();
		}
	}
	*/

	void Update() {
		//Debug.Log(Time.fixedTime - pastCheckTime);
		if( checkInterval < Time.fixedTime - pastCheckTime ) {
			//jobResult = CheckBuildResult();
			StartCoroutine("UpdateBuildResult");
			currentResult = jobResult;
			pastCheckTime = Time.fixedTime;
		}

		if( jobResult ) {
			if( currentResult == false ) {
				ResetMaterials( BlueMaterial );
				eyes.SetActive(false);
				transform.rotation = Quaternion.LookRotation(Vector3.forward);
				currentResult = true;
			}
		} else {
			if( currentResult == true ) {
				ResetMaterials( RedMaterial );
				eyes.SetActive(true);
				currentResult = false;
			}
			this.transform.Rotate( Vector3.up, 10.0f );
		}


	}

	void ResetMaterials( Material materil ) {
		MeshRenderer[] childs = this.GetComponentsInChildren<MeshRenderer>();
		foreach( MeshRenderer target in childs ) {
			target.material = materil;
		}
	}

	IEnumerator UpdateBuildResult() {
		Debug.Log("check result" + jobApiURL);
		WWW jobXml = new WWW( jobApiURL );
		yield return jobXml;

		XmlTextReader reader = new XmlTextReader(new StringReader(jobXml.text));

		while(reader.Read()) {
			switch (reader.NodeType) 
			{
			case XmlNodeType.Element:
				//Debug.Log(reader.Name);
				if( "color".Equals(reader.Name) ) {
					reader.Read();
					//Debug.Log("color: " + reader.Value);
					if( "blue".Equals(reader.Value) ) {
						jobResult = true;
						//return true;
					} else {
						jobResult = false;
						//return false;
					}
				}
				break;
			}
			
		}
		//return true; // true default.
	}
}
