using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectWPF_CD
{
    public class Card
    {
        public int ID { get; set; }
        public string CardName { get; set; }
        public string SetName { get; set; }
        public int NumOwned { get; set; }
        public bool IsFoil { get; set; }
        public int SetNum { get; set; }
        public string ImageUrl { get; set; }
        public decimal NormPrice { get; set; }
        public decimal FoilPrice { get; set; }

        public Card(int id, string cardn, string setn, int numown, bool foil, int setnum, string img, decimal nprice, decimal fprice)
        {
            this.ID = id;
            this.CardName = cardn;
            this.SetName = setn;
            this.NumOwned = numown;
            this.IsFoil = foil;
            this.SetNum = setnum;
            this.ImageUrl = img;
            this.NormPrice = nprice;
            this.FoilPrice = fprice;
        }
        
        #region Getters
        /*
        public string GetID()
        {
            return this.ID;
        }

        public string GetCardName()
        {
            return this.CardName;
        }

        public string GetSetName()
        {
            return this.SetName;
        }

        public int GetNumOwned()
        {
            return this.NumOwned;
        }

        public bool GetIsFoil()
        {
            return this.IsFoil;
        }

        public int GetSetNum()
        {
            return this.SetNum;
        }

        public string GetImageUrl()
        {
            return this.ImageUrl;
        }

        public decimal GetNormPrice()
        {
            return this.NormPrice;
        }

        public decimal GetFoilPrice()
        {
            return this.FoilPrice;
        }
        */
        #endregion

        #region Setters 
        /*
        public void SetID(int id)
        {
            if (id > 0)
            {
                this.ID = id;
            }
        }

        public void SetCardName(string cname)
        {
            if (!(string.IsNullOrEmpty(cname)))
            {
                this.CardName = cname;
            }
        }

        public void SetSetName(string sname)
        {
            if (!(string.IsNullOrEmpty(sname)))
            {
                this.SetName = sname;
            }
        }

        public void SetNumOwned(int nowned)
        {
            if (nowned > -1)
            {
                this.NumOwned = nowned;
            }
        }

        public void SetIsFoil(bool foilcond)
        {
            this.IsFoil = foilcond;
        }

        public void SetSetNum(int setnum)
        {
            if (setnum > -1)
            {
                this.SetNum = setnum;
            }
        }

        public void SetImageUrl(string imgurl)
        {
            if (!(string.IsNullOrEmpty(imgurl)))
            {
                this.ImageUrl = imgurl;
            }
        }

        public void SetNormPrice(decimal price)
        {
            if (price > -1.00M)
            {
                this.NormPrice = price;
            }
        }

        public void SetFoilPrice(decimal price)
        {
            if (price > -1.00M)
            {
                this.FoilPrice = price;
            }
        }
        */
        #endregion

        



    }
}
