using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VecinosUY.Data.Repository;
using VecinosUY.Resolver;

namespace VecinosUY.Logic
{
    [Export(typeof(IComponent))]
    public class DependencyResolver:IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUserValidator, UserValidator>();
            //registerComponent.RegisterType<ITaskService, TaskService>();
            //registerComponent.RegisterTypeWithControlledLifeTime<IUnitOfWork,UnitOfWork>();
        }
    }
}
