using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2CProfitC
{
    public class C2CHistory
    {
        public string orderNumber;
        public string tradeType;
        public string asset;
        public string fiat;
        public string fiatSymbol;
        public double amount;
        public double totalPrice;
        public double unitPrice;
		public string orderStatus;
        // private DateTime createTime ;
        public double commission;
        public string counterPartNickName;
        public string advertisementRole; 


        public C2CHistory(string orderNumber, string tradeType, string asset, string fiat, string fiatSymbol, double amount, double totalPrice, double unitPrice, string orderStatus, double commission, string counterPartNickName, string advertisementRole)
        {
            this.orderNumber = orderNumber;
            this.tradeType = tradeType;
            this.asset = asset;
            this.fiat = fiat;
            this.fiatSymbol = fiatSymbol;
            this.amount = amount;
            this.totalPrice = totalPrice;
            this.unitPrice = unitPrice;
            this.orderStatus = orderStatus;
            //this.createTime = createTime;
            this.commission = commission;
            this.counterPartNickName = counterPartNickName;
            this.advertisementRole = advertisementRole;
        }

        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        public string TradeType
        {
            get { return tradeType; }
            set { tradeType = value; }
        }

        public string Asset
        {
            get { return asset; }
            set { asset = value; }
        }

        public string Fiat
        {
            get { return fiat; }
            set { fiat = value; }
        }

        public string FiatSymbol
        {
            get { return fiatSymbol; }
            set { fiatSymbol = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        public string OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }

        public double Commission
        {
            get { return commission; }
            set { commission = value; }
        }

        public string CounterPartNickName
        {
            get { return counterPartNickName; }
            set { counterPartNickName = value; }
        }

        public string AdvertisementRole
        {
            get { return advertisementRole; }
            set { advertisementRole = value; }
        }


    }



}
