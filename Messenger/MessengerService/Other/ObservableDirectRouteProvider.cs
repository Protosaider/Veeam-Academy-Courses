﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace MessengerService.Other
{
    public class ObservableDirectRouteProvider : IDirectRouteProvider
    {
        public IReadOnlyList<RouteEntry> DirectRoutes { get; private set; }

        public IReadOnlyList<RouteEntry> GetDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IInlineConstraintResolver constraintResolver)
        {
            var realDirectRouteProvider = new DefaultDirectRouteProvider();
            var directRoutes = realDirectRouteProvider.GetDirectRoutes(controllerDescriptor, actionDescriptors, constraintResolver);
            // Store the routes in a property so that they can be retrieved later
            DirectRoutes = DirectRoutes?.Union(directRoutes).ToList() ?? directRoutes;
            return directRoutes;
        }
    }
}