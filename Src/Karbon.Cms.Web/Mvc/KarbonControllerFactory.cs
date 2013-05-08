using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Karbon.Cms.Core;
using Karbon.Cms.Web.Controllers;
using Karbon.Cms.Web.Routing;

namespace Karbon.Cms.Web.Mvc
{
    public class KarbonControllerFactory : IControllerFactory
    {
        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>
        /// The controller.
        /// </returns>
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (!controllerName.EndsWith("Controller"))
                controllerName = controllerName + "Controller";

            IController controllerObject = null;
            var controllerType = TypeFinder.FindTypes<IController>()
                .SingleOrDefault(x => x.Name == controllerName);
            if (controllerType != null)
            {
                if (typeof(KarbonController<>).IsAssignableFromExtended(controllerType))
                {
                    var model = requestContext.RouteData.Values[KarbonRoute.ModelKey];
                    controllerObject = (IController)Activator.CreateInstance(controllerType, new[]{ model });
                }
                else
                {
                    controllerObject = (IController)Activator.CreateInstance(controllerType);
                }
            }
            return controllerObject;
        }

        /// <summary>
        /// Gets the controller's session behavior.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">The name of the controller whose session behavior you want to get.</param>
        /// <returns>
        /// The controller's session behavior.
        /// </returns>
        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
