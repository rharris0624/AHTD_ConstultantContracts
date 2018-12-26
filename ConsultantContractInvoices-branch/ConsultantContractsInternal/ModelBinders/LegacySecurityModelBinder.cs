using ConsultantContractsInternal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.ModelBinding;
using System.Web.Mvc;

namespace ConsultantContractsInternal.ModelBinders
{
    public class LegacySecurityModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            string ApplicationId = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[ApplicationId]").AttemptedValue;
            string RoleId = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[RoleId]").AttemptedValue;
            string UserId = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[UserId]").AttemptedValue;
            string ResourceId = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[ResourceId]").AttemptedValue;
            string PermissionId = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[PermissionId]").AttemptedValue;
            int Sequence = (int)bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[Sequence]").ConvertTo(typeof(int));

            return new LegacySecurity
            {
                ApplicationId = ApplicationId,
                RoleId = RoleId,
                ResourceId = ResourceId,
                PermissionId = PermissionId,
                UserId = UserId,
                Sequence = Sequence
            };
        }
    }
}