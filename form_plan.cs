
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

partial class form{
    //1-1Žü‰ñ
    void around1_1(){
        Fielde1_1();if(stop_flg)return;
        expedition();
    }

    void expendjitiononly(){

        Func<int, int> rnd = (rr) => {
            Random r = new System.Random();
            return r.Next(rr);
        };
        for(int cnt = 0; cnt < 30; cnt++){
            //•ê`¨oŒ‚‰æ–Ê‚Ì‘JˆÚŠm”FêŠ
            int portchangeX = 100; int portchangeY = 500;
            //“®ìŠJŽn
            //•ê`‰æ–ÊŠm”F
            a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`"); if(stop_flg)return;
            //•ê`‚©‚çŠCˆæ‘I‘ð‰æ–Ê‚É‘JˆÚ
            a_non_b_click("oŒ‚_oŒ‚", "•ê`_oŒ‚", portchangeX, portchangeY); if(stop_flg)return;
            //•ê`‰æ–Ê‚É–ß‚é
            a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`");
            //‰“ªˆ—
            expedition();
            Task.Delay(1800000 + rnd(2000000)).Wait();
        }
        logwrite("‰“ªƒIƒ“ƒŠ[ƒvƒ‰ƒ“I—¹");
    }
}