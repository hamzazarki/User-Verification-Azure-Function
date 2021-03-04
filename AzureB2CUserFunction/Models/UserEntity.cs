using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Azure.Cosmos.Table;

namespace AzureB2CUserFunction.Helpers
{
    public class UserEntity : TableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string CIN { get; set; }
        public string ID_Partenaire { get; set; }

    }
}
