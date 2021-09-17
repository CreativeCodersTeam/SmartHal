using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace CreativeCoders.SmartHal.Web.CommandCenter.Client.Localization
{
    public class DefaultStringLocalizer : IStringLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public DefaultStringLocalizer(IStringLocalizerFactory factory)
        {
            var location = new AssemblyName(
                               typeof(Program).Assembly.FullName ??
                               throw new ArgumentException("This assembly name not found")).Name ??
                           throw new ArgumentException("This assembly name not found");

            _localizer = factory.Create("CommandCenterClient", location);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => _localizer.GetAllStrings(includeParentCultures);

        public LocalizedString this[string name] => _localizer[name];

        public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];
    }
}