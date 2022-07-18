using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Interview.Models;
using WellsFargo.Interview.Services;

namespace WellsFargo.Interview.Bootstraps
{
    public static class ServicesBootstrap
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ILineParser, CsvLineParser>();
            services.AddTransient<ILineFormatter, LineFormatter>();
            
            services.AddTransient<IArgsTypeMapper<Transaction>, ArgsTypeMapper<Transaction>>();
            services.AddTransient<IArgsTypeMapper<Security>, ArgsTypeMapper<Security>>();
            services.AddTransient<IArgsTypeMapper<Portfolio>, ArgsTypeMapper<Portfolio>>();
            services.AddTransient<IArgsTypeMapper<OMSTypeAAA>, ArgsTypeMapper<OMSTypeAAA>>();
            services.AddTransient<IArgsTypeMapper<OMSTypeBBB>, ArgsTypeMapper<OMSTypeBBB>>();
            services.AddTransient<IArgsTypeMapper<OMSTypeCCC>, ArgsTypeMapper<OMSTypeCCC>>();
            
            services.AddTransient<IDataReader, FileDataReader>();
            services.AddTransient<IDataWritter, FileDataWritter>();

            services.AddTransient<IDataService<Transaction>, DataService<Transaction>>();
            services.AddTransient<IDataService<Portfolio>, DataService<Portfolio>>();
            services.AddTransient<IDataService<Security>, DataService<Security>>();
            services.AddTransient<IDataService<OMSTypeAAA>, DataService<OMSTypeAAA>>();
            services.AddTransient<IDataService<OMSTypeBBB>, DataService<OMSTypeBBB>>();
            services.AddTransient<IDataService<OMSTypeCCC>, DataService<OMSTypeCCC>>();

            services.AddTransient<IOmsService, OmsService>();

            return services;
        }
    }
}
