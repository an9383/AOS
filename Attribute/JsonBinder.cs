using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Attribute
{
    public class JsonBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        public class JsonModelBinder : IModelBinder
        {
            public object BindModel(
                ControllerContext controllerContext,
                ModelBindingContext bindingContext)
            {
                try
                {
                    var json = controllerContext.HttpContext.Request
                               .Params[bindingContext.ModelName];

                    if (String.IsNullOrWhiteSpace(json))
                        return null;

                    // Swap this out with whichever Json deserializer you prefer.
                    return Newtonsoft.Json.JsonConvert
                           .DeserializeObject(json, bindingContext.ModelType);
                }
                catch
                {
                    return null;
                }
            }

        }
    }
}