using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using LeaveManagementSystem.ServiceLayer;
using System.Web.Mvc;

namespace LeaveManagementSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IEmployeesService,EmployeesService>();
            container.RegisterType<ILeaveDataService, LeaveDataService>();
            container.RegisterType<ILeaveDataAdminService, LeaveDataAdminService>();
            container.RegisterType<IProjectsService, ProjectsService>();
            container.RegisterType<IEmployeeProjectDetailsService, EmployeeProjectDetailsService>();
            container.RegisterType<IRoleService, RoleService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
        }
}