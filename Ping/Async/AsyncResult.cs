using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Async
{
    public class AsyncResult<TResult> : AsyncResultNoResult
    {
        // Field set when operation completes
        private TResult m_result = default(TResult);

        public AsyncResult(AsyncCallback asyncCallback, Object state) : base(asyncCallback, state) { }

        public void SetAsCompleted(Boolean completedSynchronously, TResult result)
        {
            // Save the asynchronous operation's result
            m_result = result;

            // Tell the base class that the operation completed sucessfully (no exception)
            base.SetAsCompleted(completedSynchronously, null);
        }

        new public TResult EndInvoke()
        {
            base.EndInvoke(); // Wait until operation has completed 
            return m_result;  // Return the result (if above didn't throw)
        }
    }
}
