using System;

namespace Ivy.PayPal.Core.Interfaces.Models
{
    public interface IPayPalIpnResponse
    {
        // Information about receiver:
        string Receiver_Email { get; set; }
        string Receiver_Id { get; set; }
        string Residence_Country { get; set; }

        // Information about transaction:
        int Test_Ipn { get; set; }
        string Transaction_Subject { get; set; }
        string Txn_Id { get; set; }
        string Txn_Type { get; set; }

        // Information about buyer:
        string Payer_Email { get; set; }
        string Payer_Id { get; set; }
        string Payer_Status { get; set; }
        string First_Name { get; set; }
        string Last_Name { get; set; }
        string Address_City { get; set; }
        string Address_Country { get; set; }
        string Address_State { get; set; }
        string Address_Status { get; set; }
        string Address_Country_Code { get; set; }
        string Address_Name { get; set; }
        string Address_Street { get; set; }
        string Address_Zip { get; set; }

        // Information about payment:
        string Custom { get; set; } // Special item that we configure
        decimal Handling_Amount { get; set; }
        string Item_Name { get; set; }
        int Item_Number { get; set; }
        string Mc_Currency { get; set; }
        decimal Mc_Fee { get; set; }
        decimal Mc_Gross { get; set; }
        DateTime Payment_Date { get; set; }
        decimal Payment_Fee { get; set; }
        decimal Payment_Gross { get; set; }
        string Payment_Status { get; set; }
        string Payment_Type { get; set; }
        string Protection_Eligibility { get; set; }
        int Quantity { get; set; }
        decimal Shipping { get; set; }
        decimal Tax { get; set; }

        // Other information:
        string Notify_Version { get; set; }
        string Charset { get; set; }
        string Verify_Sign { get; set; }
    }
}
