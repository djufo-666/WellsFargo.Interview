using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Interview.Services;

namespace WellsFargo.Interview.ConsoleApp
{
    public class App
    {
        private readonly IConfiguration _configuration;
        private readonly IOmsService _omsService;

        public App(
            IConfiguration configuraiton,
            IOmsService omsService
            )
        {
            _configuration = configuraiton;
            _omsService = omsService;
        }

        public async Task RunAsync()
        {
            DataSources dataSources = _configuration.GetSection("dataSources").Get<DataSources>();

            await _omsService.ReadDataAsync(dataSources.Securities, dataSources.Portfolios, dataSources.Transactions);

            await _omsService.WriteOmsAaaAsync(dataSources.OmsAAA);
            await _omsService.WriteOmsBbbAsync(dataSources.OmsBBB);
            await _omsService.WriteOmsCccAsync(dataSources.OmsCCC);
        }
    }
}
