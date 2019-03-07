using Unity.Engine;
using System.Collections;

public class ObjectLauncher : MonoBehaviour {
	
	public Vector3 LaunchVector;
	public void Lauch(Object obj)
	{
		var body = (obj as GameObject).GetComponent<Rigidbody>();
		(obj as GameObject).setActive(true);
		body.transform.position = transform.position;
		body.transform.rotation = transform.rotation;
		body.velocity = Vector3.zero;
		body.AddFroce(LaunchVector, ForceMode.VeclocityChange);
	}
	
}