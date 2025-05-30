﻿using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Services
{
    public class SyncServices : ISyncServices
    {

        private SqliteSyncProvider sqliteSyncProvider;
        private SyncAgent syncAgent;
        private WebRemoteOrchestrator webRemoteOrchestrator;
        private HttpClient httpClient;

        private ISettingServices settings;

        public SyncServices(SettingServices setting)
        {
            this.settings = setting;
            var syncApiUri = new Uri(this.settings.SyncApiUrl);

            this.httpClient = new HttpClient();

            var handler = new HttpClientHandler();
#if DEBUG
            if (DeviceInfo.Platform == DevicePlatform.Android && syncApiUri.Host == "10.0.2.2")
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                };
            }
#endif


            handler.AutomaticDecompression = DecompressionMethods.GZip;

            this.httpClient = new HttpClient(handler);

            // Check if we are trying to reach a IIS Express.
            // IIS Express does not allow any request other than localhost
            // So far,hacking the Host-Content header to mimic localhost call
            if (DeviceInfo.Platform == DevicePlatform.Android && syncApiUri.Host == "10.0.2.2")
                this.httpClient.DefaultRequestHeaders.Host = $"localhost:{syncApiUri.Port}";

            this.httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            this.webRemoteOrchestrator = new WebRemoteOrchestrator(this.settings.SyncApiUrl, client: this.httpClient, maxDownladingDegreeOfParallelism: 1);

            this.sqliteSyncProvider = new SqliteSyncProvider(this.settings.DataSource);

            var clientOptions = new SyncOptions { BatchSize = settings.BatchSize, BatchDirectory = settings.BatchDirectoryPath };

            this.syncAgent = new SyncAgent(sqliteSyncProvider, webRemoteOrchestrator, clientOptions);
        }

        public SyncAgent GetSyncAgent() => this.syncAgent;

        public HttpClient GetHttpClient() => this.httpClient;

    }
}