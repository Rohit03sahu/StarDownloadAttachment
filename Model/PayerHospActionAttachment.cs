using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadAttachmentConsole.Model
{
    [Table("tblPayerHospActionAttachments")]
    public class PayerHospActionAttachment
    {
        [Column]
        [Identity]
        [PrimaryKey]
        public int AttachId { get; set; }
        [Column]
        public int AttachHospActionId { get; set; }
        [Column]
        public string AttachName { get; set; }
        [Column]
        public string AttachPath { get; set; }
        [Column]
        public int AttachStatus { get; set; }
        [Column]
        public DateTime AttachCreatedDate { get; set; }
        [Column]
        public int attachIsLetter { get; set; }
        [Column]
        public long AttachParentId { get; set; }
        [Column]
        public int AttachmentParentType { get; set; }
        [Column]
        public int AttachmentType { get; set; }
        [Column]
        public int AttachedBy { get; set; }
        [Column]
        public DateTime? ModifiedOn { get; set; }
        [Column]
        public int ModifiedBy { get; set; }
    }

}
