namespace Task.Fluent
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Task.Data;

    public class SelectCustomer
    {
        public string CustomerId { get; set; }
        public string CompName { get; set; }
        public decimal? TotalSumOrders { get; set; }
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

    public class ProductsOnStock
    {
        public bool HasInStock { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.AppendLine(string.Format("\tHas in stock {0}", HasInStock));
            foreach(var product in Products)
            {
                build.AppendLine(string.Format("\t\tProduct {0} \t Price {1}",
                    product.ProductName, product.UnitPrice));
            }
            return build.ToString();
        }
    }

    public class ProductsCategory
    {
        public string Category { get; set; }
        public IEnumerable<ProductsOnStock> OnStock { get; set; }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.AppendLine(Category);
            foreach(var item in OnStock)
            {
                build.AppendLine(item.ToString());
            }
            return build.ToString();
        }
    }

    public class OrdersStatisticsByCity
    {
        public string City { get; set; }
        public decimal AverageIncome { get; set; }
        public double Intensity { get; set; }

        public override string ToString()
        {
            return string.Format(
                "City: {0}\tIntensity {1}\tAverage income {2}",
                City, Intensity, AverageIncome);
        }
    }
}
