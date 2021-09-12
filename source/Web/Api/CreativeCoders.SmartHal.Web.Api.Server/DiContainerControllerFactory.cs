using CreativeCoders.Di;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CreativeCoders.SmartHal.Web.Api.Server
{
    public class DiContainerControllerFactory : IControllerFactory
    {
        private readonly IDiContainer _diContainer;

        public DiContainerControllerFactory(IDiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public object CreateController(ControllerContext context)
        {
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

            var controller = _diContainer.GetInstance(controllerType);

            return controller;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            
        }
    }
}