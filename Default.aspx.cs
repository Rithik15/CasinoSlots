using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CasinoSlots
{
    public partial class Default : System.Web.UI.Page
    {
        string[] images = new string[] { "cherry", "shamrock", "horseshoe" };
        //this creates images[0]="cherry"
        //             images[1]="shamrock"
        //             images[2]="horseshoe"

        Random r = new Random();  //creates an instance of the Random class   
                                  //so we can generate random numbers below  

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnPull_Click(object sender, EventArgs e)
        {
            //when someone clicks the button, display a random image           

            int bet = 0;

            if (!int.TryParse(TxtBet.Text, out bet))
            {
                LblResult.Text = "Only integer bets please";
                return;  //only accept integer bets
            }

            LblResult.Text = "";
            LblMoney.Text = "";

            PullLeverAndPlay(bet);            

        }

        private void PullLeverAndPlay(int b)
        {
            //pick numbers between 0 and 2
            int slot1 = r.Next(0, 3);
            int slot2 = r.Next(0, 3);
            int slot3 = r.Next(0, 3);

            //display the actual matching images on the page
            Image1.ImageUrl = "/images/" + images[slot1] + ".jpg";
            Image2.ImageUrl = "/images/" + images[slot2] + ".jpg";
            Image3.ImageUrl = "/images/" + images[slot3] + ".jpg";

            DetermineWinnings(slot1, slot2, slot3, b); //pass along the random numbers + AND the bet b to the method

           
        }

        private void DetermineWinnings(int s1,int s2,int s3,int bt)
        {
            int w = 0;

            if (s1 == 0 && s2 == 0 && s3 == 0) //three in a row cherries
            {
                w = bt * 4;
                LblResult.Text = "Three Cherries";
            }
            else if ((s1 == 0 && s2 == 0) || (s1 == 0 && s3 == 0) //two cheeries ... three possible combinations
                    || (s2 == 0 && s3 == 0))
            {
                w = bt * 3;
                LblResult.Text = "Two Cherries";
            }
            else if ((s1 == 0 && (s2 != 0 && s3 != 0)) //one cherry ONLY .. so notice use of ! (NOT)
                || (s2 == 0 && (s1 != 0 && s3 != 0))
                || (s3 == 0 && (s1 != 0 && s2 != 0)))
            {
                w = bt * 2;
                LblResult.Text = "One Cherry";
            }
            else if (s1 == 2 && s2 == 2 && s3 == 2)  //three in a row horshoes
            {
                w = bt * 100;
                LblResult.Text = "Three Horshoes!!!";
            }

            LblMoney.Text = "You bet " + bt.ToString("c") + " and won " + w.ToString("c");
        }
        


    }
}