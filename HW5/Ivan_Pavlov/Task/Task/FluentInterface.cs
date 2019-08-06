using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Task.Data;

namespace Task.Fluent
{
    public class SelectCustomer
    {
        public string CustomerId { get; set; }
        public string CompName { get; set; }
        public decimal TotalSumOrders { get; set; }
        public DateTime FirstOrderDate { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}\tTotal Sum {2}\tCompany Name: {1}",
                CustomerId, CompName, TotalSumOrders);
        }
    }

    public class SupplierList
    {
        public string CustomerId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<string> SuppliersNameList { get; set; }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append(string.Format("Id: {0}\tCountry {1}\tCity {2}", 
                CustomerId, Country, City));
            if (SuppliersNameList.Count == 0)
            {
                build.AppendLine();
                return build.AppendLine("No neighbors suppliers").ToString();
            }
            foreach (string supplierName in SuppliersNameList)
            {
                build.AppendLine();
                build.AppendLine(string.Format("Supplier {0}",
                    supplierName));
            }

            return build.ToString();
        }
    }
}
