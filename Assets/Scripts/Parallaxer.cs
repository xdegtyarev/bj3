using UnityEngine;
public class Parallaxer: MonoBehaviour
{
	public float scale;
	public void Update()
	{
		transform.position = new Vector3((Camera.main.transform.position.x+240)/scale, transform.position.y, transform.position.z);
	}
}


