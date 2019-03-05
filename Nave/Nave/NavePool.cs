using System;
using System.Collections;
using System.Collections.Generic;

namespace Nave
{
    /*
	 * Any object that is to be pooled needs to implement this
	 * interface and support a parameterless constructor.
	 */
    public interface INavePool
    {
        /*
		 Should reset all the object's properties to their default state.
		 */
        void Initialize();

        /*
		 Called when an object is returned to the pool. The implementation of 
		 this method doesn't need to do anything but it can be useful for certain
		 cleanup operations (possibly including releasing attached IPoolables).
		 */
        void Release();

        /*
		 Set by the pool and used as a 'sanity check' to check this object came from
		 the pool originally
		 */
        bool PoolIsValid
        {
            get;
            set;
        }

        /*
		 Used by the pool as a 'sanity check' to ensure objects aren't freed twice.
		 */
        bool PoolIsFree
        {
            get;
            set;
        }
    }
    /*
	  Template for a generically-typed object pool - pooled objects must
	  implement the IPoolable interface and support a parameterless constructor.

	  To create a new pool - ObjectPool pool = new ObjectPool(int initial_capacity)
	 */
    public class NavePool<T> where T : INavePool, new()
    {
        // I use a Stack data structure for storing the objects as it should be 
        // more efficient than List and we don't have to worry about indexing 
        private Stack stack;
        // The total capacity of the pool - this I only really use this for debugging
        private int capacity;

        /*
		 Creates a new object pool with the specifed initial number of objects
		 */
        public NavePool(int capacity)
        {
            stack = new Stack(capacity);

            for (int i = 0; i < capacity; i++)
            {
                AddNewObject();
            }
        }

        /*
		 Adds a new object to the pool - ideally this doesn't happen very often 
		 other than when the pool is constructed
		 */
        private void AddNewObject()
        {
            T obj = new T();
            obj.PoolIsValid = true;
            stack.Push(obj);
            capacity++;
        }

        /*
		 * Releases an object from the pool - note that there's no real need 
		 * to throw the first exception here as if an object is freed twice it's not
		 * really a problem, however the fact that this is happening usually indicates 
		 * an issue with one's memory management that could cause issues later so I 
		 * prefer to leave it in.
		 */
        public void Release(T obj)
        {
            if (obj.PoolIsFree)
            {
                throw new Exception("POOL (" + this + "): Object already released " + obj);
            }
            else if (!obj.PoolIsValid)
            {
                throw new Exception("POOL (" + this + ") Object not valid " + obj);
            }
            obj.Release();
            obj.PoolIsFree = true;
            stack.Push(obj);
        }

        /*
		 * Retrieves an object from the pool - automatically create a new object if the pool
		 * has become depleted.
		 * 
		 * Calls Initialize() on the released object which should set all its parameters to
		 * their default values.
		 */
        public T Get()
        {
            if (stack.Count == 0)
            {
                AddNewObject();
            }
            T obj = (T)stack.Pop();
            obj.Initialize();
            obj.PoolIsFree = false;
            return obj;
        }
    }

}