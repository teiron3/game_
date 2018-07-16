using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

partial class form{
    //ƒƒ\ƒbƒh supply •ê`‰æ–Ê‚©‚ç‚Ì•â‹‹
    //ˆø” b :4bit‚Å‚»‚ê‚¼‚ê‚ÌŠÍ‘à‚ð‘I‘ð‚·‚é
    //        0b0000 ‚Å 1234 ‚Ì‡”Ô
    void supply(byte f){
        logwrite("test11");
        //ˆø”ƒ`ƒFƒbƒN“K³‚È’l‚Å‚È‚¢ê‡‚ÍƒGƒ‰[‚ð•Ô‚µ‚Ä“®ìI—¹
        if((f & 15) == 0){
            logwrite_msgbox("error:supply ˆø”ƒGƒ‰[");
            stop_flg = true;
            return;
        }

        //delegate •ê`‰æ–Ê‚É–ß‚é
        Action home_port_return = () =>{
            a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`");
        };

        //deligate •â‹‹ŽÀŽ{
        Action run_supplay = () =>{
            for(int i = 0; i <= 2; i++)a_click("•â‹‹_‘S•â‹‹");
        };

        //“®ìŠJŽn
        //•ê`‚©‚ç•â‹‹‰æ–Ê‚É‘JˆÚ
        a_non_b_click("•ê`_oŒ‚", "•ê`_•ê`"); if(stop_flg)return;
        a_non_b_click("•â‹‹_”R—¿", "•ê`_•â‹‹"); if(stop_flg)return;

        //1ŠÍ‘à‚Ì•â‹‹
        if((f & 8) != 0){
            run_supplay();
            if(stop_flg)return;
        }

        //2ŠÍ‘à‚Ì•â‹‹
        if((f & 4) != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ð2");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //3ŠÍ‘à‚Ì•â‹‹
        if((f & 2) != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ð3");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

        //4ŠÍ‘à‚Ì•â‹‹
        if((f & 1) != 0){
            a_b_change_c_click("•â‹‹_”äŠrêŠ1", "•â‹‹_”äŠrêŠ2", "•â‹‹_ŠÍ‘à‘I‘ð4");
            if(stop_flg)return;

            run_supplay();
            if(stop_flg)return;
        }

            home_port_return();
            logwrite("•â‹‹Š®—¹");
            return;
    }
}