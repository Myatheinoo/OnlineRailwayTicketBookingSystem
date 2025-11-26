using Domain.Configurations;
using Domain.IServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceManager
    {
        public static void SetServiceInfo(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IPermissionServices, PermissionService>();
            services.AddScoped<ITrainServices, TrainServices>();
            services.AddScoped<IRegionService, RegionSrvice>();
            services.AddScoped<ICarriageService, CarriageService>();
            services.AddScoped<IMainRouteService, MainRouteService>();
            services.AddScoped<ISubRouteService, SubRouteService>();
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }
    }
}
