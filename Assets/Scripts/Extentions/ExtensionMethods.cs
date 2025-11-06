using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods{}
//----------------------------------------------------------------------------------------------------------------------
public static class BrokerExtensions{
	/// <summary>
	/// Invokes the message via the Broker.
	/// </summary>
	/// <param name="message"></param>
	public static void InvokeExtension(this IMessage message){
		Broker.InvokeSubscribers(message.GetType(), message);
	}
}

public static class SomeOtherCoolExtensionsThatDontNeedItsOwnClassToFeelSpecialLikeBroker
{
	public static Vector3 To(this Vector3 from, Vector3 to) => to - from;
}
