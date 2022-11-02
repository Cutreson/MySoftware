using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftware
{
    public class cDeviceSetting
    {
        public int Port1 { get; set; }
        public int Port2 { get; set; }
        // Cam1 
            // Send
        public int Ready1 { get; set; }
        public int ImageOK1 { get; set; }
        public int TopL1 { get; set; }
        public int TopR1 { get; set; }
        public int BotL1 { get; set; }
        public int BotR1 { get; set; }
        public int Left1 { get; set; }
        public int Right1 { get; set; }
        public int Result1 { get; set; }
        public int Bubble1 { get; set; }
        public int CompleteImage1 { get; set; }
        public int MarkNG1 { get; set; }
        public int Lengh1 { get; set; }
        public int Width1 { get; set; }
        public int ScoreL1 { get; set; }
        public int ScoreR1 { get; set; }


        //Recive
        public int Triger1 { get; set; }
        public int Position1 { get; set; }
        public int CompleteResult1 { get; set; }

        // Cam2
            //Send
        public int Ready2 { get; set; }
        public int ImageOK2 { get; set; }
        public int TopL2 { get; set; }
        public int TopR2 { get; set; }
        public int BotL2 { get; set; }
        public int BotR2 { get; set; }
        public int Left2 { get; set; }
        public int Right2 { get; set; }
        public int Result2 { get; set; }
        public int Bubble2 { get; set; }
        public int CompleteImage2 { get; set; }
        public int MarkNG2 { get; set; }
            //Recive
        public int Triger2 { get; set; }
        public int Position2 { get; set; }
        public int CompleteResult2 { get; set; }
        public int Lengh2 { get; set; }
        public int Width2 { get; set; }
        public int ScoreL2 { get; set; }
        public int ScoreR2 { get; set; }

    }
}
