using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EzTvix.Provider.API
{

    #region  ProviderWorker

    public class ProviderWorker : Component
    {
        bool m_CancelPending = false;
        bool m_ReportsProgress = false;
        bool m_SupportsCancellation = false;

        bool m_CoverProgress = false;
        bool m_FanartProgress = false;

        public event DoWorkEventHandler DoWork;
        public event ProgressChangedEventHandler ProgressChanged;
        public event CoverChangedEventHandler CoverChanged;
        public event FanartChangedEventHandler FanartChanged;
        public event ObjectLoadedEventHandler ObjectLoaded;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        public bool WorkerSupportsCancellation
        {
            get
            {
                lock (this)
                {
                    return m_SupportsCancellation;
                }
            }
            set
            {
                lock (this)
                {
                    m_SupportsCancellation = value;
                }
            }
        }
        public bool WorkerReportsProgress
        {
            get
            {
                lock (this)
                {
                    return m_ReportsProgress;
                }
            }
            set
            {
                lock (this)
                {
                    m_ReportsProgress = value;
                }
            }
        }
        public bool WorkerReportsCoverProgress
        {
            get
            {
                lock (this)
                {
                    return m_CoverProgress;
                }
            }
            set
            {
                lock (this)
                {
                    m_CoverProgress = value;
                }
            }
        }
        public bool WorkerReportsFanartProgress
        {
            get
            {
                lock (this)
                {
                    return m_FanartProgress;
                }
            }
            set
            {
                lock (this)
                {
                    m_FanartProgress = value;
                }
            }
        }
        public bool CancellationPending
        {
            get
            {
                lock (this)
                {
                    return m_CancelPending;
                }
            }
        }

        public void RunWorkerAsync()
        {
            RunWorkerAsync(null);
        }
        public void RunWorkerAsync(object argument)
        {
            m_CancelPending = false;

            if (DoWork != null)
            {

                DoWorkProviderEventArgs args = new DoWorkProviderEventArgs(argument);
                AsyncCallback callback;

                callback = new AsyncCallback(ReportCompletion);
                DoWork.BeginInvoke(this, args, callback, args);
            }
        }

        public void ReportProgress(int percent)
        {
            if (WorkerReportsProgress)
            {
                ProgressChangedProviderEventArgs progressArgs;
                progressArgs = new ProgressChangedProviderEventArgs(percent);
                OnProgressChanged(progressArgs);
            }

        }
        public void ReportProgress(object obj)
        {
            if (WorkerReportsProgress)
            {
                ProgressChangedProviderEventArgs progressArgs;
                progressArgs = new ProgressChangedProviderEventArgs(obj);
                OnProgressChanged(progressArgs);
            }

        }

        public void ReportCoverProgress(int percent)
        {
            if (WorkerReportsCoverProgress)
            {
                CoverProgressChangedProviderEventArgs progressArgs;
                progressArgs = new CoverProgressChangedProviderEventArgs(percent);
                OnCoverProgressChanged(progressArgs);
            }

        }
        public void ReportCoverProgress(object obj)
        {
            if (WorkerReportsCoverProgress)
            {
                CoverProgressChangedProviderEventArgs progressArgs;
                progressArgs = new CoverProgressChangedProviderEventArgs(obj);
                OnCoverProgressChanged(progressArgs);
            }
        }

        public void ReportFanartProgress(int percent)
        {
            if (WorkerReportsFanartProgress)
            {
                FanartProgressChangedProviderEventArgs progressArgs;
                progressArgs = new FanartProgressChangedProviderEventArgs(percent);
                OnFanartProgressChanged(progressArgs);
            }

        }
        public void ReportFanartProgress(object obj)
        {
            if (WorkerReportsCoverProgress)
            {
                FanartProgressChangedProviderEventArgs progressArgs;
                progressArgs = new FanartProgressChangedProviderEventArgs(obj);
                OnFanartProgressChanged(progressArgs);
            }
        }


        /// <summary>
        /// Function to call to raise event "ObjectLoaded"
        /// </summary>
        /// <param name="obj">the generic object that is loaded</param>
        public void ReportObjectLoaded(object obj)
        {
            if (WorkerReportsProgress)
            {
                ObjectLoadedProviderEventArgs progressArgs;
                progressArgs = new ObjectLoadedProviderEventArgs(obj);
                OnObjectLoaded(progressArgs);
            }
        }
        public void ReportObjectLoaded(int percent, object obj)
        {
            if (WorkerReportsProgress)
            {
                ObjectLoadedProviderEventArgs progressArgs;
                progressArgs = new ObjectLoadedProviderEventArgs(percent, obj);
                OnObjectLoaded(progressArgs);
            }
        }

        public void CancelAsync()
        {
            lock (this)
            {
                m_CancelPending = true;
            }
        }

        protected virtual void OnProgressChanged(ProgressChangedProviderEventArgs progressArgs)
        {
            ProcessDelegate(ProgressChanged, this, progressArgs);
        }
        protected virtual void OnCoverProgressChanged(CoverProgressChangedProviderEventArgs progressArgs)
        {
            ProcessDelegate(CoverChanged, this, progressArgs);
        }
        protected virtual void OnFanartProgressChanged(FanartProgressChangedProviderEventArgs progressArgs)
        {
            ProcessDelegate(FanartChanged, this, progressArgs);
        }
        protected virtual void OnObjectLoaded(ObjectLoadedProviderEventArgs progressArgs)
        {
            ProcessDelegate(ObjectLoaded, this, progressArgs);
        }
        protected virtual void OnRunWorkerCompleted(RunWorkerCompletedProviderEventArgs completedArgs)
        {
            ProcessDelegate(RunWorkerCompleted, this, completedArgs);
        }


        public delegate void DoWorkEventHandler(object sender, DoWorkProviderEventArgs e);

        public delegate void ProgressChangedEventHandler(object sender, ProgressChangedProviderEventArgs e);

        public delegate void CoverChangedEventHandler(object sender, CoverProgressChangedProviderEventArgs e);

        public delegate void FanartChangedEventHandler(object sender, FanartProgressChangedProviderEventArgs e);

        public delegate void ObjectLoadedEventHandler(object sender, ObjectLoadedProviderEventArgs e);

        public delegate void RunWorkerCompletedEventHandler(object sender, RunWorkerCompletedProviderEventArgs e);


        void ProcessDelegate(Delegate del, params object[] args)
        {
            Delegate temp = del;
            if (temp == null)
            {
                return;
            }

            Delegate[] delegates = temp.GetInvocationList();
            foreach (Delegate handler in delegates)
            {
                InvokeDelegate(handler, args);
            }

        }

        void InvokeDelegate(Delegate del, object[] args)
        {
            System.ComponentModel.ISynchronizeInvoke synchronizer;
            synchronizer = del.Target as System.ComponentModel.ISynchronizeInvoke;
            if (synchronizer != null) //A Windows Forms object
            {
                if (synchronizer.InvokeRequired == false)
                {
                    del.DynamicInvoke(args);
                    return;
                }

                try
                {
                    synchronizer.Invoke(del, args);
                }
                catch
                { }
            }

            else //Not a Windows Forms object
            {

                del.DynamicInvoke(args);

            }

        }

        void ReportCompletion(IAsyncResult asyncResult)
        {

            System.Runtime.Remoting.Messaging.AsyncResult ar = (System.Runtime.Remoting.Messaging.AsyncResult)asyncResult;

            DoWorkEventHandler del;

            del = (DoWorkEventHandler)ar.AsyncDelegate;

            DoWorkProviderEventArgs doWorkArgs = (DoWorkProviderEventArgs)ar.AsyncState;

            object result = null;

            Exception error = null;

            try
            {

                del.EndInvoke(asyncResult);

                result = doWorkArgs.Result;

            }

            catch (Exception exception)
            {

                error = exception;

            }

            RunWorkerCompletedProviderEventArgs completedArgs = new RunWorkerCompletedProviderEventArgs(result, error, doWorkArgs.Cancel);

            OnRunWorkerCompleted(completedArgs);

        }

        public void resetEventHandler()
        {
            this.DoWork = null;
            this.ProgressChanged = null;
            this.FanartChanged = null;
            this.CoverChanged = null;
            this.ObjectLoaded = null;
            this.RunWorkerCompleted = null;
        }
    }

    #endregion

    #region AsyncCompletedEventArgs
    public class AsyncCompletedProviderEventArgs : EventArgs
    {
        public AsyncCompletedProviderEventArgs(bool cancelled, Exception ex)
        {
            Cancelled = cancelled;
            Error = ex;
        }
        public AsyncCompletedProviderEventArgs() { }

        public readonly Exception Error;

        public readonly bool Cancelled;

    }

    #endregion

    #region CancelEventArgs
    public class CancelProviderEventArgs : EventArgs
    {
        private bool _cancel = false;
        public bool Cancel
        {
            get
            {
                return _cancel;
            }
            set
            {
                _cancel = value;
            }
        }
    }

    #endregion

    #region DoWorkProviderEventArgs
    public class DoWorkProviderEventArgs : CancelProviderEventArgs
    {
        public bool Result
        {
            get
            {
                return false;
            }
        }

        public readonly object Argument;

        public DoWorkProviderEventArgs(object objArgument)
        {
            Argument = objArgument;
        }
    }

    #endregion

    #region ProgressChangedProviderEventArgs
    public class ProgressChangedProviderEventArgs : EventArgs
    {
        public readonly int ProgressPercentage;
        public readonly object loadedObject;

        public ProgressChangedProviderEventArgs(int intProgressPercentage)
        {
            ProgressPercentage = intProgressPercentage;
        }
        public ProgressChangedProviderEventArgs(object obj)
        {
            loadedObject = obj;
        }
    }

    #endregion

    #region CoverProgressChangedProviderEventArgs
    public class CoverProgressChangedProviderEventArgs : EventArgs
    {
        public readonly int ProgressPercentage;
        public readonly object loadedObject;

        public CoverProgressChangedProviderEventArgs(int intProgressPercentage)
        {
            ProgressPercentage = intProgressPercentage;
        }
        public CoverProgressChangedProviderEventArgs(object obj)
        {
            loadedObject = obj;
        }
    }
    #endregion

    #region FanartProgressChangedProviderEventArgs
    public class FanartProgressChangedProviderEventArgs : EventArgs
    {
        public readonly int ProgressPercentage;
        public readonly object loadedObject;

        public FanartProgressChangedProviderEventArgs(int intProgressPercentage)
        {
            ProgressPercentage = intProgressPercentage;
        }
        public FanartProgressChangedProviderEventArgs(object obj)
        {
            loadedObject = obj;
        }
    }
    #endregion

    #region ObjectLoadedProviderEventArgs
    public class ObjectLoadedProviderEventArgs : EventArgs
    {
        public readonly object loadedObject;
        public readonly int ProgressPercentage;
        public ObjectLoadedProviderEventArgs(int intProgressPercentage)
        {
            ProgressPercentage = intProgressPercentage;
        }
        public ObjectLoadedProviderEventArgs(object obj)
        {
            loadedObject = obj;
        }
        public ObjectLoadedProviderEventArgs(int intProgressPercentage, object obj)
        {
            ProgressPercentage = intProgressPercentage;
            loadedObject = obj;
        }


    }

    #endregion

    #region RunWorkerCompletedEventArgs
    public class RunWorkerCompletedProviderEventArgs : AsyncCompletedProviderEventArgs
    {
        public readonly object Result;

        public RunWorkerCompletedProviderEventArgs(object objResult, Exception exException, bool bCancel)
            : base(bCancel, exException)
        {
            Result = objResult;
        }
    }
    #endregion

}

