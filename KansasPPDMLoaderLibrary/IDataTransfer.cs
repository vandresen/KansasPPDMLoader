﻿using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary
{
    public interface IDataTransfer
    {
        Task Transferdata(string connectionString, string datatype);
    }
}