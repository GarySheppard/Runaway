using UnityEngine;
using System.Collections;

public class PropExplosion : MonoBehaviour
{
	public float explosionRadius;

	public void BreakDown(float explosionForce)
	{
		Vector3 centerPos = transform.position;

		foreach (Transform child in transform)
		{
			Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
			rb.AddExplosionForce(explosionForce, centerPos, explosionRadius, Random.Range(0f, 0.5f), ForceMode.Impulse);
			//Destroy(gameObject, 5f);
		}
	}
}
