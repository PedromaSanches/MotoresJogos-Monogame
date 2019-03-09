using System;
using System.Collections;
using System.Collections.Generic;


/********* Código retirado de: https://blog.bitbull.com/2016/06/30/optimising-memory-use-in-monogame/ *********/

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

    }
    /*
	  Template for a generically-typed object pool - pooled objects must
	  implement the IPoolable interface and support a parameterless constructor.

	  To create a new pool - ObjectPool pool = new ObjectPool(int initial_capacity)
	 */
    public class NavePool<T> where T : INavePool, new()
    {
        // Living Ships
        private List<T> live_stack;
        // Dead Ships
        private List<T> dead_stack;

        // The total capacity of the pool - this I only really use this for debugging
        private int capacity;

        /*
		 Creates a new object pool with the specifed initial number of objects
		 */
        public NavePool(int capacity, T template)
        {
            this.capacity = capacity;
            live_stack = new List<T>(this.capacity);
            dead_stack = new List<T>(this.capacity);

            for (int i = 0; i < this.capacity; i++)
            {
                AddNewObject(template);
            }
        }

        /*
		 Adds a new object to the pool - ideally this doesn't happen very often 
		 other than when the pool is constructed
		 */
        private void AddNewObject(T obj)
        {
            live_stack.Add(obj);
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
            live_stack.Remove(obj);
            dead_stack.Add(obj);
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
            if (live_stack.Count == 0)
            {
                //There aren't any living ship so we revive the dead ones
                ReviveDead();
            }
            T obj = live_stack[0];
            Release(obj);
            obj.Initialize();
            return obj;
        }

        //Add all the ships in the dead stack to the living stack, clear dead_stack
        public void ReviveDead()
        {
            foreach (T obj in dead_stack)
            {
                live_stack.Add(obj);
            }
            foreach (T obj in live_stack)
            {
               dead_stack.Remove(obj);
            }
        }
    }

}