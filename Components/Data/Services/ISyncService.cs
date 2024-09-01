using Dotmim.Sync;
using System.Net.Http;

namespace AttendanceManagement.Services
{
    public interface ISyncServices
    {
        SyncAgent GetSyncAgent();
        HttpClient GetHttpClient();
    }
}