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
                live_stack.Add(template);
            }
        }


        //Gets a new obj from the Live Stack (removes from the list)
        public T GetNew()
        {
            if (live_stack.Count == 0)
            {
                //There aren't any living ship so we revive the dead ones
                ReviveAllDead();
            }
            T obj = live_stack[0];
            live_stack.Remove(obj);
            obj.Initialize();
            return obj;
        }

        //Puts 
        public void Kill(T obj)
        {
            obj.Release();
            dead_stack.Add(obj);
        }

        //Revive a sinlge ship
        public T ReviveDead()
        {
            if (dead_stack.Count == 0)
            {
                return default(T); 
            }
            T obj = dead_stack[0];
            dead_stack.Remove(obj);
            obj.Initialize();
            live_stack.Add(obj);
            return obj;
        }

        //Add all the ships in the dead stack to the living stack, clear dead_stack
        public void ReviveAllDead()
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