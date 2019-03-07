using UnityEngine;
using Sanches.Pooling;

public class MonoPool : MonoBehaviour
{
	
	public int Capacity;
	public IPool<GameObject> Pool { get; private set;}
	public GameObject Prototype;
	public enum PoolType { Queue, List }
	public PoolType PoolType = PoolType.Queue;
	public bool Expandable = true;
	
	void Awake()
	{
		switch(PoolType)
		{
			case PoolType.Queue:
				Pool = new QueuePool<GameObject>(()=>Instantiate(Prototype), Capacity);
				break;
			case PoolType.List:
				Pool = new ListPool<GameObject>(()=>Instantiate(Prototype), Capacity, g=>g.activeHierarchy, Expandable);
				break;
			default:
				return;
		}
		
		
	}
	
}