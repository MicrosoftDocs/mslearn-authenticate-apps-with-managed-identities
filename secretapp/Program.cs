using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace secretapp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await GetSecretFromKeyVault().ConfigureAwait(false);
        }

        private static async Task GetSecretFromKeyVault()
        {
            var keyVaultName = "<key vault name>";
            Uri keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net");

            SecretClient secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential());

            var keyVaultSecretName = "<secret name>";
            
            try
            {
                var secret = await secretClient.GetSecretAsync(keyVaultSecretName).ConfigureAwait(false);

                Console.WriteLine($"Secret: {secret.Value}");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Something went wrong: {exp.Message}");
            }
        }
    }
}
