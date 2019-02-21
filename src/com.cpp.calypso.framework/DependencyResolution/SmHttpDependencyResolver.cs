//using StructureMap;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Http.Dependencies;

//namespace com.cpp.calypso.framework
//{

//    public class SmHttpDependencyResolver : IDependencyResolver
//    {

//        private readonly IContainer _container;

//        public SmHttpDependencyResolver(IContainer container)
//        {
//            _container = container;
//        }

//        public object GetService(Type serviceType)
//        {
//            if (serviceType == null) return null;
//            try
//            {
//                return serviceType.IsAbstract || serviceType.IsInterface
//                         ? _container.TryGetInstance(serviceType)
//                         : _container.GetInstance(serviceType);
//            }
//            catch
//            {

//                return null;
//            }
//        }

//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            return _container.GetAllInstances(serviceType).Cast<object>();
//        }

//        public IDependencyScope BeginScope()
//        {
//            var child = _container.GetNestedContainer();
//            return new SmHttpDependencyResolver(child);
//        }

//        public void Dispose()
//        {
//            _container.Dispose();
//        }
//    }
//}
