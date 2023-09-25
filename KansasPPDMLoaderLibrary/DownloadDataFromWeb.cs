using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary
{
    public class DownloadDataFromWeb
    {
        private readonly string wellBoreUrl = @"https://www.kgs.ku.edu/PRS/Ora_Archive/ks_wells.zip";

        public DownloadDataFromWeb()
        {
        }

        public async Task DownloadWellBores()
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] zipData = await client.GetByteArrayAsync(wellBoreUrl);
                using (MemoryStream zipStream = new MemoryStream(zipData))
                {
                    using (ZipArchive archive = new ZipArchive(zipStream))
                    {
                        if (archive != null)
                        {
                            if (archive.Entries.Count > 0)
                            {
                                ZipArchiveEntry? entry = archive.GetEntry("ks_wells.txt");
                                if (entry != null)
                                {
                                    
                                }
                                else
                                {
                                    Console.WriteLine("File not found in the zip archive.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("The zip archive is empty.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to create the ZipArchive.");
                        }
                    }
                }
            }
        }
    }
}
