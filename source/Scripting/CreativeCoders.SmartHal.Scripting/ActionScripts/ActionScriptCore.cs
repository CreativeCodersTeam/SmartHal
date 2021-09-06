using System;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.Reflection;
using CreativeCoders.Scripting.Base;
using CreativeCoders.Scripting.CSharp;
using CreativeCoders.Scripting.CSharp.Exceptions;
using CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts
{
    [UsedImplicitly]
    public class ActionScriptCore : IActionScriptCore
    {
        private static readonly ILogger Log = LogManager.GetLogger<ActionScriptCore>();
        
        private readonly CSharpScriptRuntime<ActionScriptImplementation> _scriptRuntime;

        private readonly IScriptRuntimeSpace _scriptSpace;

        public ActionScriptCore(CSharpScriptRuntime<ActionScriptImplementation> scriptRuntime)
        {
            _scriptRuntime = scriptRuntime;
            _scriptSpace = CreateScriptSpace();
        }
        
        private IScriptRuntimeSpace CreateScriptSpace()
        {
            return _scriptRuntime.CreateSpace("CreativeCoders.SmartHal.Scripts.ActionScripts");
        }
        
        public IActionScript CreateActionScript(ScriptPackage scriptPackage)
        {
            var scriptObject = CreateScriptObject(scriptPackage);

            if (scriptObject == null)
            {
                return null;
            }

            var scriptType = scriptObject.GetType();

            var executeMethod = scriptType.GetMethod("Execute", Type.EmptyTypes);

            if (executeMethod != null)
            {
                var execute = ExpressionUtils.CreateCallAction(scriptObject, executeMethod);
                
                return new ActionScript(scriptObject, scriptPackage.Name, () =>
                {
                    execute();
                    return Task.CompletedTask;
                });
            }

            var executeAsyncMethod = scriptType.GetMethod("ExecuteAsync", Type.EmptyTypes);

            return executeAsyncMethod == null 
                ? null
                : new ActionScript(scriptObject, scriptPackage.Name, ExpressionUtils.CreateCallFunc<Task>(scriptObject, executeAsyncMethod));
        }

        private IActionScriptObject CreateScriptObject(ScriptPackage scriptPackage)
        {
            try
            {
                var script = _scriptSpace.Build(scriptPackage);

                if (script != null)
                {
                    return script.CreateObject<IActionScriptObject>();
                }

                Log.Error($"Script object '{scriptPackage.Name}' creation failed.");
                return null;

            }
            catch (ScriptCompilationFailedException e)
            {
                Log.Error($"Script object '{scriptPackage.Name}' creation failed.", e);
                
                e.CompilationResultMessages.ForEach(x => Log.Error($"{x.MessageType}: {x.Message}"));
                
                return null;
            }
        }
    }
}