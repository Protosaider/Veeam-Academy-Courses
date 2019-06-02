using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.ServiceLocator
{
    public sealed class CContainer : IContainer
    {
		internal static CContainer Create() => new CContainer();
        private CContainer() { }


        private readonly IList<CRegisteredObject> _registeredObjects = new List<CRegisteredObject>();

        public void Register<TConcreteRealization>(ELifeCycle lifeCycle)
        {
            Register<TConcreteRealization, TConcreteRealization>(lifeCycle);
        }

        public void Register<TTypeToResolve, TConcreteRealization>(ELifeCycle lifeCycle)
        {
            _registeredObjects.Add(new CRegisteredObject(typeof(TTypeToResolve), typeof(TConcreteRealization), lifeCycle));
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
        }

        public Object Resolve(Type typeToResolve)
        {
            return ResolveObject(typeToResolve);
        }

        private Object ResolveObject(Type typeToResolve)
        {
            var registeredObject = _registeredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            return registeredObject == null ? null : GetInstance(registeredObject);
		}

        private Object GetInstance(CRegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null ||
                registeredObject.LifeCycle == ELifeCycle.Transient)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                var par = parameters.ToArray();
                registeredObject.CreateInstance(par);
            }
            return registeredObject.Instance;
        }

        //This is a recursive operation that ensures the entire object graph is instantiated
        //Only registered types can be instantiated!
        private IEnumerable<Object> ResolveConstructorParameters(CRegisteredObject registeredObject)
        {
            //var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            var constructorInfo = registeredObject.ConcreteType.GetConstructors();
            foreach (var item in constructorInfo)
            {
                foreach (var parameter in item.GetParameters())
                {
                    //yield return ResolveObject(parameter.ParameterType);
                    var resolvedObject = ResolveObject(parameter.ParameterType);
                    if (resolvedObject != null)
                        yield return resolvedObject;
                }
            }
        }

    }
}
