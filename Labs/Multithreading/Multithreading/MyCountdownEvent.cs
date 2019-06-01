using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    public class CMyCountdownEvent : IDisposable
    {
        private Int32 _initialCount;
        private volatile Int32 _currentCount;
        private volatile Boolean _isDisposed;
        private readonly Object _lockObj;
        private readonly ManualResetEvent manualResetEvent;

        public Int32 InitialCount => _initialCount;
        public Int32 CurrentCount => _currentCount;
        public Boolean IsSet => _currentCount <= 0;

        //использовать только AutoResetEvent, ManualResetEvent, Mutex, Semaphore, lock().
        public CMyCountdownEvent(Int32 initialCount)
        {
            if (initialCount < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid initalization of the event's count: initialCount value is below zero");
            }

            _initialCount = initialCount;
            _currentCount = initialCount;

            _lockObj = new Object();
            manualResetEvent = new ManualResetEvent(false);

            if (initialCount == 0)
                manualResetEvent.Set();
        }
        //Если вызывается Signal после достижении нуля, то генерируется исключение
        //уменьшает count на 1
        public void Signal()
        {
            ThrowIfDisposed();

            lock (_lockObj)
            {
                if (_currentCount <= 0)
                    throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");

                Int32 newCount = Interlocked.Decrement(ref _currentCount);

                if (newCount == 0)
                    manualResetEvent.Set();
                else if (newCount < 0) //there was a thread already which decremented it to zero and set the event
                    throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
            }
        }
        //Если signalCount превышает оставшиеся счетчики, то генерируется исключение
        //уменьшает count на указанное число
        public void Signal(Int32 signalCount)
        {
            ThrowIfDisposed();

            lock (_lockObj)
            {
                if (_initialCount < signalCount)
                    throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
                _initialCount -= signalCount;

                if (_initialCount == 0)
                    manualResetEvent.Set();
            }
        }

        //Если timeout истекает до того, как count достигнет 0, то возвращается false, иначе true. 
        //блокирует поток, пока count не станет равным 0
        public Boolean Wait(TimeSpan timeout)
        {
            ThrowIfDisposed();
            //lock(_lockObj)
            //{
            //    //while (_initialCount > 0)
            //    //{
            //    //    //Monitor.Pulse(_lockObj);
            //    //    res = manualResetEvent.WaitOne(timeout);
            //    //    //Monitor.Wait(_lockObj);
            //    //}
            //    //var res = true;
            //    //res = manualResetEvent.WaitOne(timeout);
            //    //return res;
            //}   
            return manualResetEvent.WaitOne(timeout);
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("CountdownEvent is disposed already");
            }
        }

        #region DisposePattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                manualResetEvent.Dispose();
                _isDisposed = true;
            }
        } 

        #endregion
    }

}
