using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Snap : MonoBehaviour {

	public float snapValue = 1f;
	public float offsetX = 0f;
	public float offsetY = 0f;
	public float offsetZ = 0f;

	void Update () {
		float snapInverse = 1.0f / snapValue;

		float x, y, z;


		x = (Mathf.Round ((transform.position.x+offsetX) * snapInverse) / snapInverse)-offsetX;
		y = (Mathf.Round ((transform.position.y+offsetY) * snapInverse) / snapInverse)-offsetY;
		z = (Mathf.Round ((transform.position.z+offsetZ) * snapInverse) / snapInverse)-offsetZ;

		transform.position = new Vector3 (x, y, z);
	}
}
