using System;
using System.Collections.Generic;
using System.Text;

namespace Lind.DI
{
    /// <summary>
    /// 组件生命周期
    /// </summary>
    public enum LifeCycle
    {
        CurrentScope,
        CurrentRequest,
        Global,
    }
}
