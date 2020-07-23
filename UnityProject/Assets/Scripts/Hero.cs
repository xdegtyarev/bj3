using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
	public tk2dAnimatedSprite sprite;

	public float moveSpeed = 80.0f;

	float minDistanceToInterract = 10f;

	void OnEnable()
	{
		TouchManager.TouchBeganEvent += HandleTouchBeganEvent;
	}

	void OnDisable()
	{
		TouchManager.TouchBeganEvent -= HandleTouchBeganEvent;
	}

	RaycastHit hitInfo;
	void HandleTouchBeganEvent(TouchInfo touch)
	{
		if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hitInfo)) {
			if (hitInfo.collider.GetComponent(typeof(IInterrectable)) != null) {
				Debug.Log(Vector3.Distance(transform.position,hitInfo.collider.transform.position));
				if (Vector3.Distance(transform.position,hitInfo.collider.transform.position) > minDistanceToInterract) {
					Debug.Log("MANDI");
					MoveAndInterract(hitInfo.collider.GetComponent(typeof(IInterrectable)));
				} else {
					Debug.Log("I");
					Interract(hitInfo.collider.GetComponent(typeof(IInterrectable)) as IInterrectable);
				}
			} else {
				Move(hitInfo.point.x);
			}
		} else {
			Move(hitInfo.point.x);
		}
	}
	
	void Move(float x)
	{
		StopMove();
		if (x < transform.position.x) {
			sprite.scale = new Vector3(-1, 1, 1);
		} else {
			sprite.scale = new Vector3(1, 1, 1);
		}
		sprite.Play("Walk");
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", new Vector3(x, transform.position.y, transform.position.z), 
			"speed", moveSpeed, 
			"easetype", iTween.EaseType.linear,
			"oncomplete", "OnMoveComplete", 
			"oncompletetarget", gameObject));
	}

	void Move(float x,string hook,object param)
	{
		StopMove();
		if (x < transform.position.x) {
			sprite.scale = new Vector3(-1, 1, 1);
		} else {
			sprite.scale = new Vector3(1, 1, 1);
		}
		sprite.Play("Walk");
		Debug.Log("new x: " + x);
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", new Vector3(x, transform.position.y, transform.position.z), 
			"speed", moveSpeed, 
			"easetype", iTween.EaseType.linear,
			"oncomplete", hook, 
			"oncompletetarget", gameObject,
			"oncompleteparams", param));
	}


	void OnMoveComplete()
	{
		StopMove();
	}

	void MoveAndInterract(Component component)
	{
		StopMove();
		Move(component.transform.position.x, "Interract", component);
	}

	void Interract(IInterrectable component)
	{
		Debug.Log("Interracting");
		StopMove();
		component.Interract();
	}

	void StopMove()
	{
		iTween.Stop(gameObject);
		sprite.Play("Idle");
	}
}
