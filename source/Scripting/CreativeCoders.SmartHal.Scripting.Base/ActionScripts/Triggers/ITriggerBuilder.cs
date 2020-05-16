using System;
using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers
{
    public interface ITriggerBuilder
    {
        void Build(Func<Task> executeAsync);
    }
}