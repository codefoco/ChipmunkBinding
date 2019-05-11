using System;
using System.Collections.Generic;
using System.Text;

namespace ChipmunkBinding
{
    internal class PostStepCallbackInfo
    {
        Action<Space, object, object> callback;
        object data;

        public PostStepCallbackInfo(Action<Space, object, object> c, object d)
        {
            callback = c;
            data = d;
        }

        public Action<Space, object, object> Callback => callback;
        public object Data => data;
    }
}
