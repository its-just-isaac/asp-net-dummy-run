using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Monolith.Core.ModuleIntegration
{
    /// <summary>
    /// Custom feature provider to allow "internal" controllers
    /// </summary>
    public class InternalControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            bool isCustomController = !typeInfo.IsAbstract
                                      && typeof(ControllerBase).IsAssignableFrom(typeInfo)
                                      && IsInternal(typeInfo);
            return isCustomController || base.IsController(typeInfo);

            bool IsInternal(Type t) =>
                !t.IsVisible
                && !t.IsPublic
                && t.IsNotPublic
                && !t.IsNested
                && !t.IsNestedPublic
                && !t.IsNestedFamily
                && !t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem;
        }
    }
}