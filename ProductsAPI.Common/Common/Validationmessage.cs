using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Common.Common
{
    public static class SaveUpdateMessage
    {
        public const string Saved = "Save Success";
        public const string Update = "Updated";
        public const string Deleted = "Delete Success";
        public const string Error = "Something went Wrong Please try after sometime";
        public const string Busy = "System is Busy try After sometime";
        public const string NoRecordsInsert = "No records is there to Insert";
        public const string NoRecordsUpdate = "No records is there to Update";
    }
}
