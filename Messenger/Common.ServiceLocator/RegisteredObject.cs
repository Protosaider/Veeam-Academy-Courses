using System;

namespace Common.ServiceLocator
{
	internal sealed class CRegisteredObject
    {
        public CRegisteredObject(Type typeToResolve, Type concreteType, ELifeCycle lifeCycle)
        {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;
        }

        public Type TypeToResolve { get; }
        public Type ConcreteType { get; }
        public ELifeCycle LifeCycle { get; }

        public Object Instance { get; private set; }

        public void CreateInstance(params Object[] args)
        {
            Instance = Activator.CreateInstance(ConcreteType, args);
        }
    }
}
