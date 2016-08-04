using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WSACancelPOs.WSAWebService;

namespace WSAClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt = "Y";
            Console.WriteLine();
            while (opt == "Y" || opt == "y")
            {

                var purchaseOrderNumber = "";
                Console.Write("Give me Purchase Order Number or \"ALL\" (also hit <enter> and it means \"ALL\"): ");
                purchaseOrderNumber = Console.ReadLine();
                purchaseOrderNumber = (purchaseOrderNumber == null || string.IsNullOrEmpty(purchaseOrderNumber))
                    ? "ALL"
                    : purchaseOrderNumber;


                PurchaseOrderSoapClient service = new PurchaseOrderSoapClient();

                string result;

                var purchaseOrderInfoRequest = new PurchaseOrderInfoRequest()
                {
                    Authentication = new clsAuthentication()
                    {
                        UserName = "APITestLYRIC",
                        Password = "WSA@cc3ss!"
                    },
                    PurchaseOrderNumber = purchaseOrderNumber
                };

                try
                {
                    var CancelFulfillmentResponse = service.CancelFulfillment(purchaseOrderInfoRequest);


                    //GetPurchaseOrderResponse: WSAClient.WSAWebService.clsPurchaseOrder
                    if (CancelFulfillmentResponse == null || CancelFulfillmentResponse.Comment == null
                        || String.IsNullOrEmpty(CancelFulfillmentResponse.Comment)
                    )
                    {
                        result = String.Format("PO Number: \"{0}\", \nComment: \"empty\" or null", purchaseOrderNumber);
                    }
                    else
                    {
                        result = String.Format("PO Number: \"{0}\", \nComment: \"{1}\",", 
                                purchaseOrderNumber, CancelFulfillmentResponse.Comment);
                    }
                }
                catch (Exception)
                {

                    result = String.Format("PO Number: \"{0}\" does NOT exist",   purchaseOrderNumber);
                }

                Console.WriteLine(result);
                Console.Write("\nDo you want to get another PO? (Y/N) => ");
                opt = Console.ReadLine();
                Console.WriteLine();
            }
        }
    }
}
