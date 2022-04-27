using DtoKit.Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net.Leksi.Dto;
using Net.Leksi.RestContract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Tests
    {
        private static IHost _host;

        static Tests()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(serviceCollection =>
                {
                    DtoKit.Demo.Setup.Configure(serviceCollection);
                    serviceCollection.AddTransient<BaseConnector>();
                }).Build();
        }

        static IEnumerable<Type> DtoTypes()
        {
            DtoServiceProvider sp = _host.Services.GetService<DtoServiceProvider>();

            foreach(var item in sp)
            {
                yield return item.ServiceType;
            }
        }

        [Test]
        [TestCaseSource(nameof(DtoTypes))]
        public void Test2(Type type)
        {
            DtoBuilder dtoBuilder = _host.Services.GetRequiredService<DtoBuilder>();

            dtoBuilder.ValueRequest += arg =>
            {
                Console.WriteLine(arg.Path);

                arg.IsCommited = arg.IsLeaf;
            };

            dtoBuilder.BuildOfType(type);
        }

        [Test]
        public async Task GetShipCalls()
        {
            Connector connector = new(_host.Services);

            connector.BaseAddress = new Uri("https://localhost:7145");

            Task<HttpResponseMessage> task = connector.GetShipCalls(DateTime.Now, 42, new ShipCallsFilter { Voyage = "SER22001", PortName = "SPB-BRONKA", 
                VesselName = "FINNSUN", From = DateTime.Parse("2022-01-01"), To = DateTime.Now});
            HttpResponseMessage result = task.Result;
            Console.WriteLine(result.StatusCode);

        }
    }
}