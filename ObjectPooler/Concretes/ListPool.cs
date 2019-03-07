using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sanches.Concretes
{
	public class ListPool<T> : IPool<T> where T : class
	{
		Func<T> produce;
		int capacity;
		List<T> objects;
		Func<T, bool> useTest;
		bool expandable;
		
		public ListPool<T>(Func<T> factoryMehtod, int maxSize, Func<T, bool> inUse, bool expandable = true)
		{
			produce = factoryMehtod;
			capacity = maxSize;
			object = new List<T>(maxSize);
			useTest = inUse;
			this.expandable = expandable;
		}
		
		public T GetInstance()
		{
			var count = objects.Count;
			
			foreach(var item in objects)
			{
				if(!useTest(item))
				{
					return item;
				}
			}
			
			if(count >= capacity && !expandable)
			{
				return null;
			}
			else
			{
				var obj = produce();
				objects.Add(obj);
				return(obj);
			}
			
			
		}
	}
}