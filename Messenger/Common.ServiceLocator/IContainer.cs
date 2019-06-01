using System;

namespace Common.ServiceLocator
{
	internal interface IContainer
    {
        void Register<TConcreteRealization>(ELifeCycle lifeCycle);
        void Register<TTypeToResolve, TConcreteRealization>(ELifeCycle lifeCycle);
        TTypeToResolve Resolve<TTypeToResolve>();
        Object Resolve(Type typeToResolve);
    }
}
