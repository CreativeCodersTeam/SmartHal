using System.Linq;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Templates;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Repositories
{
    [UsedImplicitly]
    public class ThingTemplateRepository : RepositoryBase<IThingTemplate>, IThingTemplateRepository
    {
        private readonly IThingTemplate _defaultTemplate;
        
        public ThingTemplateRepository()
        {
            _defaultTemplate = new DefaultThingTemplate();
        }
        
        public IThingTemplate GetTemplate(string templateName)
        {
            var foundTemplate = this.FirstOrDefault(template => template.Name == templateName);

            return foundTemplate ?? _defaultTemplate;
        }
    }
}