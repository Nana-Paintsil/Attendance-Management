using AttendanceManagement.Services;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync;
using System.Net.Mail;
using System.Net;

namespace AttendanceManagement.Models
{
    public  class Sync
    {
        private SyncAgent syncAgent;
        public Sync(ISyncServices sync)
        {
            this.syncAgent = sync.GetSyncAgent();
        }

        public bool IsSyncing = false;
        public async Task ExecuteSyncCommand(SyncType syncType)
        {
            IsSyncing = true;

            try
            {
                /*

                var progress = new Progress<ProgressArgs>(args =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        this.SyncProgress = args.ProgressPercentage;

                    });
                });
                */
                this.syncAgent.LocalOrchestrator.OnConflictingSetup(async args =>
                {
                    await this.syncAgent.LocalOrchestrator.DeprovisionAsync(connection: args.Connection, transaction: args.Transaction);
                    await this.syncAgent.LocalOrchestrator.ProvisionAsync(args.ServerScopeInfo, connection: args.Connection, transaction: args.Transaction);
                    args.Action = ConflictingSetupAction.Continue;
                });

                    
                    var r = await this.syncAgent.SynchronizeAsync(syncType);

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            finally
            {
                IsSyncing = false;
            }
        }



    }
}
