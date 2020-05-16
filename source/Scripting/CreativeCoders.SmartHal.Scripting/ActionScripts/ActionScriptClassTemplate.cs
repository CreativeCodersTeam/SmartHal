using CreativeCoders.Core;
using CreativeCoders.Scripting.CSharp.ClassTemplating;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers;
using CreativeCoders.SmartHal.Scripting.Base.Api;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts
{
    [UsedImplicitly]
    public class ActionScriptClassTemplate : ScriptClassTemplate
    {
        public ActionScriptClassTemplate(IClassFactory classFactory)
        {
            Usings.Add("System");
            Usings.Add("System.Linq");
            Usings.Add("System.Threading.Tasks");
            Usings.Add("CreativeCoders.SmartHal.Kernel.Base.Items");
            Usings.Add("CreativeCoders.SmartHal.Kernel.Base.Items.DataTypes");
            Usings.Add("CreativeCoders.SmartHal.Scripting.ActionScripts");
            Usings.Add("CreativeCoders.SmartHal.Scripting.Base.Api");
            Usings.Add("CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers");
            Usings.Add("CreativeCoders.SmartHal.Scripting.Base.ActionScripts");

            ImplementsInterfaces.Add(nameof(IActionScriptObject));
            
            Members.AddRawContent("$$code$$");

            Injections.AddProperty("Items", classFactory.Create<IItemsScriptApi>);
            Injections.AddProperty("Trigger", classFactory.Create<ITriggerApi>);

            Members.AddRawContent("public IItemApi Item(string itemName) => Items.GetItem(itemName);");
        }
    }
}