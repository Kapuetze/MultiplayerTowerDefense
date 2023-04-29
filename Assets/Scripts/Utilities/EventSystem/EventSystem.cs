using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities.EventSystem
{
	public abstract class EventBase<T> where T : EventBase<T> {
        /*
         * The base Event,
         * might have some generic text
         * for doing Debug.Log?
         */
        public string Description;

        private bool hasFired;
        public delegate void EventListener(T info);
        private static event EventListener listeners;
        
        public static void RegisterListener(EventListener listener) {
            listeners += listener;
        }

        public static void UnregisterListener(EventListener listener) {
            listeners -= listener;
        }

        public void FireEvent() {
            if (hasFired) {
                throw new Exception("This event has already fired, to prevent infinite loops you can't refire an event");
            }
            hasFired = true;
            if (listeners != null) {
                listeners(this as T);
            }
        }
    }
}
