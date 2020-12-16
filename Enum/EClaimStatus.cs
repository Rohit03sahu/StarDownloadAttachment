using System;
using System.Collections.Generic;
using System.Text;

namespace IhxPayerIntegrationDBRetry.Enum
{
    public enum EClaimStatus
    {
        NewAdmission = 1,
        PreAuthApproved,
        EnhancementRaised,
        EnhancementApproved,
        InformationRequired,
        InformationSubmitted,
        Denied,
        DischargeDocumentsSubmitted,
        EnhancemenDenied,
        DocumentsSubmitted,
        DocumentsReceived,
        ClaimSettled,
        PreauthRequestRejected,
        InProgress,
        ClaimProcessed,
        ClaimDenied,
        ClaimQueryRequired,
        CreditRequested,
        CreditRequestRejected,
        CreditPartiallyPaid,
        CreditPaid,
        CreditChargesCalculated,
        PreauthUtilized,
        Loanclosed,
        RECONSIDERATIONREQUEST

    }
}
