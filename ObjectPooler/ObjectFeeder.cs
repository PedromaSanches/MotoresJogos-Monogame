using Unity.Engine;
using System.Collections;

public class ObjectFeeder : MonoBehaviour 
{
	public MonoPool SourcePool;
	public string Message;
	public GameObject Destination;
	public float InvokeDelay = 1f;
	public float InvokePeriod = 0.5f;
	
	void Start()
	{
		InvokeRepeating("FeedObject", InvokeDelay, InvokePeriod);
	}
	
	//Update is called once per frame
	void FeedObject() 
	{
		Destination.SendMessage(Message, SourcePool.Pool.GetInstance());
	}
	
}