using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace StockMarket
{
    public class Investor
    {
        
        public Investor(string fullName, string emailAddress, decimal moneyToInvest, string brokerName)
        {
            FullName = fullName;
            EmailAddress = emailAddress;
            BrokerName = brokerName;
            MoneyToInvest = moneyToInvest;
            Portfolio = new List<Stock>();
        }

        public void BuyStock(Stock stock)
        {
            if (stock.MarketCapitalization > 10000 && MoneyToInvest - stock.PricePerShare >= 0 )
            {
                MoneyToInvest -= stock.PricePerShare;
                Portfolio.Add(stock);
            }
        }

        public string SellStock(string companyName, decimal sellPrice)
        {
            if (!Portfolio.Any(x => x.CompanyName == companyName))
            {
                return $"{companyName} does not exist.";
            }
            else if (Portfolio.Any(x => x.CompanyName == companyName && x.PricePerShare > sellPrice))
            {
                return $"Cannot sell {companyName}.";
            }
            else if (Portfolio.Any(x => x.CompanyName == companyName && x.PricePerShare <= sellPrice))
            {
                Portfolio.Remove(Portfolio.Where(x => x.CompanyName == x.CompanyName).First());
            }

            return $"{companyName} was sold.";
        }

        public Stock FindStock(string companyName)
        {
            if (Portfolio.Any(x => x.CompanyName == companyName))
            {
                return Portfolio.First(x => x.CompanyName == companyName);
            }

            return null;
        }

        public Stock FindBiggestCompany()
        {
            decimal maxCapitalisation = 0;
            
            if (!Portfolio.Any())
            {
                return null;
            }
            
            foreach (var stock in Portfolio)
            {
                if (stock.MarketCapitalization > maxCapitalisation)
                {
                    maxCapitalisation = stock.MarketCapitalization;
                }
            }

            return Portfolio.First(x => x.MarketCapitalization == maxCapitalisation);
        }

        public string InvestorInformation()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The investor {FullName} with a broker {BrokerName} has stocks:");
            Portfolio.ForEach(x => sb.AppendLine(x.ToString()));
            return sb.ToString().TrimEnd();
        }

        public int Count => Portfolio.Count;

        public string EmailAddress { get; set; }
        
        public string FullName { get; set; }
        
        public decimal MoneyToInvest { get; set; }

        public string BrokerName { get; set; }
        
        public List<Stock> Portfolio { get; set; }
    }
}
