using System;
using UnityEngine;
public class Interrector: MonoBehaviour, IInterrectable
{
	public bool triggered;
	public tk2dTextMesh textMesh;
	public string successText;
	public string failedText;
	public string futherText;
	public Interrector[] conditions;

	public bool IsTriggered()
	{
		return triggered;
	}
	
	public void Interract()
	{
		if(triggered){
			OnFurtherInterraction();
		}
		else{
			bool interractable = true;
			foreach (var o in conditions) {
				if (!o.IsTriggered()) {
					interractable = false;
					break;
				}
			}
			if (interractable) {
				triggered = true;
				OnInterraction();
			} else {

				OnInterractionFailed();
			}
		}
	}
	//API
	public virtual void OnInterraction()
	{
		textMesh.text = successText;
		textMesh.Commit();
	}

	public virtual void OnInterractionFailed()
	{
		textMesh.text = failedText;
		textMesh.Commit();
	}

	public virtual void OnFurtherInterraction()
	{
		textMesh.text = futherText;
		textMesh.Commit();
	}
}

