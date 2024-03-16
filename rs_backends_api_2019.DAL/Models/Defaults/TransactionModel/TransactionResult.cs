using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.DAL.Models.Defaults.TransactionModel
{
    public class TransactionResult
    {
        public bool success { get; set; } = false;

        public Exception exception { get; set; } = null;
    }
}
