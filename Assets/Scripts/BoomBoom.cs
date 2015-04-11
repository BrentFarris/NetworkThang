using UnityEngine;
using System.Collections;

public class BoomBoom : MonoBehaviour
{
	[RPC]
	private void ScaleUp(float amount, string newName)
	{
		gameObject.name = "I'm bigger cause " + newName;
		transform.localScale += Vector3.one * amount;
	}

	private void Update()
	{
		if (!networkView.isMine) return;

		if (Input.GetKeyDown(KeyCode.Space))
			networkView.RPC("ScaleUp", RPCMode.All, 1.25f, "I'mma badass");
	}

	//private void Start()
	//{
	//	ImCoo("Brent", "Brent", "Brent");
	//}

	//private void ImCoo(params string[] names)
	//{
	//	foreach (string n in names)
	//		Debug.Log(n);
	//}
}