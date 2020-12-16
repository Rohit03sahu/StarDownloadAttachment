using DownloadAttachmentConsole.Model;
using IhxPayerIntegrationDBRetry.Enum;
using LinqToDB;
using System;
using System.Linq;

namespace DownloadAttachmentConsole
{
    public class DBUtil
    {

        #region Object Declaration

        private static DBUtil instance = null;
        private DBUtil() { }
        public static DBUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBUtil();
                }
                return instance;
            }
        }

        #endregion

        #region Methods


        public bool UpdateAttachmentData(PayerHospActionAttachment payerHospActionAttachment)
        {
            using (var dbConn = new SqlDbConnection())
            {
                using (var tran = dbConn.BeginTransaction())
                {
                    var data = dbConn.GetTable<PayerHospActionAttachment>().Where(x => x.AttachName == payerHospActionAttachment.AttachName).ToList();
                    if (data.HasRecords())
                    {
                        data.ForEach(x =>
                        {
                            x.AttachPath = payerHospActionAttachment.AttachPath;
                            x.ModifiedOn = DateTime.Now;
                            x.ModifiedBy = -1;
                            dbConn.Update<PayerHospActionAttachment>(x);
                        });

                    }
                    else
                    {
                        Console.WriteLine("No DB records found for :"+payerHospActionAttachment.AttachName);
                    }


                    tran.Commit();
                }
            }
            return true;
        }
        #endregion
    }
}
