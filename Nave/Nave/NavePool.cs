using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


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


        /*
        Set a Position in world space for the object
        */
        void SetNaveModel(NaveModel model);

        /*
        Set a Position in world space for the object
        */
        void Draw();
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
        public NavePool(int capacity)
        {
            this.capacity = capacity;
            live_stack = new List<T>(this.capacity);
            dead_stack = new List<T>(this.capacity);

            for (int i = 0; i < this.capacity; i++)
            {
                T obj = new T();
                obj.Initialize();
                live_stack.Add(obj);
            }
        }

        //Auxilially to set a nave model to shit
        public void SetModel(int index, NaveModel naveModel)
        {
            live_stack[index].SetNaveModel(naveModel);
        }

        public int Size()
        {
            return capacity;
        }

        //Gets a new obj from the Live Stack (removes from the list)
        public T Get(int index)
        {
            if (live_stack.Count == 0)
            {
                //There aren't any living ship so we revive the dead ones
                ReviveAllDead();
            }
            return live_stack[index];
        }

        //Puts 
        public void Kill(T obj)
        {
            live_stack.Remove(obj);
            obj.Release();
            dead_stack.Add(obj);
        }

        //Revive a sinlge ship
        public void ReviveDead()
        {
            for (int i = 0; i < dead_stack.Count; i++)
            {
                T obj = dead_stack[i];
                obj.Initialize();
                live_stack.Add(obj);
            }

            for (int i = 0; i < live_stack.Count; i++)
            {
                dead_stack.Remove(dead_stack[i]);
            }
        }

        //Add all the ships in the dead stack to the living stack, clear dead_stack
        public void ReviveAllDead()
        {
            foreach (T obj in dead_stack)
            {
                obj.Initialize();
                live_stack.Add(obj);
            }
            foreach (T obj in live_stack)
            {
               dead_stack.Remove(obj);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < this.capacity; i++)
            {
                live_stack[i].Draw();
            }

        }
    }

}