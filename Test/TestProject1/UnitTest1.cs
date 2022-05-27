using DtoKit.Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net.Leksi.Dto;
using Net.Leksi.RestContract;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Security;
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
                    serviceCollection.AddTransient<HttpConnector>();
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
            HttpConnector httpConnector = _host.Services.GetRequiredService<HttpConnector>();
            httpConnector.BaseAddress = new Uri("https://localhost:7145");

            httpConnector.BeforeRequest += args => Console.WriteLine(args.Caller);
            httpConnector.AfterResponse += args => Console.WriteLine(args.Response.StatusCode);

            Connector connector = new(httpConnector);


            Task<HttpResponseMessage> task = connector.GetShipCalls(DateTime.Now, 42.2, new ShipCallsFilter { Voyage = "SER22001", PortName = "SPB-BRONKA", 
                VesselName = "FINNSUN", From = DateTime.Parse("2022-01-01"), To = DateTime.Now});
            HttpResponseMessage result = task.Result;
            Console.WriteLine(result.StatusCode);

        }

        [Test]
        public void TestSession()
        {
        }

        [Test]
        public async Task TestOracle()
        {
            Func<Task<DbDataReader>> getReader = async () =>
            {
                SecureString securePasswd = new();

                foreach (char ch in "")
                {
                    securePasswd.AppendChar(ch);
                }

                securePasswd.MakeReadOnly();

                OracleCredential credential = new OracleCredential("edipro", securePasswd);
                OracleConnection conn = new(
                    "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.17.9)(PORT = 1521)) ) (CONNECT_DATA = (SID = term) (SERVER = DEDICATED) ) )",
                    credential
                    );
                Task t = conn.OpenAsync();

                OracleCommand cmd = new OracleCommand("select sysdate from dual", conn);

                await t;

                return await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection);
            };

            using DbDataReader dataReader = await getReader();

            while (await dataReader.ReadAsync())
            {
                Console.WriteLine(dataReader[0]);
            }
        }


    }
}