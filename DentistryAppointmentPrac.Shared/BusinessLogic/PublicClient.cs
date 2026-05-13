using Azure.Storage.Blobs;


namespace DentistryAppointmentPrac.Shared.BusinessLogic
{
    public class PublicClient
    {
        public BlobContainerClient Client { get; }
        public PublicClient(BlobContainerClient client) => Client = client;
    }
}
