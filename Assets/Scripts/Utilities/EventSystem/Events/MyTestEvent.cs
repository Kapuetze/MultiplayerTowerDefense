using System.Collections;
using System.Collections.Generic;

namespace Utilities.EventSystem
{
    public class MyTestEvent : EventBase<MyTestEvent>
    {
        public int VerbosityLevel;
    }
}
