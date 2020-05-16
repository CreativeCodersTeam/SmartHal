using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.Base.Repositories
{
    public interface IThingTemplateRepository : IRepositoryBase<IThingTemplate>
    {
        IThingTemplate GetTemplate(string templateName);
    }
}