using UnityEngine;
using System.Collections;

public class SingleButtonInput : MonoBehaviour {
	public delegate bool GetButtonDelegate();
	public delegate bool GetButtonDownDelegate();

	private static GetButtonDelegate getButtonDelegate = DefaultGetButtonDelegate;
	private static GetButtonDownDelegate getButtonDownDelegate = DefaultGetButtonDownDelegate;

	public static bool GetButton()
	{
		return getButtonDelegate();
	}

	public static bool GetButtonDown()
	{
		return getButtonDownDelegate();
	}

	public static void SetGetButtonDelegate(GetButtonDelegate d)
	{
		getButtonDelegate = d;
	}

	public static void SetGetButtonDownDelegate(GetButtonDownDelegate d)
	{
		getButtonDownDelegate = d;
	}

	private static bool DefaultGetButtonDelegate()
	{
		return Input.GetKey(KeyCode.Space);
	}

	private static bool DefaultGetButtonDownDelegate()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
}