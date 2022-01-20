using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginAthenaHealth.API.Read;
using PluginAthenaHealth.API.Utility;
using PluginAthenaHealth.DataContracts;
using PluginAthenaHealth.Helper;
using Xunit;
using Record = Naveego.Sdk.Plugins.Record;

namespace PluginAthenaHealthTest.Plugin
{
    public class PluginIntegrationTest
    {
        private Settings GetSettings()
        {
            // add values to test
            return new Settings
            {
                ClientId = "",
                ClientSecret = "",
                PracticeId = "",
                ProductionPractice = false,
                StartDate = "",
                EndDate = ""
            };
        }

        private ConnectRequest GetConnectSettings()
        {
            var settings = GetSettings();

            return new ConnectRequest
            {
                SettingsJson = JsonConvert.SerializeObject(settings),
                OauthConfiguration = new OAuthConfiguration(),
                OauthStateJson = ""
            };
        }

        private Schema GetTestSchema(string endpointId = null, string id = "test", string name = "test")
        {
            Endpoint endpoint = endpointId == null
                ? EndpointHelper.GetEndpointForId("AllBookedAppointments")
                : EndpointHelper.GetEndpointForId(endpointId);


            return new Schema
            {
                Id = id,
                Name = name,
                PublisherMetaJson = JsonConvert.SerializeObject(endpoint),
            };
        }

        [Fact]
        public async Task ConnectSessionTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var request = GetConnectSettings();
            var disconnectRequest = new DisconnectRequest();

            // act
            var response = client.ConnectSession(request);
            var responseStream = response.ResponseStream;
            var records = new List<ConnectResponse>();

            while (await responseStream.MoveNext())
            {
                records.Add(responseStream.Current);
                client.Disconnect(disconnectRequest);
            }

            // assert
            Assert.Single(records);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }

        [Fact]
        public async Task ConnectTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var request = GetConnectSettings();

            // act
            var response = client.Connect(request);

            // assert
            Assert.IsType<ConnectResponse>(response);
            Assert.Equal("", response.SettingsError);
            Assert.Equal("", response.ConnectionError);
            Assert.Equal("", response.OauthError);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }
        
        [Fact]
        public async Task DiscoverSchemasAllTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var connectRequest = GetConnectSettings();

            var request = new DiscoverSchemasRequest
            {
                Mode = DiscoverSchemasRequest.Types.Mode.All,
                SampleSize = 10
            };

            // act
            client.Connect(connectRequest);
            var response = client.DiscoverSchemas(request);

            // assert
            Assert.IsType<DiscoverSchemasResponse>(response);
            Assert.Equal(3, response.Schemas.Count);
            //
            // var schema = response.Schemas[0];
            // Assert.Equal($"cclf1", schema.Id);
            // Assert.Equal("cclf1", schema.Name);
            // Assert.Equal($"", schema.Query);
            // Assert.Equal(10, schema.Sample.Count);
            // Assert.Equal(17, schema.Properties.Count);
            //
            // var property = schema.Properties[0];
            // Assert.Equal("field1", property.Id);
            // Assert.Equal("field1", property.Name);
            // Assert.Equal("", property.Description);
            // Assert.Equal(PropertyType.String, property.Type);
            // Assert.False(property.IsKey);
            // Assert.True(property.IsNullable);
            //
            // var schema2 = response.Schemas[1];
            // Assert.Equal($"Custom Name", schema2.Id);
            // Assert.Equal("Custom Name", schema2.Name);
            // Assert.Equal($"", schema2.Query);
            // Assert.Equal(10, schema2.Sample.Count);
            // Assert.Equal(17, schema2.Properties.Count);
            //
            // var property2 = schema2.Properties[0];
            // Assert.Equal("field1", property2.Id);
            // Assert.Equal("field1", property2.Name);
            // Assert.Equal("", property2.Description);
            // Assert.Equal(PropertyType.String, property2.Type);
            // Assert.False(property2.IsKey);
            // Assert.True(property2.IsNullable);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }

        [Fact]
        public async Task DiscoverSchemasRefreshTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var connectRequest = GetConnectSettings();

            var request = new DiscoverSchemasRequest
            {
                Mode = DiscoverSchemasRequest.Types.Mode.Refresh,
                SampleSize = 10,
                ToRefresh =
                {
                    // GetTestSchema("PatientBalances") //good
                    GetTestSchema("AllBookedAppointments")
                    //GetTestSchema("AllPatients")
                }
            };

            // act
            client.Connect(connectRequest);
            var response = client.DiscoverSchemas(request);

            // assert
            Assert.IsType<DiscoverSchemasResponse>(response);
            Assert.Equal(1, response.Schemas.Count);
            //
            var schema = response.Schemas[0];
            Assert.Equal("test", schema.Id);
            Assert.Equal("test", schema.Name);
            Assert.Equal("", schema.Query);
            Assert.Equal(10, schema.Sample.Count);
            Assert.Equal(93, schema.Properties.Count);
            
            var property = schema.Properties[0];
            Assert.Equal("appointmentid", property.Id);
            Assert.Equal("appointmentid", property.Name);
            Assert.Equal("", property.Description);
            Assert.Equal(PropertyType.String, property.Type);
            Assert.True(property.IsKey);
            Assert.False(property.IsNullable);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }

        [Fact]
        public async Task ReadStreamTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var schema = GetTestSchema("BookedAppointments_Last7Days");

            var connectRequest = GetConnectSettings();

            var schemaRequest = new DiscoverSchemasRequest
            {
                Mode = DiscoverSchemasRequest.Types.Mode.Refresh,
                ToRefresh = {schema}
            };

            var request = new ReadRequest()
            {
                DataVersions = new DataVersions
                {
                    JobId = "test"
                },
                JobId = "test",
            };

            // act
            client.Connect(connectRequest);
            var schemasResponse = client.DiscoverSchemas(schemaRequest);
            request.Schema = schemasResponse.Schemas[0];

            var response = client.ReadStream(request);
            var responseStream = response.ResponseStream;
            var records = new List<Record>();

            while (await responseStream.MoveNext())
            {
                records.Add(responseStream.Current);
            }

            // assert
            Assert.Equal(17, records.Count);

            var record = JsonConvert.DeserializeObject<Dictionary<string, object>>(records[0].DataJson);
            Assert.Equal("3101903", record["appointmentid"]);
            Assert.Equal(false, record["coordinatorenterprise"]);
            Assert.Equal("API-22129", record["scheduledby"]);
            Assert.Equal("05/11/2020", record["patient.registrationdate"]);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }

        [Fact]
        public async Task ReadStreamLimitTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var schema = GetTestSchema("PatientBalances");

            var connectRequest = GetConnectSettings();

            var schemaRequest = new DiscoverSchemasRequest
            {
                Mode = DiscoverSchemasRequest.Types.Mode.Refresh,
                ToRefresh = {schema}
            };

            var request = new ReadRequest()
            {
                DataVersions = new DataVersions
                {
                    JobId = "test"
                },
                JobId = "test",
                Limit = 1
            };

            // act
            client.Connect(connectRequest);
            var schemasResponse = client.DiscoverSchemas(schemaRequest);
            request.Schema = schemasResponse.Schemas[0];

            var response = client.ReadStream(request);
            var responseStream = response.ResponseStream;
            var records = new List<Record>();

            while (await responseStream.MoveNext())
            {
                records.Add(responseStream.Current);
            }

            // assert
            Assert.Equal(1, records.Count);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }
        [Fact]
        public async Task ConfigureWriteTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var connectRequest = GetConnectSettings();

            var request = new ConfigureWriteRequest
            {
                Form = new ConfigurationFormRequest
                {
                    DataJson = JsonConvert.SerializeObject(new ConfigureWriteFormData
                    {
                       FileStorageMethod = Constants.GoogleCloudStorage,
                       GoogleCloudStorageCredentialPath = @"C:\Dev\Athena Health\test-proj-337418-dc606f7ce7d3.json"
                    })
                }
            };

            // act
            client.Connect(connectRequest);
            var response = client.ConfigureWrite(request);

            // assert
            Assert.IsType<ConfigureWriteResponse>(response);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }
        [Fact]
        public async Task WriteTest()
        {
            // setup
            Server server = new Server
            {
                Services = {Publisher.BindService(new PluginAthenaHealth.Plugin.Plugin())},
                Ports = {new ServerPort("localhost", 0, ServerCredentials.Insecure)}
            };
            server.Start();

            var port = server.Ports.First().BoundPort;

            var channel = new Channel($"localhost:{port}", ChannelCredentials.Insecure);
            var client = new Publisher.PublisherClient(channel);

            var schema = GetTestSchema("PatientCharts");

            var connectRequest = GetConnectSettings();

            var schemaRequest = new DiscoverSchemasRequest
            {
                Mode = DiscoverSchemasRequest.Types.Mode.Refresh,
                ToRefresh = {schema}
            };

            var records = new List<Record>()
            {
                {
                    new Record
                    {
                        Action = Record.Types.Action.Upsert,
                        CorrelationId = "test",
                        RecordId = "record1",
                        DataJson = $"{{\"patientid\":\"571025\"," +
                                   $"\"appointmentid\":\"1111\"," +
                                   $"\"practiceid\":{GetSettings().PracticeId}, " +
                                   $"\"departmentid\":\"67\"," +
                                   $"\"localFilePath\":\"\"," +
                                   $"\"gcsBucket\":\"aunalytics-demo-bucket-1\"," +
                                   $"\"fileName\":\"123.pdf\"," +
                                   $"\"documentSubclass\":\"CLINICALDOCUMENT\"}}", //appointmentid is ph
                    }
                }
            };

            var recordAcks = new List<RecordAck>();

            // act
            client.Connect(connectRequest);

            var schemasResponse = client.DiscoverSchemas(schemaRequest);

            var prepareWriteRequest = new PrepareWriteRequest()
            {
                Schema = schemasResponse.Schemas[0],
                CommitSlaSeconds = 1000,
                DataVersions = new DataVersions
                {
                    JobId = "jobUnitTest",
                    ShapeId = "shapeUnitTest",
                    JobDataVersion = 1,
                    ShapeDataVersion = 1
                }
            };
            client.PrepareWrite(prepareWriteRequest);

            using (var call = client.WriteStream())
            {
                var responseReaderTask = Task.Run(async () =>
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        var ack = call.ResponseStream.Current;
                        recordAcks.Add(ack);
                    }
                });

                foreach (Record record in records)
                {
                    await call.RequestStream.WriteAsync(record);
                }

                await call.RequestStream.CompleteAsync();
                await responseReaderTask;
            }

            // assert
            Assert.Equal(1, recordAcks.Count);
            Assert.Equal("", recordAcks[0].Error);
            Assert.Equal("test", recordAcks[0].CorrelationId);

            // cleanup
            await channel.ShutdownAsync();
            await server.ShutdownAsync();
        }
    }
}