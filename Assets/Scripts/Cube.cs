using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
	public float networkInterpolationSpeed = 0.1f;
	private Vector3 finalDestination = Vector3.zero;
	private Quaternion finalRotation = Quaternion.identity;
	private Vector3 finalScale = Vector3.zero;

	private void Start()
	{
		finalDestination = transform.position;
		finalRotation = transform.rotation;
		finalScale = transform.localScale;
	}

	private void Update()
	{
		if (!networkView.isMine)
		{
			transform.position = Vector3.Lerp(transform.position, finalDestination, networkInterpolationSpeed);
			transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, networkInterpolationSpeed);
			transform.localScale = Vector3.Lerp(transform.localScale, finalScale, networkInterpolationSpeed);
		}
	}

	private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 currentPosition = transform.position;
			Quaternion currentRotation = transform.rotation;
			Vector3 currentScale = transform.localScale;

			stream.Serialize(ref currentPosition);
			stream.Serialize(ref currentRotation);
			stream.Serialize(ref currentScale);
		}
		else
		{
			stream.Serialize(ref finalDestination);
			stream.Serialize(ref finalRotation);
			stream.Serialize(ref finalScale);
		}
	}
}