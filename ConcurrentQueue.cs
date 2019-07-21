using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureNative.SDK
{
    public class ConcurrentQueue<T>
    {
        private Object _locker = new Object();
        private Queue<T> _queue = new Queue<T>();

        public ConcurrentQueue()
        {

        }


        public void Enqueue(T message)
        {
            lock (_locker)
            {
                _queue.Enqueue(message);
            }
        }

        public T Dequeue()
        {
            lock (_locker)
            {
                var item = _queue.Dequeue();
                return item;
            }
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }
    }
}
