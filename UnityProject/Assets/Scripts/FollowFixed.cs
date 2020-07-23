using UnityEngine;

public class FollowFixed : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
	void Update () {
  		transform.position = target.transform.position + offset;
	}
}
