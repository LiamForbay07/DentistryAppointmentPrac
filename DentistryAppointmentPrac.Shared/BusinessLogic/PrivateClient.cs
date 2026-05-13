using Azure.Storage.Blobs;

namespace DentistryAppointmentPrac.Shared.BusinessLogic
{
    public class PrivateClient
    {
        public BlobContainerClient Client { get; }
        public PrivateClient(BlobContainerClient client) => Client = client;
    }
}
