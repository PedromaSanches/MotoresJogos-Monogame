using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sanches.Concretes
{
	public class QueuePool<T> : IPool<T>
	{
		Func<T> produce;
		int capacity;
		T[] objects;
		int index;
		
		public QueuePool<T>(Func<T> factoryMehtod, int maxSize)
		{
			produce = factoryMehtod;
			capacity = maxSize;
			index = -1;
			object = new T[maxSize];
		}
		
		public T GetInstance()
		{
			index = (index + 1) % capacity;
			
			if(objects[index] == null)
			{
				objects[index] = produce();
			}
			
			return objects[index];
		}
	}
}