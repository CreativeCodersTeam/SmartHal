using System.Diagnostics.CodeAnalysis;
using CreativeCoders.CodeCompilation;
using CreativeCoders.Scripting.CSharp;
using CreativeCoders.Scripting.CSharp.Preprocessors;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts
{
    [UsedImplicitly]
    public class ActionScriptImplementation : CSharpScriptImplementation
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public ActionScriptImplementation(ActionScriptClassTemplate scriptClassTemplate, ICompilerFactory compilerFactory)
            : base(scriptClassTemplate, compilerFactory.CreateCompiler())
        {
            SourcePreprocessors.Add(new UsingsPreprocessor());
        }
    }
}