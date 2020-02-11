using System;

namespace C_Sharp_DojoDachi.Models
{
    public class Dachi
    {
        public int fullness { get; set; } = 20;
        public int happiness { get; set; } = 20;
        public int meals { get; set; } = 3;
        public int energy { get; set; } = 50;
        public string status { get; set; } = "Alive";

        // public Dachi (int fullness, int happiness, int meals, int energy, string status)
        // {
        //     fullness = 20;
        //     happiness = 20;
        //     meals = 3;
        //     energy = 50;
        //     status = "Alive";
        // }

        public Dachi feed()
        {
            Random feedRand = new Random();
            // int chance =  feedRand.Next(1,5);
            // if(chance == 1)
            // {
            //     this.meals-=1;
            //     int amount = feedRand.Next(51,101);
            //     this.fullness += amount;
            //     this.status = $"Your dachi found some superdojos and is {amount} fuller!!!";
            //     return this;
            // } 
            // else 
            // {         
            int num = feedRand.Next(5,11);
            this.meals-=1;
            this.fullness += num;
            this.status = $"You feed Dachi! Your meals lost -1 and fulness get +{num}";
            return this;
            // }
        }

        
    }
    

}